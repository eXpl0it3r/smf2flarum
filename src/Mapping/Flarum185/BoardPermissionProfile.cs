using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

public class BoardPermissionProfile : Profile
{
    public BoardPermissionProfile()
    {
        CreateMap<BoardPermission, GroupPermission>()
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => (uint)src.IdGroup))
            .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => MapSmfPermissionToFlarum(src.Permission, src.AddDeny == 1)))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Group, opt => opt.Ignore());
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