using System;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGSparkline.Samples.Styling
{
    public partial class SparklineStyling : SampleContainer
    {
        public SparklineStyling()
        {
            InitializeComponent();
        }

        // Start Markers animation
        private void OnClickToggleMarkers(object sender, RoutedEventArgs e)
        {
            if (buttonStart.IsChecked == true)
            {
                xamSparkline1.MarkerVisibility = Visibility.Visible;
                xamSparkline2.MarkerVisibility = Visibility.Visible;
                xamSparkline3.MarkerVisibility = Visibility.Visible;
                this.MyStoryboard.Begin();
            }
            else
            {
                this.MyStoryboard.Stop();
                xamSparkline1.MarkerVisibility = Visibility.Collapsed;
                xamSparkline2.MarkerVisibility = Visibility.Collapsed;
                xamSparkline3.MarkerVisibility = Visibility.Collapsed;
            }
        }

        // Display Customized Normal Range
        private void checkBoxNormalRange_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkBoxNormalRange.IsChecked == true)
            {
                xamSparkline1.NormalRangeVisibility = Visibility.Visible;
                xamSparkline2.NormalRangeVisibility = Visibility.Visible;
                xamSparkline3.NormalRangeVisibility = Visibility.Visible;
                xamSparkline4.NormalRangeVisibility = Visibility.Visible;
            }
            else
            {
                xamSparkline1.NormalRangeVisibility = Visibility.Collapsed;
                xamSparkline2.NormalRangeVisibility = Visibility.Collapsed;
                xamSparkline3.NormalRangeVisibility = Visibility.Collapsed;
                xamSparkline4.NormalRangeVisibility = Visibility.Collapsed;
            }
        }

    }
}
