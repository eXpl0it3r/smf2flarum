namespace Schema.Smf2019;

public partial class LogComment
{
    public uint IdComment { get; set; }

    public uint IdMember { get; set; }

    public string MemberName { get; set; } = null!;

    public string CommentType { get; set; } = null!;

    public uint IdRecipient { get; set; }

    public string RecipientName { get; set; } = null!;

    public int LogTime { get; set; }

    public uint IdNotice { get; set; }

    public sbyte Counter { get; set; }

    public string Body { get; set; } = null!;
}
