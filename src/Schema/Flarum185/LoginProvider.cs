namespace Schema.Flarum185;

public partial class LoginProvider
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public string Provider { get; set; } = null!;

    public string Identifier { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public virtual User User { get; set; } = null!;
}
