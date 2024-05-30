namespace Schema.Smf2019;

public partial class Calendar
{
    public ushort IdEvent { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ushort IdBoard { get; set; }

    public uint IdTopic { get; set; }

    public string Title { get; set; } = null!;

    public uint IdMember { get; set; }
}
