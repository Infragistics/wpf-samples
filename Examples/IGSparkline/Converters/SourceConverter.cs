using System;
using System.Windows.Data;
using System.Collections.Generic;
using IGSparkline.Model;

namespace IGSparkline.Converters
{
    public class SourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<int> convertedValues = new List<int>();

            Category catValue = value as Category;

            if (catValue != null)
            {
                foreach (Product prod in catValue.Products)
                {
                    convertedValues.Add(prod.UnitsInStock - prod.UnitsOnOrder);
                }
            }
            return convertedValues;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        } 
    }
}
