using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Editing
{
    public partial class ChartCrosshairCoordinates : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartCrosshairCoordinates()
        {
            InitializeComponent();
        }

        private void OnCategoryChartMouseMove(object sender, MouseEventArgs e)
        {
            var series = this.CategoryChart.Series.FirstOrDefault();
            if (series == null) return;

            var position = e.GetPosition(series);

            // calculate crosshair coordinates on CategoryDateTimeXAxis 
            if (((XamDataChart)series.SeriesViewer).Axes.OfType<CategoryDateTimeXAxis>().Any())
            {
                var xAxis = ((XamDataChart)series.SeriesViewer).Axes.OfType<CategoryDateTimeXAxis>().First();
                var yAxis = ((XamDataChart)series.SeriesViewer).Axes.OfType<NumericYAxis>().First();

                var viewport = new Rect(0, 0, xAxis.ActualWidth, yAxis.ActualHeight);
                var window = series.SeriesViewer.WindowRect;

                bool isInverted = xAxis.IsInverted;
                ScalerParams param = new ScalerParams(window, viewport, isInverted);
                var unscaledX = xAxis.GetUnscaledValue(position.X, param);

                isInverted = yAxis.IsInverted;
                param = new ScalerParams(window, viewport, isInverted);
                var unscaledY = yAxis.GetUnscaledValue(position.Y, param);

                DateTime xDate = new DateTime((long)unscaledX);

                this.CategoryXCoordinateTextBlock.Text = String.Format("{0:T}", xDate);
                this.CategoryYCoordinateTextBlock.Text = String.Format("{0:0.00}", unscaledY);
            }
        }
        private void OnScatterChartMouseMove(object sender, MouseEventArgs e)
        {
            var series = this.ScatterChart.Series.FirstOrDefault();
            if (series == null) return;

            var position = e.GetPosition(series);

            // calculate crosshair coordinates on NumericXAxis 
            if (((XamDataChart)series.SeriesViewer).Axes.OfType<NumericXAxis>().Any())
            {
                var xAxis = ((XamDataChart)series.SeriesViewer).Axes.OfType<NumericXAxis>().First();
                var yAxis = ((XamDataChart)series.SeriesViewer).Axes.OfType<NumericYAxis>().First();

                var viewport = new Rect(0, 0, xAxis.ActualWidth, yAxis.ActualHeight);
                var window = series.SeriesViewer.WindowRect;

                bool isInverted = xAxis.IsInverted;
                ScalerParams param = new ScalerParams(window, viewport, isInverted);
                var unscaledX = xAxis.GetUnscaledValue(position.X, param);

                isInverted = yAxis.IsInverted;
                param = new ScalerParams(window, viewport, isInverted);
                var unscaledY = yAxis.GetUnscaledValue(position.Y, param);

                this.ScatterXCoordinateTextBlock.Text = String.Format("{0:0.00}", unscaledX);
                this.ScatterYCoordinateTextBlock.Text = String.Format("{0:0.00}", unscaledY);
            }
        }
    }
    public class DataCollection : ObservableCollection<DataPoint>
    {
        private static readonly Random DataGenerator = new Random();

        public DataCollection()
        {
            this.Generate();
        }

        private void Generate()
        {
            this.Clear();
            double curr = 100;
            DateTime date = new DateTime(2010, 1, 1, 12, 00, 00);

            for (int i = 0; i < 4000; i++)
            {
                if (DataGenerator.NextDouble() > .5)
                {
                    curr += DataGenerator.NextDouble();
                }
                else
                {
                    curr -= DataGenerator.NextDouble();
                }
                this.Add(
                    new DataPoint
                    {
                        Date = date,
                        Index = i,
                        Label = i.ToString(),
                        Value = curr
                    });
                date = date.AddSeconds(1);
            }

        }
    }
    public class DataPoint
    {
        public string Label { get; set; }
        public double Value { get; set; }
        public int Index { get; set; }
        public DateTime Date { get; set; }
    }
}
