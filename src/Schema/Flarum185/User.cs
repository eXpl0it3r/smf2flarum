namespace Schema.Flarum185;

public partial class User
{
    public uint Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsEmailConfirmed { get; set; }

    public string Password { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public byte[]? Preferences { get; set; }

    public DateTime? JoinedAt { get; set; }

    public DateTime? LastSeenAt { get; set; }

    public DateTime? MarkedAllAsReadAt { get; set; }

    public DateTime? ReadNotificationsAt { get; set; }

    public uint DiscussionCount { get; set; }

    public uint CommentCount { get; set; }

    public DateTime? ReadFlagsAt { get; set; }

    public DateTime? SuspendedUntil { get; set; }

    public string? SuspendReason { get; set; }

    public string? SuspendMessage { get; set; }

    public virtual ICollection<AccessToken> AccessTokens { get; set; } = new List<AccessToken>();

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<Discussion> DiscussionHiddenUsers { get; set; } = new List<Discussion>();

    public virtual ICollection<Discussion> DiscussionLastPostedUsers { get; set; } = new List<Discussion>();

    public virtual ICollection<Discussion> DiscussionUsers { get; set; } = new List<Discussion>();

    public virtual ICollection<DiscussionUser> DiscussionUsersNavigation { get; set; } = new List<DiscussionUser>();

    public virtual ICollection<EmailToken> EmailTokens { get; set; } = new List<EmailToken>();

    public virtual ICollection<Flag> Flags { get; set; } = new List<Flag>();

    public virtual ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();

    public virtual ICollection<LoginProvider> LoginProviders { get; set; } = new List<LoginProvider>();

    public virtual ICollection<Notification> NotificationFromUsers { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationUsers { get; set; } = new List<Notification>();

    public virtual ICollection<PasswordToken> PasswordTokens { get; set; } = new List<PasswordToken>();

    public virtual ICollection<Post> PostEditedUsers { get; set; } = new List<Post>();

    public virtual ICollection<Post> PostHiddenUsers { get; set; } = new List<Post>();

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual ICollection<PostMentionsUser> PostMentionsUsers { get; set; } = new List<PostMentionsUser>();

    public virtual ICollection<Post> PostUsers { get; set; } = new List<Post>();

    public virtual ICollection<TagUser> TagUsers { get; set; } = new List<TagUser>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
