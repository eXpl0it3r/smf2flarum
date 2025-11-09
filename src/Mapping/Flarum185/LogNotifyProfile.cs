namespace Mapping.Flarum185;

using AutoMapper;
using Schema.Smf2019;
using Schema.Flarum185;

/// <summary>
/// AutoMapper profile for mapping SMF notification logs to Flarum notifications.
/// Maps SMF LogNotify entries to Flarum Notification entities for topic subscription tracking.
/// </summary>
public class LogNotifyProfile : Profile
{
    public LogNotifyProfile()
    {
        CreateMap<LogNotify, Notification>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Auto-generated
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdMember))
            .ForMember(dest => dest.FromUserId, opt => opt.Ignore()) // Not available in SMF LogNotify
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "discussionRenamed")) // Default notification type for topic changes
            .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.IdTopic))
            .ForMember(dest => dest.Data, opt => opt.Ignore()) // Will be populated with serialized notification data
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow)) // SMF doesn't store notification creation time
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.ReadAt, opt => opt.MapFrom(src => src.Sent == 1 ? DateTime.UtcNow : (DateTime?)null))
            .ForMember(dest => dest.FromUser, opt => opt.Ignore()) // Navigation property
            .ForMember(dest => dest.User, opt => opt.Ignore()); // Navigation property
    }
}