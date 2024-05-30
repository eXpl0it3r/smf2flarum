namespace Schema.Smf2019;

public partial class LogBanned
{
    public uint IdBanLog { get; set; }

    public uint IdMember { get; set; }

    public string Ip { get; set; } = null!;

    public string Email { get; set; } = null!;

    public uint LogTime { get; set; }
}
