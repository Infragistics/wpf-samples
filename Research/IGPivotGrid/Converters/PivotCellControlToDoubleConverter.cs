using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Globalization;
using Infragistics.Controls.Grids;
using IGPivotGrid.Samples.Display;

namespace IGPivotGrid.Converters
{
    public class PivotCellControlToDoubleConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PivotCellControl cellControl = value as PivotCellControl;
            PivotCell cell = cellControl.Cell as PivotCell;

            int column = ConditionalFormatting.pivotGridReference.DataColumns.IndexOf(cell.DataColumn);
            int row = ConditionalFormatting.pivotGridReference.DataRows.IndexOf(cell.DataRow);

            object cellVal =
                ConditionalFormatting.pivotGridReference.DataSource.Result.Cells[row, column].Value;
            if (cellVal == null)
                cellVal = 0.0;
            double val = Double.Parse(cellVal.ToString());
            double ratioMultiplier = val / ConditionalFormatting.rowMaxValue;
            if (ratioMultiplier > 1) ratioMultiplier = 1;

            return ratioMultiplier * 90;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        #endregion
    }

}
