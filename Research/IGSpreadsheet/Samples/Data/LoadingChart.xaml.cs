using IGSpreadsheet.Samples.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Documents.Excel.Charts;
using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace IGSpreadsheet.Samples.Data
{
    /// <summary>
    /// Interaction logic for LoadingChart.xaml
    /// </summary>
    public partial class LoadingChart : SampleContainer
    {
       public LoadingChart()
        {
            InitializeComponent();
           
            Stream stream = Tools.GetLocalizedFileAsStream("ChartData.xlsx");
            Workbook workbook = Workbook.Load(stream);

            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = "Sheet1";

            sheet.DefaultColumnWidth =500 * 20;
            sheet.Rows[1].Height = 150 * 20;

            //Get the cells for the four charts that will placed in the Worksheet.
            WorksheetCell cell1 = sheet.GetCell("A2");
            WorksheetCell cell2 = sheet.GetCell("B2");
            WorksheetCell cell3 = sheet.GetCell("C2");
            WorksheetCell cell4 = sheet.GetCell("D2");

            var dataCellAddress = "A4:D6";
            var positionBR = new Point(90, 90);

            ////Create Line chart.
            WorksheetChart chart1 = sheet.Shapes.AddChart(Infragistics.Documents.Excel.Charts.ChartType.Line, cell1, new Point(0, 0), cell1, positionBR);
            chart1.ChartTitle = new ChartTitle() { Text = new FormattedString("Line Chart") };
            chart1.SetSourceData(dataCellAddress, true);

            //Create Column chart.
            WorksheetChart chart2 = sheet.Shapes.AddChart(Infragistics.Documents.Excel.Charts.ChartType.ColumnClustered, cell2, new Point(0, 0), cell2, positionBR);
            chart2.ChartTitle = new ChartTitle() { Text = new FormattedString("Column Chart") };
            chart2.SetSourceData(dataCellAddress, true);

            //Create Area chart.
            WorksheetChart chart3 = sheet.Shapes.AddChart(Infragistics.Documents.Excel.Charts.ChartType.Area, cell3, new Point(0, 0), cell3, positionBR);
            chart3.ChartTitle = new ChartTitle() { Text = new FormattedString("Area Chart") };
            chart3.SetSourceData(dataCellAddress, true);

            //Create Pie chart.
            WorksheetChart chart4 = sheet.Shapes.AddChart(Infragistics.Documents.Excel.Charts.ChartType.Pie, cell4, new Point(0, 0), cell4, positionBR);       
            chart4.SetSourceData(dataCellAddress, false);

            spreadsheet.Workbook = workbook;     
        }
    }
}
