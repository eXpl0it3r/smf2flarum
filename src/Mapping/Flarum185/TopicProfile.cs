using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

public class TopicProfile : Profile
{
    public TopicProfile()
    {
        CreateMap<Topic, Discussion>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdTopic))
            .ForMember(dest => dest.Title, opt => opt.Ignore()) // Will be set manually from first message
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => (int)src.NumReplies + 1)) // +1 for first post
            .ForMember(dest => dest.ParticipantCount, opt => opt.Ignore()) // Will be calculated
            .ForMember(dest => dest.PostNumberIndex, opt => opt.MapFrom(src => src.NumReplies + 1))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Will be set from first message
            .ForMember(dest => dest.UserId, opt => opt.Ignore()) // Will be set after posts are created
            .ForMember(dest => dest.FirstPostId, opt => opt.Ignore()) // Will be set after posts are created
            .ForMember(dest => dest.LastPostedAt, opt => opt.Ignore()) // Will be set from last message
            .ForMember(dest => dest.LastPostedUserId, opt => opt.Ignore()) // Will be set after posts are created
            .ForMember(dest => dest.LastPostId, opt => opt.Ignore()) // Will be set after posts are created
            .ForMember(dest => dest.LastPostNumber, opt => opt.MapFrom(src => src.NumReplies + 1))
            .ForMember(dest => dest.HiddenAt, opt => opt.Ignore())
            .ForMember(dest => dest.HiddenUserId, opt => opt.Ignore())
            .ForMember(dest => dest.Slug, opt => opt.Ignore()) // Will be generated from title
            .ForMember(dest => dest.IsPrivate, opt => opt.Ignore())
            .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.Approved == 1))
            .ForMember(dest => dest.IsSticky, opt => opt.MapFrom(src => src.IsSticky == 1))
            .ForMember(dest => dest.IsLocked, opt => opt.MapFrom(src => src.Locked == 1))
            .ForMember(dest => dest.DiscussionTags, opt => opt.Ignore())
            .ForMember(dest => dest.DiscussionUsers, opt => opt.Ignore())
            .ForMember(dest => dest.FirstPost, opt => opt.Ignore())
            .ForMember(dest => dest.HiddenUser, opt => opt.Ignore())
            .ForMember(dest => dest.LastPost, opt => opt.Ignore())
            .ForMember(dest => dest.LastPostedUser, opt => opt.Ignore())
            .ForMember(dest => dest.Posts, opt => opt.Ignore())
            .ForMember(dest => dest.Tags, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}