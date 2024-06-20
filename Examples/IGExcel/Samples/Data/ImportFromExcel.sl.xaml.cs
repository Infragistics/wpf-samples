using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using IGExcel.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGExcel.Samples.Data
{
    /// <summary>
    /// Interaction logic for ImportFromExcel.xaml
    /// </summary>
    public partial class ImportFromExcel : SampleContainer
    {
        public ImportFromExcel()
        {
            InitializeComponent();
        }

        private void Sample_Loaded(object sender, RoutedEventArgs e)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;

            List<string> excelFormats = new List<string>
                                            {
                                                ExcelStrings.ExcelEngine_Combo_XLSX,
                                                ExcelStrings.ExcelEngine_Combo_XLS
                                            };

            this.ComboBox_ExcelFormat.ItemsSource = excelFormats;
            this.ComboBox_ExcelFormat.SelectedIndex = 0;
        }

        private void BtnClearData_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
        }

        private void TestExcel_Click(object sender, RoutedEventArgs e)
        {
            Workbook dataWorkbook;
            string resourceName = "IGExcel.Resources." + this.GetExcelFileName();

            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(resourceName))
            {
                dataWorkbook = Workbook.Load(stream);
            }

            if (dataWorkbook == null)
            {
                return;
            }

            Worksheet sheetOne = dataWorkbook.Worksheets[0];

            ObservableCollection<Product> products = new ObservableCollection<Product>();

            foreach (WorksheetRow row in sheetOne.Rows)
            {
                Product product = new Product
                {
                    SKU = row.Cells[0].Value.ToString(),
                    Name = row.Cells[1].Value.ToString(),
                    Category = row.Cells[2].Value.ToString(),
                    Supplier = row.Cells[3].Value.ToString(),
                    OnBackOrder = (bool)row.Cells[4].Value,
                    QuantityPerUnit = row.Cells[5].Value.ToString(),
                    UnitPrice = (double)row.Cells[6].Value,
                    UnitsInStock = (int)(double)row.Cells[7].Value
                };
                products.Add(product);
            }

            if (this.DataContext == null)
                this.DataContext = products;
        }

        private string GetExcelFileName()
        {
            string isoCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string excelFile = string.Empty;
            string fileExt = ".xls";

            if (ComboBox_ExcelFormat.SelectedIndex == 0)
                fileExt = ".xlsx";

            if (isoCode.ToLower() != "ja")
            {
                excelFile = "test" + fileExt;
            }
            else
            {
                excelFile = "test_ja" + fileExt;
            }

            return excelFile;
        }
    }
}
