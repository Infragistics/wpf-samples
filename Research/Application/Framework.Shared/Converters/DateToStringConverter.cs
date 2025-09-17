using System;
using System.Globalization;
using System.Reflection;

#if Xamarin
using Xamarin.Forms;
#else
using System.Windows;
#endif

namespace Infragistics.Framework
{
    public class DateToStringConverter : IValueConverter
    {
        public DateToStringConverter()
        {
            DateFormat = "MM dd yyyy";
        }
        public string DateFormat { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = DateTime.MaxValue;
            if (value is DateTime)
            {
                date = (DateTime)value;
            }
            else
            {
                return "No Date";
            }
            var format = DateFormat; 
            if (parameter != null)
            {
                format = parameter.ToString();
            }

            return date.ToString(format);
        }
        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            return new NotImplementedException();
        }
    }
}