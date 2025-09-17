using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.Reporting;

namespace IGReporting.Samples.Organization
{
    /// <summary>
    /// Interaction logic for AutoDocPagination.xaml
    /// </summary>
    public partial class AutoDocPagination : SampleContainer
    {
        public AutoDocPagination()
        {
            InitializeComponent();
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);
            this.XamDataGrid1.DataSource = dt.DefaultView;
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
        /// This method shows how to print XamDataGrid1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            // Create report and add visual to print
            Report reportObj = new Report();

            // Create section with chart
            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(XamDataGrid1);
            reportObj.Sections.Add(section);
            // Put report to progress control
            progressInfo.Report = reportObj;

            // Put report to preview control
            XamReportPreview1.GeneratePreview(reportObj, false, false);

            tbiPreview.IsSelected = true;
        }
    }
}
