using System;
using System.Windows.Data;
using System.Globalization;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace Infragistics.Samples.Shared.Converters
{
    public class ProductCategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ProductCategory selectedItem = null;
            ProductCategoryProvider values = parameter as ProductCategoryProvider;
            if (values != null)
            {
                string selectedValue = (string)value;
                foreach (ProductCategory item in values)
                {
                    if (item.Value == selectedValue)
                    {
                        selectedItem = item;
                        break;
                    }
                }
            }
            return selectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;

            ProductCategory selectedType = value as ProductCategory;
            if (selectedType != null)
            {
                result = selectedType.Value;
            }

            return result;
        }

    }
}