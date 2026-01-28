using IGSpreadsheet.Samples.Shared;
using IGSpreadsheet.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Ribbon;
using System.IO;
using System.Windows;

namespace IGSpreadsheet.Samples.Editing
{
    public partial class EditingProtection : SampleContainer
    {
        public EditingProtection()
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

        private void SetAllowDeletingColumns(object sender, RoutedEventArgs e)
        {
            CheckBoxTool cbt = (CheckBoxTool)sender;
            if (cbt.IsChecked ?? false)
            {
                this.xamSpreadsheet1.ActiveWorksheet.Unprotect();
                this.xamSpreadsheet1.ActiveWorksheet.Protect(true);
            }
            else
            {
                this.xamSpreadsheet1.ActiveWorksheet.Unprotect();
                this.xamSpreadsheet1.ActiveWorksheet.Protect(false);
            }
        }

        private void SetAllowDeletingRows(object sender, RoutedEventArgs e)
        {
            CheckBoxTool cbt = (CheckBoxTool)sender;
            if (cbt.IsChecked ?? false)
            {
                this.xamSpreadsheet1.ActiveWorksheet.Unprotect();
                this.xamSpreadsheet1.ActiveWorksheet.Protect((bool?)null, true);
            }
            else
            {
                this.xamSpreadsheet1.ActiveWorksheet.Unprotect();
                this.xamSpreadsheet1.ActiveWorksheet.Protect((bool?)null, false);
            }
        }

        private void SetAllowInsertingColumns(object sender, RoutedEventArgs e)
        {
            CheckBoxTool cbt = (CheckBoxTool)sender;
            if (cbt.IsChecked ?? false)
            {
                this.xamSpreadsheet1.ActiveWorksheet.Unprotect();
                this.xamSpreadsheet1.ActiveWorksheet.Protect((bool?)null, null, null, null, null, null, null, null, true);
            }
            else
            {
                this.xamSpreadsheet1.ActiveWorksheet.Unprotect();
                this.xamSpreadsheet1.ActiveWorksheet.Protect((bool?)null, null, null, null, null, null, null, null, false);
            }
        }

        private void SetAllowInsertingRows(object sender, RoutedEventArgs e)
        {
            CheckBoxTool cbt = (CheckBoxTool)sender;
            if (cbt.IsChecked ?? false)
            {
                this.xamSpreadsheet1.ActiveWorksheet.Unprotect();
                this.xamSpreadsheet1.ActiveWorksheet.Protect((bool?)null, null, null, null, null, null, null, null, null, null, true);
            }
            else
            {
                this.xamSpreadsheet1.ActiveWorksheet.Unprotect();
                this.xamSpreadsheet1.ActiveWorksheet.Protect((bool?)null, null, null, null, null, null, null, null, null, null, false);
            }
        }
    }
}
