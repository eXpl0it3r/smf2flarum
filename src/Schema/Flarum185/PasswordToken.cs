namespace Schema.Flarum185;

public partial class PasswordToken
{
    public string Token { get; set; } = null!;

    public uint UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
