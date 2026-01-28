using System.Collections.ObjectModel;
using IGTimeline.Resources;
using Infragistics.Controls.Timelines;

namespace IGTimeline.Samples
{
    public class BicycleTimelineModel : TimelineBaseModel
    {
        public BicycleTimelineModel()
        {
            this.Title = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbikes_TitleLabel;
            this.Entries = new ObservableCollection<NumericTimeEntry>();
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbike_Details1, Time = 1886, Title = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbike_Title1 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbike_Details2, Time = 1898, Title = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbike_Title2 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbike_Details3, Time = 1900, Title = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbike_Title3 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbike_Details4, Time = 1903, Title = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbike_Title4 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbike_Details5, Time = 1914, Title = TimelineStrings.XWT_SimpleTimeline_SeriesMotorbike_Title5 });
        }
    }
}