using Infragistics.Samples.Framework;
using System.Windows;

namespace IGDiagram.Samples.Display
{
    public partial class LineJumps : SampleContainer
    {
        public LineJumps()
        {
            InitializeComponent();
        }

        private void XamDiagramToolbox_Loaded(object sender, RoutedEventArgs e)
        {
            this.xamDiagramToolbox.Categories[0].IsExpanded = false;
            this.xamDiagramToolbox.Categories[1].IsExpanded = false;
            this.xamDiagramToolbox.Categories[2].IsExpanded = true;
        }
    }
}
