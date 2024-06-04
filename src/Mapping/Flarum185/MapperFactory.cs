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
        });

        return new Mapper(configuration);
    }
}