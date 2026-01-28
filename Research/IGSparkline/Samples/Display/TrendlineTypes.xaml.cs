using System.Windows.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;

namespace IGSparkline.Samples.Display
{
    public partial class TrendlineTypes : SampleContainer
    {
        public TrendlineTypes()
        {
            InitializeComponent();
        }
        // Chart type selection
        private void ComboBoxChartType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = this.ComboBoxChartType as ComboBox;
            if (cb == null) return;

            switch (cb.SelectedIndex)
            {
                case 0:
                    this.xamSparkline1.DisplayType = SparklineDisplayType.Line;
                    break;
                case 1:
                    this.xamSparkline1.DisplayType = SparklineDisplayType.Area;
                    break;
                case 2:
                    this.xamSparkline1.DisplayType = SparklineDisplayType.Column;
                    break;
                case 3:
                    this.xamSparkline1.DisplayType = SparklineDisplayType.WinLoss;
                    break;
            }
        }
        // Select a Trend line type
        private void ComboBoxTrendlines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = this.ComboBoxTrendlines as ComboBox;
            if (cb == null) return;

            switch (cb.SelectedIndex)
            {
                case 0:
                    this.xamSparkline1.TrendLineType = TrendLineType.CubicFit;
                    break;
                case 1:
                    this.xamSparkline1.TrendLineType = TrendLineType.CumulativeAverage;
                    break;
                case 2:
                    this.xamSparkline1.TrendLineType = TrendLineType.ExponentialAverage;
                    break;
                case 3:
                    this.xamSparkline1.TrendLineType = TrendLineType.ExponentialFit;
                    break;
                case 4:
                    this.xamSparkline1.TrendLineType = TrendLineType.LinearFit;
                    break;
                case 5:
                    this.xamSparkline1.TrendLineType = TrendLineType.LogarithmicFit;
                    break;
                case 6:
                    this.xamSparkline1.TrendLineType = TrendLineType.ModifiedAverage;
                    break;
                case 7:
                    this.xamSparkline1.TrendLineType = TrendLineType.None;
                    break;
                case 8:
                    this.xamSparkline1.TrendLineType = TrendLineType.PowerLawFit;
                    break;
                case 9:
                    this.xamSparkline1.TrendLineType = TrendLineType.QuadraticFit;
                    break;
                case 10:
                    this.xamSparkline1.TrendLineType = TrendLineType.QuarticFit;
                    break;
                case 11:
                    this.xamSparkline1.TrendLineType = TrendLineType.QuinticFit;
                    break;
                case 12:
                    this.xamSparkline1.TrendLineType = TrendLineType.SimpleAverage;
                    break;
                case 13:
                    this.xamSparkline1.TrendLineType = TrendLineType.WeightedAverage;
                    break;
            }
        }
    }
}
