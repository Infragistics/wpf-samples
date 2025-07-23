using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;
using System.Xml.Linq;
using IGTimeline.Resources;
using Infragistics.Controls.Timelines;
using Infragistics.Samples.Shared.DataProviders;

namespace IGTimeline.Samples
{
    public partial class NumericTimeTimeline : Infragistics.Samples.Framework.SampleContainer
    {
        XmlDataProvider _dataSource;

        public NumericTimeTimeline()
        {
            InitializeComponent();
            this.SampleLoaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, EventArgs e)
        {
            this._dataSource = new XmlDataProvider();
            this._dataSource.GetXmlDataCompleted += OnGetXmlDataCompleted;
            this._dataSource.GetXmlDataAsync("RomanHistory.xml");

            this.Timeline.Series[0].Title = TimelineStrings.XWT_NumericTimeline_SeriesRomanEmpireLabel;
            this.Timeline.Series[1].Title = TimelineStrings.XWT_NumericTimeline_SeriesRomanKingdomLabel;
            this.Timeline.Series[2].Title = TimelineStrings.XWT_NumericTimeline_SeriesByzantineLabel;
            this.Timeline.Series[3].Title = TimelineStrings.XWT_NumericTimeline_SeriesMedievalRomeLabel;

            this.SampleTitle.Text = TimelineStrings.XWT_NumericTimeline_SampleTitleLabel;
        }
        void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.HasError) return;

            XDocument doc = e.Result;
            List<XElement> seriesElements = doc.Element("RomanHistory").Elements("Series").ToList();

            Debug.Assert(seriesElements.Count == this.Timeline.Series.Count, 
                "Invalid number of <Series> in RomanHistory.xml");

            int tickCount = 0;

            var timer = new DispatcherTimer();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += delegate
            {
                int index = tickCount++;

                if (index == 0)
                    timer.Interval = TimeSpan.FromMilliseconds(100);

                if (index >= this.Timeline.Series.Count)
                {
                    timer.Stop();
                    return;
                }

                var entries =
                  from entryElem in seriesElements[index].Elements("NumericTimeEntry")
                  let duration = entryElem.Attribute("Duration").Value
                  select new NumericTimeEntry
                  {
                      Time = (double)entryElem.Attribute("Time"),
                      Title = (string)entryElem.Attribute("Title"),
                      Details = (string)entryElem.Attribute("Details"),
                      Duration = string.IsNullOrEmpty(duration) ? 0.0 : double.Parse(duration)
                  };

                var series = this.Timeline.Series[index] as NumericTimeSeries;
                foreach (NumericTimeEntry entry in entries)
                {
                    series.Entries.Add(entry);
                }
            };

            timer.Start();
        }
    }
}
