using AutoMapper;
using Schema.Smf2019;

namespace Mapping.Flarum185;

// NOTE: Flarum attachments are typically handled by extensions (like flarum/attachments).
// This converter provides a foundation but requires the appropriate Flarum extension to be installed.
// The exact schema depends on the extension used.
public class AttachmentProfile : Profile
{
    public AttachmentProfile()
    {
        // This is a placeholder mapping - actual implementation depends on the Flarum attachment extension
        // Common extensions: flarum/attachments, blomstra/upload-manager
        CreateMap<Attachment, AttachmentDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdAttach))
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.IdMsg))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdMember))
            .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => src.Filename))
            .ForMember(dest => dest.FileExtension, opt => opt.MapFrom(src => src.Fileext))
            .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.Downloads, opt => opt.MapFrom(src => src.Downloads))
            .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width))
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
            .ForMember(dest => dest.FileHash, opt => opt.MapFrom(src => src.FileHash));
    }
}

// DTO for attachment data - not a direct Flarum entity
// This represents the data structure that would need to be processed
// by the specific attachment extension being used
public class AttachmentDto
{
    public uint Id { get; set; }
    public uint PostId { get; set; }
    public uint UserId { get; set; }
    public string Filename { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public uint FileSize { get; set; }
    public uint Downloads { get; set; }
    public uint Width { get; set; }
    public uint Height { get; set; }
    public string FileHash { get; set; } = null!;
}