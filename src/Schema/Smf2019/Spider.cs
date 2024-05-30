namespace Schema.Smf2019;

public partial class Spider
{
    public ushort IdSpider { get; set; }

    public string SpiderName { get; set; } = null!;

    public string UserAgent { get; set; } = null!;

    public string IpInfo { get; set; } = null!;
}
