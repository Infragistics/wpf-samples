using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Documents.Excel;
using IGExcel.Resources;
using System.IO;
using Infragistics.Controls.Layouts;
using Infragistics.Documents.Excel.Sorting;
using Infragistics.Documents.Excel.Filtering;

namespace IGExcel.Samples.Data
{
    public partial class NamedTables : SampleContainer
    {
        Workbook workbook;
        private Dictionary<string, Grid> grids;

        public NamedTables()
        {
            InitializeComponent();
        }
        private void OnLoadExcelClick(object sender, RoutedEventArgs e)
        {
            LoadWorkbook();
        }

        private void OnExportExcelClick(object sender, RoutedEventArgs e)
        {
            RemoveTables();
            SaveWorkbook();
        }

        private void SaveWorkbook()
        {
            SaveFileDialog dialog = new SaveFileDialog { Filter = "Excel files|*.xlsx", DefaultExt = "xlsx" };
            Stream exportStream;

            if (workbook != null)
            {
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        exportStream = dialog.OpenFile();
                        workbook.Save(exportStream);
                        exportStream.Close();
                    }
                    catch(System.IO.IOException ioException)
                    {
                        MessageBox.Show(ioException.Message);
                    }
                }
            }
        }

        private void LoadWorkbook()
        {
            InvalidateWorkbook();
            string excelResourceFile = "IGExcel.Resources.NamedTables.xlsx";

            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(excelResourceFile))
            {
                workbook = Workbook.Load(stream);
            }

            if (workbook == null)
            {
                return;
            }

            ShowTableSummary(workbook.Worksheets["Table"]);
        }

        private void InvalidateWorkbook()
        {
            workbook = null;
            grids = new Dictionary<string, Grid>();
            xtmNamedTables.Items.Clear();
            btnExportExcel.IsEnabled = true;
        }

        #region Layout Methods

        private List<string> worksheetTableNames;
        private List<string> tablesToRemove = new List<string>();
        private List<string> worksheetTableAreaStyles;

        private void ShowTableSummary(Worksheet worksheet)
        {
            XamTile xt;
            for (int i = 0; i < worksheet.Tables.Count; i++)
            {
                xt = new XamTile();
                FillTileContent(xt, worksheet.Tables[i]);
                if (i == 0)
                {
                    xt.IsMaximized = true;
                }
                else
                {
                    xt.IsMaximized = false;

                }
                xtmNamedTables.Items.Add(xt);
            }
        }

        private void FillTileContent(XamTile tile, WorksheetTable table)
        {
            tile.Name = table.Name;
            tile.Header = table.Name;
            tile.IsExpandedWhenMinimized = false;
            tile.Content = CreateTileContentGrid(table);
        }

        private Grid CreateTileContentGrid(WorksheetTable table)
        {
            Grid mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.4, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition());

            Grid gridTableInfo = CreateNamedTableGrid(table);
            mainGrid.Children.Add(gridTableInfo);
            Grid.SetColumn(gridTableInfo, 0);
            Grid.SetRow(gridTableInfo, 0);

            Grid headerCellsGrid = CreateNamedTableHeaderCellGrid(table);
            headerCellsGrid.Name = String.Format("HeaderGrid{0}", table.Name);
            grids.Add(headerCellsGrid.Name, headerCellsGrid);
            mainGrid.Children.Add(headerCellsGrid);
            Grid.SetColumn(headerCellsGrid, 0);
            Grid.SetRow(headerCellsGrid, 1);

            Grid totalRowGrid = CreateNamedTableTotalCellGrid(table);
            totalRowGrid.Name = String.Format("TotalRowGrid{0}", table.Name);
            grids.Add(totalRowGrid.Name, totalRowGrid);
            mainGrid.Children.Add(totalRowGrid);
            Grid.SetColumn(totalRowGrid, 0);
            Grid.SetRow(totalRowGrid, 1);

            totalRowGrid.Visibility = System.Windows.Visibility.Collapsed;

            return mainGrid;
        }

        private Grid CreateNamedTableGrid(WorksheetTable table)
        {
            Grid gridTable = new Grid();

            gridTable.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            gridTable.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            gridTable.ColumnDefinitions.Add(new ColumnDefinition());
            gridTable.ColumnDefinitions.Add(new ColumnDefinition());
            gridTable.ColumnDefinitions.Add(new ColumnDefinition());

            gridTable.RowDefinitions.Add(new RowDefinition());
            gridTable.RowDefinitions.Add(new RowDefinition());
            gridTable.RowDefinitions.Add(new RowDefinition());

            TextBlock tbName = new TextBlock();
            tbName.Text = ExcelStrings.NamedTables_TableName; //Table name: ;
            tbName.Margin = new Thickness(15, 5, 15, 5);
            tbName.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            gridTable.Children.Add(tbName);
            Grid.SetColumn(tbName, 0);
            Grid.SetRow(tbName, 0);

            TextBlock tbTableName = new TextBlock();
            tbTableName.Text = table.Name;
            tbTableName.Margin = new Thickness(15, 5, 15, 5);
            tbTableName.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            gridTable.Children.Add(tbTableName);
            Grid.SetColumn(tbTableName, 1);
            Grid.SetRow(tbTableName, 0);

            TextBlock tbStandartLabel = new TextBlock();
            tbStandartLabel.Text = ExcelStrings.NamedTables_TableStyle; //Table style:
            tbStandartLabel.Margin = new Thickness(15, 5, 15, 5);
            tbStandartLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            gridTable.Children.Add(tbStandartLabel);
            Grid.SetColumn(tbStandartLabel, 0);
            Grid.SetRow(tbStandartLabel, 1);

            ComboBox cmbStandardStyles = new ComboBox();
            cmbStandardStyles.Width = 150;
            cmbStandardStyles.Margin = new Thickness(15, 5, 15, 5);
            cmbStandardStyles.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            cmbStandardStyles.Tag = table.Name;
            FillStandartStyles(cmbStandardStyles);
            cmbStandardStyles.SelectionChanged += new SelectionChangedEventHandler(OnStandartStylesSelectionChanged);
            cmbStandardStyles.SelectedIndex = worksheetTableNames.IndexOf(table.Style.Name);
            gridTable.Children.Add(cmbStandardStyles);
            Grid.SetColumn(cmbStandardStyles, 1);
            Grid.SetRow(cmbStandardStyles, 1);

            CheckBox cbToRemove = new CheckBox();
            cbToRemove.IsChecked = false;
            cbToRemove.Content = ExcelStrings.NamedTables_ConvertTableToRange; // Convert this table to a range in generated file
            cbToRemove.Margin = new Thickness(15, 5, 15, 5);
            cbToRemove.Tag = table.Name;
            cbToRemove.Unchecked += new RoutedEventHandler(OnRemoveTableUnchecked);
            cbToRemove.Checked += new RoutedEventHandler(OnRemoveTableChecked);
            gridTable.Children.Add(cbToRemove);
            Grid.SetColumn(cbToRemove, 0);
            Grid.SetRow(cbToRemove, 2);
            Grid.SetColumnSpan(cbToRemove, 3);

            RadioButton rbShowHeaders = new RadioButton();
            rbShowHeaders.IsChecked = true;
            rbShowHeaders.Content = ExcelStrings.NamedTables_ShowHeaderCell; //"Show header cell information";
            rbShowHeaders.GroupName = "HeadersAndTotals";
            rbShowHeaders.Tag = table.Name;
            rbShowHeaders.Margin = new Thickness(15, 5, 5, 5);
            rbShowHeaders.Click += new RoutedEventHandler(OnShowHeadersClick);
            gridTable.Children.Add(rbShowHeaders);
            Grid.SetColumn(rbShowHeaders, 2);
            Grid.SetRow(rbShowHeaders, 0);

            RadioButton rbShowTotalCells = new RadioButton();
            rbShowTotalCells.IsChecked = false;
            rbShowTotalCells.Content = ExcelStrings.NamedTables_ShowTotalRow; //"Show total row information";
            rbShowTotalCells.GroupName = "HeadersAndTotals";
            rbShowTotalCells.Tag = table.Name;
            rbShowTotalCells.Margin = new Thickness(15, 5, 5, 5);
            rbShowTotalCells.Click += new RoutedEventHandler(OnShowTotalCellsClick);
            gridTable.Children.Add(rbShowTotalCells);
            Grid.SetColumn(rbShowTotalCells, 2);
            Grid.SetRow(rbShowTotalCells, 1);

            return gridTable;
        }

        private Grid CreateNamedTableHeaderCellGrid(WorksheetTable table)
        {
            Grid columnGrid = new Grid();
            columnGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            columnGrid.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            columnGrid.ColumnDefinitions.Add(new ColumnDefinition()); // Name
            columnGrid.ColumnDefinitions.Add(new ColumnDefinition()); // Sort condition
            columnGrid.ColumnDefinitions.Add(new ColumnDefinition()); // Filter 

            columnGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25) });
            TextBlock tbInfo = new TextBlock();
            tbInfo.Text = String.Format("{0} {1} {2}",
                ExcelStrings.NamedTables_HeaderCellGridInfo_01, table.Name, ExcelStrings.NamedTables_HeaderCellGridInfo_02);
            tbInfo.TextWrapping = TextWrapping.Wrap;
            columnGrid.Children.Add(tbInfo);
            Grid.SetRow(tbInfo, 0);
            Grid.SetColumn(tbInfo, 0);
            Grid.SetColumnSpan(tbInfo, 4);

            columnGrid.RowDefinitions.Add(new RowDefinition());
            string[] names = new string[] { ExcelStrings.NamedTables_ColumnName, ExcelStrings.NamedTables_SortCondition, 
                ExcelStrings.NamedTables_Filter }; // Column name, Sort condition, Fiter
            for (int i = 0; i < 3; i++)
            {
                TextBlock tb = new TextBlock();
                tb.Text = names[i];
                columnGrid.Children.Add(tb);
                Grid.SetRow(tb, 1);
                Grid.SetColumn(tb, i);
            }

            for (int i = 0; i < table.Columns.Count; i++)
            {
                columnGrid.RowDefinitions.Add(new RowDefinition());

                TextBlock tb = GetTableColumnName(table.Columns[i]);
                columnGrid.Children.Add(tb);
                Grid.SetRow(tb, i + 2);
                Grid.SetColumn(tb, 0);

                ComboBox cbSortDirection = new ComboBox();
                cbSortDirection.Width = 120;
                cbSortDirection.Margin = new Thickness(2);
                cbSortDirection.Tag = table.Columns[i];
                cbSortDirection.SelectionChanged += new SelectionChangedEventHandler(OnSortSelectionChanged);
                FillSortDirection(cbSortDirection);
                if ((table.Columns[i]).SortCondition == null)
                {
                    cbSortDirection.SelectedIndex = 2;
                }
                else
                {
                    cbSortDirection.SelectedItem = table.Columns[i].SortCondition.SortDirection.ToString();
                }
                columnGrid.Children.Add(cbSortDirection);
                Grid.SetRow(cbSortDirection, i + 2);
                Grid.SetColumn(cbSortDirection, 1);

                ComboBox cbHandpickFilters = new ComboBox();
                cbHandpickFilters.Width = 120;
                cbHandpickFilters.Margin = new Thickness(2);
                cbHandpickFilters.Tag = table.Columns[i];
                cbHandpickFilters.SelectionChanged += new SelectionChangedEventHandler(OnHandpickFiltersSelectionChanged);
                FillHandpickFilterTypes(cbHandpickFilters);
                if ((table.Columns[i]).Filter == null)
                {
                    cbHandpickFilters.SelectedIndex = 0;
                }
                else
                {
                    cbHandpickFilters.SelectedItem = table.Columns[i].Filter.ToString();
                }
                columnGrid.Children.Add(cbHandpickFilters);
                Grid.SetRow(cbHandpickFilters, i + 2);
                Grid.SetColumn(cbHandpickFilters, 2);
            }

            return columnGrid;
        }

        private Grid CreateNamedTableTotalCellGrid(WorksheetTable table)
        {
            Grid totalCellsGrid = new Grid();
            totalCellsGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            totalCellsGrid.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            totalCellsGrid.ColumnDefinitions.Add(new ColumnDefinition()); // Column name Label 
            totalCellsGrid.ColumnDefinitions.Add(new ColumnDefinition());// Total Row cells Label 
            totalCellsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(280) }); // Total Row cells Formula
            totalCellsGrid.ColumnDefinitions.Add(new ColumnDefinition()); // Total Row cells Value

            totalCellsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25) });
            TextBlock tbInfo = new TextBlock();
            tbInfo.Margin = new Thickness(5);
            tbInfo.Text = String.Format("{0} {1} {2}", ExcelStrings.NamedTables_TotalRowGridInfo_01, table.Name, ExcelStrings.NamedTables_TotalRowGridInfo_02);
            tbInfo.TextWrapping = TextWrapping.Wrap;
            totalCellsGrid.Children.Add(tbInfo);
            Grid.SetRow(tbInfo, 0);
            Grid.SetColumn(tbInfo, 0);
            Grid.SetColumnSpan(tbInfo, 4);

            TextBlock tb;

            totalCellsGrid.RowDefinitions.Add(new RowDefinition());
            string[] names = new string[] { ExcelStrings.NamedTables_ColumnName, ExcelStrings.NamedTables_Label, 
                ExcelStrings.NamedTables_Formula, ExcelStrings.NamedTables_Value }; // Column name, Label, Formula, Value

            for (int i = 0; i < 4; i++)
            {
                tb = new TextBlock();
                tb.Margin = new Thickness(5);
                tb.Text = names[i];
                totalCellsGrid.Children.Add(tb);
                Grid.SetRow(tb, 1);
                Grid.SetColumn(tb, i);
            }

            for (int i = 0; i < table.Columns.Count; i++)
            {
                totalCellsGrid.RowDefinitions.Add(new RowDefinition());

                tb = GetTableColumnName(table.Columns[i]);
                tb.Width = 150;
                tb.Margin = new Thickness(5);
                totalCellsGrid.Children.Add(tb);
                Grid.SetRow(tb, i + 2);
                Grid.SetColumn(tb, 0);

                tb = new TextBlock();
                tb.Width = 150;
                tb.Text = table.Columns[i].TotalLabel;
                tb.Margin = new Thickness(5);
                totalCellsGrid.Children.Add(tb);
                Grid.SetRow(tb, i + 2);
                Grid.SetColumn(tb, 1);

                tb = GetFormulaInformation(table.Columns[i]);
                tb.Margin = new Thickness(5);
                tb.TextWrapping = TextWrapping.Wrap;
                totalCellsGrid.Children.Add(tb);
                Grid.SetRow(tb, i + 2);
                Grid.SetColumn(tb, 2);

                tb = new TextBlock();
                tb.Margin = new Thickness(5);
                tb.Width = 150;
                if (table.Columns[i].TotalCell != null && table.Columns[i].TotalCell.Value != null)
                {
                    tb.Text = table.Columns[i].TotalCell.Value.ToString();
                }
                totalCellsGrid.Children.Add(tb);
                Grid.SetRow(tb, i + 2);
                Grid.SetColumn(tb, 3);
            }

            if (!(table.IsTotalsRowVisible))
            {
                tbInfo.Text += ExcelStrings.NamedTables_TotalRowNotVisible; // Total row is not visible for this table.
            }

            return totalCellsGrid;
        }

        private void RemoveTables()
        {
            foreach (string s in tablesToRemove)
            {
                WorksheetTable table = workbook.GetTable(s);
                workbook.Worksheets["Table"].Tables.Remove(table);
            }
        }

        #endregion // Layout Methods

        #region Combo and CheckBox Handlers

        void OnShowHeadersClick(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            string tableName = rb.Tag.ToString();
            SetVisibility(tableName, true);
        }

        void OnShowTotalCellsClick(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            string tableName = rb.Tag.ToString();
            SetVisibility(tableName, false);
        }

        void OnRemoveTableUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            string tableName = cb.Tag.ToString();
            if (tablesToRemove.IndexOf(tableName) > -1)
            {
                tablesToRemove.Remove(tableName);
            }
        }

        void OnRemoveTableChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            string tableName = cb.Tag.ToString();
            tablesToRemove.Add(tableName);
        }

        void OnSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            WorksheetTableColumn tableColumn = (WorksheetTableColumn)cb.Tag;
            if (cb.SelectedIndex != 2)
            {
                SortDirection sd = (SortDirection)cb.SelectedIndex;
                tableColumn.SortCondition = new OrderedSortCondition(sd);
            }
        }

        void OnHandpickFiltersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            WorksheetTableColumn tableColumn = (WorksheetTableColumn)cb.Tag;
            if (cb.SelectedItem != null)
            {
                ApplyFilter(tableColumn, cb.SelectedItem.ToString());
            }
        }

        void OnStandartStylesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            WorksheetTable table = workbook.GetTable(cb.Tag.ToString());
            table.Style = workbook.StandardTableStyles[cb.SelectedIndex];
        }

        #endregion // Combo and CheckBox Handlers

        #region Helper Methods

        private void SetVisibility(string gridName, bool isVisible)
        {
            Grid headerGrid = grids[String.Format("HeaderGrid{0}", gridName)];
            Grid totalRowGrid = grids[String.Format("TotalRowGrid{0}", gridName)];

            if (isVisible)
            {
                headerGrid.Visibility = System.Windows.Visibility.Visible;
                totalRowGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                headerGrid.Visibility = System.Windows.Visibility.Collapsed;
                totalRowGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private TextBlock GetTableColumnName(WorksheetTableColumn column)
        {
            TextBlock tb = new TextBlock();
            tb.Text = column.Name;
            tb.TextWrapping = TextWrapping.Wrap;
            return tb;
        }

        private TextBlock GetFormulaInformation(WorksheetTableColumn column)
        {
            TextBlock tb = new TextBlock();
            if (column.TotalFormula == null)
            {
                tb.Text = ExcelStrings.NamedTables_NoFormulaApplied; // No formula applied;
            }
            else
            {
                tb.Text = column.TotalFormula.ToString();
            }
            return tb;
        }

        private void FillStandartStyles(ComboBox cb)
        {
            worksheetTableNames = new List<string>();
            foreach (WorksheetTableStyle wTableStyle in workbook.StandardTableStyles)
            {
                worksheetTableNames.Add(wTableStyle.Name);
            }
            cb.ItemsSource = worksheetTableNames.ToArray();
        }

        private void FillTableAreaStyles(ComboBox cb)
        {
            worksheetTableAreaStyles = new List<string>();
            foreach (WorksheetTableStyleArea tableStyleArea in Enum.GetValues(typeof(WorksheetTableStyleArea)))
            {
                worksheetTableAreaStyles.Add(tableStyleArea.ToString());
            }
            cb.ItemsSource = worksheetTableAreaStyles.ToArray();
        }

        private void FillSortDirection(ComboBox cb)
        {
            string[] sortOptions = new string[] { ExcelStrings.NamedTables_SortDirection_Ascending, ExcelStrings.NamedTables_SortDirection_Descending, 
                ExcelStrings.NamedTables_NotApplied }; // Ascending, Descending, Not Applied
            cb.ItemsSource = sortOptions;
        }

        string[] filterTypes = new string[] { ExcelStrings.NamedTables_NotApplied, ExcelStrings.NamedTables_AboveAverage, 
            ExcelStrings.NamedTables_BelowAverage, ExcelStrings.NamedTables_YearToDate }; // Not applied, Above Average, Below Average, Year to date

        private void FillHandpickFilterTypes(ComboBox cb)
        {

            cb.ItemsSource = filterTypes;
        }

        private void ApplyFilter(WorksheetTableColumn column, string selectedFilter)
        {
            int index = Array.IndexOf(filterTypes, selectedFilter);
            // Here only few of available filter methods are used. You can check the documentation for full list of available filter functions.
            switch (index)
            {
                case 0:
                    column.ClearFilter();
                    break;
                case 1:
                    column.ApplyAverageFilter(AverageFilterType.AboveAverage);
                    break;
                case 2:
                    column.ApplyAverageFilter(AverageFilterType.BelowAverage);
                    break;
                case 3:
                    column.ApplyYearToDateFilter();
                    break;
                default:
                    break;
            }
        }

        #endregion // Helper Methods
    }
}