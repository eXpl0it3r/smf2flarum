using AutoMapper;

namespace Mapping.Flarum185;

public static class MapperFactory
{
    public static Mapper Create()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<GroupProfile>();
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<GroupUserProfile>();
            cfg.AddProfile<BoardProfile>();
            cfg.AddProfile<TopicProfile>();
            cfg.AddProfile<MessageProfile>();
        });

        return new Mapper(configuration);
    }
}