using System.Windows;
using Infragistics.Samples.Framework;

namespace IGSparkline.Samples.Display
{
    public partial class SparklineMarkers : SampleContainer
    {
        public SparklineMarkers()
        {
            InitializeComponent();
        }
        // High Markers
        private void CheckBox1_Click(object sender, RoutedEventArgs e)
        {
            if (cbx1.IsChecked == true)
            {
                xamSparkline1.HighMarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline2.HighMarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline3.HighMarkerVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                xamSparkline1.HighMarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline2.HighMarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline3.HighMarkerVisibility = System.Windows.Visibility.Collapsed;
            }
            xamSparkline1.UpdateLayout();
        }

        // Low Markers
        private void CheckBox2_Click(object sender, RoutedEventArgs e)
        {
            if (cbx2.IsChecked == true)
            {
                xamSparkline1.LowMarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline2.LowMarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline3.LowMarkerVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                xamSparkline1.LowMarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline2.LowMarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline3.LowMarkerVisibility = System.Windows.Visibility.Collapsed;
            }
            xamSparkline1.UpdateLayout();
        }
        // First Markers
        private void CheckBox3_Click(object sender, RoutedEventArgs e)
        {
            if (cbx3.IsChecked == true)
            {
                xamSparkline1.FirstMarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline2.FirstMarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline3.FirstMarkerVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                xamSparkline1.FirstMarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline2.FirstMarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline3.FirstMarkerVisibility = System.Windows.Visibility.Collapsed;
            }
            xamSparkline1.UpdateLayout();
        }
        // Last Markers
        private void CheckBox4_Click(object sender, RoutedEventArgs e)
        {
            if (cbx4.IsChecked == true)
            {
                xamSparkline1.LastMarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline2.LastMarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline3.LastMarkerVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                xamSparkline1.LastMarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline2.LastMarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline3.LastMarkerVisibility = System.Windows.Visibility.Collapsed;
            }
            xamSparkline1.UpdateLayout();
        }
        // Negative Markers
        private void CheckBox5_Click(object sender, RoutedEventArgs e)
        {
            if (cbx5.IsChecked == true)
            {
                xamSparkline1.NegativeMarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline2.NegativeMarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline3.NegativeMarkerVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                xamSparkline1.NegativeMarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline2.NegativeMarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline3.NegativeMarkerVisibility = System.Windows.Visibility.Collapsed;
            }
        }
        // All Markers
        private void CheckBox6_Click(object sender, RoutedEventArgs e)
        {
            if (cbx6.IsChecked == true)
            {
                xamSparkline1.MarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline2.MarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline3.MarkerVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                xamSparkline1.MarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline2.MarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline3.MarkerVisibility = System.Windows.Visibility.Collapsed;
            }
            xamSparkline1.UpdateLayout();
        }

    }
}
