using Infragistics.Samples.Framework;

namespace IGDiagram.Samples.Display
{
    public partial class DiagramToolbox : SampleContainer
    {
        public DiagramToolbox()
        {
            InitializeComponent();

            xamDiagramToolbox.Categories[0].IsExpanded = false;
            xamDiagramToolbox.Categories[1].IsExpanded = false;
        }
    }
}
