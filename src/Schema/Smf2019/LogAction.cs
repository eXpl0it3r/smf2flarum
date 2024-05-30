namespace Schema.Smf2019;

public partial class LogAction
{
    public uint IdAction { get; set; }

    public byte IdLog { get; set; }

    public uint LogTime { get; set; }

    public uint IdMember { get; set; }

    public string Ip { get; set; } = null!;

    public string Action { get; set; } = null!;

    public ushort IdBoard { get; set; }

    public uint IdTopic { get; set; }

    public uint IdMsg { get; set; }

    public string Extra { get; set; } = null!;
}
