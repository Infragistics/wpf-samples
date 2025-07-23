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
using IGSpreadsheet.Samples.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;

namespace IGSpreadsheet.Samples.Display
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ConfiguringCustomSortDialog : SampleContainer
    {
        #region Constructor
        public ConfiguringCustomSortDialog()
        {
            InitializeComponent();
            //Load Excel file.
            LoadFile();
  
            // overriding default theme
                 
        }
        #endregion

        #region Load Workbook
        private void LoadFile()
        {
            Stream stream = Tools.GetLocalizedFileAsStream("CustomSort.xlsx");
            Workbook xamWorkbook = Workbook.Load(stream);
            xamWorkbook.CalculationMode = CalculationMode.Manual;
            if (xamWorkbook != null)
            {
                this.xamSpreadsheet1.Workbook = xamWorkbook;
            }
        }
        #endregion
    }
}
