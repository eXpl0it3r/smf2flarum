using CommandLine;
using smf2flarum;

await Parser.Default.ParseArguments<Options>(args)
    .WithParsedAsync(async o =>
    {
        var migrator = Migrator.Create(o);
        await migrator.ExecuteAsync();
    });