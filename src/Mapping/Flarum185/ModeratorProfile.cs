namespace Mapping.Flarum185;

using AutoMapper;
using Schema.Smf2019;
using Schema.Flarum185;

/// <summary>
/// AutoMapper profile for converting SMF board-specific moderators to Flarum group assignments.
/// Creates appropriate group memberships and permissions for SMF moderators.
/// </summary>
public class ModeratorProfile : Profile
{
    public ModeratorProfile()
    {
        // Convert SMF Moderator to Flarum GroupUser (adding user to moderator group)
        CreateMap<Moderator, GroupUser>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdMember))
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => 4)) // Flarum Moderator group ID
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Group, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

        // Create board-specific permissions for moderators
        CreateMap<Moderator, List<GroupPermission>>()
            .ConvertUsing((src, dest, context) => CreateModeratorPermissions(src));
    }

    /// <summary>
    /// Creates Flarum group permissions for SMF board moderators.
    /// Grants tag-specific moderation permissions based on the board they moderate.
    /// </summary>
    private static List<GroupPermission> CreateModeratorPermissions(Moderator moderator)
    {
        var permissions = new List<GroupPermission>();
        var groupId = 4u; // Flarum Moderator group
        var tagId = (uint)moderator.IdBoard; // Board maps to Tag

        // Standard moderator permissions for the specific tag/board
        var moderatorPermissions = new[]
        {
            $"tag{tagId}.discussion.moderate", // Can moderate discussions in this tag
            $"tag{tagId}.discussion.edit", // Can edit discussions in this tag
            $"tag{tagId}.discussion.delete", // Can delete discussions in this tag
            $"tag{tagId}.discussion.lock", // Can lock discussions in this tag
            $"tag{tagId}.discussion.sticky", // Can sticky discussions in this tag
            $"tag{tagId}.post.moderate", // Can moderate posts in this tag
            $"tag{tagId}.post.edit", // Can edit posts in this tag
            $"tag{tagId}.post.delete", // Can delete posts in this tag
            $"tag{tagId}.user.warn", // Can warn users in this tag
            $"tag{tagId}.viewFlags", // Can view reports in this tag
        };

        foreach (var permission in moderatorPermissions)
        {
            permissions.Add(new GroupPermission
            {
                GroupId = groupId,
                Permission = permission,
                CreatedAt = DateTime.UtcNow
            });
        }

        return permissions;
    }
}

/// <summary>
/// DTO for moderator assignment data that requires post-processing.
/// </summary>
public class ModeratorAssignmentDto
{
    public uint UserId { get; set; }
    public ushort BoardId { get; set; }
    public string Username { get; set; } = null!;
    public string BoardName { get; set; } = null!;
    public DateTime AssignedAt { get; set; }
}