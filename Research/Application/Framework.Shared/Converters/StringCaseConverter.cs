using System;
using System.Globalization;

#if Xamarin
using Xamarin.Forms; 
#else
using System.Windows;
#endif

namespace Infragistics.Framework
{
    public enum StringCase
    {
        Original,
        Upper,
        Lower,
    }
    public class StringCaseConverter : IValueConverter
    {
        public StringCaseConverter()
        {
            Case = StringCase.Upper;
        }
        public StringCaseConverter(bool isUpper)
        {
            Case = isUpper ? StringCase.Upper : StringCase.Lower;
        }
        /// <summary> Gets or sets PropertyName </summary>
        public StringCase Case { get; set; }

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
            if (value == null) return "NUL";

            var str = value.ToString();

            if (Case == StringCase.Lower) return str.ToLower();
            if (Case == StringCase.Upper) return str.ToUpper();

            return str;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}