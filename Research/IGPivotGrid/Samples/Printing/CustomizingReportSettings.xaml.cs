using IGPivotGrid.Resources;
using Infragistics.Olap.Data;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Windows.Reporting;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace IGPivotGrid.Samples.Printing
{
    public partial class CustomizingReportSettings : SampleContainer
    {
        Report report;

        public CustomizingReportSettings()
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

            // 7. Put report to preview control
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

            // 7. Call print method
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

            // 7. Call export method
            report.Export(OutputFormat.XPS, "xamPivotGrid_Report", true);
        }

        void GenerateReport()
        {
            // 1. Create Report object
            report = new Report();

            // 2. Applying report's settings
            report.ReportSettings.HorizontalPaginationMode = (HorizontalPaginationMode)cmbHorizontalPaginationMode.Value;
            report.ReportSettings.PagePrintOrder = (PagePrintOrder)cmbPagePrintOrder.Value;
             
            // 3. Create EmbeddedVisualReportSection section. 
            // Put the grid you want to print as a parameter of section's constructor
            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(this.xamPivotGrid);

            // 4. Add created section to report's section collection
            report.Sections.Add(section);

            // 5. (Optional) If you have progress indicator set its Report property to created report
            progressInfo.Report = report;
        }


        private void HeaderTemplate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show(((IMember)btn.DataContext).Caption, PivotGridStrings.XPG_HeaderClicked, MessageBoxButton.OK);
        }

        private void DataCellTemplate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show(btn.DataContext.ToString(), PivotGridStrings.XPG_DataCellClicked, MessageBoxButton.OK);
        }
    }
}
