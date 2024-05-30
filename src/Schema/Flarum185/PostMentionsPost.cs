namespace Schema.Flarum185;

public partial class PostMentionsPost
{
    public uint PostId { get; set; }

    public uint MentionsPostId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Post MentionsPost { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
