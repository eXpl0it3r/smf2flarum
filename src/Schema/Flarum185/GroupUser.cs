namespace Schema.Flarum185;

public partial class GroupUser
{
    public uint UserId { get; set; }

    public uint GroupId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
