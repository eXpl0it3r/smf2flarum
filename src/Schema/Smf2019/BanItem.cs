namespace Schema.Smf2019;

public partial class BanItem
{
    public uint IdBan { get; set; }

    public ushort IdBanGroup { get; set; }

    public byte IpLow1 { get; set; }

    public byte IpHigh1 { get; set; }

    public byte IpLow2 { get; set; }

    public byte IpHigh2 { get; set; }

    public byte IpLow3 { get; set; }

    public byte IpHigh3 { get; set; }

    public byte IpLow4 { get; set; }

    public byte IpHigh4 { get; set; }

    public string Hostname { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public uint IdMember { get; set; }

    public uint Hits { get; set; }
}
