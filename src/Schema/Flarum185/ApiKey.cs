namespace Schema.Flarum185;

public partial class ApiKey
{
    public string Key { get; set; } = null!;

    public uint Id { get; set; }

    public string? AllowedIps { get; set; }

    public string? Scopes { get; set; }

    public uint? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastActivityAt { get; set; }

    public virtual User? User { get; set; }
}
