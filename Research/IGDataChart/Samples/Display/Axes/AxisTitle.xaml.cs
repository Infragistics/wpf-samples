using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Display.Axes
{
    public partial class AxisTitle : Infragistics.Samples.Framework.SampleContainer
    {
        public AxisTitle()
        {
            InitializeComponent(); 
        }

        private void AxisRotationSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (AxisRotationSlider != null && this.DataChart.Axes.Count == 2)
            {
                var value = (int)e.NewValue;
                var axis = this.DataChart.Axes.OfType<NumericYAxis>().First();
                axis.TitleSettings.Angle = value;
            }
        }
        private void AxisMarginSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (AxisRotationSlider != null && this.DataChart.Axes.Count == 2)
            {
                var value = (int)e.NewValue;
                var axis = this.DataChart.Axes.OfType<NumericYAxis>().First();
                axis.TitleSettings.Margin = new Thickness(value);
            }
        }
  
       

    }
}
