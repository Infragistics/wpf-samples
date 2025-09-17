using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using System.Windows.Media;

namespace IGSparkline.Samples.Styling
{
    public partial class SparklineStyling : SampleContainer
    {
        public SparklineStyling()
        {
            InitializeComponent();
        }

        // Start Markers animation
        private void OnLoadImage(object sender, RoutedEventArgs e)
        {
            if (buttonStart.IsChecked == true)
            {
                xamSparkline1.MarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline2.MarkerVisibility = System.Windows.Visibility.Visible;
                xamSparkline3.MarkerVisibility = System.Windows.Visibility.Visible;
                ClickState.Storyboard.Begin();
            }
            else
            {
                ClickState.Storyboard.Stop();
                xamSparkline1.MarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline2.MarkerVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline3.MarkerVisibility = System.Windows.Visibility.Collapsed;
            }
        }

        // Display Customized Normal Range
        private void checkBoxNormalRange_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkBoxNormalRange.IsChecked == true)
            {
                xamSparkline1.NormalRangeVisibility = System.Windows.Visibility.Visible;
                xamSparkline2.NormalRangeVisibility = System.Windows.Visibility.Visible;
                xamSparkline3.NormalRangeVisibility = System.Windows.Visibility.Visible;
                xamSparkline4.NormalRangeVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                xamSparkline1.NormalRangeVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline2.NormalRangeVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline3.NormalRangeVisibility = System.Windows.Visibility.Collapsed;
                xamSparkline4.NormalRangeVisibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
