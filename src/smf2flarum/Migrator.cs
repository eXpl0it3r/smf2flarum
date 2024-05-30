using Microsoft.EntityFrameworkCore;
using Schema.Flarum185;
using Schema.Smf2019;

namespace smf2flarum;

public class Migrator
{
    private readonly SmfContext _smfContext;
    private readonly FlarumContext _flarumContext;

    public static Migrator Create(Options options)
    {
        var smfContext = new SmfContext(new DbContextOptionsBuilder<SmfContext>().UseMySQL(options.SmfConnectionString).Options);
        var flarumContext = new FlarumContext(new DbContextOptionsBuilder<FlarumContext>().UseMySQL(options.FlarumConnectionString).Options);

        return new Migrator(smfContext, flarumContext);
    }

    public async Task ExecuteAsync()
    {
        Console.WriteLine($"Got {await _smfContext.Settings.CountAsync()} number SMF setting entries");
        Console.WriteLine($"Got {await _flarumContext.Settings.CountAsync()} number Flarum setting entries");
    }

    private Migrator(SmfContext smfContext, FlarumContext flarumContext)
    {
        _smfContext = smfContext;
        _flarumContext = flarumContext;
    }
}