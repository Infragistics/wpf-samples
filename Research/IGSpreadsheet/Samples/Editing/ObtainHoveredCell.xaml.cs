using IGSpreadsheet.Resources;
using IGSpreadsheet.Samples.Shared;
using IGSpreadsheet.Shared;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using System.IO;
using System.Windows;

namespace IGSpreadsheet.Samples.Editing
{
    public partial class ObtainHoveredCell : SampleContainer
    {
        public ObtainHoveredCell()
        {
            InitializeComponent();
            LoadFile();
            Utils.SetDefaultDialogTitle(xamSpreadsheet1);
  
            // overriding default theme
                 
        }

        private void LoadFile()
        {
            Stream stream = Tools.GetLocalizedFileAsStream("Sample1.xlsx");
            Workbook wb = Workbook.Load(stream);
            if (wb != null)
            {
                this.xamSpreadsheet1.Workbook = wb;
            }
        }

        void xamSpreadsheet1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SpreadsheetHitTestResult result = this.xamSpreadsheet1.HitTest(e.GetPosition(this.xamSpreadsheet1));
            if (result is SpreadsheetHeaderHitTestResult)
            {
                SpreadsheetHeaderHitTestResult headerResult = (SpreadsheetHeaderHitTestResult)result;
                if (headerResult.ScrollRegion is SpreadsheetColumnScrollRegion)
                {
                    this.tb1.Text = SpreadsheetStrings.lblColumn + headerResult.Index;
                }
                else if (headerResult.ScrollRegion is SpreadsheetRowScrollRegion)
                {
                    this.tb1.Text = SpreadsheetStrings.lblRow + headerResult.Index;
                }
                this.tb1.FontWeight = FontWeights.Bold;
            }
            else if (result is SpreadsheetCellHitTestResult)
            {
                SpreadsheetCell sc = ((SpreadsheetCellHitTestResult)result).Cell;
                string addr = sc.ToString().Trim('{', '}');
                WorksheetCell wc = this.xamSpreadsheet1.ActiveWorksheet.Rows[sc.Row].Cells[sc.Column];
                this.tb1.FontWeight = FontWeights.Normal;
                if (wc.GetText().Equals(string.Empty))
                {
                    this.tb1.Text = string.Format(SpreadsheetStrings.lblCell, addr, SpreadsheetStrings.lblEmpty);
                }
                else
                {
                    this.tb1.Text = string.Format(SpreadsheetStrings.lblCell, addr, wc.GetText());
                }
            }
            else
            {
                this.tb1.FontWeight = FontWeights.Normal;
                this.tb1.Text = SpreadsheetStrings.lblOutOfCell;
            }
        }
    }
}
