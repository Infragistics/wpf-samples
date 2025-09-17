using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using IGPivotGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Infragistics.Olap.Data;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Tools;

namespace IGPivotGrid.Samples.Data
{
    public partial class ExcelExport : SampleContainer
    {
        public ExcelExport()
        {
            InitializeComponent();

            this.pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            //if data is still not loaded give a warning.
            if (this.pivotGrid.GridLayout == null ||
                this.pivotGrid.GridLayout.Rows == null ||
                this.pivotGrid.GridLayout.Columns == null)
            {
                MessageBox.Show(PivotGridStrings.XPG_Still_Loading);
                return;
            }

            this.SaveExport(this.CreateWorkbook());
        }

        private Workbook CreateWorkbook()
        {
            Boolean CompactLayoutEnabled = pivotGrid.AllowCompactLayout;

            if (CompactLayoutEnabled)
                pivotGrid.AllowCompactLayout = false;

            //Calculate the dimensions of the header rows and columns
            int TopHeaderHeight = 0;
            if (columnHeadersCheckBox.IsChecked.Value)
            {
                TopHeaderHeight = this.pivotGrid.GridLayout.PrivateRowsForColumnPanel.Count;
            }

            int LeftHeaderWidth = 0;
            if (rowHeadersCheckBox.IsChecked.Value)
            {
                LeftHeaderWidth = this.pivotGrid.GridLayout.PrivateColumnsForRowPanel.Count;
            }

            //Create the blank workbook and the sheet in it that will contain the PivotGrid data
            Workbook dataWorkbook = new Workbook();
            Worksheet sheetOne = dataWorkbook.Worksheets.Add(PivotGridStrings.XPG_Excel_Export_Sheet_Name);

            //Freeze header rows and columns
            if (columnHeadersCheckBox.IsChecked.Value || rowHeadersCheckBox.IsChecked.Value)
                sheetOne.DisplayOptions.PanesAreFrozen = true;
            if (rowHeadersCheckBox.IsChecked.Value)
                sheetOne.DisplayOptions.FrozenPaneSettings.FrozenRows = TopHeaderHeight;
            if (columnHeadersCheckBox.IsChecked.Value)
                sheetOne.DisplayOptions.FrozenPaneSettings.FrozenColumns = LeftHeaderWidth;

            //Set default width for the columns
            sheetOne.DefaultColumnWidth = 5000;


            int ColumnSpan, ColumnId, RowSpan, RowId;
            string CellValue;

            if (columnHeadersCheckBox.IsChecked.Value)
            {
                // Build Column Header
                foreach (PivotColumnHeaderCell cell in this.pivotGrid.GridLayout.ColumnHeaderCells)
                {
                    ColumnSpan = cell.RealColumnSpan;
                    ColumnId = this.pivotGrid.GridLayout.Columns
                        .IndexOf(cell.Column as PivotDataColumn)
                        + LeftHeaderWidth;

                    RowSpan = cell.RowSpan;
                    RowId = this.pivotGrid.GridLayout
                        .PrivateRowsForColumnPanel.IndexOf(cell.Row as PivotHeaderRow);

                    CellValue = cell.Member.Caption;
                    if (cell.Member.IsTotal && cell.IsToggleVisible == false)
                        CellValue += " " + PivotGridStrings.XPG_Excel_Export_Total;

                    if (ColumnSpan > 1 || RowSpan > 1)
                        sheetOne.MergedCellsRegions.Add(
                            RowId,
                            ColumnId,
                            RowId + RowSpan - 1,
                            ColumnId + ColumnSpan - 1
                            );

                    this.SetCellValue(sheetOne.Rows[RowId].Cells[ColumnId], CellValue, "TopHeader");
                }
            }

            if (rowHeadersCheckBox.IsChecked.Value)
            {
                // Build Row Header
                foreach (PivotRowHeaderCell cell in this.pivotGrid.GridLayout.RowHeaderCells)
                {
                    ColumnSpan = cell.ColumnSpan;
                    ColumnId = this.pivotGrid.GridLayout
                        .PrivateColumnsForRowPanel.IndexOf(cell.Column as PivotHeaderColumn);

                    RowSpan = cell.RealRowSpan;
                    RowId = this.pivotGrid.GridLayout
                        .Rows.IndexOf(cell.Row as PivotDataRow) + TopHeaderHeight;

                    CellValue = cell.Member.Caption;
                    if (cell.Member.IsTotal && cell.IsToggleVisible == false)
                        CellValue += " " + PivotGridStrings.XPG_Excel_Export_Total;

                    if (ColumnSpan > 1 || RowSpan > 1)
                        sheetOne.MergedCellsRegions.Add(
                            RowId,
                            ColumnId,
                            RowId + RowSpan - 1,
                            ColumnId + ColumnSpan - 1
                            );

                    this.SetCellValue(sheetOne.Rows[RowId].Cells[ColumnId], CellValue, "LeftHeader");
                }
            }

            // Build Data
            int currentRow = 0;
            foreach (PivotDataRow row in this.pivotGrid.GridLayout.Rows)
            {
                int currentCell = 0;
                foreach (PivotCell cell in row.Cells)
                {
                    string currentCellValue;
                    if (cell.Data != null)
                    {
                        currentCellValue = (cell.Data as ICell).FormattedValue;
                    }
                    else
                    {
                        currentCellValue = string.Empty;
                    }
                    this.SetCellValue(
                        sheetOne.Rows[currentRow + TopHeaderHeight]
                            .Cells[currentCell + LeftHeaderWidth],
                        currentCellValue,
                        "Data");
                    currentCell++;
                }
                currentRow++;
            }


            if (CompactLayoutEnabled)
                pivotGrid.AllowCompactLayout = true;
            //return the filled in workbook
            return dataWorkbook;

        }

        //Set the value and format each cell
        private void SetCellValue(WorksheetCell cell, string cellValue, string cellType)
        {
            cell.Value = cellValue;
            cell.CellFormat.ShrinkToFit = ExcelDefaultableBoolean.True;
            cell.CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
            cell.CellFormat.Alignment = HorizontalCellAlignment.Center;

            if (cellType == "TopHeader")
            {
                cell.CellFormat.Fill = CellFill.CreatePatternFill(ColorMaker.GetColor(255, 181, 212, 240), ColorMaker.GetColor(255, 255, 255, 255), FillPatternStyle.Solid);
            }
            else if (cellType == "LeftHeader")
            {
                cell.CellFormat.Fill = CellFill.CreatePatternFill(ColorMaker.GetColor(255, 200, 212, 240), ColorMaker.GetColor(255, 255, 255, 255), FillPatternStyle.Solid);
            }
            else if (cellType == "Data")
            {
                cell.CellFormat.Fill = CellFill.CreatePatternFill(ColorMaker.GetColor(255, 242, 244, 249), ColorMaker.GetColor(255, 255, 255, 255), FillPatternStyle.Solid);
            }
            else
            {
                MessageBox.Show(PivotGridStrings.XPG_Cell_Type_Not_Located);
            }
        }

        //Open a dialog and prompt the user to save the Excel workbook with the exported data
        private void SaveExport(Workbook dataWorkbook)
        {
            SaveFileProvider saveProvider = new SaveFileProvider();
            saveProvider.Dialog.Filter = PivotGridStrings.XPG_Excel_Export_ExcelFileFilter;
            saveProvider.Dialog.DefaultExt = "xls";

            bool? showDialog = saveProvider.Dialog.ShowDialog();
            if (showDialog == true)
            {
                using (Stream exportStream = saveProvider.Dialog.OpenFile())
                {
                    dataWorkbook.Save(exportStream);
                    exportStream.Close();
                }
            }
        }

        void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
