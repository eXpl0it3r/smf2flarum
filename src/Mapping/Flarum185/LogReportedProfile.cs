using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

public class LogReportedProfile : Profile
{
    public LogReportedProfile()
    {
        CreateMap<LogReported, Flag>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdReport))
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.IdMsg))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Closed == 1 ? "resolved" : "pending"))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdMember == 0 ? (uint?)null : src.IdMember))
            .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.ReasonDetail, opt => opt.MapFrom(src => src.Body))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => Mapping.Converter.UnixTimeStampToDateTime((uint)src.TimeStarted) ?? DateTime.UtcNow))
            .ForMember(dest => dest.Post, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}