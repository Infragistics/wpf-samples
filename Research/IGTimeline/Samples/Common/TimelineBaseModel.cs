using System.Collections.ObjectModel;
using Infragistics.Controls.Timelines;

namespace IGTimeline.Samples
{
    public class TimelineBaseModel
    {
        public string Title { get; set; }
        public ObservableCollection<NumericTimeEntry> Entries { get; set; }

    }
}