using System;

namespace Infragistics.Samples.Shared.Models
{
    public class TimelineData
    {
        public TimelineData(DateTime time, double duration, string title, string details)
        {
            Time = time;
            Duration = duration;
            Title = title;
            Details = details;
        }

        public TimelineData(DateTime time, string title, string details)
        {
            Time = time;
            Title = title;
            Details = details;
        }

        public DateTime Time { get; set; }
        public double Duration { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
