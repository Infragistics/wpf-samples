using Infragistics.Controls.Grids;
using Infragistics.Controls.Menus;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace IGRibbon.Samples.Organization
{
    public partial class RibbonAndGrid : SampleContainer
    {
        public RibbonAndGrid()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            XmlDataProvider xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("CustomersOrders.xml");
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            if (gxdcea.Error != null)
            {
                return;
            }

            XDocument doc = gxdcea.Result;

            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  CustomerID = d.Element("CustomerID").Value,
                                  Company = d.Element("CompanyName").Value,
                                  ContactName = d.Element("ContactName").Value,
                                  ContactTitle = d.Element("ContactTitle").Value,
                                  AddressOne = d.Element("Address").Value,
                                  City = d.Element("City").Value,
                                  Region = d.Element("Region").GetString(),
                                  Country = d.Element("Country").Value
                              });

            MyDataGrid.ItemsSource = dataSource.ToList();
        }

        private void AddContact_Click(object sender, RibbonToolEventArgs e)
        {
            MyDataGrid.AddNewRowSettings.AllowAddNewRow = AddNewRowLocation.Top;
        }

        private void DeleteContact_Click(object sender, RibbonToolEventArgs e)
        {
            // Get the selected row for deletion
            Row selectedRow = MyDataGrid.SelectionSettings.SelectedRows[0];
            if (selectedRow == null)
                return;

            // Delete selected row
            MyDataGrid.Rows.Remove(selectedRow);
        }

        private void ExportContact_Click(object sender, RibbonToolEventArgs e)
        {
            SaveFileDialog savefileDialog = new SaveFileDialog { Filter = "Excel files|*.xlsx", DefaultExt = "xlsx" };

            // Display the dialog. If the user selects a file of the correct format 
            // and clicks Save in the dialog, save the file using the Save method of the Workbook object.
            bool? showDialog = savefileDialog.ShowDialog();
            if (showDialog == true)
            {
                Workbook dataWorkbook = CreateDataBook();
                using (Stream exportStream = savefileDialog.OpenFile())
                {
                    dataWorkbook.Save(exportStream);
                    exportStream.Close();
                }
            }
        }

        private Workbook CreateDataBook()
        {
            // Create workbook
            var workbook1 = new Workbook();
            // Add worksheet
            var worksheet1 = workbook1.Worksheets.Add("Sample Worksheet1");
            workbook1.SetCurrentFormat(WorkbookFormat.Excel2007);

            foreach (Row gridRow in MyDataGrid.Rows)
            {
                var worksheetRow = worksheet1.Rows[gridRow.Index + 1];

                for (int columnIndex = 0; columnIndex < this.MyDataGrid.Columns.Count; columnIndex++)
                {
                    // Set Excel cell value to XamGrid's cell value cell by cell
                    worksheetRow.Cells[columnIndex].Value = gridRow.Cells[columnIndex].Value;

                    /* Set header appearance and text */
                    if (gridRow.Index == 0)
                    {
                        var worksheetCell = worksheet1.Rows[0].Cells[columnIndex];

                        var gridColumn = this.MyDataGrid.Columns[columnIndex] as Column;

                        // Set header cell color and and borders

                        var foregroundColorInfo = new WorkbookColorInfo(Colors.LightGray);
                        worksheetCell.CellFormat.Fill = new CellFillPattern(foregroundColorInfo, null, FillPatternStyle.Solid);

                        // Set Excel header text to key of column
                        worksheetCell.Value = gridColumn.Key;
                    }
                }
            }

            return workbook1;
        }

        private void RadioButtonTool_Checked(object sender, RibbonToolEventArgs e)
        {
            var filterSettings = e.Tool as RadioButtonTool;

            switch (filterSettings.Id)
            {
                case "FILTER_TOP":
                    this.MyDataGrid.FilteringSettings.AllowFiltering = FilterUIType.FilterRowTop;
                    break;
                case "FILTER_BOTTOM":
                    this.MyDataGrid.FilteringSettings.AllowFiltering = FilterUIType.FilterRowBottom;
                    break;
                case "FILTER_MENU":
                    this.MyDataGrid.FilteringSettings.AllowFiltering = FilterUIType.FilterMenu;
                    break;
                case "DISABLE_FILTER":
                    this.MyDataGrid.FilteringSettings.AllowFiltering = FilterUIType.None;
                    break;
                case "SUMMARY_TOP":
                    this.MyDataGrid.SummaryRowSettings.AllowSummaryRow = SummaryRowLocation.Top;
                    break;
                case "SUMMARY_BOTTOM":
                    this.MyDataGrid.SummaryRowSettings.AllowSummaryRow = SummaryRowLocation.Bottom;
                    break;
                case "SUMMARY_BOTH":
                    this.MyDataGrid.SummaryRowSettings.AllowSummaryRow = SummaryRowLocation.Both;
                    break;
                case "DISABLE_SUMMARY":
                    this.MyDataGrid.SummaryRowSettings.AllowSummaryRow = SummaryRowLocation.None;
                    break;
                case "PAGING_TOP":
                    this.MyDataGrid.PagerSettings.AllowPaging = PagingLocation.Top;
                    break;
                case "PAGING_BOTTOM":
                    this.MyDataGrid.PagerSettings.AllowPaging = PagingLocation.Bottom;
                    break;
                case "PAGING_BOTH":
                    this.MyDataGrid.PagerSettings.AllowPaging = PagingLocation.Both;
                    break;
                case "DISABLE_PAGING":
                    this.MyDataGrid.PagerSettings.AllowPaging = PagingLocation.None;
                    break;
                default:
                    break;
            }
        }

        private void CheckBoxTool_Checked(object sender, RibbonToolEventArgs e)
        {
            var enableSettings = e.Tool as CheckBoxTool;

            switch (enableSettings.Id)
            {
                case "ENABLE_SORTING":
                    this.MyDataGrid.SortingSettings.AllowSorting = true;
                    break;
                case "ENABLE_EDITING":
                    this.MyDataGrid.EditingSettings.AllowEditing = EditingType.Row;
                    break;
                case "ENABLE_ROWSELECTORS":
                    this.MyDataGrid.RowSelectorSettings.Visibility = Visibility.Visible;
                    break;
                case "ENABLE_MOVING":
                    this.MyDataGrid.ColumnMovingSettings.AllowColumnMoving = ColumnMovingType.Immediate;
                    break;
                case "ENABLE_FIXING":
                    this.MyDataGrid.FixedColumnSettings.AllowFixedColumns = FixedColumnType.Both;
                    break;
                case "ENABLE_RESIZING":
                    this.MyDataGrid.ColumnResizingSettings.AllowColumnResizing = ColumnResizingType.Immediate;
                    break;
                default:
                    break;
            }
        }

        private void CheckBoxTool_Unchecked(object sender, RibbonToolEventArgs e)
        {
            var disableSettings = e.Tool as CheckBoxTool;
            switch (disableSettings.Id)
            {
                case "ENABLE_SORTING":
                    this.MyDataGrid.SortingSettings.AllowSorting = false;
                    break;
                case "ENABLE_EDITING":
                    this.MyDataGrid.EditingSettings.AllowEditing = EditingType.None;
                    break;
                case "ENABLE_ROWSELECTORS":
                    this.MyDataGrid.RowSelectorSettings.Visibility = Visibility.Collapsed;
                    break;
                case "ENABLE_MOVING":
                    this.MyDataGrid.ColumnMovingSettings.AllowColumnMoving = ColumnMovingType.Disabled;
                    break;
                case "ENABLE_FIXING":
                    this.MyDataGrid.FixedColumnSettings.AllowFixedColumns = FixedColumnType.Disabled;
                    break;
                case "ENABLE_RESIZING":
                    this.MyDataGrid.ColumnResizingSettings.AllowColumnResizing = ColumnResizingType.Disabled;
                    break;
                default:
                    break;
            }
        }
    }
}
