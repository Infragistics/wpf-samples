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

namespace IGSpreadsheet.Samples.Display
{
    /// <summary>
    /// Interaction logic for ComboChart.xaml
    /// </summary>
    public partial class ComboChart : SampleContainer
    {
        public ComboChart()
        {
            InitializeComponent();

            var wb = new Workbook(WorkbookFormat.Excel2007);
            var ws = wb.Worksheets.Add("Sheet1");
            ws.GetCell("A1").Value = "Date";
            ws.GetCell("B1").Value = "Sold Count";
            ws.GetCell("C1").Value = "Average Price";
            ws.GetCell("A2").Value = new DateTime(2019, 1, 1);
            ws.GetCell("B2").Value = 275;
            ws.GetCell("C2").Value = 410;
            ws.GetCell("A3").Value = new DateTime(2019, 2, 1);
            ws.GetCell("B3").Value = 150;
            ws.GetCell("C3").Value = 450;
            ws.GetCell("A4").Value = new DateTime(2019, 3, 1);
            ws.GetCell("B4").Value = 225;
            ws.GetCell("C4").Value = 430;
            ws.GetCell("A5").Value = new DateTime(2019, 4, 1);
            ws.GetCell("B5").Value = 275;
            ws.GetCell("C5").Value = 425;
            ws.GetCell("A6").Value = new DateTime(2019, 5, 1);
            ws.GetCell("B6").Value = 150;
            ws.GetCell("C6").Value = 410;
            ws.GetCell("A7").Value = new DateTime(2019, 6, 1);
            ws.GetCell("B7").Value = 250;
            ws.GetCell("C7").Value = 400;
            var tbl = ws.Tables.Add("A1:C7", true);
            tbl.Columns[0].AreaFormats[WorksheetTableColumnArea.DataArea].FormatString = "mmm yy";

            ws.Shapes.AddChart(ChartType.Combo, ws.GetCell("E1"), new Point(0, 0), ws.GetCell("M12"), new Point(100, 100), (chart) =>
            {
                chart.SetComboChartSourceData("B1:C7", new ChartType[] { ChartType.ColumnClustered, ChartType.Line });
                var axis2 = chart.AxisCollection.Add(AxisType.Value, AxisGroup.Secondary);
                axis2.Position = AxisPosition.Right;
                chart.SeriesCollection[1].AxisGroup = AxisGroup.Secondary;
                
                var legend = new Legend();
                legend.Position = LegendPosition.Right;
                chart.Legend = legend;

                chart.ChartTitle = new ChartTitle();

                chart.SeriesCollection[0].XValues = new XValues(ws, "A2:A7");
                chart.SeriesCollection[1].XValues = new XValues(ws, "A2:A7");
            });

            this.spreadsheet.Workbook = wb;
        }
    }
}
