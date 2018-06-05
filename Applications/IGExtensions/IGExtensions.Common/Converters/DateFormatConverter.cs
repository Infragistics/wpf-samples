using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IGExtensions.Common.Converters
{
    public class DateFormatConverter : DependencyObject, IValueConverter
	{
        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(DateFormatConverter), new PropertyMetadata(null));

		#region IValueConverter Members
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
            if (value == null || (this.Format == null && parameter == null))
                return value;

			string result = value.ToString();
            string format = (parameter == null) ? this.Format : parameter.ToString();

			if (format == "DATE" && value is DateTime)
			{
				result = ((DateTime)value).ToShortDateString();
			}
			else if (!string.IsNullOrEmpty(format))
			{
				result = string.Format(format, value);
			}
			return result;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
		#endregion
	}
}