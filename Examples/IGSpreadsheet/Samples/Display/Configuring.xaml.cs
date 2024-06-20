using IGSpreadsheet.Samples.Shared;
using IGSpreadsheet.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Ribbon;
using System.IO;
using System.Windows;

namespace IGSpreadsheet.Samples.Display
{
    public partial class Configuring : SampleContainer
    {
        public Configuring()
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

        private void ButtonTool_Click(object sender, RoutedEventArgs e)
        {
            ButtonTool bt = sender as ButtonTool;
            if (bt != null)
            {
                switch (bt.Tag.ToString())
                {
                    case "Reload":
                        LoadFile();
                        break;
                }
            }
        }
    }
}
