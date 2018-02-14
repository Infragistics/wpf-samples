using Infragistics.Documents.Excel;
using System;
using System.Windows;
using System.Windows.Data;

namespace IgExcel.Converters
{
    public class CellStyleConverter : IMultiValueConverter
    {
        private WorkbookStyleCollection workbookStyles;

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null)
                return null;

            var style = (WorkbookStyle)values[0];

            if (values[1] != DependencyProperty.UnsetValue)
                this.workbookStyles = (WorkbookStyleCollection)values[1];

            if (style == null)
                return null;

            return style.Name;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object[] { this.workbookStyles[(string)value] };
        }
    }
}
