namespace Schema.Flarum185;

public partial class PostMentionsTag
{
    public uint PostId { get; set; }

    public uint MentionsTagId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Tag MentionsTag { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
