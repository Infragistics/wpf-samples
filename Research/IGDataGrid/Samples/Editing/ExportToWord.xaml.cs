using System;
using System.Diagnostics;
using System.Windows;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.WordWriter;

namespace IGDataGrid.Samples.Editing
{
    /// <summary>
    /// Interaction logic for ExportToWord.xaml
    /// </summary>
    public partial class ExportToWord : SampleContainer
    {
        #region Private members

        private string _fileName;

        #endregion Private members

        public ExportToWord()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(ExportToWord_Samp_Loaded);
        }

        #region Initialization

        void ExportToWord_Samp_Loaded(object sender, RoutedEventArgs e)
        {
            this.xamDataGrid.DataSource = NWindDataSource.GetTable(NWindTable.OrderDetails).DefaultView;

            foreach (Record rec in xamDataGrid.Records)
            {
                if (rec.RecordType == RecordType.GroupByField || rec.RecordType == RecordType.GroupByFieldLayout)
                {
                    GroupByRecord groupRecord = (GroupByRecord)rec;
                    if (groupRecord.Index == 3)
                        groupRecord.IsExpanded = true;
                }
            }

            // Hide the first record
            this.xamDataGrid.Records[0].Visibility = System.Windows.Visibility.Collapsed;

            // Set a file name to use for the export.
            _fileName = System.IO.Path.GetTempPath() + "WordExportedTempFile" + ".docx";
        }

        #endregion Initialization

        #region Export

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {

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
                // Get the instance of the DataPresenterWordWriter we defined in the Page's resources and call its ExportAsync
                // method passing in the XamDataGrid we want to export, the name of the file we would like
                // to export to.
                DataPresenterWordWriter exporter = (DataPresenterWordWriter)this.Resources["wordExporter"];

                exporter.ExportAsync(xamDataGrid, _fileName);
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

        private void DataPresenterWordWriter_ExportEnded(object sender, ExportEndedEventArgs e)
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

        #endregion Events
    }
}
