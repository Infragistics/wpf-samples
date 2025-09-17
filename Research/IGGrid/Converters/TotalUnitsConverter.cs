using System;
using System.Windows.Data;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Converters
{
    public class TotalUnitsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Product p = value as Product;
            if (p != null)
            {
                return p.UnitsInStock + p.UnitsOnOrder;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
