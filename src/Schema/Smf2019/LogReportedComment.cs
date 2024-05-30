namespace Schema.Smf2019;

public partial class LogReportedComment
{
    public uint IdComment { get; set; }

    public int IdReport { get; set; }

    public int IdMember { get; set; }

    public string Membername { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string MemberIp { get; set; } = null!;

    public string Comment { get; set; } = null!;

    public int TimeSent { get; set; }
}
