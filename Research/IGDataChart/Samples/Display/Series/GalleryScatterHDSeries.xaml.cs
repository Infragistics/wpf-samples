using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Display
{
    public partial class GalleryScatterHDSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GalleryScatterHDSeries()
        {
            InitializeComponent();
            this.SampleLoaded += OnSampleLoaded;
             
        }
        protected IEnumerable HighDensityData = null;

        private void OnSampleLoaded(object sender, EventArgs e)
        {
            BindNewScatterData();
        }

        private void OnSeriesProgressiveLoadStatusChanged(object sender, ProgressiveLoadStatusEventArgs e)
        {
            this.SeriesLoadingProgressBar.Value = e.CurrentStatus;
            if (e.CurrentStatus == 100)
            {
                SeriesLoadingPanel.Visibility = Visibility.Collapsed;
                //OptionPanel.Visibility = Visibility.Visible;
            }
        }

        private void BindNewScatterData()
        {
            // generate new scatter data
            var amount = 950000;
            var data = new List<ScatterDataPoint>(amount);

            // random scatter data set 
            data.AddRange(new RandomScatterData(amount / 8, new Point(5000, 3000), new Point(3000, 2000)));
            data.AddRange(new RandomScatterData(amount / 8, new Point(6000, 5000), new Point(2000, 2000)));

            data.AddRange(new RandomScatterData(amount / 4, new Point(7000, 7000), new Point(5000, 4000)));

            data.AddRange(new RandomScatterData(amount / 8, new Point(8000, 9000), new Point(2000, 2000)));
            data.AddRange(new RandomScatterData(amount / 8, new Point(9000, 11000), new Point(3000, 2000)));
            this.HighDensityData = data;

            // bind scatter data
            this.DataChart.Series[0].ItemsSource = this.HighDensityData;
        }        
    }
    public class RandomScatterData : List<ScatterDataPoint>
    {
        private Random _rand = new Random();

        public RandomScatterData(int count, Point center, Point range)
            : base(count)
        {
            DateTime before = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                var xRange = _rand.NextDouble() * range.X;
                var yRange = _rand.NextDouble() * range.Y;

                double filp = 1; // _rand.NextDouble();
                double prop = _rand.NextDouble();
                if (prop < .25)
                {
                    xRange *= filp;
                    yRange *= filp;
                }
                else if (prop >= .25 && prop < .5)
                {
                    xRange *= -filp;
                    yRange *= filp;
                }
                else if (prop >= .5 && prop < .75)
                {
                    xRange *= filp;
                    yRange *= -filp;
                }
                else 
                {
                    xRange *= -filp;
                    yRange *= -filp;
                }
                double xDispersion = _rand.NextDouble() + 0.12;
                double yDispersion = _rand.NextDouble() + 0.12;
                this.Add(new ScatterDataPoint()
                {
                    XValue = center.X + (xRange * xDispersion),
                    YValue = center.Y + (yRange * yDispersion),
                });
            }
            DateTime after = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("data generation: " + (after - before).TotalMilliseconds + "ms");
        }
    }

    public class ScatterDataPoint
    {
        public double XValue { get; set; }
        public double YValue { get; set; }
    }

    public class BooleanList : List<bool>
    {
        public BooleanList()
        {
            this.Add(true);
            this.Add(false);
        }
    }
}
