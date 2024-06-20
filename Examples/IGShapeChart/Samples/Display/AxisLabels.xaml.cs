using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGShapeChart.Samples.Display
{ 
    public partial class AxisLabels : SampleContainer
    {
        public AxisLabels()
        {
            InitializeComponent();
        }

        private void OnXAxisMarginSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Chart.XAxisLabelMargin = new Thickness(0, e.NewValue, 0, e.NewValue);
        }

        private void OnYAxisMarginSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Chart.YAxisLabelMargin = new Thickness(e.NewValue, 0, e.NewValue, 0);
        }
 
    }

     
}
