using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, Post>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdMsg))
            .ForMember(dest => dest.DiscussionId, opt => opt.MapFrom(src => src.IdTopic))
            .ForMember(dest => dest.Number, opt => opt.Ignore()) // Will be calculated based on order in topic
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => Mapping.Converter.UnixTimeStampToDateTime(src.PosterTime)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdMember == 0 ? (uint?)null : src.IdMember))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "comment"))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Body))
            .ForMember(dest => dest.EditedAt, opt => opt.MapFrom(src => src.ModifiedTime > 0 ? Mapping.Converter.UnixTimeStampToDateTime(src.ModifiedTime) : null))
            .ForMember(dest => dest.EditedUserId, opt => opt.Ignore()) // SMF doesn't track who edited
            .ForMember(dest => dest.HiddenAt, opt => opt.Ignore())
            .ForMember(dest => dest.HiddenUserId, opt => opt.Ignore())
            .ForMember(dest => dest.IpAddress, opt => opt.MapFrom(src => src.PosterIp))
            .ForMember(dest => dest.IsPrivate, opt => opt.Ignore())
            .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.Approved == 1))
            .ForMember(dest => dest.Discussion, opt => opt.Ignore())
            .ForMember(dest => dest.DiscussionFirstPosts, opt => opt.Ignore())
            .ForMember(dest => dest.DiscussionLastPosts, opt => opt.Ignore())
            .ForMember(dest => dest.EditedUser, opt => opt.Ignore())
            .ForMember(dest => dest.Flags, opt => opt.Ignore())
            .ForMember(dest => dest.HiddenUser, opt => opt.Ignore())
            .ForMember(dest => dest.PostLikes, opt => opt.Ignore())
            .ForMember(dest => dest.PostMentionsGroups, opt => opt.Ignore())
            .ForMember(dest => dest.PostMentionsPostMentionsPosts, opt => opt.Ignore())
            .ForMember(dest => dest.PostMentionsPostPosts, opt => opt.Ignore())
            .ForMember(dest => dest.PostMentionsTags, opt => opt.Ignore())
            .ForMember(dest => dest.PostMentionsUsers, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Users, opt => opt.Ignore());
    }
}