using System;
using System.Globalization;
using System.Windows.Data;

namespace IGOrgChart.Converters
{
    /// <summary>
    /// Extracts the type name of an object.
    /// </summary>
    public class TypeToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Get the name of the object's type.
            return value.GetType().Name;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Not used.
            throw new NotImplementedException();
        }

        #endregion
    }
}
