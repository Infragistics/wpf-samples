using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Converters
{
  public  class EnableConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is RecordSelectorNumberType)
            {
                RecordSelectorNumberType rs = (RecordSelectorNumberType)value;
                if (rs == RecordSelectorNumberType.DataItemIndex || rs == RecordSelectorNumberType.RecordIndex || rs == RecordSelectorNumberType.VisibleIndex)
                {
                    return true;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }
}
