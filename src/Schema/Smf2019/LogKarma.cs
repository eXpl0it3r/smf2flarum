namespace Schema.Smf2019;

public partial class LogKarma
{
    public uint IdTarget { get; set; }

    public uint IdExecutor { get; set; }

    public uint LogTime { get; set; }

    public sbyte Action { get; set; }
}
