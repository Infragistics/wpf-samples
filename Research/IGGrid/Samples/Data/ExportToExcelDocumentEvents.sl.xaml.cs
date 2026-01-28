using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Data
{
    public partial class ExportToExcelDocumentEvents : SampleContainer
    {
        private Workbook _workbook;
        private Stream _stream;
        
        public ExportToExcelDocumentEvents()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // Populate xamGrid with sample data
            DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Products.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Products")
                              select new Product
                              {
                                  SKU = d.Element("ProductID").GetString(),
                                  Name = d.Element("ProductName").GetString(),
                                  Category = d.Element("Category").GetString(),
                                  Supplier = d.Element("Supplier").GetString(),
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  UnitsOnOrder = d.Element("UnitsOnOrder").GetInt(),
                                  QuantityPerUnit = d.Element("QuantityPerUnit").GetString()
                              });

            this.xamGrid.ItemsSource = dataSource.ToList();
            this.ExpandRows(this.xamGrid.Rows);
        }

        private void ExpandRows(RowCollection rows)
        {
            foreach (Row row in rows)
            {
                row.IsExpanded = true;
            }
        }

        private void Btn_Export_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel Workbook (.xlsx)|*.xlsx";

            if (saveDialog.ShowDialog().Value)
            {
                try
                {
                    _stream = saveDialog.OpenFile();

                    // Create a Workbook object and add a sheet in it.
                    _workbook = new Workbook();
                    Worksheet worksheet = _workbook.Worksheets.Add("Sheet1");
                    _workbook.SetCurrentFormat(WorkbookFormat.Excel2007);

                    // Export xamGrid data to the created Workbook object
                    this.excelExporter.ExportAsync(this.xamGrid, _workbook);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void Btn_Clear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ListBox_Log.Items.Count > 0)
            {
                ListBox_Log.Items.Clear();
            }
        }

        private void LogEventOutput(string message)
        {
            ListBoxItem item = new ListBoxItem
            {
                Content = message
            };

            ListBox_Log.Items.Add(item);
        }

        #region Events
        private void excelExporter_ExportStarted(object sender, ExcelExportStartedEventArgs e)
        {
            this.LogEventOutput("ExportStarted");
        }

        private void excelExporter_RowExporting(object sender, XamGridExcelExporterRowExportingEventArgs e)
        {
            this.LogEventOutput("RowExporting: " + e.RowType.ToString());
        }

        private void excelExporter_GroupByRowCellExporting(object sender, XamGridExcelExporterGroupByRowCellExportingEventArgs e)
        {
            this.LogEventOutput("GroupByRowCellExporting: " + e.Value.ToString());
        }

        private void excelExporter_HeaderCellExporting(object sender, XamGridExcelExporterCellExportingEventArgs e)
        {
            this.LogEventOutput("HeaderCellExporting: " + e.ExportObject.ToString());
        }

        private void excelExporter_CellExporting(object sender, XamGridExcelExporterCellExportingEventArgs e)
        {
            this.LogEventOutput("CellExporting: " + e.ExportObject.ToString());
        }

        private void excelExporter_SummaryRowExporting(object sender, XamGridExcelExporterSummaryRowExportingEventArgs e)
        {
            this.LogEventOutput("SummaryRowExporting");
        }

        private void excelExporter_SummaryCellExporting(object sender, XamGridExcelExporterSummaryCellExportingEventArgs e)
        {
            if (e.SummaryValue != null)
                this.LogEventOutput("SummaryCellExporting: " + e.SummaryValue);
        }

        private void excelExporter_ExportEnding(object sender, ExcelExportEndingEventArgs e)
        {
            this.LogEventOutput("ExportEnding");
        }

        private void excelExporter_ExportEnded(object sender, EventArgs e)
        {
            // Saving the Workbook object to a stream and finalizing the export
            _workbook.Save(_stream);
            _stream.Close();

            this.LogEventOutput("ExportEnded");
            MessageBox.Show(GridStrings.ExcelExport_Msg);
        }
        #endregion
    }
}
