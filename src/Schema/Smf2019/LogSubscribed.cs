namespace Schema.Smf2019;

public partial class LogSubscribed
{
    public uint IdSublog { get; set; }

    public uint IdSubscribe { get; set; }

    public int IdMember { get; set; }

    public short OldIdGroup { get; set; }

    public int StartTime { get; set; }

    public int EndTime { get; set; }

    public sbyte Status { get; set; }

    public sbyte PaymentsPending { get; set; }

    public string PendingDetails { get; set; } = null!;

    public sbyte ReminderSent { get; set; }

    public string VendorRef { get; set; } = null!;
}
