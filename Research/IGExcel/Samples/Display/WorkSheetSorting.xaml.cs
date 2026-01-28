using IGExcel.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Documents.Excel.Sorting;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace IGExcel.Samples.Display
{
    /// <summary>
    /// Interaction logic for SortWorkSheet.xaml
    /// </summary>
    public partial class SortWorkSheet : SampleContainer
    {
        Worksheet sheetOne;
        SortDirection sortDirection = SortDirection.Descending;

        public SortWorkSheet()
        {
            InitializeComponent();           
        }
        private void SampleContainer_Loaded(object sender, RoutedEventArgs e)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;

            List<string> excelFormats = new List<string>
                                            {
                                                ExcelStrings.ExcelEngine_Combo_XLSX,
                                                ExcelStrings.ExcelEngine_Combo_XLS
                                            };

            this.ComboBox_ExcelFormat.ItemsSource = excelFormats;
            this.ComboBox_ExcelFormat.SelectedIndex = 0;
            ImportData();
        }
        private void ImportData()
        {
            Workbook dataWorkbook;
            string resourceName = "IGExcel.Resources." + this.GetExcelFileName();

            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(resourceName))
            {
                dataWorkbook = Workbook.Load(stream);
            }

            if (dataWorkbook == null)
            {
                return;
            }

            sheetOne = dataWorkbook.Worksheets[0];
            LoadProductCollection();
        }
    
        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            Workbook dataWorkbook = new Workbook();
            Worksheet excelSheet = dataWorkbook.Worksheets.Add("Data Sheet");

            // Export to Excel2007 format 
            if (ComboBox_ExcelFormat.SelectedIndex == 0)
            {
                dataWorkbook.SetCurrentFormat(WorkbookFormat.Excel2007);
            }
            else
            {
                dataWorkbook.SetCurrentFormat(WorkbookFormat.Excel97To2003);
            }

            // Print column header text 
            int currentColumn = 0;
            foreach (DataGridColumn column in this.dataGrid.Columns)
            {
                this.SetCellValue(excelSheet.Rows[0].Cells[currentColumn], column.Header);
                currentColumn++;
            }

            // Export Data From Grid
            IEnumerable gridData = dataGrid.ItemsSource;

            int currentRow = 1;
            WorksheetRow worksheetRow;

            foreach (Product product in gridData)
            {
                int currentCell = 0;
                worksheetRow = excelSheet.Rows[currentRow];

                this.SetCellValue(worksheetRow.Cells[currentCell], product.Name);
                this.SetCellValue(worksheetRow.Cells[++currentCell], product.Category);
                this.SetCellValue(worksheetRow.Cells[++currentCell], product.Supplier);
                this.SetCellValue(worksheetRow.Cells[++currentCell], product.OnBackOrder);
              
                currentRow++;
            }

            this.setColumnFormatting(excelSheet);
            this.SaveExport(dataWorkbook);
        }

        private void SetCellValue(WorksheetCell cell, object value)
        {
            cell.Value = value;           
            cell.CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
            cell.CellFormat.Alignment = HorizontalCellAlignment.Center;
        }
        private void setColumnFormatting(Worksheet worksheet)
        {
            // Freeze header row
            worksheet.DisplayOptions.PanesAreFrozen = true;
            worksheet.DisplayOptions.FrozenPaneSettings.FrozenRows = 1;

            // Build Column List
            worksheet.DefaultColumnWidth = 5000;
            worksheet.Columns[0].Width = 10000;
            worksheet.Columns[2].Width = 10000;                       
        }

        private void SaveExport(Workbook dataWorkbook)
        {
            SaveFileDialog dialog;
            Stream exportStream;

            // Export to xlsx excel file format
            if (ComboBox_ExcelFormat.SelectedIndex == 0)
            {
                dialog = new SaveFileDialog { Filter = "Excel files|*.xlsx", DefaultExt = "xlsx" };
            }
            else
            {
                dialog = new SaveFileDialog { Filter = "Excel files|*.xls", DefaultExt = "xls" };
            }

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    exportStream = dialog.OpenFile();
                    dataWorkbook.Save(exportStream);
                    exportStream.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }

        private string GetExcelFileName()
        {
            string isoCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string excelFile = string.Empty;
            string fileExt = ".xls";

            if (ComboBox_ExcelFormat.SelectedIndex == 0)
                fileExt = ".xlsx";

            if (isoCode.ToLower() != "ja")
            {
                excelFile = "test" + fileExt;
            }
            else
            {
                excelFile = "test_ja" + fileExt;
            }

            return excelFile;
        }

        private void BtnSortData_Click(object sender, RoutedEventArgs e)
        {
         
            if (sheetOne == null)
                return;           

            sheetOne.SortSettings.SortType = WorksheetSortType.Rows;
            sheetOne.SortSettings.SetRegion("B1:E78");
            sheetOne.SortSettings.SortConditions.Clear();                     
            sheetOne.SortSettings.SortConditions.Add(new RelativeIndex(0), new OrderedSortCondition(sortDirection));

            if (sortDirection == SortDirection.Descending)
                sortDirection = SortDirection.Ascending;
            else
                sortDirection = SortDirection.Descending;

            LoadProductCollection();

        }

        private void LoadProductCollection()
        {

            ObservableCollection<Product> products = new ObservableCollection<Product>();          

            foreach (WorksheetRow row in sheetOne.Rows)
            {
                Product product = new Product
                {
                    Name = row.Cells[1].Value.ToString(),
                    Category = row.Cells[2].Value.ToString(),
                    Supplier = row.Cells[3].Value.ToString(),
                    OnBackOrder = (bool)row.Cells[4].Value
                };
                products.Add(product);
            }
                      
            this.DataContext = products;
        }       
    }
}
