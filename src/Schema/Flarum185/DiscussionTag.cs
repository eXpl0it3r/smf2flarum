namespace Schema.Flarum185;

public partial class DiscussionTag
{
    public uint DiscussionId { get; set; }

    public uint TagId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Discussion Discussion { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
