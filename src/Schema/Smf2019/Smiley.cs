namespace Schema.Smf2019;

public partial class Smiley
{
    public ushort IdSmiley { get; set; }

    public string Code { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte SmileyRow { get; set; }

    public ushort SmileyOrder { get; set; }

    public byte Hidden { get; set; }
}
