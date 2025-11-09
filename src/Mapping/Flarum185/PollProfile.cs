namespace Mapping.Flarum185;

using AutoMapper;
using Schema.Smf2019;

/// <summary>
/// AutoMapper profile for Poll and PollChoice entities.
/// Note: Polls require the FoF Polls extension (fof/polls) to be installed in Flarum.
/// This profile creates DTOs since actual poll entities are extension-dependent.
/// </summary>
public class PollProfile : Profile
{
    public PollProfile()
    {
        // Map SMF Poll to a DTO structure for FoF Polls extension
        CreateMap<Poll, PollDto>()
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
            .ForMember(dest => dest.MaxVotes, opt => opt.MapFrom(src => src.MaxVotes))
            .ForMember(dest => dest.ExpireTime, opt => opt.MapFrom(src => 
                src.ExpireTime == 0 ? (DateTime?)null : DateTimeOffset.FromUnixTimeSeconds(src.ExpireTime).DateTime))
            .ForMember(dest => dest.HideResults, opt => opt.MapFrom(src => src.HideResults))
            .ForMember(dest => dest.ChangeVote, opt => opt.MapFrom(src => src.ChangeVote))
            .ForMember(dest => dest.GuestVote, opt => opt.MapFrom(src => src.GuestVote))
            .ForMember(dest => dest.IsLocked, opt => opt.MapFrom(src => src.VotingLocked))
            .ForMember(dest => dest.TopicId, opt => opt.Ignore()); // Poll doesn't directly reference topic - Topic references Poll via IdPoll

        // Map SMF PollChoice to DTO structure
        CreateMap<PollChoice, PollChoiceDto>()
            .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Label))
            .ForMember(dest => dest.VoteCount, opt => opt.MapFrom(src => src.Votes))
            .ForMember(dest => dest.PollId, opt => opt.MapFrom(src => src.IdPoll));
    }
}

/// <summary>
/// DTO for Poll data - corresponds to FoF Polls extension structure
/// </summary>
public class PollDto
{
    public string Question { get; set; } = null!;
    public byte MaxVotes { get; set; }
    public DateTime? ExpireTime { get; set; }
    public bool HideResults { get; set; }
    public bool ChangeVote { get; set; }
    public bool GuestVote { get; set; }
    public bool IsLocked { get; set; }
    public uint TopicId { get; set; }
}

/// <summary>
/// DTO for Poll Choice data - corresponds to FoF Polls extension structure
/// </summary>
public class PollChoiceDto
{
    public string Answer { get; set; } = null!;
    public ushort VoteCount { get; set; }
    public uint PollId { get; set; }
}