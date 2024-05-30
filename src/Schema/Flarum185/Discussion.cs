namespace Schema.Flarum185;

public partial class Discussion
{
    public uint Id { get; set; }

    public string Title { get; set; } = null!;

    public int CommentCount { get; set; }

    public uint ParticipantCount { get; set; }

    public uint PostNumberIndex { get; set; }

    public DateTime CreatedAt { get; set; }

    public uint? UserId { get; set; }

    public uint? FirstPostId { get; set; }

    public DateTime? LastPostedAt { get; set; }

    public uint? LastPostedUserId { get; set; }

    public uint? LastPostId { get; set; }

    public uint? LastPostNumber { get; set; }

    public DateTime? HiddenAt { get; set; }

    public uint? HiddenUserId { get; set; }

    public string Slug { get; set; } = null!;

    public bool IsPrivate { get; set; }

    public bool? IsApproved { get; set; }

    public bool IsSticky { get; set; }

    public bool IsLocked { get; set; }

    public virtual ICollection<DiscussionTag> DiscussionTags { get; set; } = new List<DiscussionTag>();

    public virtual ICollection<DiscussionUser> DiscussionUsers { get; set; } = new List<DiscussionUser>();

    public virtual Post? FirstPost { get; set; }

    public virtual User? HiddenUser { get; set; }

    public virtual Post? LastPost { get; set; }

    public virtual User? LastPostedUser { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual User? User { get; set; }
}
