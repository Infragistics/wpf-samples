using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter.WordWriter;

namespace IGDataGrid.Samples.Editing
{
    /// <summary>
    /// Interaction logic for WordExportingEvents.xaml
    /// </summary>
    public partial class WordExportingEvents : SampleContainer
    {
        private string _fileName;

        public WordExportingEvents()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(WordExportEvents_Samp_Loaded);
        }

        #region Initialization

        void WordExportEvents_Samp_Loaded(object sender, RoutedEventArgs e)
        {
            this.xamDataGrid.DataSource = NWindDataSource.GetTable(NWindTable.OrderDetails).DefaultView;
            this.xamDataGrid.Records.ExpandAll(true);

            // Set a file name to use for the export.
            _fileName = System.IO.Path.GetTempPath() + "WordExportedTempFile" + ".docx";
        }

        #endregion Initialization

        #region Export

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            // Clear the list of events fired during the previous export (if any).
            this.lbEvents.Items.Clear();

            if (string.IsNullOrEmpty(_fileName))
            {
                MessageBox.Show(DataGridStrings.ExportToWord_Message_FileError_Text,
                                DataGridStrings.ExportToWord_Message_FileError_Caption,
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                return;
            }

            try
            {
                // Get the instance of the DataPresenterWordWriter we defined in the Page's resources and call its asynchronous Export
                // method passing in the XamDataGrid we want to export, the name of the file we would like
                // to export to.
                if (cbExportSynchronous.IsChecked.HasValue && cbExportSynchronous.IsChecked.Value)
                {
                    DataPresenterWordWriter exporter = (DataPresenterWordWriter)this.Resources["wordWriter"];
                    exporter.Export(xamDataGrid, _fileName);
                }
                else if (cbExportAsynchronous.IsChecked.HasValue && cbExportAsynchronous.IsChecked.Value)
                {
                    DataPresenterWordWriter exporter = (DataPresenterWordWriter)this.Resources["wordWriter"];
                    exporter.ExportAsync(xamDataGrid, _fileName);
                }
            }
            catch (Exception ex)
            {
                // It's possible that the exporter does not have permission to write the file, so it's generally
                // a good idea to account for this possibility.
                MessageBox.Show(string.Format(DataGridStrings.ExportToWord_Message_WordExportError_Text, ex.Message),
                                DataGridStrings.ExportToWord_Message_WordError_Caption,
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                return;
            }
        }

        #endregion Export

        #region Events

        private void DataPresenterWordWriter_ExportStarted(object sender, ExportStartedEventArgs e)
        {
            this.lbEvents.Items.Add("ExportStarted");
        }

        private void DataPresenterWordWriter_InitializeRecord(object sender, InitializeRecordEventArgs e)
        {
            this.lbEvents.Items.Add("InitializeRecord");
        }

        private void DataPresenterWordWriter_CellExporting(object sender, CellExportingEventArgs e)
        {
            this.lbEvents.Items.Add("CellExporting: " + DataGridStrings.ExportToWord_Field + " " + e.Field.Name + " " + DataGridStrings.ExportToWord_CellValue + " " + e.Cell.Value);
        }

        private void DataPresenterWordWriter_LabelExporting(object sender, LabelExportingEventArgs e)
        {
            WordFontSettings fontSettings = new WordFontSettings { Bold = true, ForeColor = Colors.White };
            e.CellSettings.FontSettings = fontSettings;

            this.lbEvents.Items.Add("LabelExporting: " + DataGridStrings.ExportToWord_ChangedFontSettings + " " + e.Value);
        }

        private void DataPresenterWordWriter_SummaryResultsExporting(object sender, SummaryResultsExportingEventArgs e)
        {
            this.lbEvents.Items.Add("SummaryResultsExporting: " + e.Value);
        }

        private void DataPresenterWordWriter_ExportEnding(object sender, ExportEndingEventArgs e)
        {
            string msg = String.Empty;

            if (e.CancelInfo != null)
            {
                if (e.CancelInfo.Exception != null)
                {
                    msg = e.CancelInfo.Exception.Message;
                }
                else
                {
                    msg = e.CancelInfo.Reason.ToString();
                }
            }

            this.lbEvents.Items.Add("ExportEnding: " + msg);
        }

        private void DataPresenterWordWriter_ExportEnded(object sender, ExportEndedEventArgs e)
        {
            string msg = String.Empty;

            if (e.CancelInfo != null)
            {
                if (e.CancelInfo.Exception != null)
                {
                    msg = e.CancelInfo.Exception.Message;
                }
                else
                {
                    msg = e.CancelInfo.Reason.ToString();

                    // Check if the export is cancelled.
                    if (e.CancelInfo.Reason == Infragistics.Windows.DataPresenter.RecordExportCancellationReason.Cancelled)
                    {
                        File.Delete(_fileName);
                        msg += " " + DataGridStrings.ExportToWord_TmpFileDeleted;
                    }
                }
            }
            else
            {
                // Display an 'Export Completed' message.
                MessageBox.Show(DataGridStrings.ExportToWord_Message_ExportCompleted_Text,
                                DataGridStrings.ExportToWord_Message_ExportCompleted_Caption,
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                // Execute Word to display the exported document.
                try
                {
                    Process p = new Process();
                    p.StartInfo.FileName = _fileName;
                    p.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(DataGridStrings.ExportToWord_Message_WordError_Text, ex.Message),
                                    DataGridStrings.ExportToWord_Message_WordError_Caption,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                }
            }

            this.lbEvents.Items.Add("ExportEnded:" + " " + msg);
        }

        #endregion Events
    }
}
