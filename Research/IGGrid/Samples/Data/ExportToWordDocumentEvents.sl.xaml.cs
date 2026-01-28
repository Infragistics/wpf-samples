using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Word;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Data
{
    public partial class ExportToWordDocumentEvents : SampleContainer
    {
        private XamGridWordWriter _wordWriter;
        private bool _isExportSuccessful = true;

        public ExportToWordDocumentEvents()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        #region Events Handlers

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // Populate xamGrid with sample data
            DownloadDataSource();

            _wordWriter = this.Resources["wordWriter"] as XamGridWordWriter;
        }

        private void Btn_Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog { DefaultExt = "docx", Filter = "Word Document (.docx)|*.docx" };
            Stream stream = null;
            if (saveDialog.ShowDialog() == true)
            {
                try
                {
                    stream = saveDialog.OpenFile();

                    // Export xamGrid data asynchronously to a Word document
                    _wordWriter.ExportAsync(xamGrid, stream);
                    _isExportSuccessful = true;
                }
                catch (IOException exc)
                {
                    _isExportSuccessful = false;
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_Log.Items.Count > 0)
            {
                ListBox_Log.Items.Clear();
            }
        }

        #endregion Events Handlers

        #region Export Events Handlers
        private void XamGridWordWriter_ExportStarted(object sender, ExportStartedEventArgs e)
        {
            // Set default font
            e.Writer.DefaultFont.Name = "Calibri";
            e.Writer.DefaultFont.Size = 10;
            e.Writer.DefaultFont.ForeColor = Color.FromArgb(255, 54, 95, 145);

            e.Writer.DefaultTableProperties.BorderProperties.Width = 0;
            e.Writer.DefaultTableProperties.BorderProperties.Color = Colors.White;

            // Set some metadata attributes to the exported Word document
            e.WordDocumentProperties.Title = "Exported Silverlight XamGrid Data";

            LogEventOutput("ExportStarted: " + GridStrings.WordExport_EventsInfo3);
        }

        private void XamGridWordWriter_RowExporting(object sender, RowExportingEventArgs e)
        {
            LogEventOutput("RowExporting: " + e.RowType.ToString());
        }

        private void XamGridWordWriter_GroupByRowCellExporting(object sender, GroupByRowCellExportingEventArgs e)
        {
            // Set border settings to the group by row
            e.TableCellProperties.BackColor = Colors.White;
            e.TableCellProperties.BorderProperties.Color = Color.FromArgb(255, 79, 129, 189);
            e.TableCellProperties.BorderProperties.Sides = TableBorderSides.TopAndBottom;
            e.TableCellProperties.BorderProperties.Width = 2;

            LogEventOutput("GroupByRowCellExporting: " + GridStrings.WordExport_EventsInfo1);
        }

        private void XamGridWordWriter_HeaderCellExporting(object sender, CellExportingEventArgs e)
        {
            // Set styling to the header cells
            e.TableCellProperties.BackColor = Color.FromArgb(255, 211, 223, 238);
            e.TableCellProperties.BorderProperties.Width = 1;
            e.TableCellProperties.BorderProperties.Sides = TableBorderSides.Bottom;
            e.TableCellProperties.BorderProperties.Color = Color.FromArgb(255, 79, 129, 189);

            LogEventOutput("HeaderCellExporting");
        }

        private void XamGridWordWriter_CellExporting(object sender, CellExportingEventArgs e)
        {
            // Aply border style to cells
            e.TableCellProperties.BorderProperties.Sides = TableBorderSides.Bottom;
            e.TableCellProperties.BorderProperties.Width = 1;
            e.TableCellProperties.BorderProperties.Color = Color.FromArgb(255, 211, 223, 238);

            // Check values in "UnitsInStock" column and mark them
            if (e.Column.Key == "UnitsInStock" && (int)e.Row.Cells["UnitsInStock"].Value < 20)
            {
                e.TableCellProperties.BorderProperties.Color = Colors.Red;
                e.TableCellProperties.BorderProperties.Sides = TableBorderSides.All;
                e.TableCellProperties.BorderProperties.Width = 1;

                e.TableCellProperties.BackColor = Colors.Yellow;
            }

            // Check values in "UnitPrice" column and mark them
            if (e.Column.Key == "UnitPrice" && decimal.Parse(e.Row.Cells["UnitPrice"].Value.ToString()) == 0)
            {
                e.TableCellProperties.BackColor = Colors.Red;
            }

            LogEventOutput("CellExporting: " + GridStrings.WordExport_EventsInfo2);
        }

        private void XamGridWordWriter_ColumnLayoutHeaderRowCellExporting(object sender, ColumnLayoutHeaderRowCellExportingEventArgs e)
        {
            LogEventOutput("ColumnLayoutHeaderRowCellExporting");
        }

        private void XamGridWordWriter_SummaryRowExporting(object sender, SummaryRowExportingEventArgs e)
        {
            LogEventOutput("SummaryRowExporting");
        }

        private void XamGridWordWriter_SummaryCellExporting(object sender, SummaryCellExportingEventArgs e)
        {
            if (e.SummaryValue != null)
            {
                LogEventOutput("SummaryCellExporting: " + e.SummaryValue);
            }
        }

        private void XamGridWordWriter_ExportEnding(object sender, ExportEndingEventArgs e)
        {
            LogEventOutput("ExportEnding");
        }

        private void XamGridWordWriter_ExportEnded(object sender, System.EventArgs e)
        {
            LogEventOutput("ExportEnded");

            if (_isExportSuccessful)
            {
                MessageBox.Show(GridStrings.WordExport_Msg);
            }

        }
        #endregion Export Events Handlers

        #region Data
        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Products.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Products")
                              select new Product
                              {
                                  SKU = d.Element("ProductID").GetString(),
                                  Name = d.Element("ProductName").GetString(),
                                  Category = d.Element("Category").GetString(),
                                  Supplier = d.Element("Supplier").GetString(),
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  UnitsOnOrder = d.Element("UnitsOnOrder").GetInt(),
                                  QuantityPerUnit = d.Element("QuantityPerUnit").GetString(),
                              });

            this.xamGrid.ItemsSource = dataSource.ToList();

        }

        #endregion Data

        private void LogEventOutput(string message)
        {
            ListBoxItem item = new ListBoxItem
            {
                Content = message
            };

            ListBox_Log.Items.Add(item);
        }

    }
}
