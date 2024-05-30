namespace Schema.Smf2019;

public partial class LogActivity
{
    public DateTime Date { get; set; }

    public uint Hits { get; set; }

    public ushort Topics { get; set; }

    public ushort Posts { get; set; }

    public ushort Registers { get; set; }

    public ushort MostOn { get; set; }
}
