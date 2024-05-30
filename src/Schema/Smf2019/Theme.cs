namespace Schema.Smf2019;

public partial class Theme
{
    public int IdMember { get; set; }

    public byte IdTheme { get; set; }

    public string Variable { get; set; } = null!;

    public string Value { get; set; } = null!;
}
