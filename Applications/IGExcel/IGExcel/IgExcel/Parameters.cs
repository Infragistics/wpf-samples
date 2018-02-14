namespace IgExcel
{
    public class FormatCellsDialogParameters
    {
        public const string ViewName = "FormatCellsDialogView";
        public const string InitialTabNameKey = "InitialTabName";
        public const string SelectionKey = "Selection";
        public const string SelectionFormatsKey = "SelectionFormats";
        public const string WorksheetKey = "Worksheet";
        public const string NumberTabKey = "Number";
        public const string AlignmentTabKey = "Alignment";
        public const string FontTabKey = "Font";
        public const string FillTabKey = "Fill";
    }

    public class FormatAsTableDialogParameters
    {
        public const string ViewName = "FormatAsTableDialogView";
        public const string SelectionKey = "Selection";
        public const string WorksheetKey = "Worksheet";
        public const string WorkbookKey = "Workbook";
        public const string TableStyleKey = "TableStyle";
    }

    public class DimensionDialogViewParameters
    {
        public const string ViewName = "DimensionDialogView";
        public const string DialogModeKey = "DialogMode";
        public const string SelectionKey = "Selection";
        public const string WorksheetKey = "Worksheet";
        public const string WorkbookKey = "Workbook";
        public const string TableStyleKey = "TableStyle";
    }

    public class PasswordDialogViewParameters
    {
        public const string ViewName = "PasswordDialogView";
        public const string DialogModeKey = "DialogMode";
        public const string FileNameKey = "FileName";
        public const string WorkbookKey = "Workbook";
    }

    public class ZoomDialogViewParameters
    {
        public const string ViewName = "ZoomDialogView";
        public const string ZoomLevelKey = "ZoomLevel";
    }

    public class DataValidationDialogViewParameters
    {
        public const string ViewName = "DataValidationDialogView";
        public const string SelectionKey = "Selection";
        public const string WorksheetKey = "Worksheet";
    }

    public class ShellParameters
    {
        public const string NewTabName = "newTab";
        public const string SaveAsTabName = "saveAsTab";
        public const string FileDialogFilter = "Excel Workbook (*.xlsx)|*.xlsx|Excel Macro-Enabled Workbook (*.xlsm)|*.xlsm|Excel 97-2003 Workbook (*.xls)|*.xls|Excel Template (*.xltx)|*.xltx|Excel Macro-Enabled Template (*.xltm)|*.xltm";
    }

}
