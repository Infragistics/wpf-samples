using System.Windows.Data;

namespace IGExtensions.Common.Converters
{
	public class EnumToBoolConverter : IValueConverter
	{
		#region IValueConverter Members
		public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool result = value != null && value.Equals(parameter);
			return result;
		}

		public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return parameter;
		}
		#endregion
	}
}