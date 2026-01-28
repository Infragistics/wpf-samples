using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Windows.Reporting;
using Microsoft.Win32;

namespace IGReporting.Samples.Style
{
    /// <summary>
    /// Interaction logic for CustomizingHeaderFooter.xaml
    /// </summary>
    public partial class CustomizingHeaderFooter : SampleContainer
    {
        public CustomizingHeaderFooter()
        {
            InitializeComponent();
            XmlDataProvider provider = new XmlDataProvider();
            provider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(provider_GetXmlDataCompleted);
            provider.GetXmlDataAsync("Quarterbacks.xml");
        }

        /// <summary>
        /// This method create a collection of the DataItems objects by parsing the XML content and set it as ItemsSource.
        /// </summary>
        void provider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;
            XDocument doc = e.Result;
            var dataSource = doc.Element("QuarterBack").Elements("season").Select(r =>
                new Season
                {
                    Year = r.Attribute("year").Value,
                    Team = r.Element("team").Value,
                    Games = r.Element("games").Value,
                    Brate = r.Element("qbrate").Value,
                    Comp = r.Element("comp").Value,
                    Att = r.Element("att").Value,
                    Pct = r.Element("pct").Value
                });
            this.XamDataGrid1.DataSource = dataSource.ToList();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Report reportObj = CreateReport();

            progressInfo.Report = reportObj;
            reportObj.Print(true, false);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "XPS documents|*.xps";
            if (!saveDlg.ShowDialog().GetValueOrDefault())
                return;

            Report reportObj = CreateReport();

            progressInfo.Report = reportObj;
            reportObj.Export(OutputFormat.XPS, saveDlg.FileName);
        }

        private Report CreateReport()
        {
            Report reportObj = new Report();
            // set scale mode
            reportObj.ReportSettings.HorizontalPaginationMode = HorizontalPaginationMode.Mosaic;

            // aplly header template
            reportObj.PageHeaderTemplate = this.Resources["PagePresenterHeaderTemplate"] as DataTemplate;
            //apply footer template
            reportObj.PageFooterTemplate = this.Resources["PagePresenterFooterTemplate"] as DataTemplate;
            // apply header content. In our case palin text
            reportObj.PageHeader = tbPageHeader.Text;
            // apply  footer content. In our case palin text
            reportObj.PageFooter = tbPageFooter.Text;
            reportObj.ReportSettings.Margin = new Thickness(50, 0, 50, 0);
            // create section and add it to report's section collection
            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(XamDataGrid1);
            reportObj.Sections.Add(section);
            return reportObj;
        }
    }
}
