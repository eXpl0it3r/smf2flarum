namespace Schema.Smf2019;

public partial class PmRecipient
{
    public uint IdPm { get; set; }

    public uint IdMember { get; set; }

    public string Labels { get; set; } = null!;

    public byte Bcc { get; set; }

    public byte IsRead { get; set; }

    public byte IsNew { get; set; }

    public byte Deleted { get; set; }
}
