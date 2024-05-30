namespace Schema.Smf2019;

public partial class LogSpiderHit
{
    public uint IdHit { get; set; }

    public ushort IdSpider { get; set; }

    public uint LogTime { get; set; }

    public string Url { get; set; } = null!;

    public sbyte Processed { get; set; }
}
