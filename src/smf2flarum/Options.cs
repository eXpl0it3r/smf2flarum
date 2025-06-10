using CommandLine;

namespace smf2flarum;

public class Options
{
    [Option('s', "smf", Required = true, HelpText = "Connection string for the SMF database")]
    public string SmfConnectionString { get; set; } = string.Empty;

    [Option('f', "flarum", Required = true, HelpText = "Connection string for the Flarum database")]
    public string FlarumConnectionString { get; set; } = string.Empty;
}