using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Samples.Editing
{
    public partial class ChartScrollIntoView : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartScrollIntoView()
        {
            InitializeComponent();
            this.DataChart.Loaded += OnChartLoaded;
            this.XAxisScrollSlider.ValueChanged += OnXAxisScrollSliderChanged;
            this.YAxisScrollSlider.ValueChanged += OnYAxisScrollSliderChanged;
            this.BubbleDataListBox.SelectionChanged += OnBubbleDataListBoxSelectionChanged;

        }
        private void OnChartLoaded(object sender, RoutedEventArgs e)
        {

            this.DataChart.WindowScaleVertical = 0.4;
            this.DataChart.WindowScaleHorizontal = 0.2;
            this.DataChart.WindowPositionHorizontal = 0;
            this.DataChart.WindowPositionVertical = 0.3;
        }
        private void OnXAxisScrollSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CategoryXAxis xAxis = this.DataChart.Axes.OfType<CategoryXAxis>().First();

            if (xAxis == null) return;

            int index = (int)e.NewValue - 1;
            CategoryDataRandomSample data = xAxis.ItemsSource as CategoryDataRandomSample;
            if (data != null)
                xAxis.ScrollIntoView(data[index]);
        }
        private void OnYAxisScrollSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NumericYAxis yAxis = this.DataChart.Axes.OfType<NumericYAxis>().First();


            bool isInverted = yAxis.IsInverted;
            ScalerParams param = new ScalerParams(new Rect(0, 0, 1, 1), new Rect(0, 0, 1, 1), isInverted);
 
            double y = yAxis.GetScaledValue(e.NewValue, param);
            var window = this.DataChart.WindowRect;

            if (!double.IsNaN(y))
            {
                if (y < window.Top + 0.1 * window.Height)
                {
                    y = y + 0.4 * window.Height;
                    window.Y = y - 0.5 * window.Height;
                }

                if (y > window.Bottom - 0.1 * window.Height)
                {
                    y = y - 0.4 * window.Height;
                    window.Y = y - 0.5 * window.Height;
                }
            }
            this.DataChart.WindowRect = window;
        }
        private void OnBubbleDataListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var series = this.DataChart.Series.FirstOrDefault();
            if (series == null) return;

            if (e.AddedItems.Count > 0)
            {
                CategoryDataPoint selectedDataPoint = e.AddedItems[0] as CategoryDataPoint;

                CategoryDataRandomSample data = this.Resources["CategoryData"] as CategoryDataRandomSample;
                if (data != null)
                {
                    foreach (CategoryDataPoint dataPoint in data)
                    {
                        if (dataPoint == selectedDataPoint)
                        {
                            series.ScrollIntoView(dataPoint);
                            break;
                        }
                    }
                }

            }


        }
    }
}
