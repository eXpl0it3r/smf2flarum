namespace Schema.Smf2019;

public partial class LogGroupRequest
{
    public uint IdRequest { get; set; }

    public uint IdMember { get; set; }

    public ushort IdGroup { get; set; }

    public uint TimeApplied { get; set; }

    public string Reason { get; set; } = null!;
}
