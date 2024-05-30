namespace Schema.Smf2019;

public partial class MessageIcon
{
    public ushort IdIcon { get; set; }

    public string Title { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public ushort IdBoard { get; set; }

    public ushort IconOrder { get; set; }
}
