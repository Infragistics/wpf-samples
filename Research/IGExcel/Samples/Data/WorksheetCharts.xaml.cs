using IGExcel.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Documents.Excel.Charts;
using Infragistics.Samples.Framework;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGExcel.Samples.Data
{
    /// <summary>
    /// Interaction logic for WorksheetCharts.xaml
    /// </summary>
    public partial class WorksheetCharts : SampleContainer
    {
        public DataTable Data { get; set; }
        public DataTable ChartData { get; set; }

        Random r = new Random();
        string[] monthColumns = "Jan;Feb;Mar;Apr;May;Jun;Jul;Aug;Sep;Oct;Nov;Dec".Split(';');
        public WorksheetCharts()
        {
            InitializeComponent();

            this.UseDefaultTheme = true;

            CreateDataSource();
            this.DataContext = this;
        }

        #region Data Sources
        private void CreateDataSource()
        {
            Data = new DataTable();
            Data.Columns.Add("Expenses", typeof(string));

            for (int i = 0; i < monthColumns.Length; i++)
            {
                Data.Columns.Add(monthColumns[i], typeof(double));
            }

            for (int i = 1; i < 6; i++)
            {
                DataRow row = Data.NewRow();

                for (int j = 0; j < Data.Columns.Count; j++)
                {
                    if (j == 0)
                    {
                        row[j] = "Expense " + i.ToString();
                    }
                    else
                    {
                        row[j] = r.Next(100, 400);
                    }
                }
                Data.Rows.Add(row);
            }

            CreateChartData();

        }

        private void CreateChartData()
        {
            ChartData = new DataTable();

            ChartData.Columns.Add("Month", typeof(string));
            for (int i = 0; i < Data.Rows.Count; i++)
            {
                ChartData.Columns.Add(Data.Rows[i][0].ToString(), typeof(double));
            }

            for (int i = 1; i < Data.Columns.Count; i++)
            {
                DataRow row = ChartData.NewRow();
                row[0] = Data.Columns[i].ColumnName;
                ChartData.Rows.Add(row);
            }

            for (int i = 0; i < ChartData.Rows.Count; i++)
            {
                DataRow row = ChartData.Rows[i];

                for (int j = 1; j < ChartData.Columns.Count; j++)
                {
                    row[j] = Data.Rows[j - 1][i + 1];
                }
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Workbook workbook = new Workbook(WorkbookFormat.Excel2007);

            Worksheet sheet = workbook.Worksheets.Add("Sheet1");
            sheet.DefaultColumnWidth = (200 * 20);

            for (int i = 0; i < Data.Columns.Count; i++)
            {
                WorksheetRow sheetRow = sheet.Rows[2];
                sheetRow.Cells[i].Value = Data.Columns[i].ColumnName;
                sheetRow.Cells[i].CellFormat.Alignment = HorizontalCellAlignment.Center;
            }

            for (int i = 0; i < Data.Rows.Count; i++)
            {
                WorksheetRow sheetRow = sheet.Rows[i + 3];

                for (int j = 0; j < Data.Columns.Count; j++)
                {
                    WorksheetCell sheetCell = sheetRow.Cells[j];
                    sheetCell.CellFormat.Alignment = HorizontalCellAlignment.Center;
                    sheetCell.Value = Data.Rows[i][j];
                }
            } 

            WorksheetRow row = sheet.Rows[0];
            row.Height = 5000;

            WorksheetCell cell = sheet.GetCell("A1");
            WorksheetCell cell2 = sheet.GetCell("M1");

            WorksheetChart chart = sheet.Shapes.AddChart(ChartType.Line, cell, new Point(0, 0), cell2, new Point(100, 100));
            chart.SetSourceData(string.Format("Sheet1!R{0}C{1}:R{2}C{3}", 3, 2, 8, 13), true, CellReferenceMode.R1C1);
            chart.ChartTitle = new ChartTitle() { Text = new FormattedString("EXPENSE TRENDS") };

            if (workbook != null)
            {
                SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook (*.xlsx)|*.xlsx", DefaultExt = ".xlsx" };
                sfd.AddExtension = true;
                if (sfd.ShowDialog() == true)
                {
                    try
                    {
                        try
                        {
                            workbook.Save(sfd.FileName);

                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show(String.Format("{0} {1}", ExcelStrings.NewColorModel_Export_WarningMessage, sfd.FileName));
                    }
                }
            }
        }
    }
}
