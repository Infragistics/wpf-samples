using System;
using System.Diagnostics;
using System.Windows;
using IGDataGrid.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter.ExcelExporter;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for ExcelExportStyling.xaml
    /// </summary>
    public partial class ExcelExportStyling : SampleContainer
    {
        // Member Variables
        private ExportOptions _exportOptions;

        public ExcelExportStyling()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(ExcelExportStyling_Loaded);
        }

        private void ExcelExportStyling_Loaded(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.OrderDetails).DefaultView;

            this.XamDataGrid1.Records.ExpandAll(true);

            this.DataContext = ExportOptions;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            // Establish the file name to use for the export.
            string fileName = IGDataGrid.Utils.Utils.GetTempFileName(".xlsx");
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show(DataGridStrings.ExportToExcel_Message_FileError_Text,
                                DataGridStrings.ExportToExcel_Message_FileError_Caption,
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
                exporter.Export(this.XamDataGrid1, fileName, WorkbookFormat.Excel2007, this.ExportOptions);
            }
            catch (Exception ex)
            {
                // It's possible that the exporter does not have permission to write the file, so it's generally
                // a good idea to account for this possibility.
                MessageBox.Show(string.Format(DataGridStrings.ExportToExcel_Message_ExcelExportError_Text, ex.Message),
                                DataGridStrings.ExportToExcel_Message_ExcelError_Caption,
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);

                return;
            }

            // Display an 'Export Completed' message.
            MessageBox.Show(DataGridStrings.ExportToExcel_Message_ExportCompleted_Text,
                            DataGridStrings.ExportToExcel_Message_ExportCompleted_Caption,
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            // Execute Excel to display the exported workbook.
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = fileName;
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(DataGridStrings.ExportToExcel_Message_ExcelError_Text, ex.Message),
                                DataGridStrings.ExportToExcel_Message_ExcelError_Caption,
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
    }
}
