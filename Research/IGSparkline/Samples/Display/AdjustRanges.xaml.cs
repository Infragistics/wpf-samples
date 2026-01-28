using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;

namespace IGSparkline.Samples.Display
{
    public partial class AdjustRanges : SampleContainer
    {
        public AdjustRanges()
        {
            InitializeComponent();
        }
        // Select a Chart type
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
        // Show Normal Ranges
        private void CheckBoxRange_Click(object sender, RoutedEventArgs e)
        {
            this.xamSparkline1.NormalRangeVisibility = 
                CheckBoxRange.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
    }
}
