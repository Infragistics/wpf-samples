using IGExcel.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Documents.Excel.ConditionalFormatting;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace IGExcel.Samples.Display
{
    /// <summary>
    /// Interaction logic for ConditionalFormatting.xaml
    /// </summary>
    public partial class ConditionalFormatting : SampleContainer
    {
        private XmlDataProvider xmlDataProvider;
        Random r = new Random();
        public ConditionalFormatting()
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

            this.DownloadDataSource();
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


            List<Product> data = dataSource.ToList<Product>();

            for(int i=0; i < data.Count; i++)
            {
                Product p = data[i];
                if(i % 6 == 0)
                {                    
                    p.Name = string.Empty;
                }
                
                if(i % 12 == 0)
                {                    
                    p.SKU = data[0].SKU;
                }

                p.Price = r.Next(5, 100);
            }

            this.dataGrid.ItemsSource = data;
        }

        private void TestExcel_Click(object sender, RoutedEventArgs e)
        {
            Workbook dataWorkbook = new Workbook();
            Worksheet sheetOne = dataWorkbook.Worksheets.Add("Data Sheet");

            // Export to Excel2007 format 
            if (ComboBox_ExcelFormat.SelectedIndex == 0)
            {
                dataWorkbook.SetCurrentFormat(WorkbookFormat.Excel2007);
            }
            else
            {
                dataWorkbook.SetCurrentFormat(WorkbookFormat.Excel97To2003);
            }

            // Print column header text 
            int currentColumn = 0;
            foreach (DataGridColumn column in this.dataGrid.Columns)
            {
                this.SetCellValue(sheetOne.Rows[0].Cells[currentColumn], column.Header);
                currentColumn++;
            }

            // Export Data From Grid
            IEnumerable gridData = dataGrid.ItemsSource;

            int currentRow = 1;
            WorksheetRow worksheetRow;

            foreach (Product product in gridData)
            {
                int currentCell = 0;
                worksheetRow = sheetOne.Rows[currentRow];

                this.SetCellValue(worksheetRow.Cells[currentCell], product.SKU);
                this.SetCellValue(worksheetRow.Cells[++currentCell], product.Name);
                this.SetCellValue(worksheetRow.Cells[++currentCell], product.Category);
                this.SetCellValue(worksheetRow.Cells[++currentCell], product.Supplier);
                this.SetCellValue(worksheetRow.Cells[++currentCell], product.OnBackOrder);
                this.SetCellValue(worksheetRow.Cells[++currentCell], product.QuantityPerUnit);
                this.SetCellValue(worksheetRow.Cells[++currentCell], product.UnitPrice);
                this.SetCellValue(worksheetRow.Cells[++currentCell], product.UnitsInStock);

                currentRow++;
            }

            this.setColumnFormatting(sheetOne);
            this.SaveExport(dataWorkbook);
        }

        private void SetCellValue(WorksheetCell cell, object value)
        {
            cell.Value = value;
            cell.CellFormat.ShrinkToFit = ExcelDefaultableBoolean.True;
            cell.CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
            cell.CellFormat.Alignment = HorizontalCellAlignment.Center;
        }

        private void SaveExport(Workbook dataWorkbook)
        {
            SaveFileDialog dialog;
            Stream exportStream;

            // Export to xlsx excel file format
            if (ComboBox_ExcelFormat.SelectedIndex == 0)
            {
                dialog = new SaveFileDialog { Filter = "Excel files|*.xlsx", DefaultExt = "xlsx" };
            }
            else
            {
                dialog = new SaveFileDialog { Filter = "Excel files|*.xls", DefaultExt = "xls" };
            }

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    exportStream = dialog.OpenFile();
                    dataWorkbook.Save(exportStream);
                    exportStream.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }

        private void setColumnFormatting(Worksheet worksheet)
        {
            // Freeze header row
            worksheet.DisplayOptions.PanesAreFrozen = true;
            worksheet.DisplayOptions.FrozenPaneSettings.FrozenRows = 1;

            // Build Column List
            worksheet.DefaultColumnWidth = 5000;
            worksheet.Columns[1].Width = 8500;
            worksheet.Columns[3].Width = 10000;

            DuplicateConditionalFormat duplicates = worksheet.ConditionalFormats.AddDuplicateCondition("A2:A78");
            duplicates.CellFormat.Font.ColorInfo = new WorkbookColorInfo(System.Drawing.Color.Red);

            BlanksConditionalFormat blanks = worksheet.ConditionalFormats.AddBlanksCondition("B2:B78");
            blanks.CellFormat.Fill = CellFill.CreateSolidFill(System.Drawing.Color.DarkGray);

            TextOperatorConditionalFormat text = worksheet.ConditionalFormats.AddTextCondition("C2:C78", "Bev", FormatConditionTextOperator.Contains);
            text.CellFormat.Font.ColorInfo = new WorkbookColorInfo(System.Drawing.Color.Blue);

            TextOperatorConditionalFormat text2 = worksheet.ConditionalFormats.AddTextCondition("D2:D78", "Bigfoot Breweries", FormatConditionTextOperator.Contains);
            text2.CellFormat.Font.ColorInfo = new WorkbookColorInfo(System.Drawing.Color.Orange);

            OperatorConditionalFormat operatorFormatTrue = worksheet.ConditionalFormats.AddOperatorCondition("E2:E78", FormatConditionOperator.Equal);
            operatorFormatTrue.SetOperand1("TRUE");
            operatorFormatTrue.CellFormat.Font.ColorInfo = new WorkbookColorInfo(System.Drawing.Color.Green);

            OperatorConditionalFormat operatorFormatFalse = worksheet.ConditionalFormats.AddOperatorCondition("E2:E78", FormatConditionOperator.Equal);
            operatorFormatFalse.SetOperand1("FALSE");
            operatorFormatFalse.CellFormat.Font.ColorInfo = new WorkbookColorInfo(System.Drawing.Color.Red);

            IconSetConditionalFormat iconSets = worksheet.ConditionalFormats.AddIconSetCondition("G2:G78");

            OperatorConditionalFormat operatorFormat2 = worksheet.ConditionalFormats.AddOperatorCondition("H2:H78", FormatConditionOperator.Less);
            operatorFormat2.SetOperand1(30);
            operatorFormat2.CellFormat.Font.ColorInfo = new WorkbookColorInfo(System.Drawing.Color.Red);
        }
    }
}
