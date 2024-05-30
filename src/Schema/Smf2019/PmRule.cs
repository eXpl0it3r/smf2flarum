namespace Schema.Smf2019;

public partial class PmRule
{
    public uint IdRule { get; set; }

    public uint IdMember { get; set; }

    public string RuleName { get; set; } = null!;

    public string Criteria { get; set; } = null!;

    public string Actions { get; set; } = null!;

    public byte DeletePm { get; set; }

    public byte IsOr { get; set; }
}
