namespace Schema.Smf2019;

public partial class Poll
{
    public uint IdPoll { get; set; }

    public string Question { get; set; } = null!;

    public bool VotingLocked { get; set; }

    public byte MaxVotes { get; set; }

    public uint ExpireTime { get; set; }

    public byte HideResults { get; set; }

    public byte ChangeVote { get; set; }

    public byte GuestVote { get; set; }

    public uint NumGuestVoters { get; set; }

    public uint ResetPoll { get; set; }

    public int IdMember { get; set; }

    public string PosterName { get; set; } = null!;
}
