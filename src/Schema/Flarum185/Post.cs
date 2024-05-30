namespace Schema.Flarum185;

public partial class Post
{
    public uint Id { get; set; }

    public uint DiscussionId { get; set; }

    public uint? Number { get; set; }

    public DateTime CreatedAt { get; set; }

    public uint? UserId { get; set; }

    public string? Type { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string? Content { get; set; }

    public DateTime? EditedAt { get; set; }

    public uint? EditedUserId { get; set; }

    public DateTime? HiddenAt { get; set; }

    public uint? HiddenUserId { get; set; }

    public string? IpAddress { get; set; }

    public bool IsPrivate { get; set; }

    public bool? IsApproved { get; set; }

    public virtual Discussion Discussion { get; set; } = null!;

    public virtual ICollection<Discussion> DiscussionFirstPosts { get; set; } = new List<Discussion>();

    public virtual ICollection<Discussion> DiscussionLastPosts { get; set; } = new List<Discussion>();

    public virtual User? EditedUser { get; set; }

    public virtual ICollection<Flag> Flags { get; set; } = new List<Flag>();

    public virtual User? HiddenUser { get; set; }

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual ICollection<PostMentionsGroup> PostMentionsGroups { get; set; } = new List<PostMentionsGroup>();

    public virtual ICollection<PostMentionsPost> PostMentionsPostMentionsPosts { get; set; } = new List<PostMentionsPost>();

    public virtual ICollection<PostMentionsPost> PostMentionsPostPosts { get; set; } = new List<PostMentionsPost>();

    public virtual ICollection<PostMentionsTag> PostMentionsTags { get; set; } = new List<PostMentionsTag>();

    public virtual ICollection<PostMentionsUser> PostMentionsUsers { get; set; } = new List<PostMentionsUser>();

    public virtual User? User { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
