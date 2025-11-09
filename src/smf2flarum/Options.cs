using ConsoleAppFramework;

namespace smf2flarum;

public class MigrationCommands
{
    /// <summary>
    /// Migrate data from SMF to Flarum
    /// </summary>
    /// <param name="smf">Connection string for the SMF database</param>
    /// <param name="flarum">Connection string for the Flarum database</param>
    [Command("")]
    public async Task MigrateAsync(string smf, string flarum)
    {
        var options = new Options
        {
            SmfConnectionString = smf,
            FlarumConnectionString = flarum
        };
        
        var migrator = Migrator.Create(options);
        await migrator.ExecuteAsync();
    }
}

public class Options
{
    public string SmfConnectionString { get; set; } = string.Empty;
    public string FlarumConnectionString { get; set; } = string.Empty;
}