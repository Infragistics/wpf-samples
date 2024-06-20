using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Windows.Reporting;
using Microsoft.Win32;

namespace IGReporting.Samples.Performance
{
    /// <summary>
    /// Interaction logic for PrintingMultiSectionDocument.xaml
    /// </summary>
    public partial class PrintingMultiSectionDocument : SampleContainer
    {
        public PrintingMultiSectionDocument()
        {
            InitializeComponent();
            XmlDataProvider provider = new XmlDataProvider();
            provider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(provider_GetXmlDataCompleted);
            provider.GetXmlDataAsync("Orders.xml");
        }

        /// <summary>
        /// This method create a collection of the DataItems objects by parsing the XML content and set it as ItemsSource.
        /// </summary>
        void provider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;
            XDocument doc = e.Result;
            var dataSource = doc.Element("Orders").Elements("Order").Select(r =>
                new Order
                {
                    Product = r.Element("ProductName").Value,
                    CostPerUnit = decimal.Parse(r.Element("CostPerUnit").Value, CultureInfo.InvariantCulture),
                    Quantity = double.Parse(r.Element("Quantity").Value, CultureInfo.InvariantCulture),
                    Discount = int.Parse(r.Element("Discount").Value),
                    ShipAndHandle = decimal.Parse(r.Element("ShipAndHandle").Value, CultureInfo.InvariantCulture)
                });
            this.XamDataGrid1.DataSource = dataSource.ToList();
        }

        /// <summary>
        /// This method shows how to print XamDataGrid1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            // Create Report object
            Report reportObj = CreateReport();

            // Call print method
            reportObj.Print(true, false);
        }

        /// <summary>
        /// This method shows how to print XamDataGrid1 to an XPS file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "XPS documents|*.xps";
            if (!saveDlg.ShowDialog().GetValueOrDefault())
                return;

            // Create Report object
            Report reportObj = CreateReport();

            // Call export method
            reportObj.Export(OutputFormat.XPS, saveDlg.FileName);
        }

        private Report CreateReport()
        {
            // 1. Create Report object
            Report reportObj = new Report();
            reportObj.ReportSettings.Margin = new Thickness(50, 20, 50, 20);
            reportObj.PageHeaderTemplate = mainGrid.TryFindResource("PagePresenterHeaderTemplate") as DataTemplate;

            // 2. Create EmbeddedVisualReportSection sections for chart and grid. 
            // Put the grid you want to print as a parameter of section's constructor
            EmbeddedVisualReportSection sectionGrid = new EmbeddedVisualReportSection(XamDataGrid1);
            EmbeddedVisualReportSection sectionChart = new EmbeddedVisualReportSection(XamChart1);

            sectionGrid.PageHeader = "XamDataGrid Data";
            sectionChart.PageHeader = "XamDataChart Visual";

            // 3. Add sections to report's section collection
            reportObj.Sections.Add(sectionGrid);
            reportObj.Sections.Add(sectionChart);

            // Optional. If you have a progress indicator, set its Report property to the new Report.
            progressInfo.Report = reportObj;

            return reportObj;
        }
    }
}
