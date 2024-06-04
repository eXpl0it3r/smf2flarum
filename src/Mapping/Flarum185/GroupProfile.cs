using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<Membergroup, Group>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (uint)src.IdGroup))
            .ForMember(dest => dest.NameSingular, opt => opt.MapFrom(src => src.GroupName))
            .ForMember(dest => dest.NamePlural, opt => opt.MapFrom(src => src.GroupName))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.OnlineColor))
            .ForMember(dest => dest.Icon, opt => opt.Ignore())
            .ForMember(dest => dest.IsHidden, opt => opt.MapFrom(src => src.Hidden == 1))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.GroupPermissions, opt => opt.Ignore())
            .ForMember(dest => dest.GroupUsers, opt => opt.Ignore())
            .ForMember(dest => dest.PostMentionsGroups, opt => opt.Ignore());
    }
}