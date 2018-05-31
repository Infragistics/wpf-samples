using System;
using System.Windows.Data;

namespace MediaTimeline.Converters
{
	public class BoolToOrderByConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            if (value.ToString() == parameter.ToString())
                return true;
            else
                return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return parameter;
		}

		#endregion
	}
}
