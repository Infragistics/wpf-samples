using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MediaTimeline.Converters
{
	public sealed class ByteToBitmapConverter : IValueConverter
	{
		#region IValueConverter Members

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
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			byte[] arr = (byte[])value;
			if (arr != null)
			{
				using (MemoryStream s = new MemoryStream(arr))
				{
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = s;
                    image.EndInit();
                    image.Freeze();
                    return image;
				}
			}
			return null;
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
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			//NOTE: The back conversion is not used.
			throw new NotImplementedException();
		}

		#endregion
	}
}