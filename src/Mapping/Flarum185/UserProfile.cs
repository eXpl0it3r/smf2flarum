using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Member, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdMember))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.MemberName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress))
            .ForMember(dest => dest.IsEmailConfirmed, opt => opt.MapFrom(src => src.IsActivated == 1))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Passwd))
            .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Avatar))
            .ForMember(dest => dest.Preferences, opt => opt.Ignore())
            .ForMember(dest => dest.JoinedAt, opt => opt.MapFrom(src => Converter.UnixTimeStampToDateTime(src.DateRegistered)))
            .ForMember(dest => dest.LastSeenAt, opt => opt.MapFrom(src => Converter.UnixTimeStampToDateTime(src.LastLogin)))
            .ForMember(dest => dest.MarkedAllAsReadAt, opt => opt.Ignore())
            .ForMember(dest => dest.ReadNotificationsAt, opt => opt.Ignore())
            .ForMember(dest => dest.DiscussionCount, opt => opt.MapFrom(src => src.Posts))
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Posts))
            .ForMember(dest => dest.ReadFlagsAt, opt => opt.Ignore())
            .ForMember(dest => dest.SuspendedUntil, opt => opt.Ignore())
            .ForMember(dest => dest.SuspendReason, opt => opt.Ignore())
            .ForMember(dest => dest.SuspendMessage, opt => opt.Ignore())
            .ForMember(dest => dest.AccessTokens, opt => opt.Ignore())
            .ForMember(dest => dest.ApiKeys, opt => opt.Ignore())
            .ForMember(dest => dest.DiscussionHiddenUsers, opt => opt.Ignore())
            .ForMember(dest => dest.DiscussionLastPostedUsers, opt => opt.Ignore())
            .ForMember(dest => dest.DiscussionUsers, opt => opt.Ignore())
            .ForMember(dest => dest.DiscussionUsersNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.EmailTokens, opt => opt.Ignore())
            .ForMember(dest => dest.Flags, opt => opt.Ignore())
            .ForMember(dest => dest.GroupUsers, opt => opt.Ignore())
            .ForMember(dest => dest.LoginProviders, opt => opt.Ignore())
            .ForMember(dest => dest.NotificationFromUsers, opt => opt.Ignore())
            .ForMember(dest => dest.NotificationUsers, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordTokens, opt => opt.Ignore())
            .ForMember(dest => dest.PostEditedUsers, opt => opt.Ignore())
            .ForMember(dest => dest.PostHiddenUsers, opt => opt.Ignore())
            .ForMember(dest => dest.PostLikes, opt => opt.Ignore())
            .ForMember(dest => dest.PostMentionsUsers, opt => opt.Ignore())
            .ForMember(dest => dest.PostUsers, opt => opt.Ignore())
            .ForMember(dest => dest.TagUsers, opt => opt.Ignore())
            .ForMember(dest => dest.Tags, opt => opt.Ignore())
            .ForMember(dest => dest.Posts, opt => opt.Ignore());
    }
}