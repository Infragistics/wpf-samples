using System;
using System.Collections.ObjectModel;
using System.Linq;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Framework;

namespace IGSlider.Samples.Style
{
    public partial class TickmarkCustomization : Infragistics.Samples.Framework.SampleContainer
    {
        private static Random random = new Random();
        private Collection<NumericDataPoint> masterDataPoints;

        public TickmarkCustomization()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
  
            
        }

        void OnSampleLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.GenerateData();
            this.UpdateData(50);
            this.dataSlider.ThumbValueChanged += XamNumericRangeSlider_ThumbValueChanged;
        }

        private void XamNumericRangeSlider_ThumbValueChanged(object sender, ThumbValueChangedEventArgs<double> e)
        {
            this.UpdateData(e.NewValue);
        }

        private void UpdateData(double value)
        {
            Collection<NumericDataPoint> dataPoints = new Collection<NumericDataPoint>(
                this.masterDataPoints.Take<NumericDataPoint>(Convert.ToInt16(value + 1)).ToList<NumericDataPoint>());

            this.dataChart.Series[0].ItemsSource = dataPoints;
        }

        private void GenerateData()
        {
            masterDataPoints = new Collection<NumericDataPoint>();
            int numberOfDataPoints = Convert.ToInt32(this.dataSlider.MaxValue) + 1;

            for (int i = 0; i <= numberOfDataPoints; i++)
            {
                masterDataPoints.Add(new NumericDataPoint
                {
                    Label = "Test " + i,
                    X = random.Next(100, 1000),
                    Y = random.Next(1000, 10000),
                    Index = random.Next(10, 100),
                });
            }
        }
    }
}
