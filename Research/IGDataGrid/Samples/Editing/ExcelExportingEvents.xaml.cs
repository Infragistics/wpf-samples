using System;
using System.Diagnostics;
using System.Windows;
using IGDataGrid.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter.ExcelExporter;

namespace IGDataGrid.Samples.Editing
{
    /// <summary>
    /// Interaction logic for ExcelExportingEvents.xaml
    /// </summary>
    public partial class ExcelExportingEvents : SampleContainer
    {
        // Member Variables
        private ExportOptions _exportOptions;
        private string _fileName;

        public ExcelExportingEvents()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ExcelExportingEvents_Loaded);
        }

        #region Properties
        public ExportOptions ExportOptions
        {
            get
            {
                if (this._exportOptions == null)
                    this._exportOptions = new ExportOptions();

                return this._exportOptions;
            }
        }
        #endregion Properties

        #region Event Handlers

        #region Page
        private void ExcelExportingEvents_Loaded(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.OrderDetails).DefaultView;

            this.XamDataGrid1.Records.ExpandAll(true);

            // Establish the file name to use for the export.
            _fileName = IGDataGrid.Utils.Utils.GetTempFileName(".xlsx");
        }
        #endregion Page

        #region Export Button
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            // Clear the list of events fired during the previous export (if any).
            this.lbEvents.Items.Clear();

            if (string.IsNullOrEmpty(_fileName))
            {
                MessageBox.Show(DataGridStrings.Export_Message_FileError_Text,
                                DataGridStrings.Export_Message_FileError_Caption,
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);

                return;
            }

            try
            {
                // Get the instance of the DataPresenterExcelExporter we defined in the Page's resources and call its Export
                // method passing in the XamDataGrid we want to export, the name of the file we would like
                // to export to and the format of the Workbook we want to Exporter to create.
                if (cbExportSynchronous.IsChecked.HasValue && cbExportSynchronous.IsChecked.Value)
                {
                    // NOTE: there are 12 overloads to the Export method that give you control over different
                    // aspects of the exporting process.
                    DataPresenterExcelExporter exporter = (DataPresenterExcelExporter)this.Resources["Exporter"];
                    exporter.Export(this.XamDataGrid1, _fileName, WorkbookFormat.Excel2007, this.ExportOptions);
                }
                else if (cbExportAsynchronous.IsChecked.HasValue && cbExportAsynchronous.IsChecked.Value)
                {
                    // NOTE: there are 10 overloads to the ExportAsync method that give you control over different
                    // aspects of the asynchronous exporting process.
                    DataPresenterExcelExporter exporter = (DataPresenterExcelExporter)this.Resources["Exporter"];
                    exporter.ExportAsync(this.XamDataGrid1, _fileName, WorkbookFormat.Excel2007, this.ExportOptions);
                }
            }
            catch (Exception ex)
            {
                // It's possible that the exporter does not have permission to write the file, so it's generally
                // a good idea to account for this possibility.
                MessageBox.Show(string.Format(DataGridStrings.Export_Message_ExcelExportError_Text, ex.Message),
                                DataGridStrings.Export_Message_ExcelError_Caption,
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);

                return;
            }
        }
        #endregion Export Button

        #region Excel Exporter

        private void DataPresenterExcelExporter_ExportStarted(object sender, ExportStartedEventArgs e)
        {
            this.lbEvents.Items.Add("ExportStarted");
        }

        private void DataPresenterExcelExporter_CellExported(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.CellExportedEventArgs e)
        {
            this.lbEvents.Items.Add("CellExported");
        }

        private void DataPresenterExcelExporter_CellExporting(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.CellExportingEventArgs e)
        {
            this.lbEvents.Items.Add("CellExporting");
        }

        private void DataPresenterExcelExporter_HeaderAreaExported(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.HeaderAreaExportedEventArgs e)
        {
            this.lbEvents.Items.Add("HeaderAreaExported");
        }

        private void DataPresenterExcelExporter_HeaderAreaExporting(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.HeaderAreaExportingEventArgs e)
        {
            this.lbEvents.Items.Add("HeaderAreaExporting");
        }

        private void DataPresenterExcelExporter_HeaderLabelExported(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.HeaderLabelExportedEventArgs e)
        {
            this.lbEvents.Items.Add("HeaderLabelExported");
        }

        private void DataPresenterExcelExporter_HeaderLabelExporting(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.HeaderLabelExportingEventArgs e)
        {
            this.lbEvents.Items.Add("HeaderLabelExporting");
        }

        private void DataPresenterExcelExporter_InitializeRecord(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.InitializeRecordEventArgs e)
        {
            this.lbEvents.Items.Add("InitializeRecord");
        }

        private void DataPresenterExcelExporter_RecordExported(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.RecordExportedEventArgs e)
        {
            this.lbEvents.Items.Add("RecordExported");
        }

        private void DataPresenterExcelExporter_RecordExporting(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.RecordExportingEventArgs e)
        {
            this.lbEvents.Items.Add("RecordExporting");
        }

        private void DataPresenterExcelExporter_SummaryAreaExported(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.SummaryRowExportedEventArgs e)
        {
            this.lbEvents.Items.Add("SummaryAreaExported");
        }

        private void DataPresenterExcelExporter_SummaryAreaExporting(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.SummaryRowExportingEventArgs e)
        {
            this.lbEvents.Items.Add("SummaryAreaExporting");
        }

        private void DataPresenterExcelExporter_SummaryCellExported(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.SummaryCellExportedEventArgs e)
        {
            this.lbEvents.Items.Add("SummaryCellExported");
        }

        private void DataPresenterExcelExporter_SummaryCellExporting(object sender, Infragistics.Windows.DataPresenter.ExcelExporter.SummaryCellExportingEventArgs e)
        {
            this.lbEvents.Items.Add("SummaryCellExporting");
        }

        private void DataPresenterExcelExporter_ExportEnded(object sender, ExportEndedEventArgs e)
        {
            // Display an 'Export Completed' message.
            MessageBox.Show(DataGridStrings.Export_Message_ExportCompleted_Text,
                            DataGridStrings.Export_Message_ExportCompleted_Caption,
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            // Execute Excel to display the exported workbook.
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = _fileName;
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(DataGridStrings.Export_Message_ExcelError_Text, ex.Message),
                                DataGridStrings.Export_Message_ExcelError_Caption,
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }

            this.lbEvents.Items.Add("ExportEnded");
        }

        #endregion Event Handlers

        #endregion Excel Exporter
    }
}
