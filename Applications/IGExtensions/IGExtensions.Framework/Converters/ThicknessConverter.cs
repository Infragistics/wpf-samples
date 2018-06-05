using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IGExtensions.Framework.Converters
{
    /// <summary>
    /// Represents value contverter for converting <see cref="Thickness"/> to a double
    /// </summary>
    public class ThicknessConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is Thickness)) return 2.0;

            var thickness = (Thickness)value;

            if (parameter is string && !string.IsNullOrEmpty((string)parameter))
            {
                var direction = (string) parameter;
                if (direction.ToLower() == "top")
                {
                    return thickness.Top;
                }
                if (direction.ToLower() == "bottom")
                {
                    return thickness.Bottom;
                }
                if (direction.ToLower() == "left")
                {
                    return thickness.Left;
                }
                if (direction.ToLower() == "right")
                {
                    return thickness.Right;
                }
            }

            return thickness.Top + thickness.Bottom + thickness.Left + thickness.Right;
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
}