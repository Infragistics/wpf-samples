using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;
using Microsoft.Win32;

namespace IGExcel.Samples.Performance
{
    /// <summary>
    /// Interaction logic for ExportingALargeSetOfData.xaml
    /// </summary>
    public partial class ExportingALargeSetOfData : SampleContainer
    {
        public ExportingALargeSetOfData()
        {
            InitializeComponent();
        }

        private void Sample_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundWorker workerThread = new BackgroundWorker();
            workerThread.DoWork += new DoWorkEventHandler(workerThread_DoWork);
            workerThread.RunWorkerAsync();
        }

        private void workerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create a large set of sample data
            // In this case, we will generate 10 000 records
            int i = 0;
            var result = (from d in Enumerable.Range(0, 10000)
                          select new OrderViewModel
                          {
                              Index = ++i,
                              OrderNumber = string.Format("{0:185000#}", d),
                              SalesPrice = RandomizeUtility.GenerateRandomDouble(10, 300),
                              Quantity = RandomizeUtility.GenerateRandomNumber(1, 10)
                          }).ToList();

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() => this.DisplayData(result)));
        }

        private void DisplayData(IEnumerable dataSource)
        {
            dataGrid.ItemsSource = dataSource;
        }

        private void btnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Excel files|*.xlsx", DefaultExt = "xlsx" };

            if (saveFileDialog.ShowDialog() == true)
            {
                Workbook dataWorkbook = CreateDataBook();
                Stream exportStream;
                try
                {
                    exportStream = saveFileDialog.OpenFile();
                    dataWorkbook.Save(exportStream);
                    exportStream.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }

        private Workbook CreateDataBook()
        {
            // Create a Workbook object
            Workbook dataWorkbook = new Workbook();
            dataWorkbook.SetCurrentFormat(WorkbookFormat.Excel2007);

            // Create a Worksheet object that contains the actual data in the workbook
            Worksheet sheetOne = dataWorkbook.Worksheets.Add("Data Sheet");

            // Print column header text 
            int currentColumn = 0;
            foreach (DataGridColumn column in this.dataGrid.Columns)
            {
                this.SetCellValue(sheetOne.Rows[0].Cells[currentColumn], column.Header);
                currentColumn++;
            }

            int currentRow = 1;
            WorksheetRow worksheetRow;

            IEnumerable gridData = dataGrid.ItemsSource;

            foreach (OrderViewModel order in gridData)
            {
                int currentCell = 0;
                worksheetRow = sheetOne.Rows[currentRow];

                this.SetCellValue(worksheetRow.Cells[currentCell], order.Index);
                this.SetCellValue(worksheetRow.Cells[++currentCell], order.OrderNumber);
                this.SetCellValue(worksheetRow.Cells[++currentCell], order.SalesPrice);
                this.SetCellValue(worksheetRow.Cells[++currentCell], order.Quantity);
                this.SetCellValue(worksheetRow.Cells[++currentCell], order.Total);

                currentRow++;
            }

            this.SetColumnFormatting(sheetOne);

            return dataWorkbook;
        }

        private void SetCellValue(WorksheetCell cell, object value)
        {
            cell.Value = value;
        }

        private void SetColumnFormatting(Worksheet worksheet)
        {
            // Freeze the header row
            worksheet.DisplayOptions.PanesAreFrozen = true;
            worksheet.DisplayOptions.FrozenPaneSettings.FrozenRows = 1;

            // Build Column List
            // Format data alignment and formatting in cells
            worksheet.Columns[0].Width = 2000;
            worksheet.Columns[1].Width = 4000;
            worksheet.Columns[2].Width = 4000;
            worksheet.Columns[3].Width = 3000;
            worksheet.Columns[4].Width = 4000;


            worksheet.Columns[0].CellFormat.Alignment = HorizontalCellAlignment.Left;

            worksheet.Columns[2].CellFormat.Alignment = HorizontalCellAlignment.Right;
            worksheet.Columns[2].CellFormat.FormatString = "\"$\"#,##0.00;(\"$\"#,##0.00)";

            worksheet.Columns[3].CellFormat.Alignment = HorizontalCellAlignment.Right;

            worksheet.Columns[4].CellFormat.Alignment = HorizontalCellAlignment.Right;
            worksheet.Columns[4].CellFormat.FormatString = "\"$\"#,##0.00;(\"$\"#,##0.00)";
        }
    }
}
