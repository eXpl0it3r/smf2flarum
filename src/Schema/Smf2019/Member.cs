namespace Schema.Smf2019;

public partial class Member
{
    public uint IdMember { get; set; }

    public string MemberName { get; set; } = null!;

    public uint DateRegistered { get; set; }

    public uint Posts { get; set; }

    public ushort IdGroup { get; set; }

    public string Lngfile { get; set; } = null!;

    public uint LastLogin { get; set; }

    public string RealName { get; set; } = null!;

    public short InstantMessages { get; set; }

    public short UnreadMessages { get; set; }

    public byte NewPm { get; set; }

    public string BuddyList { get; set; } = null!;

    public string PmIgnoreList { get; set; } = null!;

    public int PmPrefs { get; set; }

    public string ModPrefs { get; set; } = null!;

    public string MessageLabels { get; set; } = null!;

    public string Passwd { get; set; } = null!;

    public string OpenidUri { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string PersonalText { get; set; } = null!;

    public byte Gender { get; set; }

    public DateTime Birthdate { get; set; }

    public string WebsiteTitle { get; set; } = null!;

    public string WebsiteUrl { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Icq { get; set; } = null!;

    public string Aim { get; set; } = null!;

    public string Yim { get; set; } = null!;

    public string Msn { get; set; } = null!;

    public sbyte HideEmail { get; set; }

    public sbyte ShowOnline { get; set; }

    public string TimeFormat { get; set; } = null!;

    public string Signature { get; set; } = null!;

    public float TimeOffset { get; set; }

    public string Avatar { get; set; } = null!;

    public sbyte PmEmailNotify { get; set; }

    public ushort KarmaBad { get; set; }

    public ushort KarmaGood { get; set; }

    public string Usertitle { get; set; } = null!;

    public sbyte NotifyAnnouncements { get; set; }

    public sbyte NotifyRegularity { get; set; }

    public sbyte NotifySendBody { get; set; }

    public sbyte NotifyTypes { get; set; }

    public string MemberIp { get; set; } = null!;

    public string MemberIp2 { get; set; } = null!;

    public string SecretQuestion { get; set; } = null!;

    public string SecretAnswer { get; set; } = null!;

    public byte IdTheme { get; set; }

    public byte IsActivated { get; set; }

    public string ValidationCode { get; set; } = null!;

    public uint IdMsgLastVisit { get; set; }

    public string AdditionalGroups { get; set; } = null!;

    public string SmileySet { get; set; } = null!;

    public ushort IdPostGroup { get; set; }

    public uint TotalTimeLoggedIn { get; set; }

    public string PasswordSalt { get; set; } = null!;

    public string IgnoreBoards { get; set; } = null!;

    public sbyte Warning { get; set; }

    public string PasswdFlood { get; set; } = null!;

    public byte PmReceiveFrom { get; set; }
}
