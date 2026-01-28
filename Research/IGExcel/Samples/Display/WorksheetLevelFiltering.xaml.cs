using IGExcel.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Documents.Excel.Filtering;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.ExcelExporter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
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

namespace IGExcel.Samples.Data
{
    /// <summary>
    /// Interaction logic for WorksheetLevelFiltering.xaml
    /// </summary>
    public partial class WorksheetLevelFiltering : SampleContainer
    {
        private ExportOptions _exportOptions;
        private Workbook _workbook;
       
        public WorksheetLevelFiltering()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ExportToExcel_Loaded);
        }

        private void ExportToExcel_Loaded(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.OrderDetails).DefaultView;

            this.XamDataGrid1.Records.ExpandAll(true);
           
        }
    
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
           
            // Establish the file name to use for the export.
            string fileName = IGExcel.Utils.Utils.GetTempFileName(".xlsx");
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show(ExcelStrings.ExportToExcel_Message_FileError_Text,
                                ExcelStrings.ExportToExcel_Message_FileError_Caption,
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                

                return;
            }
           
            try
            {
                // Get the instance of the DataPresenterExcelExporter we defined in the Page's resources and call its Export
                // method passing in the XamDataGrid we want to export, the name of the file we would like
                // to export to and the format of the Workbook we want to Exporter to create.
                //
                // NOTE: there are 11 overloads to the Export method that give you control over different
                // aspects of the exporting process.
                DataPresenterExcelExporter exporter = (DataPresenterExcelExporter)this.Resources["Exporter"];
                //Worksheet ws1 = _workbook.Worksheets.Add("One");
                exporter.Export(this.XamDataGrid1, this.FilteredWorkbook, this.ExportOptions);
                
            }
            catch (Exception ex)
            {
                // It's possible that the exporter does not have permission to write the file, so it's generally
                // a good idea to account for this possibility.
                MessageBox.Show(string.Format(ExcelStrings.ExportToExcel_Message_ExcelExportError_Text, ex.Message),
                                ExcelStrings.ExportToExcel_Message_ExcelError_Caption,
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);

                return;
            }

            // Display an 'Export Completed' message.
            MessageBox.Show(ExcelStrings.ExportToExcel_Message_ExportCompleted_Text,
                            ExcelStrings.ExportToExcel_Message_ExportCompleted_Caption,
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            // Execute Excel to display the exported workbook.
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = fileName;
                //Apply Filter Settings
                ApplyFilterSettings();
                _workbook.Save(fileName);
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(ExcelStrings.ExportToExcel_Message_ExcelError_Text, ex.Message),
                                ExcelStrings.ExportToExcel_Message_ExcelError_Caption,
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }
        }

        public ExportOptions ExportOptions
        {
            get
            {
                if (this._exportOptions == null)
                    this._exportOptions = new ExportOptions();

                return this._exportOptions;
            }
        }

        public Workbook FilteredWorkbook
        {
            get
            {
                if (this._workbook == null)
                    this._workbook = new Workbook(WorkbookFormat.Excel2007);

                return this._workbook;
            }
        }

        private string GetUnusedFileName()
        {
            for (int i = 1; i < 5000; i++)
            {
                string fileName = "DataPresenterExportToExcelTest" + i.ToString() + ".xlsx";
                if (false == System.IO.File.Exists(fileName))
                    return fileName;
            }

            return string.Empty;
        }

        public void ApplyFilterSettings()
        {
            //Add a custom filter based on the value of OrderID.
            Worksheet _worksheet = _workbook.Worksheets[0];
            WorksheetFilterSettings filterSettings = _worksheet.FilterSettings;
            filterSettings.SetRegion("A2:E2156");
            WorksheetRegion region = filterSettings.Region;
            filterSettings.ApplyCustomFilter(0, new CustomFilterCondition(ExcelComparisonOperator.Equals, 10256));
           
        }

    }
}
