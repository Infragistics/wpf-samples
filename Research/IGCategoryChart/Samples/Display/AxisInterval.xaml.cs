using IGCategoryChart.Samples.ViewModels;
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
using System.ComponentModel;
using IGCategoryChart.Samples.Models;

namespace IGCategoryChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for CategoryChart_AxisInterval.xaml
    /// </summary>
    public partial class AxisInterval : Infragistics.Samples.Framework.SampleContainer
    {
        public AxisInterval()
        {
            InitializeComponent();

            Models.WeatherData data = new Models.WeatherData();
          
            chart1.ItemsSource =data;
        }

        private void xMinorIntervalToggle_Click(object sender, RoutedEventArgs e)
        {
            if (xMinorIntervalToggle.IsChecked == true)
            {
                chart1.XAxisMinorStroke = new SolidColorBrush(Colors.Red);
            }
            else if (xMinorIntervalToggle.IsChecked == false)
            {
                chart1.XAxisMinorStroke = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void xMinorIntervalThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            chart1.XAxisMinorStrokeThickness = e.NewValue;
        }

        private void xMinorIntervalValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            chart1.XAxisMinorInterval = e.NewValue;
        }

        private void xMajorIntervalToggle_Click(object sender, RoutedEventArgs e)
        {
            if (xMajorIntervalToggle.IsChecked == true)
            {
                chart1.XAxisMajorStroke = new SolidColorBrush(Colors.Green);
            }
            else if (xMajorIntervalToggle.IsChecked == false)
            {
                chart1.XAxisMajorStroke = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void xMajorIntervalThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            chart1.XAxisMajorStrokeThickness = e.NewValue;
        }

        private void yMinorIntervalToggle_Click(object sender, RoutedEventArgs e)
        {
            if (yMinorIntervalToggle.IsChecked == true)
            {
                chart1.YAxisMinorStroke = new SolidColorBrush(Colors.Red);
            }
            else if (yMinorIntervalToggle.IsChecked == false)
            {
                chart1.YAxisMinorStroke = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void yMinorIntervalThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            chart1.YAxisMinorStrokeThickness = e.NewValue;
        }

        private void yMinorIntervalValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            chart1.YAxisMinorInterval = e.NewValue;
        }

        private void yMajorIntervalToggle_Click(object sender, RoutedEventArgs e)
        {
            if (yMajorIntervalToggle.IsChecked == true)
            {
                chart1.YAxisMajorStroke = new SolidColorBrush(Colors.Green);
            }
            else if (yMajorIntervalToggle.IsChecked == false)
            {
                chart1.YAxisMajorStroke = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void yMajorIntervalThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            chart1.YAxisMajorStrokeThickness = e.NewValue;
        }
    }

    public class ValueLabelConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var intervalState = parameter.ToString() + ": " + value;

            return intervalState;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
