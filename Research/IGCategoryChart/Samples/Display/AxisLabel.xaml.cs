using IGCategoryChart.Samples.Models;
using IGCategoryChart.Samples.ViewModels;
using Infragistics.Samples.Framework;
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
using System.Globalization;

namespace IGCategoryChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for CategoryChart_AxisLabel.xaml
    /// </summary>
    public partial class AxisLabel : SampleContainer
    {
        public AxisLabel()
        {
            InitializeComponent();

            chart1.ItemsSource = new WeatherData();
        }
    }

    public class MarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)parameter == "X")
            {
                Thickness verticalMargin = new Thickness();
                verticalMargin.Top = (double)value;
                verticalMargin.Bottom = (double)value;

                return verticalMargin;
            }
            if ((string)parameter == "Y")
            {
                Thickness horizontalMargin = new Thickness();
                horizontalMargin.Left = (double)value;
                horizontalMargin.Right = (double)value;

                horizontalMargin.Top = 0;
                horizontalMargin.Bottom = 0;

                return horizontalMargin;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Visibility.Visible;

            }
            else if (!(bool)value)
            {
                return Visibility.Collapsed;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
