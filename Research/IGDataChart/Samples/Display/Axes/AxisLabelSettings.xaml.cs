using System;
using System.Windows;

namespace IGDataChart.Samples.Display.Axes
{
    public partial class AxisLabelSettings : Infragistics.Samples.Framework.SampleContainer
    {
        public AxisLabelSettings()
        {
            InitializeComponent(); 
        }

        private void OnXAxisMarginSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataChart == null) return;
            this.DataChart.Axes["XAxis"].LabelSettings.Margin = new Thickness(0, e.NewValue, 0, e.NewValue);
        }
        private void OnYAxisMarginSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataChart == null) return;
            this.DataChart.Axes["YAxis"].LabelSettings.Margin = new Thickness(e.NewValue, 0, e.NewValue,0);
        }
    }
}
