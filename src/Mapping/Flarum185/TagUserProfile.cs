namespace Mapping.Flarum185;

using AutoMapper;
using Schema.Smf2019;
using Schema.Flarum185;

/// <summary>
/// AutoMapper profile for converting SMF board subscriptions to Flarum tag following.
/// Uses LogNotify data to determine which boards/tags users are following.
/// </summary>
public class TagUserProfile : Profile
{
    public TagUserProfile()
    {
        // Convert SMF LogNotify (board/topic subscription) to Flarum TagUser (tag following)
        CreateMap<LogNotify, TagUser>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdMember))
            .ForMember(dest => dest.TagId, opt => opt.MapFrom(src => (uint)src.IdBoard)) // Board maps to Tag
            .ForMember(dest => dest.MarkedAsReadAt, opt => opt.MapFrom(src => 
                src.Sent == 1 ? DateTime.UtcNow : (DateTime?)null)) // If notification was sent, assume read
            .ForMember(dest => dest.IsHidden, opt => opt.MapFrom(src => false)) // Default to not hidden
            .ForMember(dest => dest.Tag, opt => opt.Ignore()) // Navigation property
            .ForMember(dest => dest.User, opt => opt.Ignore()); // Navigation property

        // Create a grouped version for board-level subscriptions
        CreateMap<IGrouping<(uint UserId, ushort BoardId), LogNotify>, TagUser>()
            .ConvertUsing((src, dest, context) => CreateTagSubscription(src));
    }

    /// <summary>
    /// Creates a TagUser subscription from grouped LogNotify entries for a specific user-board combination.
    /// </summary>
    private static TagUser CreateTagSubscription(IGrouping<(uint UserId, ushort BoardId), LogNotify> group)
    {
        var key = group.Key;
        var notifications = group.ToList();
        
        // Find the most recent notification to determine read status
        var latestNotification = notifications.OrderByDescending(n => n.IdTopic).FirstOrDefault();
        
        return new TagUser
        {
            UserId = key.UserId,
            TagId = (uint)key.BoardId,
            MarkedAsReadAt = latestNotification?.Sent == 1 ? DateTime.UtcNow : null,
            IsHidden = false // SMF doesn't have concept of hidden tags
        };
    }
}

/// <summary>
/// DTO for tag subscription data that includes additional context.
/// </summary>
public class TagSubscriptionDto
{
    public uint UserId { get; set; }
    public uint TagId { get; set; }
    public string Username { get; set; } = null!;
    public string TagName { get; set; } = null!;
    public DateTime? LastReadAt { get; set; }
    public int TopicSubscriptionCount { get; set; }
    public bool IsActive { get; set; }
}