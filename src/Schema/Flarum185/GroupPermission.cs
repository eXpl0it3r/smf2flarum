namespace Schema.Flarum185;

public partial class GroupPermission
{
    public uint GroupId { get; set; }

    public string Permission { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Group Group { get; set; } = null!;
}
