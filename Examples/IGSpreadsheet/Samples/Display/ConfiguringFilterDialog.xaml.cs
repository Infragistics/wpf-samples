using IGSpreadsheet.Samples.Shared;
using IGSpreadsheet.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Documents.Excel.Filtering;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Ribbon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IGSpreadsheet.Samples.Filtering
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ConfiguringFilterDialog : SampleContainer
    {
        #region Private Members         
        private WorksheetTable xamTable;
        WorksheetRegion tableRegion;
        #endregion

        #region Constructor
        public ConfiguringFilterDialog()
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
            Stream stream = Tools.GetLocalizedFileAsStream("Sample3.xlsx");
            Workbook xamWorkbook = Workbook.Load(stream);
            if (xamWorkbook != null)
            {
                this.xamSpreadsheet1.Workbook = xamWorkbook;
            }

            SetCurrentWorksheetInfo();
            PopulateCombos();
            
        }
        #endregion
   
        #region Populate dropdown combo's in UI
        private void PopulateCombos()
        {
            foreach (WorksheetTableColumn item in xamTable.Columns)
            {
                cboColumn.Items.Add(new ComboBoxItem { Content = item.Name });
            }

            foreach (WorksheetCell item in tableRegion.Worksheet.Rows[1].Cells)
            {
                cboRegion.Items.Add(new ComboBoxItem { Content = item.Value });
            }
            cboColumn.SelectedIndex = 0;
            cboRegion.SelectedIndex = 0;
        }
        #endregion
      
        #region SetCurrentWorksheetInfo
        private void SetCurrentWorksheetInfo()
        {
            Worksheet xamWorksheet = this.xamSpreadsheet1.SelectedWorksheets[0];
            WorksheetFilterSettings xamWorksheetFilterSettings = xamWorksheet.FilterSettings;
            
            this.xamTable = this.xamSpreadsheet1.SelectedWorksheets[0].Tables[0];
            tableRegion = this.xamTable.WholeTableRegion;

        }

        #endregion // SetCurrentWorksheetInfo
        
        #region ShowDialog_Click
        private void ButtonTool_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button bt = sender as System.Windows.Controls.Button;
            if (bt != null)
            {
                switch (bt.Uid.ToString())
                {
                    case "loadDialogForWorksheet":
                        this.xamSpreadsheet1.ShowFilterDialogForWorksheet(tableRegion.Worksheet.Columns[cboRegion.SelectedIndex].Index, Infragistics.Controls.Grids.SpreadsheetFilterDialogOption.Custom);
                        break;
                    case "loadDialogForTable":
                        if (this.xamSpreadsheet1.ActiveWorksheet.Tables.Count > 0)
                        {                           
                            this.xamSpreadsheet1.ShowFilterDialogForTable(xamTable.Columns[cboColumn.SelectedIndex], Infragistics.Controls.Grids.SpreadsheetFilterDialogOption.Custom);
                        }
                        
                        break;
                }
            }

        }
        #endregion
    }
}
