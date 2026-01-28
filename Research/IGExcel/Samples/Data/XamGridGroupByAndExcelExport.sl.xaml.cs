using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGExcel.Samples.Data
{
    public partial class XamGridGroupByAndExcelExport : SampleContainer
    {
        private int _initialColumnsCount = 0;
        private int _currentMergedRow = 0;
        private int _currentMergedColumn = 0;
        private int _countGrouped = 0;
        private int _countNotGrouped = 0;
        private int _outlineLevel = 1;
        private int _childrenCount = 0;
        private Worksheet sheet;
        private XmlDataProvider xmlDataProvider;

        public XamGridGroupByAndExcelExport()
        {
            InitializeComponent();
        }

        void Sample_Loaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            // create xml data provider that will load data from xml file
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            xmlDataProvider.GetXmlDataAsync("Patients.xml"); // xml file name
        }

        /// <summary>
        /// This method create a collection of the DataItems objects by parsing the XML content and set it as ItemsSource.
        /// </summary>
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("PatientAdmittance")
                              select new PatientAdmittance
                              {
                                  FirstName = d.Attribute("FirstName").Value,
                                  LastName = d.Attribute("LastName").Value,
                                  DOB = d.Attribute("DOB").GetDateTime(),
                                  Gender = d.Attribute("Gender").Value,
                                  AdmittanceDate = d.Attribute("AdmittanceDate").GetDateTime(),
                                  Location = d.Attribute("Location").Value,
                                  Severity = d.Attribute("Severity").Value
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();

            _initialColumnsCount = GetInitialColumnsCount(dataGrid);
        }

        private int GetInitialColumnsCount(XamGrid grid)
        {
            int currentColumn = 0;
            foreach (Column column in grid.Columns)
            {
                if (column.Visibility == Visibility.Visible)
                {
                    currentColumn++;
                }
            }

            return currentColumn;
        }

        private void BtnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog { Filter = "Excel files|*.xls", DefaultExt = "xls" };

            bool? showDialog = dialog.ShowDialog();
            if (showDialog == true)
            {
                Workbook dataWorkbook = CreateDataBook();
                using (Stream exportStream = dialog.OpenFile())
                {
                    dataWorkbook.Save(exportStream);
                    exportStream.Close();
                }
            }
        }

        private Workbook CreateDataBook()
        {
            Workbook workbook = new Workbook(WorkbookFormat.Excel97To2003);
            sheet = workbook.Worksheets.Add("Data Sheet");

            _currentMergedRow = 0;
            _currentMergedColumn = 0;

            var groupByQuery = from row in dataGrid.Rows
                               where row.RowType == RowType.GroupByRow
                               select row;

            var simpleQuery = from row in dataGrid.Rows
                              where row.RowType == RowType.DataRow
                              select row;

            foreach (var query in groupByQuery)
            {
                int currentRow = 1;
                _childrenCount = 0;
                _countGrouped = 0;
                _countNotGrouped = 0;
                _childrenCount = GetQueryElementItemsCount(query as GroupByRow);
                _outlineLevel = 1;

                for (int i = 0; i < _childrenCount; i++)
                {
                    sheet.Rows[_currentMergedRow + i + 1].OutlineLevel = _outlineLevel;
                }

                _currentMergedColumn = 0;
                IterateGroupByRow(query as GroupByRow);

                currentRow++;
            }

            foreach (var query in simpleQuery)
            {
                int currentRow = 1;

                IterateRow(query);
                currentRow++;
            }

            return workbook;
        }

        private int GetQueryElementItemsCount(GroupByRow query)
        {
            RowCollection children = query.Rows;
            foreach (Row child in children)
            {
                if (child.RowType == RowType.GroupByRow)
                {
                    _countGrouped++;
                    GetQueryElementItemsCount(child as GroupByRow);
                }
                else
                    _countNotGrouped++;
            }

            return _countGrouped + _countNotGrouped;
        }

        private void IterateGroupByRow(GroupByRow groupByRow)
        {
            // Create a merged region
            WorksheetMergedCellsRegion mergedRegion = sheet.MergedCellsRegions.Add(_currentMergedRow, _currentMergedColumn, _currentMergedRow, _currentMergedColumn + _initialColumnsCount);
            mergedRegion.Value = groupByRow.GroupByData.Value.ToString();
            _currentMergedRow++;
            _currentMergedColumn++;

            if (groupByRow.HasChildren)
            {
                RowCollection groupByRowChildren = groupByRow.Rows;
                foreach (Row child in groupByRowChildren)
                {
                    if (child.RowType == RowType.GroupByRow)
                    {
                        IterateGroupByRow(child as GroupByRow);
                        _currentMergedColumn--;
                    }
                    else
                        IterateRow(child);
                }
            }
        }

        private void IterateRow(Row row)
        {
            int columnIndex = _currentMergedColumn;

            foreach (Cell cell in row.Cells)
            {
                sheet.Rows[_currentMergedRow].Cells[columnIndex].Value = cell.Value;
                sheet.Columns[columnIndex].Width = 4000;
                columnIndex++;
            }

            _currentMergedRow++;
        }
    }
}
