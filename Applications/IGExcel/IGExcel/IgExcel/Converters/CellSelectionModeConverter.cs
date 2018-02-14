using IgExcel.Dialogs;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IgExcel.Converters
{
    public class BoolToCellSelectionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DialogCellSelectionMode)
            {
                switch ((DialogCellSelectionMode)value)
                {
                    case DialogCellSelectionMode.NotInCellSelection:
                        return false;
                    case DialogCellSelectionMode.FromThird:
                    case DialogCellSelectionMode.FromFourth:
                        return true;
                }
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && targetType == typeof(DialogCellSelectionMode))
            {
                if ((bool)value)
                {
                    if (parameter != null)
                    {
                        switch (parameter.ToString())
                        {
                            case "3": return DialogCellSelectionMode.FromThird;
                            case "4": return DialogCellSelectionMode.FromFourth;
                        }
                    }
                    throw new NotImplementedException();
                }
                else
                {
                    return DialogCellSelectionMode.NotInCellSelection;
                }
            }
            throw new NotImplementedException();
        }
    }

    public class MainDialogVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DialogCellSelectionMode)
            {
                switch ((DialogCellSelectionMode)value)
                {
                    case DialogCellSelectionMode.NotInCellSelection:
                        return Visibility.Visible;
                    case DialogCellSelectionMode.FromThird:
                    case DialogCellSelectionMode.FromFourth:
                        return Visibility.Collapsed;
                }
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CellSelectionDialogVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DialogCellSelectionMode)
            {
                switch ((DialogCellSelectionMode)value)
                {
                    case DialogCellSelectionMode.NotInCellSelection: return Visibility.Collapsed;
                    case DialogCellSelectionMode.FromThird:
                    case DialogCellSelectionMode.FromFourth:
                        return Visibility.Visible;
                }
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
