namespace Schema.Flarum185;

public partial class RegistrationToken
{
    public string Token { get; set; } = null!;

    public string? Payload { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string Provider { get; set; } = null!;

    public string Identifier { get; set; } = null!;

    public string? UserAttributes { get; set; }
}
