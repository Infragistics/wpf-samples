using System;
using System.Windows;
using System.Windows.Data;
using Infragistics.Controls;
using Infragistics.Controls.Timelines;

namespace IGTimeline.Samples
{
    public partial class TimelinesSynchronization : Infragistics.Samples.Framework.SampleContainer
    {
        public TimelinesSynchronization()
        {
            InitializeComponent();
            this.SampleLoaded += OnSampleLoaded;
            
        }

        void OnSampleLoaded(object sender, System.EventArgs e)
        {
            // Initialize Timeline series
            var filmTimelineSource = new FilmTimelineModel();

            var filmTimelineSeries = (this.FilmTimeline.Series[0] as NumericTimeSeries);
            if (filmTimelineSeries != null)
            {
                filmTimelineSeries.Title = filmTimelineSource.Title;
                filmTimelineSeries.Entries = filmTimelineSource.Entries;
            }

            var bicycleTimelineSource = new BicycleTimelineModel();
            var bicycleTimelineSeries = (this.BicycleTimeline.Series[0] as NumericTimeSeries);
            if (bicycleTimelineSeries != null)
            {
                bicycleTimelineSeries.Title = bicycleTimelineSource.Title;
                bicycleTimelineSeries.Entries = bicycleTimelineSource.Entries;
            }
        }

        #region Synchronisation

        //The binding used in the synchronisation
        //The Mode is set to TwoWay so that if one of the zoombars changes -> all change
        private Binding _binding = new Binding { ElementName = "Zoombar", Path = new PropertyPath("Range"), Mode = BindingMode.TwoWay };

        private void Timeline_Loaded(object sender, RoutedEventArgs e)
        {
            //When a xamTimeline control loads, it's zoombar's Range
            //property is data bound to the Range property of the
            //xamZoombar instance in the sample
            XamTimeline timeline = sender as XamTimeline;
            if (timeline != null)
            {
                this.Dispatcher.BeginInvoke((Action)(() =>
                {
                    timeline.Zoombar.SetBinding(XamZoombar.RangeProperty, _binding);
                }));
                
                
            }
        }

        #endregion
    }
}
