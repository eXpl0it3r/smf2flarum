namespace Schema.Flarum185;

public partial class Flag
{
    public uint Id { get; set; }

    public uint PostId { get; set; }

    public string Type { get; set; } = null!;

    public uint? UserId { get; set; }

    public string? Reason { get; set; }

    public string? ReasonDetail { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User? User { get; set; }
}
