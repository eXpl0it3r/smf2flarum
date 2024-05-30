namespace Schema.Smf2019;

public partial class PersonalMessage
{
    public uint IdPm { get; set; }

    public uint IdPmHead { get; set; }

    public uint IdMemberFrom { get; set; }

    public byte DeletedBySender { get; set; }

    public string FromName { get; set; } = null!;

    public uint Msgtime { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;
}
