namespace Schema.Smf2019;

public partial class LogNotify
{
    public uint IdMember { get; set; }

    public uint IdTopic { get; set; }

    public ushort IdBoard { get; set; }

    public byte Sent { get; set; }
}
