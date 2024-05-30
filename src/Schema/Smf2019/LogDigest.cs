namespace Schema.Smf2019;

public partial class LogDigest
{
    public uint IdTopic { get; set; }

    public uint IdMsg { get; set; }

    public string NoteType { get; set; } = null!;

    public byte Daily { get; set; }

    public uint Exclude { get; set; }
}
