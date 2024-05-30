namespace Schema.Smf2019;

public partial class LogMemberNotice
{
    public uint IdNotice { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;
}
