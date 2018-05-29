using System;
using System.Globalization;
using System.Windows.Data;

namespace IGExtensions.Common.Converters
{
    /// <summary>
    /// 
    /// </summary>
	public sealed class StringFormatConverter : IValueConverter
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
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return string.Empty;
			if (parameter is string && !string.IsNullOrEmpty((string) parameter))
			{
				return string.Format(string.Format("{{0:{0}}}", parameter), value);
			}
			return value.ToString();
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
			//NOTE: The back conversion is not used.
			throw new System.NotImplementedException();
		}

		#endregion
	}
    public sealed class ValueFormatConverter : IValueConverter
    {
        #region IValueConverter Members

        //private string _ConvertParameter;
        ///// <summary>
        ///// Gets or sets ConvertParameter property 
        ///// </summary>
        //public string ConvertParameter
        //{
        //    get {  return _ConvertParameter; }
        //    set { if (_ConvertParameter == value) return; _ConvertParameter = value; OnPropertyChanged("ConvertParameter"); }
        //}

        public string FormatParameter { get; set; }

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
            if (value == null) return string.Empty;
            if (!string.IsNullOrEmpty(this.FormatParameter))
            {
                return string.Format(string.Format("{{0:{0}}}", this.FormatParameter), value);
            }
            return value.ToString();
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
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }

        #endregion
    }
    //public sealed class StringMultiFormatConverter : IMultiValueConverter
    //{
    //    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return string.Format((string)values[1], values[0]);
    //    }
    //}
}
