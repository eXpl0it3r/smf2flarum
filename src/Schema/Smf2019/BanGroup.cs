namespace Schema.Smf2019;

public partial class BanGroup
{
    public uint IdBanGroup { get; set; }

    public string Name { get; set; } = null!;

    public uint BanTime { get; set; }

    public uint? ExpireTime { get; set; }

    public byte CannotAccess { get; set; }

    public byte CannotRegister { get; set; }

    public byte CannotPost { get; set; }

    public byte CannotLogin { get; set; }

    public string Reason { get; set; } = null!;

    public string Notes { get; set; } = null!;
}
