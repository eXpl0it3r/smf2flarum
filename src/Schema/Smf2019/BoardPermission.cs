namespace Schema.Smf2019;

public partial class BoardPermission
{
    public short IdGroup { get; set; }

    public ushort IdProfile { get; set; }

    public string Permission { get; set; } = null!;

    public sbyte AddDeny { get; set; }
}
