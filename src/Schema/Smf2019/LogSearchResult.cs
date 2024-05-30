namespace Schema.Smf2019;

public partial class LogSearchResult
{
    public byte IdSearch { get; set; }

    public uint IdTopic { get; set; }

    public uint IdMsg { get; set; }

    public ushort Relevance { get; set; }

    public ushort NumMatches { get; set; }
}
