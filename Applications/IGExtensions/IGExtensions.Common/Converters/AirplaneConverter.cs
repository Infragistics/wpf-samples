using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using IGExtensions.Common.Models;
 
namespace IGExtensions.Common.Converters
{
    public class AirplaneVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var airplane = value as AirplanePosition;
           //   if (plane == null || !plane.Flight.IsInProgress) 
            if (airplane == null || airplane.Flight.IsCompleted || airplane.Flight.IsPending) 
                return Visibility.Collapsed;

            return Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AirplaneBearingConverter : IValueConverter
    {
        public double BearingOffset { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bearing = BearingOffset;
            var plane = value as Airplane;
            if (plane == null) return 0;
            bearing += plane.Flight.FlightBearing;
            return bearing;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AirplaneOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var airplane = value as AirplanePosition;
            var airplane = value as AirlineFlight;// Airplane;
            //if (plane == null || plane.Flight.IsStarted || plane.Flight.IsCompleted) return 0;
            //if (plane == null) return 0;
            if (airplane == null || airplane.Flight.IsCompleted) return 0;
            if (airplane.Flight.IsPending) return 0;

            var opacity = Math.Pow((1 - Math.Abs(airplane.Flight.Progress - .5)), 2);
            return opacity;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AirplaneProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return 0.8;

            if (value == null || !(value is double)) return 0;
            var progress = (double)value;
            if (progress >= 1) return 0;
            if (progress <= 0) return 0;

            var opacity = Math.Pow((1 - Math.Abs(progress - .5)), 2);
            return opacity;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AirplaneProgressVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           // return Visibility.Visible;
            
            if (value == null || !(value is double)) return Visibility.Collapsed;
            var progress = (double)value;
            if (progress >= 1) return Visibility.Collapsed;
            if (progress <= 0) return Visibility.Collapsed;

            return Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AirportRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 7 + Math.Sqrt(double.Parse(value.ToString())) * 10;
            //return 7 + Math.Sqrt(double.Parse(value.ToString())) * 5;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
   
    public class AirportVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is double)) return Visibility.Collapsed;
            var flights = (double)value;

            if (flights > 0) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}