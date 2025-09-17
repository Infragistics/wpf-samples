using IGSpreadsheet.Samples.Shared;
using IGSpreadsheet.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using System.IO;

namespace IGSpreadsheet.Samples.Data
{
    public partial class LoadingData : SampleContainer
    {
        public LoadingData()
        {
            InitializeComponent();
            InitializeSample();
            Utils.SetDefaultDialogTitle(this.xamSpreadsheet1);
        }

        private void InitializeSample()
        {
            Stream stream = Tools.GetLocalizedFileAsStream("Sample2.xlsx");
            Workbook wb = Workbook.Load(stream);
            if (wb != null)
            {
                this.xamSpreadsheet1.Workbook = wb;
            }
  
            // overriding default theme
                 
        }
    }
}
