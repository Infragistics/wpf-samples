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
    /// <summary>
    /// Interaction logic for ShapeChartLegend.xaml
    /// </summary>
    public partial class AxisTitle : SampleContainer
    {
        public AxisTitle()
        {
            InitializeComponent();
        }
        private void xAxisMarginSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Chart.XAxisTitleMargin = new Thickness(e.NewValue);
        }
        private void yAxisMarginSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Chart.YAxisTitleMargin = new Thickness(e.NewValue);
        }
    }
}
