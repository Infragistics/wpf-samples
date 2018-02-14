using Infragistics.Controls.Grids;
using System;
using System.Windows.Data;

namespace IgExcel.Converters
{
    public class SelectionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var state = (SpreadsheetCellSelectionMode)value;

                switch (state)
                {
                    case SpreadsheetCellSelectionMode.AddToSelection:
                        return ResourceStrings.ResourceStrings.Text_AddToSelection;
                    case SpreadsheetCellSelectionMode.ExtendSelection:
                        return ResourceStrings.ResourceStrings.Text_ExtendSelection;
                    case SpreadsheetCellSelectionMode.Normal:
                        return string.Empty;
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("SelectionModeConverter.ConvertBack");
        }
    }
}
