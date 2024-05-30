namespace Schema.Smf2019;

public partial class LogError
{
    public uint IdError { get; set; }

    public uint LogTime { get; set; }

    public uint IdMember { get; set; }

    public string Ip { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Session { get; set; } = null!;

    public string ErrorType { get; set; } = null!;

    public string File { get; set; } = null!;

    public uint Line { get; set; }
}
