using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using IGExtensions.Common.Models;

namespace IGExtensions.Common.Converters
{
    /// <summary>
    /// Converts a Boolean value to <see cref="Visibility"/> enumerable values based on properties
    /// </summary>
    public class VisibilityConverter : IValueConverter
    {
        public VisibilityConverter()
        {
            this.TrueValue = Visibility.Visible;
            this.FalseValue = Visibility.Collapsed;
        }
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return this.FalseValue;
            if (!(value is bool)) return this.FalseValue; 
            return (bool)value ? this.TrueValue : this.FalseValue; 
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            return ((Visibility)value) == this.TrueValue ? true : false;
        }
    }
    public class VisibilityToBoolConverter : IValueConverter
    {
        public VisibilityToBoolConverter()
        {
            this.VisibleValue = true;
            this.CollapsedValue = false;
        }
        public bool VisibleValue { get; set; }
        public bool CollapsedValue { get; set; }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (Visibility)value == Visibility.Visible ? this.VisibleValue : this.CollapsedValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Visible;
            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    public class VisibilityThresholdConverter : IValueConverter
    {
        public VisibilityThresholdConverter()
        {
            Threshold = 1.0;
        }
        // Threshold
        public double Threshold { get; set; }
      
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is double)) return Visibility.Collapsed;
            var current = (double)value;

            if (current >= this.Threshold) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class VisibilityFromBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is bool)) return Visibility.Visible;
            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //public class VisibilityFromSelectableElementConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var element = value as ISelectable;
    //        if (element == null) return Visibility.Collapsed;
    //        return element.IsSelected ? Visibility.Visible : Visibility.Collapsed;
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}