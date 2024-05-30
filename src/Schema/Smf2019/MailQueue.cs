namespace Schema.Smf2019;

public partial class MailQueue
{
    public uint IdMail { get; set; }

    public int TimeSent { get; set; }

    public string Recipient { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Headers { get; set; } = null!;

    public sbyte SendHtml { get; set; }

    public sbyte Priority { get; set; }

    public bool Private { get; set; }
}
