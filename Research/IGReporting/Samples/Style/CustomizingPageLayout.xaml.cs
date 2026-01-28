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
    /// Interaction logic for CustomizingPageLayout.xaml
    /// </summary>
    public partial class CustomizingPageLayout : SampleContainer
    {
        public CustomizingPageLayout()
        {
            InitializeComponent();
            XmlDataProvider provider = new XmlDataProvider();
            provider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(provider_GetXmlDataCompleted);
            provider.GetXmlDataAsync("Movies.xml");
        }

        /// <summary>
        /// This method create a collection of the DataItems objects by parsing the XML content and set it as ItemsSource.
        /// </summary>
        void provider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;
            XDocument doc = e.Result;
            var dataSource = doc.Element("NewDataSet").Elements("TopMovies").Select(r =>
                new Movie
                {
                    Title = r.Element("Title").Value,
                    Duration = r.Element("Running_x0020_Time").Value,
                    Release = r.Element("Release_x0020_Date").Value,
                    MPAA = r.Element("MPAA_x0020_Rating").Value,
                    Critics = r.Element("Critics_x0020_Rating").Value,
                    CumulativeGross = r.Element("Cumulative_x0020_Gross").Value
                });
            this.XamDataGrid1.DataSource = dataSource.ToList();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Report reportObj = new Report();
            reportObj.ReportSettings.HorizontalPaginationMode = HorizontalPaginationMode.Mosaic;
            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(XamDataGrid1);
            section.PagePresenterStyle = this.TryFindResource("PagePresenterStyle") as System.Windows.Style;
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
            reportObj.ReportSettings.HorizontalPaginationMode = HorizontalPaginationMode.Mosaic;
            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(XamDataGrid1);
            section.PagePresenterStyle = this.TryFindResource("PagePresenterStyle") as System.Windows.Style;
            reportObj.Sections.Add(section);

            progressInfo.Report = reportObj;
            reportObj.Export(OutputFormat.XPS, saveDlg.FileName);
        }
    }
}
