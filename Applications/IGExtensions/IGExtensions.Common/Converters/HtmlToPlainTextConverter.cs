using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace IGExtensions.Common.Converters
{
	/// <summary>
	/// This convertor to perform common operations on a string, such as ToUpper. Pass the String object's methodName as the parameter.
	/// </summary>
	/// <see cref="String"/>
	public class HtmlToPlainTextConverter : IValueConverter
	{
		/// <summary>
		/// Modifies the source data before passing it to the target for display in the UI.
		/// </summary>
		/// <param name="value">The source data being passed to the target.</param>
		/// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
		/// <param name="parameter">An optional parameter to be used in the converter logic.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>
		/// The value to be passed to the target dependency property.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return string.Empty;
			}

			string html = value.ToString();
			// remove scripts and the between
			html = Regex.Replace(html, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

			//remove style and the between
			html = Regex.Replace(html, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

			// Remove Comments
			html = Regex.Replace(html, "<!--.*?-->", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

			//remove html
			html = Regex.Replace(html, @"<[^>]*>", "", RegexOptions.IgnoreCase);

			//convert special characters (&nbsp 
			html = html.Replace("&nbsp;", " ");
			html = html.Replace("&rsquo;", "'");
			html = html.Replace("&lsquo;", "'");
			html = html.Replace("&#8217;", "'");
			html = html.Replace("&#8220;", "\"");
			html = html.Replace("&#8221;", "\"");

			if (html.Length > 300)
			{
				html = html.Substring(0, 300) + "...";
			}

			return html.Trim();

		}

		/// <summary>
		/// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
		/// </summary>
		/// <param name="value">The target data being passed to the source.</param>
		/// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
		/// <param name="parameter">An optional parameter to be used in the converter logic.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>
		/// The value to be passed to the source object.
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
