namespace Schema.Smf2019;

public partial class LogPackage
{
    public int IdInstall { get; set; }

    public string Filename { get; set; } = null!;

    public string PackageId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Version { get; set; } = null!;

    public int IdMemberInstalled { get; set; }

    public string MemberInstalled { get; set; } = null!;

    public int TimeInstalled { get; set; }

    public int IdMemberRemoved { get; set; }

    public string MemberRemoved { get; set; } = null!;

    public int TimeRemoved { get; set; }

    public sbyte InstallState { get; set; }

    public string FailedSteps { get; set; } = null!;

    public string ThemesInstalled { get; set; } = null!;

    public string DbChanges { get; set; } = null!;
}
