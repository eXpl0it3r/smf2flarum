namespace Schema.Smf2019;

public partial class Topic
{
    public uint IdTopic { get; set; }

    public sbyte IsSticky { get; set; }

    public ushort IdBoard { get; set; }

    public uint IdFirstMsg { get; set; }

    public uint IdLastMsg { get; set; }

    public uint IdMemberStarted { get; set; }

    public uint IdMemberUpdated { get; set; }

    public uint IdPoll { get; set; }

    public short IdPreviousBoard { get; set; }

    public int IdPreviousTopic { get; set; }

    public uint NumReplies { get; set; }

    public uint NumViews { get; set; }

    public sbyte Locked { get; set; }

    public short UnapprovedPosts { get; set; }

    public sbyte Approved { get; set; }
}
