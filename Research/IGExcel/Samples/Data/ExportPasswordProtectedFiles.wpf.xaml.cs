using IGExcel.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Windows.DataPresenter.ExcelExporter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace IGExcel.Samples.Data
{
    public partial class ExportPasswordProtectedFiles : SampleContainer
    {
        private XmlDataProvider xmlDataProvider;

        public ExportPasswordProtectedFiles()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            this.DownloadDataSource();

            List<WorkbookFormat> list = Enum.GetValues(typeof(WorkbookFormat)).OfType<WorkbookFormat>().ToList();
            list.RemoveAt(6);
            ComboBox_ExcelFormat.ItemsSource = list;
        }

        private void DownloadDataSource()
        {
            // create xml data provider that will load data from xml file
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            xmlDataProvider.GetXmlDataAsync("Products.xml"); // xml file name
        }

        /// <summary>
        /// This method create a collection of the DataItems objects by parsing the XML content and set it as ItemsSource.
        /// </summary>
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Products")
                              select new Product
                              {
                                  SKU = d.Element("ProductID").Value,
                                  Name = d.Element("ProductName").Value,
                                  Category = d.Element("Category").Value,
                                  Supplier = d.Element("Supplier").Value,
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  OnBackOrder = d.Element("UnitsInStock").GetInt() == 0,
                                  QuantityPerUnit = d.Element("QuantityPerUnit").Value
                              });

            this.xamDataGrid.DataSource = dataSource.ToList<Product>();
        }

        private void TestExcel_Click(object sender, RoutedEventArgs e)
        {
            string fileName = null;
            DataPresenterExcelExporter exporter = new DataPresenterExcelExporter();
            Workbook workbook = new Workbook((WorkbookFormat)ComboBox_ExcelFormat.SelectedItem);
            workbook = exporter.Export(xamDataGrid, workbook);

            if (txtPswToOpen.Text != string.Empty)
            {
                SecureString ss = new SecureString();
                txtPswToOpen.Text.ToCharArray().ToList().ForEach(p => ss.AppendChar(p));
                workbook.SetOpenPassword(ss);
            }

            if (txtPswToFileWrite.Text != string.Empty)
            {
                SecureString ss = new SecureString();
                txtPswToFileWrite.Text.ToCharArray().ToList().ForEach(p => ss.AppendChar(p));
                workbook.SetFileWriteProtectionPassword(ss);
            }

            switch ((WorkbookFormat)ComboBox_ExcelFormat.SelectedItem)
            {
                case WorkbookFormat.Excel2007:
                    fileName = "Excel.xlsx";
                    break;
                case WorkbookFormat.Excel2007MacroEnabled:
                    fileName = "Excel.xlsm";
                    break;
                case WorkbookFormat.Excel2007MacroEnabledTemplate:
                    fileName = "Excel.xltm";
                    break;
                case WorkbookFormat.Excel2007Template:
                    fileName = "Excel.xltx";
                    break;
                case WorkbookFormat.Excel97To2003:
                    fileName = "Excel.xls";
                    break;
                case WorkbookFormat.Excel97To2003Template:
                    fileName = "Excel.xlt";
                    break;
                case WorkbookFormat.StrictOpenXml:
                    fileName = "Excel.xml";
                    break;
            }

            string path = Path.GetTempPath();
            fileName = path + fileName;
            try
            {
                workbook.Save(fileName);
                Process.Start(fileName);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    String.Format(ExcelStrings.ErrMsg_FileAlreadyExistsAndInUse, fileName),
                    CommonStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}