namespace Schema.Smf2019;

public partial class Category
{
    public byte IdCat { get; set; }

    public sbyte CatOrder { get; set; }

    public string Name { get; set; } = null!;

    public bool? CanCollapse { get; set; }
}
