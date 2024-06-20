using Infragistics.Documents.Excel;
using Infragistics.Documents.Excel.Charts;
using Microsoft.Win32;
using System;
using System.Windows;

namespace CreateBarChart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Workbook workbook = new Workbook(WorkbookFormat.Excel2007);
            Worksheet worksheet = workbook.Worksheets.Add("Sheet1");

            int firstRow = 0;
            int firstColumn = 0;
            int rowCount = 8;
            int colCount = 5;

            for (int r = firstRow; r < firstRow + rowCount; r++)
            {
                int relativeRow = 1 + r - firstRow;

                var row = worksheet.Rows[r];

                for (int c = firstColumn; c < firstColumn + colCount; c++)
                {
                    int relativeCol = 1 + c - firstColumn;
                    object cellValue;
                    if (r == firstRow && c == 0)
                        cellValue = "";
                    else if (r == firstRow)
                        cellValue = "Header " + (relativeCol - 1).ToString();
                    else if (c == firstColumn)
                        cellValue = "Row " + r.ToString();
                    else
                        cellValue = (((double)(relativeRow * 100) + (double)relativeCol) / 100d);

                    row.SetCellValue(c, cellValue);
                }
            }

            var chart = worksheet.Shapes.AddChart(ChartType.BarClustered, new Rect(5000, 100, 10000, 5000), (c) =>
               {
                   c.SetSourceData("A1:E8", true);
                   c.ChartTitle = new ChartTitle() { Text = new FormattedString("My Chart Data") };
                   c.Legend = new Legend() { Position = LegendPosition.Bottom };
               });

            SaveExport(workbook);
        }

        private void SaveExport(Workbook workbook)
        {
            SaveFileDialog dialog = new SaveFileDialog { Filter = "Excel files|*.xlsx", DefaultExt = "xlsx" };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var stream = dialog.OpenFile())
                    {
                        workbook.Save(stream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
