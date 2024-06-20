using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Reporting;
using Microsoft.Win32;

namespace IGReporting.Samples.Performance
{
    /// <summary>
    /// Interaction logic for PrintingAnyVisualElement.xaml
    /// </summary>
    public partial class PrintingAnyVisualElement : SampleContainer
    {
        public PrintingAnyVisualElement()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            // 1. Create Report object
            Report reportObj = new Report();

            // 2. Create EmbeddedVisualReportSection section. 
            // Put the visual you want to print as a parameter of section's constructor
            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(XamChart1);

            // 3. Add created section to report's section collection
            reportObj.Sections.Add(section);

            // Optional. If you have progress indicator set its Report property to created report
            progressInfo.Report = reportObj;

            // 4. Call print method
            reportObj.Print(true, false);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "XPS documents|*.xps";
            if (!saveDlg.ShowDialog().GetValueOrDefault())
                return;

            // 1. Create Report object
            Report reportObj = new Report();

            // 2. Create EmbeddedVisualReportSection section. 
            // Put the visual you want to print as a parameter of section's constructor
            EmbeddedVisualReportSection section = new EmbeddedVisualReportSection(XamChart1);

            // 3. Add created section to report's section collection
            reportObj.Sections.Add(section);

            // Optional. If you have progress indicator set its Report property to created report
            progressInfo.Report = reportObj;

            // 4. Call Export method and put the file name you want to create
            reportObj.Export(OutputFormat.XPS, saveDlg.FileName);
        }
    }
}
