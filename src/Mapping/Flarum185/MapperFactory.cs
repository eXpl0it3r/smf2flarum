using AutoMapper;

namespace Mapping.Flarum185;

public static class MapperFactory
{
    public static Mapper Create()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            // Core forum entity profiles
            cfg.AddProfile<GroupProfile>();
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<GroupUserProfile>();
            cfg.AddProfile<BoardProfile>();
            cfg.AddProfile<TopicProfile>();
            cfg.AddProfile<MessageProfile>();
            
            // Extended entity profiles
            cfg.AddProfile<LogTopicProfile>();
            cfg.AddProfile<LogReportedProfile>();
            cfg.AddProfile<LogKarmaProfile>();
            cfg.AddProfile<CategoryProfile>();
            cfg.AddProfile<BoardPermissionProfile>();
            cfg.AddProfile<AttachmentProfile>();
            cfg.AddProfile<PollProfile>();
            cfg.AddProfile<LogNotifyProfile>();
            cfg.AddProfile<SettingProfile>();
            
            // High-priority missing entity profiles
            cfg.AddProfile<PostMentionsProfile>();
            cfg.AddProfile<PersonalMessageProfile>();
            cfg.AddProfile<ModeratorProfile>();
            cfg.AddProfile<TagUserProfile>();
        });

        return new Mapper(configuration);
    }
}