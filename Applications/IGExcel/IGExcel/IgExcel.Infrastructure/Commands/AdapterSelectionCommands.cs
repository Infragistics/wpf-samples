using IgExcel.Infrastructure.Adapters;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace IgExcel.Infrastructure.Commands
{
    #region AdapterSelectionCommandBase
    internal abstract class AdapterSelectionCommandBase : ICommand
    {
        protected readonly SpreadSelectionSheetAdapter Adapter;

        internal AdapterSelectionCommandBase(SpreadSelectionSheetAdapter adapter)
        {
            this.Adapter = adapter;
        }

        public virtual bool CanExecute(object parameter)
        {
            var spreadSheet = this.Adapter.SpreadSheet;
            return spreadSheet != null;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;

            if (null != handler)
                handler(this, EventArgs.Empty);
        }

        public abstract void Execute(object parameter);
    }
    #endregion //AdapterSelectionCommandBase

    #region AdapterSetBorderCommand
    internal class AdapterSetBorderCommand : AdapterSelectionCommandBase
    {
        internal AdapterSetBorderCommand(SpreadSelectionSheetAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            SpreadsheetCellRangeBorders borders = this.Adapter.SheetCellRangeBorders;
            var borderColor = this.Adapter.BorderColor;
            CellBorderLineStyle style = this.Adapter.CellBorderLineStyle;

            ExcelBorders borderCommnd = this.Adapter.LastAppliedBorderSettings;

            if (parameter != null)
            {
                borderCommnd = (ExcelBorders)Enum.Parse(typeof(ExcelBorders), parameter.ToString());
            }

            this.Adapter.LastAppliedBorderSettings = borderCommnd;

            switch (borderCommnd)
            {
                case ExcelBorders.BottomBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                case ExcelBorders.TopBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.TopBorder;
                    break;
                case ExcelBorders.LeftBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.LeftBorder;
                    break;
                case ExcelBorders.RightBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.RightBorder;
                    break;
                case ExcelBorders.NoBorder:
                    borders = SpreadsheetCellRangeBorders.AllBorder;
                    style = CellBorderLineStyle.None;
                    break;
                case ExcelBorders.AllBorders:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.AllBorder;
                    break;
                case ExcelBorders.OutsideBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.OutsideBorder;
                    break;
                case ExcelBorders.ThickBoxBorder:
                    style = CellBorderLineStyle.Thick;
                    borders = SpreadsheetCellRangeBorders.OutsideBorder;
                    break;
                case ExcelBorders.BottomDoubleBorder:
                    style = CellBorderLineStyle.Double;
                    borders = SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                case ExcelBorders.ThickBottomBorder:
                    style = CellBorderLineStyle.Thick;
                    borders = SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                case ExcelBorders.TopAndBottomBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.TopBorder | SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                case ExcelBorders.TopAndThickBottomBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.TopBorder;
                    this.Adapter.SpreadSheet.ActiveSelectionCellRangeFormat.SetBorders(borders, borderColor, style);
                    style = CellBorderLineStyle.Thick;
                    borders = SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                case ExcelBorders.TopAndDoubleBottomBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.TopBorder;
                    this.Adapter.SpreadSheet.ActiveSelectionCellRangeFormat.SetBorders(borders, borderColor, style);
                    style = CellBorderLineStyle.Double;
                    borders = SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                default:
                    break;
            }

            this.Adapter.SheetCellRangeBorders = borders;
            this.Adapter.BorderColor = borderColor;
            this.Adapter.CellBorderLineStyle = style;


            this.Adapter.SpreadSheet.ActiveSelectionCellRangeFormat.SetBorders(borders, borderColor, style);
        }
    }
    #endregion //AdapterSetBorderCommand

    #region AdapterSetCellFillCommand
    internal class AdapterSetCellFillCommand : AdapterSelectionCommandBase
    {
        internal AdapterSetCellFillCommand(SpreadSelectionSheetAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            var color = (Color)parameter;

            if (color != null)
            {
                if (color.A == 0 && color != Colors.Transparent)
                    return;

                if (color == Colors.Transparent)
                {
                    this.Adapter.SpreadSheet.ActiveSelectionCellRangeFormat.Fill = CellFill.NoColor;
                }
                else
                {
                    this.Adapter.SpreadSheet.ActiveSelectionCellRangeFormat.Fill = CellFill.CreateSolidFill(color);
                }
            }
            
        }
    }
    #endregion //AdapterSetCellFillCommand

    #region AdapterSetFontColorCommand
    internal class AdapterSetFontColorCommand : AdapterSelectionCommandBase
    {
        internal AdapterSetFontColorCommand(SpreadSelectionSheetAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            var color = (Color)parameter;

            if (color != null)
            {
                if (color.A == 0 && color != Colors.Transparent)
                    return;

                if (color == Colors.Transparent)
                {
                    this.Adapter.SpreadSheet.ActiveSelectionCellRangeFormat.Font.ColorInfo = WorkbookColorInfo.Automatic;
                }
                else
                {
                    this.Adapter.SpreadSheet.ActiveSelectionCellRangeFormat.Font.ColorInfo = new WorkbookColorInfo(color);
                }
            }
        }
    }
    #endregion //AdapterSetFontColorCommand
}
