namespace Schema.Smf2019;

public partial class LogSpiderStat
{
    public ushort IdSpider { get; set; }

    public ushort PageHits { get; set; }

    public uint LastSeen { get; set; }

    public DateTime StatDate { get; set; }
}
