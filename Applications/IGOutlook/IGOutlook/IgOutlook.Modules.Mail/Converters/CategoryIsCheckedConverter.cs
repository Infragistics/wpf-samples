using IgOutlook.Business.Calendar;
using IgOutlook.Business.Mail;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace IgOutlook.Modules.Mail.Converters
{
    public class CategoryIsCheckedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return false;
            
            if (values[0] is ActivityCategory == false) return false;

            ActivityCategory category = (ActivityCategory)values[0];
            string categoryName = (string)values[1];

            return category.CategoryName == categoryName;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
