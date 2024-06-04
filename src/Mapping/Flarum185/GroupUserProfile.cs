using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

public class GroupUserProfile : Profile
{
    public GroupUserProfile()
    {
        CreateMap<Member, GroupUser>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdMember))
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.IdGroup))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => Converter.UnixTimeStampToDateTime(src.DateRegistered)))
            .ForMember(dest => dest.Group, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}