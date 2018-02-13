using System;
using System.Windows.Data;

namespace MediaTimeline.Converters
{
	public class StringToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string sortBy = (string)value;
			if (sortBy == "relevance")
				return "Relevance";
			else if (sortBy == "date")
				return "Published";
			else
				return "View Count";
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string sortBy = (string)value;
			if (sortBy == "Relevance")
				return "relevance";
			else if (sortBy == "Published")
				return "date";
			else
				return "viewCount";
		}
	}
}