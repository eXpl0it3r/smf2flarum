namespace Schema.Smf2019;

public partial class LogReported
{
    public uint IdReport { get; set; }

    public uint IdMsg { get; set; }

    public uint IdTopic { get; set; }

    public ushort IdBoard { get; set; }

    public uint IdMember { get; set; }

    public string Membername { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public int TimeStarted { get; set; }

    public int TimeUpdated { get; set; }

    public int NumReports { get; set; }

    public sbyte Closed { get; set; }

    public sbyte IgnoreAll { get; set; }
}
