namespace Schema.Smf2019;

public partial class CustomField
{
    public short IdField { get; set; }

    public string ColName { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public string FieldDesc { get; set; } = null!;

    public string FieldType { get; set; } = null!;

    public short FieldLength { get; set; }

    public string FieldOptions { get; set; } = null!;

    public string Mask { get; set; } = null!;

    public sbyte ShowReg { get; set; }

    public sbyte ShowDisplay { get; set; }

    public string ShowProfile { get; set; } = null!;

    public sbyte Private { get; set; }

    public sbyte Active { get; set; }

    public sbyte Bbc { get; set; }

    public sbyte CanSearch { get; set; }

    public string DefaultValue { get; set; } = null!;

    public string Enclose { get; set; } = null!;

    public sbyte Placement { get; set; }
}
