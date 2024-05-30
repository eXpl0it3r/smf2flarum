namespace Schema.Flarum185;

public partial class PostMentionsGroup
{
    public uint PostId { get; set; }

    public uint MentionsGroupId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Group MentionsGroup { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
