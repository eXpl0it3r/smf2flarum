namespace Schema.Smf2019;

public partial class Attachment
{
    public uint IdAttach { get; set; }

    public uint IdThumb { get; set; }

    public uint IdMsg { get; set; }

    public uint IdMember { get; set; }

    public sbyte IdFolder { get; set; }

    public byte AttachmentType { get; set; }

    public string Filename { get; set; } = null!;

    public string FileHash { get; set; } = null!;

    public string Fileext { get; set; } = null!;

    public uint Size { get; set; }

    public uint Downloads { get; set; }

    public uint Width { get; set; }

    public uint Height { get; set; }

    public string MimeType { get; set; } = null!;

    public sbyte Approved { get; set; }
}
