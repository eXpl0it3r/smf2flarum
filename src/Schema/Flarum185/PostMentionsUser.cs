namespace Schema.Flarum185;

public partial class PostMentionsUser
{
    public uint PostId { get; set; }

    public uint MentionsUserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User MentionsUser { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
