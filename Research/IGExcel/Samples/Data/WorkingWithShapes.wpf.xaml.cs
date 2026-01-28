using IGExcel.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Documents.Excel.PredefinedShapes;
using Infragistics.Samples.Framework;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace IGExcel.Samples.Data
{
    /// <summary>
    /// Interaction logic for WorkingWithShapes.xaml
    /// </summary>
    public partial class WorkingWithShapes : SampleContainer
    {
        public WorkingWithShapes()
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

        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            Workbook dataWorkbook = new Workbook();

            if (ComboBox_ExcelFormat.SelectedIndex == 0)
            {
                dataWorkbook.SetCurrentFormat(WorkbookFormat.Excel2007);
            }
            else
            {
                dataWorkbook.SetCurrentFormat(WorkbookFormat.Excel97To2003);
            }

            Worksheet sheetOne = dataWorkbook.Worksheets.Add(ExcelStrings.ExcelShapes_SheetName);

            if (checkBoxRectangle.IsChecked == true)
            {
                RectangleShape shape = new RectangleShape();
                shape.TopLeftCornerCell = sheetOne.Rows[6].Cells[0];
                shape.TopLeftCornerPosition = new Point(50, 0);
                shape.BottomRightCornerCell = sheetOne.Rows[11].Cells[1];
                shape.BottomRightCornerPosition = new Point(100, 100);

                shape.Fill = ShapeFill.FromColor(Color.FromArgb(0x7D, 0xC1, 0x3C, 0x4D));
                shape.Outline = ShapeOutlineSolid.FromColor(Colors.Black);

                sheetOne.Shapes.Add(shape);
            }
            if (checkBoxDiamond.IsChecked == true)
            {
                WorksheetShapeWithText shape = (WorksheetShapeWithText)sheetOne.Shapes.Add(
                    PredefinedShapeType.Diamond,
                    sheetOne.Rows[1].Cells[2], new Point(50, 100),
                    sheetOne.Rows[13].Cells[5], new Point(50, 100));
                shape.Fill = ShapeFill.FromColor(Color.FromArgb(0xFF, 0x4A, 0xB4, 0xFF));
                shape.Outline = null;
                var formattedText =
                    new Infragistics.Documents.Excel.FormattedText(ExcelStrings.ExcelShapes_Diamod_InsideText);
                shape.Text = formattedText;
                FormattedTextFont formattedTextFont = shape.Text.GetFont(0);
                formattedTextFont.ColorInfo = new WorkbookColorInfo(Colors.Red);
            }
            if (checkBoxLine.IsChecked == true)
            {
                LineShape shape = new LineShape();
                shape.TopLeftCornerCell = sheetOne.Rows[1].Cells[0];
                shape.TopLeftCornerPosition = new Point(50, 50);
                shape.BottomRightCornerCell = sheetOne.Rows[3].Cells[2];
                shape.BottomRightCornerPosition = new Point(50, 50);
                shape.Outline = ShapeOutlineSolid.FromColor(Colors.Black);

                sheetOne.Shapes.Add(shape);
            }

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
    }
}
