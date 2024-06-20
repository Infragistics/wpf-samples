using IGSpreadsheet.Samples.Shared;
using Infragistics.Documents.Excel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGSpreadsheet.Samples.Display
{
    /// <summary>
    /// Interaction logic for ConditionalFormats.xaml
    /// </summary>
    public partial class ConditionalFormats : SampleContainer
    {
        public ConditionalFormats()
        {
            InitializeComponent();
        }

        private void SampleContainer_Loaded(object sender, RoutedEventArgs e)
        {
            Stream stream = Tools.GetLocalizedFileAsStream("ConditionalFormatData.xlsx");
            Workbook wb = Workbook.Load(stream);
            if (wb != null)
            {
                this.spreadsheet1.Workbook = wb;
            }
  
            // overriding default theme
                 
        }
    }
}
