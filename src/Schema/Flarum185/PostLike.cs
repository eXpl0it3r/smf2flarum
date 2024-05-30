namespace Schema.Flarum185;

public partial class PostLike
{
    public uint PostId { get; set; }

    public uint UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
