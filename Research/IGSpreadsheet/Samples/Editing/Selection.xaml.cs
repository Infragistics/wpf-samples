using IGSpreadsheet.Samples.Shared;
using IGSpreadsheet.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using System.IO;

namespace IGSpreadsheet.Samples.Editing
{
    public partial class Selection : SampleContainer
    {
        public Selection()
        {
            InitializeComponent();
            InitializeSample();
            Utils.SetDefaultDialogTitle(xamSpreadsheet1);
  
            // overriding default theme
                 
        }

        private void InitializeSample()
        {
            Stream stream = Tools.GetLocalizedFileAsStream("Sample1.xlsx");
            Workbook wb = Workbook.Load(stream);
            if (wb != null)
            {
                this.xamSpreadsheet1.Workbook = wb;
            }
        }
    }
}
