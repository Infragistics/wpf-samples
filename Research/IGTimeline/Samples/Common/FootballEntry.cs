
namespace IGTimeline.Samples.Common
{
    public class FootballEntry
    {
        public FootballEntry()
        {

        }

        public double EventTime { get; set; }
        public string EventMinutes
        {
            get
            {
                return this.EventTime.ToString();
            }
        }
        public FootballEntryType EventType { get; set; }
        public string TeamName { get; set; }
        public string PlayerName { get; set; }
    }

    public enum FootballEntryType
    {
        Goal,
        YellowCard,
        RedCard,
        Foul
    }
}
