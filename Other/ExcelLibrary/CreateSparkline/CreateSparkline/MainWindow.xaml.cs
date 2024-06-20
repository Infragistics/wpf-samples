using Infragistics.Documents.Excel;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows;

namespace CreateSparkline
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
            Worksheet worksheet = workbook.Worksheets.Add("Sparkline Demo");

            var worksheetRow = worksheet.Rows[1]; //row 2
            worksheetRow.SetCellValue(0, 5); //a2
            worksheetRow.SetCellValue(1, -5); //b2
            worksheetRow.SetCellValue(2, 10); //c2
            worksheetRow.SetCellValue(3, -10); //d2
            worksheetRow.SetCellValue(4, 15); //e2
            worksheetRow.SetCellValue(5, -15); //f2

            var sparkline = worksheet.SparklineGroups.Add(SparklineType.Line, "G2", "A2:F2");

            //series color
            sparkline.ColorSeries = new WorkbookColorInfo(Color.Black);

            //high point
            sparkline.HighPoint = true;
            sparkline.ColorHighPoint = new WorkbookColorInfo(Color.Green);

            //low point
            sparkline.LowPoint = true;
            sparkline.ColorLowPoint = new WorkbookColorInfo(Color.Red);

            //negative points
            sparkline.NegativePoints = true;
            sparkline.ColorNegativePoints = new WorkbookColorInfo(Color.LightGray);

            //first point
            sparkline.FirstPoint = true;
            sparkline.ColorFirstPoint = new WorkbookColorInfo(Color.HotPink);

            //last point
            sparkline.LastPoint = true;
            sparkline.ColorLastPoint = new WorkbookColorInfo(Color.Blue);

            //markers
            sparkline.Markers = true;
            sparkline.ColorMarkers = new WorkbookColorInfo(Color.Orange);

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
