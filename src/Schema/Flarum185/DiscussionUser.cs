namespace Schema.Flarum185;

public partial class DiscussionUser
{
    public uint UserId { get; set; }

    public uint DiscussionId { get; set; }

    public DateTime? LastReadAt { get; set; }

    public uint? LastReadPostNumber { get; set; }

    public string? Subscription { get; set; }

    public virtual Discussion Discussion { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
