namespace Schema.Flarum185;

public partial class Tag
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? Color { get; set; }

    public string? BackgroundPath { get; set; }

    public string? BackgroundMode { get; set; }

    public int? Position { get; set; }

    public uint? ParentId { get; set; }

    public string? DefaultSort { get; set; }

    public bool IsRestricted { get; set; }

    public bool IsHidden { get; set; }

    public uint DiscussionCount { get; set; }

    public DateTime? LastPostedAt { get; set; }

    public uint? LastPostedDiscussionId { get; set; }

    public uint? LastPostedUserId { get; set; }

    public string? Icon { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<DiscussionTag> DiscussionTags { get; set; } = new List<DiscussionTag>();

    public virtual ICollection<Tag> InverseParent { get; set; } = new List<Tag>();

    public virtual Discussion? LastPostedDiscussion { get; set; }

    public virtual User? LastPostedUser { get; set; }

    public virtual Tag? Parent { get; set; }

    public virtual ICollection<PostMentionsTag> PostMentionsTags { get; set; } = new List<PostMentionsTag>();

    public virtual ICollection<TagUser> TagUsers { get; set; } = new List<TagUser>();
}
