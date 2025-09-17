using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGExcel.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;

namespace IGExcel.Samples.Display
{
    public partial class NewColorModel : SampleContainer
    {
        Workbook workbook;
        Worksheet worksheet;

        public NewColorModel()
        {
            InitializeComponent();
            InitializeWorkbook();
        }

        private void InitializeWorkbook()
        {
            workbook = new Workbook(WorkbookFormat.Excel2007);
            worksheet = workbook.Worksheets.Add("TestSheet");
            InitializeCombos();
        }

        private void btnCreateExcel_Click(object sender, RoutedEventArgs e)
        {
            CellFormatColorInfoTest();
        }

        private void CellFormatColorInfoTest()
        {
            if (workbook == null || worksheet == null)
            {
                return;
            }

            worksheet.SetDefaultColumnWidth(96, WorksheetColumnWidthUnit.Pixel);

            CellFill solidFill = CellFill.CreateSolidFill(new WorkbookColorInfo(primaryColor, selectedTint));
            ApplyCellFormat("A2:B3", solidFill);
            worksheet.GetCell("D2").Value = String.Format(ExcelStrings.NewColorModel_Export_Description_SolidColorFill,
                primaryColor.ToString(), selectedTint.ToString());

            CellFill patternFill = CellFill.CreatePatternFill(new WorkbookColorInfo(backgroundPatternColor), new WorkbookColorInfo(patternColor),
                selectedFillPatternStyle);
            ApplyCellFormat("A5:B6", patternFill);
            worksheet.GetCell("D5").Value = String.Format(ExcelStrings.NewColorModel_Export_Description_PatternFill,
                backgroundPatternColor.ToString(), patternColor.ToString(), selectedFillPatternStyle.ToString());

            CellFill gradientFill;
            string cellInfo;
            if (isLinearGradientUser)
            {
                gradientFill = CellFill.CreateLinearGradientFill(45, new WorkbookColorInfo(gradientFirstColor), new WorkbookColorInfo(gradientSecondColor));
                cellInfo = ExcelStrings.NewColorModel_Export_Description_LinearGradient;
            }
            else
            {
                gradientFill = CellFill.CreateRectangularGradientFill(.5, .5, .5, .5, new WorkbookColorInfo(gradientFirstColor), new WorkbookColorInfo(gradientSecondColor));
                cellInfo = ExcelStrings.NewColorModel_Export_Description_RectangularGradient;
            }
            ApplyCellFormat("A8:B9", gradientFill);
            worksheet.GetCell("D8").Value = String.Format(ExcelStrings.NewColorModel_Export_Description_Color_One_and_Two, cellInfo,
                gradientFirstColor.ToString(), gradientSecondColor.ToString());

            SaveWorkbook();
        }

        private void SaveWorkbook()
        {
            SaveFileDialog dialog = new SaveFileDialog { Filter = "Excel files|*.xlsx", DefaultExt = "xlsx" };
            Stream exportStream;

            if (workbook != null)
            {
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        exportStream = dialog.OpenFile();
                        workbook.Save(exportStream);
                        exportStream.Close();
                    }
                    catch (System.IO.IOException ioException)
                    {
                        MessageBox.Show(ioException.Message);
                    }
                }
            }
        }

        private void ApplyCellFormat(string regionName, CellFill cellFillFormat)
        {
            WorksheetRegion region = worksheet.GetRegion(regionName);
            foreach (WorksheetCell wCell in region)
            {
                wCell.CellFormat.Fill = cellFillFormat;
            }
        }

        #region Combo handlers

        private void cmbTint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            selectedTint = double.Parse(cb.SelectedItem.ToString());
        }

        private void xcpSolidColor_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            primaryColor = (Color)e.NewColor;
        }

        private void xcpPatternBackgroundColor_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            backgroundPatternColor = (Color)e.NewColor;
        }

        private void xcpPatternColor_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            patternColor = (Color)e.NewColor;
        }

        private void xcpGradientFirstColor_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            gradientFirstColor = (Color)e.NewColor;
        }

        private void xcpGradientSecondColor_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            gradientSecondColor = (Color)e.NewColor;
        }

        private void cmbPatternStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            selectedFillPatternStyle = (FillPatternStyle)cb.SelectedIndex;
        }

        private void cmbGradientChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            isLinearGradientUser = (cb.SelectedIndex == 0);
        }

        #endregion // Combo handlers

        #region Helper methods

        private double selectedTint;
        private FillPatternStyle selectedFillPatternStyle;
        private bool isLinearGradientUser;

        private Color primaryColor;
        private Color backgroundPatternColor;
        private Color patternColor;
        private Color gradientFirstColor;
        private Color gradientSecondColor;

        private void InitializeCombos()
        {
            FillTint();
            FillPatternTypes();
            FillGradientChoices();
            SetFillColors();
        }

        private void FillTint()
        {
            string[] tints = new string[19];
            for (int i = -9; i < 10; i++)
            {
                tints[(i + 9)] = String.Format("{0:0.0}", i / 10d);
            }
            cmbTint.ItemsSource = tints;
            cmbTint.SelectedIndex = 12;
        }

        private void FillPatternTypes()
        {
            List<String> names = new List<string>();
            foreach (FillPatternStyle fps in Enum.GetValues(typeof(FillPatternStyle)))
            {
                names.Add(fps.ToString());
            }
            cmbPatternStyle.ItemsSource = names;
            cmbPatternStyle.SelectedIndex = 1;
        }

        private void FillGradientChoices()
        {
            cmbGradientChoice.ItemsSource = new string[] { "Linear", "Rectangular" };
            cmbGradientChoice.SelectedIndex = 1;
        }

        private void SetFillColors()
        {
            primaryColor = (Color)xcpSolidColor.SelectedColor;
            patternColor = (Color)xcpPatternColor.SelectedColor;
            backgroundPatternColor = (Color)xcpPatternBackgroundColor.SelectedColor;
            gradientFirstColor = (Color)xcpGradientFirstColor.SelectedColor;
            gradientSecondColor = (Color)xcpGradientSecondColor.SelectedColor;
        }

        #endregion // Helper methods
    }
}