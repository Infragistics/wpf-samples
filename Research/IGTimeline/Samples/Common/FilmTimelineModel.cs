using System.Collections.ObjectModel;
using IGTimeline.Resources;
using Infragistics.Controls.Timelines;

namespace IGTimeline.Samples
{
    public class FilmTimelineModel : TimelineBaseModel
    {
        public FilmTimelineModel()
        {
            this.Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_TitleLabel;
            this.Entries = new ObservableCollection<NumericTimeEntry>();
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details1, Time = 1886, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title1 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details2, Time = 1888, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title2 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details3, Time = 1889, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title3 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details4, Time = 1890, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title4 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details5, Time = 1891, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title5 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details6, Time = 1892, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title6 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details7, Time = 1893, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title7 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details8, Time = 1894, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title8 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details9, Time = 1895, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title9 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details10, Time = 1896, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title10 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details11, Time = 1897, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title11 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details12, Time = 1898, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title12 });
            this.Entries.Add(new NumericTimeEntry { Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details13, Time = 1899, Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title13 });
        }
    }
}