using System;
using System.Globalization;
using System.Windows.Data;

namespace IGTagCloud.Converters
{
    /// <summary>
    /// This convertor to perform common operations on a string, such as ToUpper. Pass the String object's methodName as the parameter.
    /// </summary>
    /// <see cref="String"/>
    public class MaxLengthStringConverter : IValueConverter
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

            if (parameter == null)
            {
                return value;
            }

            string returnString = value.ToString();
            if (returnString.Length > Int32.Parse(parameter.ToString()))
            {
                return returnString.Substring(0, Int32.Parse(parameter.ToString()));
            }
            else
            {
                return returnString;
            }
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