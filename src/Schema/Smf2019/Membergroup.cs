namespace Schema.Smf2019;

public partial class Membergroup
{
    public ushort IdGroup { get; set; }

    public string GroupName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string OnlineColor { get; set; } = null!;

    public int MinPosts { get; set; }

    public ushort MaxMessages { get; set; }

    public string Stars { get; set; } = null!;

    public sbyte GroupType { get; set; }

    public sbyte Hidden { get; set; }

    public short IdParent { get; set; }
}
