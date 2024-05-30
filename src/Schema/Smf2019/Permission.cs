namespace Schema.Smf2019;

public partial class Permission
{
    public short IdGroup { get; set; }

    public string Permission1 { get; set; } = null!;

    public sbyte AddDeny { get; set; }
}
