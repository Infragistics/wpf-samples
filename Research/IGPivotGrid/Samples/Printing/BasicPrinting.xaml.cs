using Infragistics.Samples.Framework;
using Infragistics.Windows.Reporting;
using Microsoft.Win32;
using System;
using System.Windows;

namespace IGPivotGrid.Samples.Printing
{
    public partial class BasicPrinting : SampleContainer
    {

        Report report;

        private Uri _themePrint = new Uri("/IGPivotGrid;component/Assets/XamPivotGrid.Printing.xaml", UriKind.RelativeOrAbsolute);

        public BasicPrinting()
        {
            InitializeComponent();             
        }

        /// <summary>
        /// This method shows how to print XamPivotGrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            GenerateReport();

            // 6. Put report to preview control
            this.xamReportPreview.GeneratePreview(report, false, false);

            tbiPreview.IsSelected = true;
        }

        /// <summary>
        /// This method shows how to print XamPivotGrid in printer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            GenerateReport();              

            // 6. Call print method
            report.Print(true, false);
        }

        /// <summary>
        /// This method shows how to print XamPivotGrid in XPS file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            GenerateReport();
            
            // 6. Call export method
            report.Export(OutputFormat.XPS, "xamPivotGrid_Report", true);
        }

        void GenerateReport()
        {
            // 1. Create Report object
            report = new Report();

            // 2. Create EmbeddedVisualReportSection section. 
            // Put the grid you want to print as a parameter of section's constructor
            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(this.xamPivotGrid);

            // 3. (Optional) Apply a theme to the report (Printing Theme)
            section.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = _themePrint });

            // 4. Add created section to report's section collection
            report.Sections.Add(section);

            // 5. (Optional) If you have progress indicator set its Report property to created report
            progressInfo.Report = report;
        }

    }
}
