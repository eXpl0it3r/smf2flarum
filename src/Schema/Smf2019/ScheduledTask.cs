namespace Schema.Smf2019;

public partial class ScheduledTask
{
    public short IdTask { get; set; }

    public int NextTime { get; set; }

    public int TimeOffset { get; set; }

    public short TimeRegularity { get; set; }

    public string TimeUnit { get; set; } = null!;

    public sbyte Disabled { get; set; }

    public string Task { get; set; } = null!;
}
