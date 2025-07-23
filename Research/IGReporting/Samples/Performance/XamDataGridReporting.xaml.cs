using System;
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
    /// Interaction logic for XamDataGridReporting.xaml
    /// </summary>
    public partial class XamDataGridReporting : SampleContainer
    {
        public XamDataGridReporting()
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

        /// <summary>
        /// This method shows how to print XamDataGrid1 in printer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            // 1. Create Report object
            Report reportObj = new Report();

            // 2. Create EmbeddedVisualReportSection section. 
            // Put the grid you want to print as a parameter of section's constructor
            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(XamDataGrid1);

            // 3. Add created section to report's section collection
            reportObj.Sections.Add(section);

            // Optional. If you have progress indicator set its Report property to created report
            progressInfo.Report = reportObj;

            // 4. Call print method
            reportObj.Print(true, false);
        }

        /// <summary>
        /// This method shows how to print XamDataGrid1 in XPS file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "XPS documents|*.xps";
            if (!saveDlg.ShowDialog().GetValueOrDefault())
                return;

            // 1. Create Report object
            Report reportObj = new Report();

            // 2. Create EmbeddedVisualReportSection section. 
            // Put the grid you want to print as a parameter of section's constructor
            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(XamDataGrid1);

            // 3. Add created section to report's section collection
            reportObj.Sections.Add(section);

            // Optional. If you have progress indicator set its Report property to created report
            progressInfo.Report = reportObj;

            // 4. Call export method
            reportObj.Export(OutputFormat.XPS, saveDlg.FileName);
        }
    }
}
