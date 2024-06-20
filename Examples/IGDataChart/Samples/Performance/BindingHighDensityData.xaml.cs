using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Infragistics.Controls.Charts;
using System.Windows.Media;
using Infragistics;

namespace IGDataChart.Samples.Performance
{
    public partial class BindingHighDensityData : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingHighDensityData()
        {
            InitializeComponent();
            this.SampleLoaded += OnSampleLoaded;
             
        }
        protected IEnumerable HighDensityData = null;
        protected bool IsGeneratingData;

        private void OnSampleLoaded(object sender, EventArgs e)
        {
            BindNewScatterData();
        }

        private void OnDataAmountSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DataAmountSlider == null)
            {
                return;
            }
            DataAmountSlider.Value = Math.Round(DataAmountSlider.Value);
        }

        private void OnGenerateScatterDataButtonClick(object sender, RoutedEventArgs e)
        {
            this.SeriesLoadingProgressBar.Opacity = 1;
            BindNewScatterData();
        }

        private void OnSeriesResolutionSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SeriesResolutionSlider == null)
            {
                return;
            }
            SeriesResolutionSlider.Value = Math.Round(SeriesResolutionSlider.Value);
            this.DataChart.Series[0].Resolution = SeriesResolutionSlider.Value;
        }

        private void OnSeriesProgressiveLoadStatusChanged(object sender, ProgressiveLoadStatusEventArgs e)
        {
            this.SeriesLoadingProgressBar.Value = e.CurrentStatus;
            if (e.CurrentStatus == 100)
            {
                this.SeriesLoadingProgressBar.Opacity = 0;
                ToggleSampleControls(true);
            }
        }

        private void BindNewScatterData()
        {
            if(IsGeneratingData) return;
            
            ToggleSampleControls(false);

            if ((bool)this.UseBruteForce.IsChecked)
            {
                ToggleSampleControls(true);
                this.SeriesLoadingProgressBar.Opacity = 0;
            }

            
            // generate new scatter data
            var amount = (int)DataAmountSlider.Value;
            if (this.SCircularcatterDataRadioButton.IsChecked.HasValue && this.SCircularcatterDataRadioButton.IsChecked.Value)
            {
                this.HighDensityData = new CircleScatterData(amount); 
            }
            else if (this.TrigometricScatterDataRadioButton.IsChecked.HasValue && this.TrigometricScatterDataRadioButton.IsChecked.Value)
            {
                this.HighDensityData = new TrigometricScatterData(amount); 
            }
            else if (this.RandomScatterDataRadioButton.IsChecked.HasValue && this.RandomScatterDataRadioButton.IsChecked.Value)
            {
                var data = new List<ScatterDataPoint>(amount);

                // random scatter data set 
                data.AddRange(new RandomScatterData(amount / 8, new Point(5000, 3000), new Point(3000, 2000)));
                data.AddRange(new RandomScatterData(amount / 8, new Point(6000, 5000), new Point(2000, 2000)));

                data.AddRange(new RandomScatterData(amount / 4, new Point(7000, 7000), new Point(5000, 4000)));

                data.AddRange(new RandomScatterData(amount / 8, new Point(8000, 9000), new Point(2000, 2000)));
                data.AddRange(new RandomScatterData(amount / 8, new Point(9000, 11000), new Point(3000, 2000)));

                this.HighDensityData = data;
            }
            // bind scatter data
            this.DataChart.Series[0].ItemsSource = this.HighDensityData;
            
            
        }
        private void ToggleSampleControls(bool isEnabled)
        {
            this.GenerateScatterDataButton.IsEnabled = isEnabled;
            this.DataAmountSlider.IsEnabled = isEnabled;

            IsGeneratingData = !isEnabled;
        }

        private void MinColorPicker_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            Color MinSelectedColor = (Color)e.NewColor;
            ((HighDensityScatterSeries)this.DataChart.Series[0]).HeatMinimumColor = MinSelectedColor;
        }

        private void MaxColorPicker_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            Color MaxSelectedColor = (Color)e.NewColor;
            ((HighDensityScatterSeries)this.DataChart.Series[0]).HeatMaximumColor = MaxSelectedColor;
        }

        private void PointExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PointExtentSlider == null)
            {
                return;
            }
            PointExtentSlider.Value = Math.Round(PointExtentSlider.Value);
            ((HighDensityScatterSeries)this.DataChart.Series[0]).PointExtent = (int)PointExtentSlider.Value;
        }


        private void UseBruteForce_Click(object sender, RoutedEventArgs e)
        {
            ((HighDensityScatterSeries)this.DataChart.Series[0]).UseBruteForce = (bool)this.UseBruteForce.IsChecked;
            if (!(bool)this.UseBruteForce.IsChecked)
            {
                ToggleSampleControls(false);
                this.SeriesLoadingProgressBar.Opacity = 1;
            }
        }
        
    }
    public class RandomScatterData : List<ScatterDataPoint>
    {
        private Random _rand = new Random();

        public RandomScatterData(int count, Point center, Point range)
            : base(count)
        {
            //center = new Point(0, 0);

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
                    //yVal -= error;
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

            //before = DateTime.Now;
            //this.Sort((item1, item2) => item1.XValue.CompareTo(item2.XValue));
            //after = DateTime.Now;
            //System.Diagnostics.Debug.WriteLine("data sort: " + (after - before).TotalMilliseconds + "ms");
        }
    }
    public class TrigometricScatterData : List<ScatterDataPoint>
    {
        private Random _rand = new Random();

        public TrigometricScatterData(int count)
            : base(count)
        {
            DateTime before = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                var xVal = _rand.NextDouble() * Math.PI * 3.14;
                var yVal = Math.Sin(xVal);
                var error = _rand.NextDouble() * .05;
                if (_rand.NextDouble() > .5)
                {
                    yVal += error;
                }
                else
                {
                    yVal -= error;
                }
                this.Add(new ScatterDataPoint()
                {
                    XValue = xVal,
                    YValue = yVal 
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
   
    public class CircleScatterData : List<ScatterDataPoint>
    {
        private Random _rand = new Random();

        public CircleScatterData(int count)
            : base(count)
        {
            DateTime before = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                var val = _rand.NextDouble() * 10000;
                val = Math.Log(val) / Math.Log(10000);
                var ang = _rand.NextDouble() * 2.0 * Math.PI;
                var x = Math.Cos(ang) * val;
                var y = Math.Sin(ang) * val;

                Add(new ScatterDataPoint() { XValue = x, YValue = y });
            }
            DateTime after = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("data generation: " + (after - before).TotalMilliseconds + "ms");
        }
    }
    public class CircleTestData2 : List<ScatterDataPoint>
    {
        private Random _rand = new Random();

        public CircleTestData2(int count) : base(count)
        {
            DateTime before = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                var val = _rand.NextDouble() * 10000;
                val = Math.Log(val) / Math.Log(10000);
                var ang = _rand.NextDouble() * 2.0 * Math.PI;
                var x = 1- Math.Cos(ang) * -val;
                var y = 1- Math.Sin(ang) * -val;

                Add(new ScatterDataPoint() { XValue = x, YValue = y });
            }
            DateTime after = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("data generation: " + (after - before).TotalMilliseconds + "ms");
        }
    }
}
