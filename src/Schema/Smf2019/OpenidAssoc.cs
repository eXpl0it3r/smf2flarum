namespace Schema.Smf2019;

public partial class OpenidAssoc
{
    public string ServerUrl { get; set; } = null!;

    public string Handle { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public int Issued { get; set; }

    public int Expires { get; set; }

    public string AssocType { get; set; } = null!;
}
