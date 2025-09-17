using System;
using System.Windows;
using System.Windows.Data;
using IGGeographicMap.Models;

namespace IGGeographicMap.Converters
{
    public class AirplaneVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var plane = value as Airplane;
            if (plane == null || !plane.Flight.IsInProgress) 
                return Visibility.Collapsed;

            return Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AirplaneOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var plane = value as Airplane;
            if (plane == null || plane.Flight.IsStarted || plane.Flight.IsCompleted) return 0;

            var opacity = Math.Pow((1 - Math.Abs(plane.Flight.Progress - .5)), 2);
            return opacity;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AirplaneProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is double)) return 0;
            var progress = (double)value;
            if (progress >= 1) return 0;
            if (progress <= 0) return 0;

            var opacity = Math.Pow((1 - Math.Abs(progress - .5)), 2);
            return opacity;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AirplaneProgressVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is double)) return Visibility.Collapsed;
            var progress = (double)value;
            if (progress >= 1) return Visibility.Collapsed;
            if (progress <= 0) return Visibility.Collapsed;

            return Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AirportRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 7 + Math.Sqrt(double.Parse(value.ToString())) * 5;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AirportRadiusVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is double)) return Visibility.Collapsed;
            var flights = (double)value;

            if (flights > 0) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}