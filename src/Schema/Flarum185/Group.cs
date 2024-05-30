namespace Schema.Flarum185;

public partial class Group
{
    public uint Id { get; set; }

    public string NameSingular { get; set; } = null!;

    public string NamePlural { get; set; } = null!;

    public string? Color { get; set; }

    public string? Icon { get; set; }

    public bool IsHidden { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();

    public virtual ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();

    public virtual ICollection<PostMentionsGroup> PostMentionsGroups { get; set; } = new List<PostMentionsGroup>();
}
