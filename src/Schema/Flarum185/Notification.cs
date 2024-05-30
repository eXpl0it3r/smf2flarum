namespace Schema.Flarum185;

public partial class Notification
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public uint? FromUserId { get; set; }

    public string Type { get; set; } = null!;

    public uint? SubjectId { get; set; }

    public byte[]? Data { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? ReadAt { get; set; }

    public virtual User? FromUser { get; set; }

    public virtual User User { get; set; } = null!;
}
