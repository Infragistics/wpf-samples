using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IGExtensions.Common.Converters
{
    /// <summary>
    /// Represents a value converter for converting <see cref="bool"/> value to <see cref="Visibility"/> enum.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter 
    {
        public BoolToVisibilityConverter()
        {
            this.TrueValue = Visibility.Visible;
            this.FalseValue = Visibility.Collapsed;
        }
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }
      
        /// <summary>
        /// 
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (bool)value ?
                this.TrueValue : this.FalseValue; 
        }

        /// <summary>
        /// 
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            return ((Visibility)value) == this.TrueValue ? true : false;
            
            //var visibility = (Visibility)value;
            //return (visibility == Visibility.Visible);
        }
    }
    
}