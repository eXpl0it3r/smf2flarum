using ConsoleAppFramework;
using smf2flarum;

var app = ConsoleApp.Create();
app.Add<MigrationCommands>();
app.Run(args);