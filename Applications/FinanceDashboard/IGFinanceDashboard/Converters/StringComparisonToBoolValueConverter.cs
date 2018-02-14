using System;
using System.Windows.Data;
using IGShowcase.FinanceDashboard.ViewModels;

namespace IGShowcase.FinanceDashboard.Converters
{
    public class StringComparisonToBoolValueConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null) return true;

            //var left = double.Parse(value.ToString());
            //var right = double.Parse(parameter.ToString());
            //return (left == right);
            var left = value.ToString();
            var right = parameter.ToString();
            
            return string.Compare(left, right, culture, System.Globalization.CompareOptions.None) == 0;
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
            throw new NotImplementedException();
        }

        #endregion
    }
   

}


