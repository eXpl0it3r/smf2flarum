namespace Schema.Smf2019;

public partial class PollChoice
{
    public uint IdPoll { get; set; }

    public byte IdChoice { get; set; }

    public string Label { get; set; } = null!;

    public ushort Votes { get; set; }
}
