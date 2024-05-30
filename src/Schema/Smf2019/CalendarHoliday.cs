namespace Schema.Smf2019;

public partial class CalendarHoliday
{
    public ushort IdHoliday { get; set; }

    public DateTime EventDate { get; set; }

    public string Title { get; set; } = null!;
}
