using IGExcel.Resources;
using IGSparkline.Model;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for WorkingWithSparklines.xaml
    /// </summary>
    public partial class WorkingWithSparklines : SampleContainer
    {
        Workbook workbook;
        Worksheet sheet1;
        Worksheet sheet2;
        public WorkingWithSparklines()
        {
            InitializeComponent();

            workbook = new Workbook(WorkbookFormat.Excel2007);

            GenerateWorksheetData();
                        
            var line_sparkline = sheet1.SparklineGroups.Add(SparklineType.Line, "Sparklines!A1:A1",  "Data!A1:A11");
            var column_sparkline = sheet1.SparklineGroups.Add(SparklineType.Column, "Sparklines!B1:B1", "Data!A1:A11");            
        }
        public void GenerateWorksheetData()
        {
            sheet1 = workbook.Worksheets.Add("Sparklines");
            sheet2 = workbook.Worksheets.Add("Data");

            sheet1.Columns[0].Width = 10000;
            sheet1.Columns[1].Width = 10000;
            sheet1.Rows[0].Height = 3000;

            TestData data = new TestData();

            int rowIndex = 0;
            List<string> columnNames = new List<string>();

            var type = data.FirstOrDefault().GetType().GetProperties();

            foreach (PropertyInfo property in type)
            {
                columnNames.Add(property.Name);
            }

            WorksheetRow row = sheet2.Rows[rowIndex++];

            int cellValue = 0;
            for (int columnIndex = 0; rowIndex < data.Count + 1; rowIndex++)
            {

                row.Cells[columnIndex].Value = data[cellValue].Value;
                cellValue++;
                row = sheet2.Rows[cellValue];

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (workbook != null)
            {
                SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook (*.xlsx)|*.xlsx", DefaultExt = ".xlsx" };
                sfd.AddExtension = true;
                if (sfd.ShowDialog() == true)
                {
                    try
                    {
                        try
                        {
                            workbook.Save(sfd.FileName);

                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show(String.Format("{0} {1}", ExcelStrings.NewColorModel_Export_WarningMessage, sfd.FileName));
                    }
                }
            }
        }
    }
}
