using IGSpreadsheet.Resources;
using IGSpreadsheet.Samples.Shared;
using IGSpreadsheet.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace IGSpreadsheet.Samples.Data
{
    public partial class DefiningData : SampleContainer
    {
        private double[,] percentsFor2013 = new double[12, 5] {
            { 14.3, 30.2, 48.4, 4.2, 1.9 },
            { 13.5, 29.6, 50.0, 4.1, 1.8 },
            { 13.0, 28.5, 51.7, 4.1, 1.8 },
            { 12.7, 27.9, 52.7, 4.0, 1.7 },
            { 12.6, 27.7, 52.9, 4.0, 1.6 },
            { 12.0, 28.9, 52.1, 3.9, 1.7 },
            { 11.8, 28.9, 52.8, 3.6, 1.6 },
            { 11.8, 28.2, 52.9, 3.9, 1.8 },
            { 12.1, 27.8, 53.2, 3.9, 1.7 },
            { 11.7, 27.2, 54.1, 3.8, 1.7 },
            { 10.5, 26.8, 54.8, 4.0, 1.8 },
            {  9.0, 26.8, 55.8, 3.8, 1.9 } };

        private double[,] percentsFor2014 = new double[8, 5] {
            { 22.85, 18.90, 43.67,  9.73, 1.30 },
            { 22.49, 19.21, 43.89,  9.74, 1.34 },
            { 22.58, 18.75, 43.66,  9.91, 1.38 },
            { 21.43, 18.62, 45.22,  9.79, 1.39 },
            { 20.79, 18.74, 45.60, 10.01, 1.34 },
            { 20.98, 17.95, 45.46, 10.30, 1.37 },
            { 21.38, 17.52, 45.28, 10.60, 1.39 },
            { 20.31, 17.50, 46.26, 10.81, 1.47 } };

        public DefiningData()
        {
            InitializeComponent();
            InitializeSample();
            Utils.SetDefaultDialogTitle(this.xamSpreadsheet1);
        }

        private void InitializeSample()
        {
            // create a workbook and a new worksheet in it
            Workbook wb = new Workbook();
            WorksheetCollection wss = wb.Worksheets;

            // create the first worksheet (for year 2013)
            Worksheet ws = wss.Add("2013");
            CreateWorksheetContent(ws, SpreadsheetStrings.tableTitle2013, percentsFor2013);

            // create the second worksheet (for year 2014)
            ws = wss.Add("2014");
            CreateWorksheetContent(ws, SpreadsheetStrings.tableTitle2014, percentsFor2014);

            // create hyperlinks in both worksheets for linking each other
            CreateHyperlink(wss[0], SpreadsheetStrings.seeUsage + "2014", wss[1].GetCell("A1"));
            CreateHyperlink(wss[1], SpreadsheetStrings.seeUsage + "2013", wss[0].GetCell("A1"));

            this.xamSpreadsheet1.Workbook = wb;
  
            // overriding default theme
                 
        }

        private void CreateWorksheetContent(Worksheet ws, string wsTitle, double[,] percents)
        {
            // create a merged cell and add content in it
            ws.MergedCellsRegions.Add(1, 1, 1, 6);
            ws.Rows[1].Cells[1].Value = wsTitle;

            // add an image to the worksheet
            // create a stream to the image
            Stream stream = Tools.GetLocalizedFileAsStream("browsers.jpg");
            // create an image from the stream
            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
            Graphics gfx = Graphics.FromImage(img);
            // create a worksheet image object
            WorksheetImage wsi = new WorksheetImage(img);
            // set image object bounds
            wsi.SetBoundsInTwips(ws,
                new Rect(Tools.PixelsToTwips(70),
                Tools.PixelsToTwips(50),
                Tools.PixelsToTwips(img.Width / 2),
                Tools.PixelsToTwips(img.Height / 2)));
            // set image border
            wsi.Outline = ShapeOutline.FromColor(Colors.Black);
            // add the image to the shapes collection of the worksheet
            ws.Shapes.Add(wsi);

            // create the fill pattern for the table header
            CellFillPattern cfpHeader = CellFill.CreateSolidFill(Colors.LightBlue);

            // create the fill pattern for the table rows
            CellFillPattern cfpRow = CellFillPattern.CreateSolidFill(Colors.White);

            // create the fill pattern for the table alternate rows
            CellFillPattern cfpRowAlternate = CellFillPattern.CreateSolidFill(Colors.AliceBlue);

            // set table data starting row and column
            int startX = 2;
            int startY = 8;
            int browsersCount = 5;
            WorksheetHyperlink link;

            // create table headers
            for (int x = startX - 1; x < startX + browsersCount; x++)
            {
                // set bigger column width on the first two columns
                if (x == startX - 1) ws.Columns[x].Width = 3000;
                if (x == startX) ws.Columns[x].Width = 4500;
                // set header background color for all header cells
                ws.Rows[startY - 1].Cells[x].CellFormat.Fill = cfpHeader;
                // add columns header text
                if (x != startX - 1)
                {
                    // create the hyperlinks
                    link = new WorksheetHyperlink(
                        ws.Rows[startY - 1].Cells[x], // position the hyperlink in the correct cell
                        SpreadsheetStrings.ResourceManager.GetString("lblBrowser" + (x - 1) + "Link"),
                        SpreadsheetStrings.ResourceManager.GetString("lblBrowser" + (x - 1) + "Name")
                    );
                    ws.Hyperlinks.Add(link); // add the hyperlink in the worksheet's "Hyperlinks" collection
                    ws.Columns[x].CellFormat.FormatString = "0.0";
                    ws.Columns[x].CellFormat.FormatOptions = WorksheetCellFormatOptions.ApplyNumberFormatting;
                }
            }

            // create table data
            for (int y = startY; y < startY + percents.GetLength(0); y++)
            {
                for (int x = startX - 1; x < startX + browsersCount; x++)
                {
                    if (x == startX - 1)
                    {
                        // first column - add month name
                        ws.Rows[y].Cells[x].Value = SpreadsheetStrings.ResourceManager.GetString("lblMonth" + (y - startY + 1));
                    }
                    else
                    {
                        // not first column - add percents
                        ws.Rows[y].Cells[x].Value = percents[y - startY, x - startX];
                    }
                    // draw row background colors
                    if (y % 2 == 0)
                    {
                        ws.Rows[y].Cells[x].CellFormat.Fill = cfpRowAlternate;
                    }
                    else
                    {
                        ws.Rows[y].Cells[x].CellFormat.Fill = cfpRow;
                    }
                }
            }
        }

        private void CreateHyperlink(Worksheet ws, string DisplayText, object target)
        {
            // create a hyperlink
            WorksheetHyperlink link = new WorksheetHyperlink(
                ws.Rows[21].Cells[1], // set hyperlink location using WorksheetCell object
                target,               // set hyperlink target
                DisplayText);         // set hyperlink display text
            // create a merged cell region for the hyperlink
            ws.MergedCellsRegions.Add(21, 1, 21, 6);
            // add the hyperlink in the worksheet's "Hyperlinks" collection
            ws.Hyperlinks.Add(link);
        }
    }
}
