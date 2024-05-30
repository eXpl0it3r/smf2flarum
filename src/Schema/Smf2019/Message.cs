namespace Schema.Smf2019;

public partial class Message
{
    public uint IdMsg { get; set; }

    public uint IdTopic { get; set; }

    public ushort IdBoard { get; set; }

    public uint PosterTime { get; set; }

    public uint IdMember { get; set; }

    public uint IdMsgModified { get; set; }

    public string Subject { get; set; } = null!;

    public string PosterName { get; set; } = null!;

    public string PosterEmail { get; set; } = null!;

    public string PosterIp { get; set; } = null!;

    public sbyte SmileysEnabled { get; set; }

    public uint ModifiedTime { get; set; }

    public string ModifiedName { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public sbyte Approved { get; set; }
}
