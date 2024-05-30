namespace Schema.Smf2019;

public partial class Session
{
    public string SessionId { get; set; } = null!;

    public uint LastUpdate { get; set; }

    public string Data { get; set; } = null!;
}
