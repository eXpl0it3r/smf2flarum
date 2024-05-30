namespace Schema.Smf2019;

public partial class LogOnline
{
    public string Session { get; set; } = null!;

    public int LogTime { get; set; }

    public uint IdMember { get; set; }

    public ushort IdSpider { get; set; }

    public uint Ip { get; set; }

    public string Url { get; set; } = null!;
}
