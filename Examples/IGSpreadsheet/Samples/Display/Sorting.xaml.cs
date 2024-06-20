using IGSpreadsheet.Samples.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Documents.Excel.Sorting;
using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;


namespace IGSpreadsheet.Samples.Display
{
    /// <summary>
    /// Interaction logic for Sorting.xaml
    /// </summary>
    public partial class Sorting : SampleContainer
    {
        public Sorting()
        {
            InitializeComponent();
           
            ComboBox_Sorting.ItemsSource = Enum.GetValues(typeof(SortDirection));
            ComboBox_Sorting.SelectedIndex = 0;
  
            // overriding default theme
                                       
        }

       private void ComboBox_Sorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stream stream = Tools.GetLocalizedFileAsStream("Sample1.xlsx");
            Workbook wb = Workbook.Load(stream);
            if (wb != null)
            {
                this.xamSpreadsheet1.Workbook = wb;
            }

            var Worksheet = wb.Worksheets[0];
            Worksheet.SortSettings.SortType = Infragistics.Documents.Excel.WorksheetSortType.Rows;
            Worksheet.SortSettings.CaseSensitive = true;
            Worksheet.SortSettings.SetRegion("B8:I15");
            Worksheet.SortSettings.SortConditions.Add(new RelativeIndex(0), new OrderedSortCondition((SortDirection)ComboBox_Sorting.SelectedValue));
        }
    }
}
