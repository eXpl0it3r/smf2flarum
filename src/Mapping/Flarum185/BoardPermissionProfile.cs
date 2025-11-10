using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

public class BoardPermissionProfile : Profile
{
    public BoardPermissionProfile()
    {
        CreateMap<BoardPermission, GroupPermission>()
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => MapSmfGroupToFlarumGroup(src.IdGroup) ?? 0))
            .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => MapSmfPermissionToFlarum(src.Permission, src.AddDeny == 1)))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Group, opt => opt.Ignore());

        // Map a single BoardPermission to multiple GroupPermissions
        CreateMap<BoardPermission, IEnumerable<GroupPermission>>()
            .ConvertUsing((src, dest, context) => CreateGroupPermissions(src));
    }

    /// <summary>
    /// Creates Flarum group permissions from SMF board permission.
    /// Can create multiple permissions from a single board permission.
    /// </summary>
    private static IEnumerable<GroupPermission> CreateGroupPermissions(BoardPermission boardPermission)
    {
        var flarumPermission = MapSmfPermissionToFlarum(boardPermission.Permission, boardPermission.AddDeny == 1);
        
        // If permission mapping failed or was denied, return empty
        if (string.IsNullOrEmpty(flarumPermission))
            yield break;

        // Map SMF group ID to Flarum group ID using the same logic as in Migrator
        var flarumGroupId = MapSmfGroupToFlarumGroup(boardPermission.IdGroup);
        if (!flarumGroupId.HasValue)
            yield break;

        // Create the primary permission
        yield return new GroupPermission
        {
            GroupId = flarumGroupId.Value,
            Permission = flarumPermission,
            CreatedAt = null
        };

        // Some SMF permissions might map to multiple Flarum permissions
        var additionalPermissions = GetAdditionalPermissions(boardPermission.Permission, boardPermission.AddDeny == 1);
        foreach (var additional in additionalPermissions)
        {
            yield return new GroupPermission
            {
                GroupId = flarumGroupId.Value,
                Permission = additional,
                CreatedAt = null
            };
        }
    }

    /// <summary>
    /// Maps SMF group ID to Flarum group ID using the same mapping as used in user migration.
    /// </summary>
    private static uint? MapSmfGroupToFlarumGroup(short smfGroupId)
    {
        // Use the same group mapping as in Migrator.cs
        var groupMapping = new Dictionary<int, uint>
        {
            { 1, 1 },  // Administrator => Admin
            { 2, 4 },  // Global Moderator => Mod  
            { 3, 4 },  // Moderator => Mod
            { 4, 3 },  // Newbie => Member
            { 5, 3 },  // Jr. Member => Member
            { 6, 3 },  // Full Member => Member
            { 7, 3 },  // Sr. Member => Member
            { 8, 3 }   // Hero Member => Member
        };

        return groupMapping.TryGetValue(smfGroupId, out var flarumGroupId) ? flarumGroupId : null;
    }

    /// <summary>
    /// Gets additional Flarum permissions that should be granted alongside the primary permission.
    /// </summary>
    private static IEnumerable<string> GetAdditionalPermissions(string smfPermission, bool isGranted)
    {
        if (!isGranted) yield break;

        // Some SMF permissions imply multiple Flarum permissions
        switch (smfPermission)
        {
            case "moderate_board":
                yield return "discussion.hide";
                yield return "discussion.lock";
                yield return "discussion.sticky";
                break;
            case "delete_any":
                yield return "discussion.delete";
                break;
            case "modify_any":
                yield return "discussion.edit";
                break;
        }
    }

    private static string MapSmfPermissionToFlarum(string smfPermission, bool isGranted)
    {
        // Only include permissions that are granted (AddDeny = 1)
        if (!isGranted) return "";

        return smfPermission switch
        {
            "moderate_board" => "discussion.moderate",
            "post_new" => "startDiscussion",
            "post_reply_own" => "discussion.reply",
            "post_reply_any" => "discussion.reply",
            "post_attachment" => "discussion.attachment",
            "delete_own" => "discussion.delete",
            "delete_any" => "discussion.deletePosts",
            "modify_own" => "discussion.edit",
            "modify_any" => "discussion.editPosts",
            "mark_any_notify" => "discussion.viewFlags",
            "mark_notify" => "discussion.viewFlags",
            "report_any" => "discussion.flag",
            "lock_own" => "discussion.lock",
            "lock_any" => "discussion.lock",
            "remove_own" => "discussion.hide",
            "remove_any" => "discussion.hidePosts",
            "view_attachments" => "discussion.viewAttachments",
            _ => $"smf.{smfPermission}" // Preserve unknown permissions with prefix
        };
    }
}