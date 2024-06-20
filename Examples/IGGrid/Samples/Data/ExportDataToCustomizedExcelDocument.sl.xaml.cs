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
    public partial class ExportDataToCustomizedExcelDocument : SampleContainer
    {

        public ExportDataToCustomizedExcelDocument()
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

        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
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
            ExpandRows(this.xamGrid.Rows);
        }

        private void ExpandRows(RowCollection rows)
        {
            foreach (Row row in rows)
            {
                row.IsExpanded = true;
            }
        }

        private void Btn_Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel Workbook (.xlsx)|*.xlsx";

            if (saveDialog.ShowDialog().Value)
            {
                try
                {
                    Stream stream = saveDialog.OpenFile();

                    Workbook exportedWorkbook = this.ExportWorkbook();
                    exportedWorkbook.Save(stream);
                    stream.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private Workbook ExportWorkbook()
        {
            // Create a Workbook object and add a sheet in it.
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets.Add("Sheet1");
            workbook.SetCurrentFormat(WorkbookFormat.Excel2007);

            // Export xamGrid data to the created Workbook object
            this.excelExporter.Export(this.xamGrid, workbook);

            return workbook;
        }

        private void excelExporter_ExportEnded(object sender, EventArgs e)
        {
            MessageBox.Show(GridStrings.ExcelExport_Msg);
        }

    }
}
