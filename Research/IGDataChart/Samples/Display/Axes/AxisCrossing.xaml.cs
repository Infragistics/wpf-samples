using System.Linq;
using System.Windows;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Display.Axes
{
    public partial class AxisCrossing : Infragistics.Samples.Framework.SampleContainer
    {
        public AxisCrossing()
        {
            InitializeComponent();
        }

        private void XAxisCrossingValueSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataChart == null || this.DataChart.Axes.Count < 2) return;
              
            var axis = DataChart.Axes.OfType<CategoryXAxis>().First();
            axis.CrossingValue = e.NewValue;
        }
        private void YAxisCrossingValueSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataChart == null || this.DataChart.Axes.Count < 2) return;
            
            var axis = DataChart.Axes.OfType<NumericYAxis>().First();
            axis.CrossingValue = e.NewValue;
        }
    }
}
