namespace Schema.Flarum185;

public partial class AccessToken
{
    public uint Id { get; set; }

    public string Token { get; set; } = null!;

    public uint UserId { get; set; }

    public DateTime? LastActivityAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Type { get; set; } = null!;

    public string? Title { get; set; }

    public string? LastIpAddress { get; set; }

    public string? LastUserAgent { get; set; }

    public virtual User User { get; set; } = null!;
}
