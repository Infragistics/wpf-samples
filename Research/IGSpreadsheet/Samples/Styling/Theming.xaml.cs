using IGSpreadsheet.Samples.Shared;
using IGSpreadsheet.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace IGSpreadsheet.Samples.Style
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent(); 
            InitializeData();
            Utils.SetDefaultDialogTitle(Spreadsheet);
        }
  
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme  
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }

        private void InitializeData()
        {
            Stream stream = Tools.GetLocalizedFileAsStream("Sample1.xlsx");
            Workbook wb = Workbook.Load(stream);
            if (wb != null)
            {
                this.Spreadsheet.Workbook = wb;
            }
        }
    }
}
