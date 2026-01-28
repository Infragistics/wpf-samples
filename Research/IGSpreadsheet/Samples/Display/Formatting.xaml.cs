using System;
using System.Collections.Generic;
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
using Infragistics.Samples.Framework;
using System.IO;
using IGSpreadsheet.Samples.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Controls.Grids;

namespace IGSpreadsheet.Samples.Display
{
    /// <summary>
    /// Interaction logic for Formatting.xaml
    /// </summary>
    public partial class Formatting : SampleContainer
    {
        public Formatting()
        {
            InitializeComponent();
            InitializeSample();
  
            // overriding default theme
                 
        }

        private void InitializeSample()
        {
            Stream stream = Tools.GetLocalizedFileAsStream("Sample1.xlsx");
            Workbook wb = Workbook.Load(stream);
            if (wb != null)
            {
                xamSpreadsheet1.Workbook = wb;
            }
            xamSpreadsheet1.ActiveSelection.AddCellRange(
  new SpreadsheetCellRange(9, 5, 15, 7)
);
        }
    }
}
