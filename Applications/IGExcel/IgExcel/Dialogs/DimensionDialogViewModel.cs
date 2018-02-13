using IgExcel.Infrastructure;
using IgExcel.Infrastructure.Dialogs;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Prism.Commands;
using Prism.Regions;
using System;

namespace IgExcel.Dialogs
{
    public class DimensionDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Private Variables

        private DimensionDialogMode mode;
        private string dimensionDescription;
        private double? dimensionValue;
        private SpreadsheetSelection selection;
        private Worksheet activeWorksheet;

        #endregion //Private Variables

        #region Public Properties

        public double? DimensionValue
        {
            get { return dimensionValue; }
            set { SetProperty<double?>(ref dimensionValue, value); }
        }
        public string DimensionDescription
        {
            get { return dimensionDescription; }
            set { SetProperty<string>(ref dimensionDescription, value); }
        }

        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        #endregion //Public Properties

        #region Constructor

        public DimensionDialogViewModel()
        {
            OkCommand = new DelegateCommand(ExecuteOk);
            CancelCommand = new DelegateCommand(ExecuteCancel);
        }

        #endregion //Constructor

        #region Commands

        private bool CanExecuteOk()
        {
            return true;
        }

        private void ExecuteOk()
        {
            if (this.DimensionValue.HasValue)
            {
                switch (this.mode)
                {
                    case DimensionDialogMode.RowHeight:
                        foreach (var range in this.selection.CellRanges)
                        {
                            for (int i = range.FirstRow; i <= range.LastRow; i++)
                            {
                                this.activeWorksheet.Rows[i].Height = (int)this.DimensionValue.Value * 20;
                            }
                        }
                        break;
                    case DimensionDialogMode.ColumnWidth:
                        foreach (var range in this.selection.CellRanges)
                        {
                            for (int i = range.FirstColumn; i <= range.LastColumn; i++)
                            {
                                this.activeWorksheet.Columns[i].SetWidth(this.DimensionValue.Value, WorksheetColumnWidthUnit.CharacterPaddingExcluded);
                            }
                        }
                        break;
                    case DimensionDialogMode.DeafaultWidth:
                        this.activeWorksheet.SetDefaultColumnWidth(this.DimensionValue.Value, WorksheetColumnWidthUnit.CharacterPaddingExcluded);
                        break;
                }
            }

            RequestClose();
        }

        private void ExecuteCancel()
        {
            RequestClose();
        }



        #endregion //Commands

        #region IDialogAware Members

        public bool CanCloseDialog()
        {
            return true;
        }

        public event Action RequestClose;

        void CloseDialog()
        {
            if (RequestClose != null)
            {
                RequestClose();
            }
        }

        #endregion //IDialogAware Members

        #region INavigationAware

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.mode = (DimensionDialogMode)navigationContext.Parameters[DimensionDialogViewParameters.DialogModeKey];
            this.selection = (SpreadsheetSelection)navigationContext.Parameters[FormatAsTableDialogParameters.SelectionKey];
            this.activeWorksheet = (Worksheet)navigationContext.Parameters[FormatAsTableDialogParameters.WorksheetKey];

            SetStrings(this.mode);
            ReadDimension(this.mode, this.selection, this.activeWorksheet);
        }

        #endregion //INavigationAware

        #region Private Methods

        private void SetStrings(DimensionDialogMode dialogMode)
        {
            switch (dialogMode)
            {
                case DimensionDialogMode.RowHeight:
                    this.Title = ResourceStrings.ResourceStrings.Text_RowHeightTitle;
                    this.DimensionDescription = ResourceStrings.ResourceStrings.Lbl_RowHeight;
                    this.IconSource = "pack://application:,,,/IgExcel.Infrastructure;component/Images/RowHeight.ico";
                    break;
                case DimensionDialogMode.ColumnWidth:
                    this.Title = ResourceStrings.ResourceStrings.Text_ColumnWidthTitle;
                    this.DimensionDescription = ResourceStrings.ResourceStrings.Lbl_ColumnWidth;
                    this.IconSource = "pack://application:,,,/IgExcel.Infrastructure;component/Images/ColumnWidth.ico";
                    break;
                case DimensionDialogMode.DeafaultWidth:
                    this.Title = ResourceStrings.ResourceStrings.Text_StandartWidth;
                    this.DimensionDescription = ResourceStrings.ResourceStrings.Lbl_StandartColumnWidth;
                    this.IconSource = "pack://application:,,,/IgExcel.Infrastructure;component/Images/ColumnWidth.ico";
                    break;
            }
        }

        private void ReadDimension(DimensionDialogMode dialogMode, SpreadsheetSelection selection, Worksheet worksheet)
        {
            switch (dialogMode)
            {
                case DimensionDialogMode.RowHeight:
                    var rowHeight = this.activeWorksheet.Rows[this.selection.CellRanges[0].FirstRow].GetCellBoundsInTwips(this.selection.CellRanges[0].FirstColumn).Height;

                    foreach (var range in this.selection.CellRanges)
                    {
                        for (int i = range.FirstRow; i <= range.LastRow; i++)
                        {
                            var row = this.activeWorksheet.Rows[i];
                            if (rowHeight != row.GetCellBoundsInTwips(range.FirstColumn).Height)
                            {
                                rowHeight = -1;
                                break;
                            }
                        }

                        if (rowHeight == -1)
                            break;
                    }

                    if (rowHeight != -1)
                    {
                        this.DimensionValue = rowHeight / 20;
                    }
                    break;
                case DimensionDialogMode.ColumnWidth:
                    var columnWidth = this.activeWorksheet.Columns[this.selection.CellRanges[0].FirstColumn].Width;

                    foreach (var range in this.selection.CellRanges)
                    {

                        for (int i = range.FirstColumn; i <= range.LastColumn; i++)
                        {
                            if (columnWidth != this.activeWorksheet.Columns[i].Width)
                            {
                                columnWidth = -2;
                            }
                        }

                        if (columnWidth == -2)
                            break;
                    }

                    if (columnWidth == -1)
                    {
                        this.DimensionValue = worksheet.GetDefaultColumnWidth(WorksheetColumnWidthUnit.CharacterPaddingExcluded);
                    }
                    else if (columnWidth != -2)
                    {
                        this.DimensionValue = this.activeWorksheet.Columns[this.selection.CellRanges[0].FirstColumn].GetWidth(WorksheetColumnWidthUnit.CharacterPaddingExcluded);
                    }
                    break;
                case DimensionDialogMode.DeafaultWidth:
                    this.DimensionValue = worksheet.GetDefaultColumnWidth(WorksheetColumnWidthUnit.CharacterPaddingExcluded);
                    break;
            }
        }

        #endregion //Private Methods
    }
}
