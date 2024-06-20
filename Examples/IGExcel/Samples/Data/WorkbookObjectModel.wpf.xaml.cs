using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGExcel.Resources;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Microsoft.Win32;

namespace IGExcel.Samples.Data
{
    /// <summary>
    /// Interaction logic for WorkbookObjectModel.xaml
    /// </summary>
    public partial class WorkbookObjectModel : SampleContainer
    {
        private Workbook _dataWorkbook;
        private WorksheetCell _selectedCell;
        private List<string> _cellFormats;

        public WorkbookObjectModel()
        {
            InitializeComponent();

            this.dataGrid.Columns[0].Header = ExcelStrings.Excel_Unit_Price;
            this.dataGrid.Columns[1].Header = ExcelStrings.Excel_Quantity;
            this.dataGrid.Columns[2].Header = ExcelStrings.Excel_Discount;
            this.dataGrid.Columns[3].Header = ExcelStrings.Excel_Total;

            _cellFormats = new List<string>
            {
                "General",
                "0",
                "0.00",
                "#,##0",
                "#,##0.00",
                "#,##0_);(#,##0)",
                "#,##0_);[Red](#,##0)",
                "#,##0.00_);(#,##0.00)",
                "#,##0.00_);[Red](#,##0.00)",
                "\"$\"#,##0.00",
                "$#,##0_);($#,##0)",
                "$#,##0_);[Red]($#,##0)",
                "0%",
                "0.00%",
                "0.00E+00",
                "##0.0E+0",
                "# ?/?",
                "# ??/??",
                "dd-mm-yy",
                "dd-mmm-yy",
                "dd-mmm",
                "mmm-yy",
                "_($* #,##0_);_($* (#,##0);_($* \"-\"_);_(@_)",
                "_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)",
                "_($* #,##0.00_);_($* (#,##0.00);_($* \"-\"??_);_(@_)",
                "_(* #,##0.00_);_(* (#,##0.00);_(* \"-\"??_);_(@_)",
                "$#,##0.00"
            };

            comboBoxFormat.ItemsSource = _cellFormats;

            string resourceName = "IGExcel.Resources.orders.xlsx";

            //Load the Excel file.
            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(resourceName))
            {
                _dataWorkbook = Workbook.Load(stream);
            }

            if (_dataWorkbook == null)
            {
                return;
            }

            dataGrid.ItemsSource = _dataWorkbook.Worksheets[0].Rows;
        }

        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGrid.CurrentCell != null)
            {
                WorksheetRow selectedRow = dataGrid.CurrentCell.Item as WorksheetRow;

                if (selectedRow == null) return;

                _selectedCell = selectedRow.Cells[dataGrid.CurrentColumn.DisplayIndex];

                comboBoxFormat.SelectionChanged -= SetFormat;
                comboBoxFormat.SelectedIndex = _cellFormats.IndexOf(_selectedCell.CellFormat.FormatString);
                comboBoxFormat.SelectionChanged += SetFormat;

                textBoxFormula.Text = _selectedCell.Formula == null
                                          ? "="
                                          : _selectedCell.Formula.ToString();

                textBoxComment.Text = _selectedCell.Comment == null
                                          ? string.Empty
                                          : _selectedCell.Comment.Text.ToString();
            }
        }

        private void SetFormat(object sender, SelectionChangedEventArgs e)
        {
            if (_selectedCell != null)
            {
                _selectedCell.CellFormat.FormatString = comboBoxFormat.SelectedItem.ToString();
            }
        }

        private void SetFormula(object sender, RoutedEventArgs e)
        {
            if (_selectedCell != null)
            {
                try
                {
                    _selectedCell.ApplyFormula(textBoxFormula.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                dataGrid.Items.Refresh();
            }
        }

        private void SetComment(object sender, RoutedEventArgs e)
        {
            if (_selectedCell != null)
            {
                WorksheetCellComment comment = new WorksheetCellComment();
                FormattedString formattedString = new FormattedString(textBoxComment.Text);

                comment.Text = formattedString;

                _selectedCell.Comment = comment;
            }
        }

        private void SaveWorkbook(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog { Filter = "Excel files|*.xlsx", DefaultExt = "xlsx" };

            if (dialog.ShowDialog() == true)
            {
                if (!dialog.FileName.EndsWith(".xlsx"))
                {
                    dialog.FileName += ".xlsx";
                }

                using (Stream exportStream = dialog.OpenFile())
                {
                    _dataWorkbook.Save(exportStream);
                    exportStream.Close();
                }
            }
        }
    }
}
