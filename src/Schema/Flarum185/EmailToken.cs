namespace Schema.Flarum185;

public partial class EmailToken
{
    public string Token { get; set; } = null!;

    public string Email { get; set; } = null!;

    public uint UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
