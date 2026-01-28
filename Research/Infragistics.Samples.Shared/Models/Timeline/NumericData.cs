
namespace Infragistics.Samples.Shared.Models
{
    public class NumericData
    {
        public NumericData()
        {

        }

        public NumericData(int time, double duration, string title, string details)
        {
            Time = time;
            Duration = duration;
            Title = title;
            Details = details;
        }

        public NumericData(int time, string title, string details)
        {
            Time = time;
            Title = title;
            Details = details;
        }

        public int Time { get; set; }
        public double Duration { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
