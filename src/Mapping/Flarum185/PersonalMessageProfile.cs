namespace Mapping.Flarum185;

using AutoMapper;
using Schema.Smf2019;

/// <summary>
/// AutoMapper profile for exporting SMF private messages to DTOs.
/// Since Flarum doesn't have built-in private messaging, this exports data
/// for extensions like fof/byobu or flarum-private-messages.
/// </summary>
public class PersonalMessageProfile : Profile
{
    public PersonalMessageProfile()
    {
        // Map SMF PersonalMessage to DTO for extension compatibility
        CreateMap<PersonalMessage, PersonalMessageDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPm))
            .ForMember(dest => dest.ThreadId, opt => opt.MapFrom(src => src.IdPmHead))
            .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.IdMemberFrom))
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.FromName))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Body))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => 
                Mapping.Converter.UnixTimeStampToDateTime(src.Msgtime) ?? DateTime.UtcNow))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.DeletedBySender == 1));

        // Map SMF PmRecipient to DTO for recipient tracking
        CreateMap<PmRecipient, PmRecipientDto>()
            .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.IdPm))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdMember))
            .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.Labels))
            .ForMember(dest => dest.IsBcc, opt => opt.MapFrom(src => src.Bcc == 1))
            .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => src.IsRead == 1))
            .ForMember(dest => dest.IsNew, opt => opt.MapFrom(src => src.IsNew == 1))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Deleted == 1))
            .ForMember(dest => dest.ReadAt, opt => opt.MapFrom(src => 
                src.IsRead == 1 ? DateTime.UtcNow : (DateTime?)null)); // SMF doesn't store read timestamp
    }
}

/// <summary>
/// DTO for private message data - compatible with Flarum PM extensions.
/// Structure is designed to work with fof/byobu and similar extensions.
/// </summary>
public class PersonalMessageDto
{
    public uint Id { get; set; }
    public uint ThreadId { get; set; }
    public uint SenderId { get; set; }
    public string SenderName { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    
    // Recipients will be populated separately
    public List<PmRecipientDto> Recipients { get; set; } = new();
}

/// <summary>
/// DTO for private message recipient data.
/// </summary>
public class PmRecipientDto
{
    public uint MessageId { get; set; }
    public uint UserId { get; set; }
    public string Labels { get; set; } = null!;
    public bool IsBcc { get; set; }
    public bool IsRead { get; set; }
    public bool IsNew { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? ReadAt { get; set; }
}

/// <summary>
/// DTO for private message thread data - groups related messages.
/// </summary>
public class PmThreadDto
{
    public uint ThreadId { get; set; }
    public string Subject { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<uint> ParticipantIds { get; set; } = new();
    public List<PersonalMessageDto> Messages { get; set; } = new();
}