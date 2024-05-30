namespace Schema.Smf2019;

public partial class Subscription
{
    public uint IdSubscribe { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Cost { get; set; } = null!;

    public string Length { get; set; } = null!;

    public short IdGroup { get; set; }

    public string AddGroups { get; set; } = null!;

    public sbyte Active { get; set; }

    public sbyte Repeatable { get; set; }

    public sbyte AllowPartial { get; set; }

    public sbyte Reminder { get; set; }

    public string EmailComplete { get; set; } = null!;
}
