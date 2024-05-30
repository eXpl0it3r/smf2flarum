namespace Schema.Smf2019;

public partial class AdminInfoFile
{
    public byte IdFile { get; set; }

    public string Filename { get; set; } = null!;

    public string Path { get; set; } = null!;

    public string Parameters { get; set; } = null!;

    public string Data { get; set; } = null!;

    public string Filetype { get; set; } = null!;
}
