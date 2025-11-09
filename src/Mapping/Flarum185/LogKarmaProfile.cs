using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

// NOTE: LogKarma in SMF tracks user-to-user karma votes, not post-specific likes.
// This mapping is included for completeness but may need post association logic
// in the migration process to determine which post the karma was for.
public class LogKarmaProfile : Profile
{
    public LogKarmaProfile()
    {
        CreateMap<LogKarma, PostLike>()
            .ForMember(dest => dest.PostId, opt => opt.Ignore()) // Needs to be resolved from context - SMF karma is user-to-user
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdExecutor))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => Mapping.Converter.UnixTimeStampToDateTime(src.LogTime) ?? DateTime.UtcNow))
            .ForMember(dest => dest.Post, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}