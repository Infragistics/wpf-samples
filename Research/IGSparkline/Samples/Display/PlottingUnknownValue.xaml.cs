using System.Windows;
using System.Windows.Controls;
using IGSparkline.Model;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System.Windows.Media;

namespace IGSparkline.Samples.Display
{
    public partial class PlottingUnknownValue : SampleContainer
    {
        public PlottingUnknownValue()
        {
            InitializeComponent();
            this.SampleLoaded += new System.EventHandler(PlottingUnknownValue_SampleLoaded);
        }
        void PlottingUnknownValue_SampleLoaded(object sender, System.EventArgs e)
        {
            // Populate items to hold the data points and display underneath the chart
            foreach (TestDataItem item in this.xamSparkline1.ItemsSource)
            {
                var b = new Border();
                b.Style = this.Resources["itemsBorder"] as Style;
                var t = new TextBlock();
                t.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                t.Foreground = new SolidColorBrush(Colors.Gray);
                t.Text = item.Value.ToString();
                b.Child = t;
                sp2.Children.Add(b);
            }
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
            }
        }
        // Enable Markers
        private void checkBoxMarkers_Click(object sender, RoutedEventArgs e)
        {
            this.xamSparkline1.MarkerVisibility =
                (checkBoxMarkers.IsChecked == true) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
        // Unknown Value: Dont Plot
        private void OnRadioButtonDontPlot_click(object sender, RoutedEventArgs e)
        {
            this.xamSparkline1.UnknownValuePlotting = UnknownValuePlotting.DontPlot;
        }
        // Unknown Value: Linear Interpolate
        private void OnRadioButtonInterpolate_click(object sender, RoutedEventArgs e)
        {
            this.xamSparkline1.UnknownValuePlotting = UnknownValuePlotting.LinearInterpolate;
        }
        // Select a data point to make null (unknown value) and then use the RadioButton options to either interpolate or don't plot
        private void ComboBoxNullValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = this.ComboBoxNullValue as ComboBox;
            if ((cb == null) || (cb.SelectedItem == null))
                return;

            // Index variable for items, holding the data points located underneath the chart
            int nullIndex = 0;

            var nullItem = ((TestDataItem)cb.SelectedItem).Label;

            var testData = new TestData();
            foreach (TestDataItem item in testData)
            {
                // Reset the opacity of the items located underneath the chart
                sp2.Children[nullIndex].Opacity = 1;

                if (item.Label == nullItem)
                {
                    item.Value = null;

                    // Set opacity for null items located underneath the chart
                    sp2.Children[nullIndex].Opacity = 0.25;
                }
                nullIndex++;
            }
            this.xamSparkline1.ItemsSource = testData;
        }
        // Refresh the Spark line data
        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            this.xamSparkline1.ItemsSource = new TestData();
            this.ComboBoxNullValue.ClearValue(ComboBox.SelectedItemProperty);

            // Clear and RePopulate items to hold the data points and display underneath the chart
            sp2.Children.Clear();
            foreach (TestDataItem item in this.xamSparkline1.ItemsSource)
            {
                var b = new Border();
                b.Style = this.Resources["itemsBorder"] as Style;
                var t = new TextBlock();
                t.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                t.Foreground = new SolidColorBrush(Colors.Black);
                t.Text = item.Value.ToString();
                b.Child = t;
                sp2.Children.Add(b);
            }
        }
    }
}
