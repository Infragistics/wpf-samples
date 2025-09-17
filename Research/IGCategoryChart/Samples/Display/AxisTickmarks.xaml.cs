using IGCategoryChart.Samples.Models;
using IGCategoryChart.Samples.ViewModels;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
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

namespace IGCategoryChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for AxisTickmarks.xaml
    /// </summary>
    public partial class AxisTickmarks : Infragistics.Samples.Framework.SampleContainer
    {
        public AxisTickmarks()
        {
            InitializeComponent();
            chart1.ItemsSource = new RevenueByDate();
        }
    }

    public class BrushToColorConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var color = new SolidColorBrush();
            color.Color = (Color)value;
            return color;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
