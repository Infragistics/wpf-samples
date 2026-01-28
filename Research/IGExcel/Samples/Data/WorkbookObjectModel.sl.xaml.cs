using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;

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

        private void dataGrid_ActiveCellChanged(object sender, EventArgs e)
        {
            int columnIndex = dataGrid.Columns.IndexOf(dataGrid.ActiveCell.Column);
            WorksheetRow selectedRow = dataGrid.ActiveCell.Control.DataContext as WorksheetRow;
            _selectedCell = selectedRow.Cells[columnIndex];

            comboBoxFormat.SelectedIndex = _cellFormats.IndexOf(_selectedCell.CellFormat.FormatString);

            textBoxFormula.Text = _selectedCell.Formula == null
                                      ? "="
                                      : _selectedCell.Formula.ToString();

            textBoxComment.Text = _selectedCell.Comment == null
                                      ? string.Empty
                                      : _selectedCell.Comment.Text.ToString();
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

                dataGrid.InvalidateData();
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
                using (Stream exportStream = dialog.OpenFile())
                {
                    _dataWorkbook.Save(exportStream);
                    exportStream.Close();
                }
            }
        }
    }
}
