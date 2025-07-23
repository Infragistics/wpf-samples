using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Windows.Reporting;
using Microsoft.Win32;

namespace IGReporting.Samples.Organization
{
    /// <summary>
    /// Interaction logic for ScalingAndPrintOrder.xaml
    /// </summary>
    public partial class ScalingAndPrintOrder : SampleContainer
    {
        public ScalingAndPrintOrder()
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
            Report reportObj = new Report();
            // set print order. it can be first left than down (horizontal) or first(down than left(vertical)
            reportObj.ReportSettings.PagePrintOrder = (PagePrintOrder)cmbPagePrintOrder.Value;
            // set scale mode
            reportObj.ReportSettings.HorizontalPaginationMode = (HorizontalPaginationMode)cmbHorizontalPaginationMode.Value;

            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(XamDataGrid1);
            reportObj.Sections.Add(section);

            progressInfo.Report = reportObj;
            reportObj.Print(true, false);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "XPS documents|*.xps";
            if (!saveDlg.ShowDialog().GetValueOrDefault())
                return;

            Report reportObj = new Report();
            // set print order. it can be first left than down (horizontal) or first(down than left(vertical)
            reportObj.ReportSettings.PagePrintOrder = (PagePrintOrder)cmbPagePrintOrder.Value;
            // set scale mode
            reportObj.ReportSettings.HorizontalPaginationMode = (HorizontalPaginationMode)cmbHorizontalPaginationMode.Value;

            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(XamDataGrid1);
            reportObj.Sections.Add(section);

            progressInfo.Report = reportObj;
            reportObj.Export(OutputFormat.XPS, saveDlg.FileName);
        }
    }
}
