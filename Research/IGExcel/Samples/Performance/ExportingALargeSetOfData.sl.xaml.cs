using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;

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

            Dispatcher.BeginInvoke(new ThreadStart(() => this.DisplayData(result)));
        }

        private void DisplayData(IEnumerable dataSource)
        {
            // Set ItemsSource to the xamGrid control
            igGrid.ItemsSource = dataSource;
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

            // Freeze the header row
            sheetOne.DisplayOptions.PanesAreFrozen = true;
            sheetOne.DisplayOptions.FrozenPaneSettings.FrozenRows = 1;

            WorksheetRow worksheetRow;
            WorksheetCell worksheetCell;
            Column gridColumn;

            foreach (Row gridRow in igGrid.Rows)
            {
                worksheetRow = sheetOne.Rows[gridRow.Index + 1];
                for (int index = 0; index < igGrid.Columns.Count; index++)
                {
                    worksheetRow.Cells[index].Value = gridRow.Cells[index].Value;

                    // Set header appearance and text 
                    if (gridRow.Index == 0)
                    {
                        worksheetCell = sheetOne.Rows[0].Cells[index];

                        gridColumn = igGrid.Columns[index] as Column;

                        // Set Excel header text to key of column
                        if (gridColumn != null)
                            worksheetCell.Value = gridColumn.HeaderText;
                    }
                }
            }

            // Build Column List
            // Format data alignment and formatting in cells

            sheetOne.DefaultColumnWidth = 5000;

            sheetOne.Columns[0].CellFormat.Alignment = HorizontalCellAlignment.Left;

            sheetOne.Columns[1].CellFormat.Alignment = HorizontalCellAlignment.Right;
            sheetOne.Columns[1].CellFormat.FormatString = "\"$\"#,##0.00;(\"$\"#,##0.00)";

            sheetOne.Columns[2].CellFormat.Alignment = HorizontalCellAlignment.Right;

            sheetOne.Columns[3].CellFormat.Alignment = HorizontalCellAlignment.Right;
            sheetOne.Columns[3].CellFormat.FormatString = "\"$\"#,##0.00;(\"$\"#,##0.00)";

            return dataWorkbook;
        }
    }
}
