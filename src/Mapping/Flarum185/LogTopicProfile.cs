using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

public class LogTopicProfile : Profile
{
    public LogTopicProfile()
    {
        CreateMap<LogTopic, DiscussionUser>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdMember))
            .ForMember(dest => dest.DiscussionId, opt => opt.MapFrom(src => src.IdTopic))
            .ForMember(dest => dest.LastReadAt, opt => opt.Ignore()) // Will be set from message timestamp
            .ForMember(dest => dest.LastReadPostNumber, opt => opt.Ignore()) // Will be calculated from IdMsg
            .ForMember(dest => dest.Subscription, opt => opt.Ignore()) // No SMF equivalent
            .ForMember(dest => dest.Discussion, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}