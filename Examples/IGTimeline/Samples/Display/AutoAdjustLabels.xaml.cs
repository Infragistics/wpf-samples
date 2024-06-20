using System;
using System.Collections.ObjectModel;
using System.Windows;
using IGTimeline.Resources;
using Infragistics.Controls.Timelines;

namespace IGTimeline.Samples
{
    public partial class AutoAdjustLabels : Infragistics.Samples.Framework.SampleContainer
    {
        public AutoAdjustLabels()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.xamTimeLine.Series.Clear();
            Random r = new Random();

            ObservableCollection<DateTimeData> dt_collection = new ObservableCollection<DateTimeData>();
            dt_collection.Add(new DateTimeData()
            {
                Time = new DateTime(2011, 1, 7, 7, 10, 10),
                Duration = new TimeSpan(10, 0, 0),
                Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title1,
                Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details1
            });
            dt_collection.Add(new DateTimeData()
            {
                Time = new DateTime(2011, 2, 7, 7, 10, 10),
                Duration = new TimeSpan(20, 0, 0),
                Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title2,
                Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details2
            });
            dt_collection.Add(new DateTimeData()
            {
                Time = new DateTime(2011, 3, 7, 7, 10, 10),
                Duration = new TimeSpan(15, 0, 0),
                Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title3,
                Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details3
            });
            dt_collection.Add(new DateTimeData()
            {
                Time = new DateTime(2011, 3, 7, 7, 10, 10),
                Duration = new TimeSpan(18, 0, 0),
                Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title4,
                Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details4
            });
            dt_collection.Add(new DateTimeData()
            {
                Time = new DateTime(2011, 3, 25, 7, 10, 10),
                Duration = new TimeSpan(12, 0, 0),
                Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title5,
                Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details5
            });
            dt_collection.Add(new DateTimeData()
            {
                Time = new DateTime(2011, 4, 25, 10, 10, 10),
                Duration = new TimeSpan(10, 0, 0),
                Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title6,
                Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details6
            });
            dt_collection.Add(new DateTimeData()
            {
                Time = new DateTime(2011, 4, 1, 1, 10, 10),
                Duration = new TimeSpan(15, 0, 0),
                Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title7,
                Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details7
            });
            dt_collection.Add(new DateTimeData()
            {
                Time = new DateTime(2011, 4, 10, 10, 10, 10),
                Duration = new TimeSpan(7, 0, 0),
                Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title8,
                Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details8
            });
            dt_collection.Add(new DateTimeData()
            {
                Time = new DateTime(2011, 4, 20, 10, 10, 10),
                Duration = new TimeSpan(11, 0, 0),
                Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title9,
                Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details9
            });
            dt_collection.Add(new DateTimeData()
            {
                Time = new DateTime(2011, 5, 10, 10, 10, 10),
                Duration = new TimeSpan(16, 0, 0),
                Title = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Title10,
                Details = TimelineStrings.XWT_SimpleTimeline_SeriesFilm_Details10
            });

            DateTimeSeries series = new DateTimeSeries();
            series.Title = TimelineStrings.XWT_DateTimeTimeline_SeriesTitle;
            series.DataSource = dt_collection;

            // Data Mapping
            series.DataMapping = "Time=Time;Duration=Duration;Title=Title;Details=Details";
            this.xamTimeLine.Series.Add(series);
        }
    }

    public class DateTimeData
    {
        public DateTimeData() { }
        public DateTimeData(DateTime newTime, TimeSpan newDuration, string newTitle, string newDetails)
        {
            Time = newTime;
            Duration = newDuration;
            Title = newTitle;
            Details = newDetails;
        }
        public DateTime Time { get; set; }
        public TimeSpan Duration { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
