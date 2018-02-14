using Infragistics.Controls.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IgExcel.Converters
{
    public class CellStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var state = (SpreadsheetCellEditMode)value;

                switch (state)
                {
                    case SpreadsheetCellEditMode.ArrowKeysNavigateBetweenCells:
                        return ResourceStrings.ResourceStrings.Text_Enter;
                    case SpreadsheetCellEditMode.ArrowKeysNavigateInCell:
                        return ResourceStrings.ResourceStrings.Text_Edit;
                    case SpreadsheetCellEditMode.NotInEditMode:
                        return ResourceStrings.ResourceStrings.Text_Ready;
                }
            }

            return ResourceStrings.ResourceStrings.Text_Ready;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("CellStateConverter.ConvertBack");
        }
    }
}
