using System;
using System.Windows;
using IGTimeline.Resources;
using Infragistics.Controls.Timelines;

namespace IGTimeline.Samples
{
    public partial class DateTimeTimeline : Infragistics.Samples.Framework.SampleContainer
    {
        public DateTimeTimeline()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DateTimeSeries series = new DateTimeSeries();
            series.Title = TimelineStrings.XWT_DateTimeTimeline_SeriesTitle;

            DateTime time = DateTime.Today;
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(19), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle1 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(20), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle2 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(20).AddMinutes(15), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle3 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(20).AddMinutes(30), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle4 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(20).AddMinutes(40), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle5 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(20).AddMinutes(50), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle6 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(21), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle7 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(21).AddMinutes(10), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle8 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(21).AddMinutes(20), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle9 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(21).AddMinutes(30), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle10 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(21).AddMinutes(40), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle11 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(21).AddMinutes(50), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle12 });
            series.Entries.Add(new DateTimeEntry { Time = time.AddHours(22), Title = TimelineStrings.XWT_DateTimeTimeline_EventTitle13 });
            this.timeline.Series.Add(series);

            this.SampleTitle.Text = TimelineStrings.XWT_DateTimeTimeline_SampleTitleLabel;
        }
    }
}
