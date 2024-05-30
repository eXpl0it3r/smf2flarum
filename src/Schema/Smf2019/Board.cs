namespace Schema.Smf2019;

public partial class Board
{
    public ushort IdBoard { get; set; }

    public byte IdCat { get; set; }

    public byte ChildLevel { get; set; }

    public ushort IdParent { get; set; }

    public short BoardOrder { get; set; }

    public uint IdLastMsg { get; set; }

    public uint IdMsgUpdated { get; set; }

    public string MemberGroups { get; set; } = null!;

    public ushort IdProfile { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public uint NumTopics { get; set; }

    public uint NumPosts { get; set; }

    public sbyte CountPosts { get; set; }

    public byte IdTheme { get; set; }

    public byte OverrideTheme { get; set; }

    public short UnapprovedPosts { get; set; }

    public short UnapprovedTopics { get; set; }

    public string Redirect { get; set; } = null!;
}
