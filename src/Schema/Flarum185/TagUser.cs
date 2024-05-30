namespace Schema.Flarum185;

public partial class TagUser
{
    public uint UserId { get; set; }

    public uint TagId { get; set; }

    public DateTime? MarkedAsReadAt { get; set; }

    public bool IsHidden { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
