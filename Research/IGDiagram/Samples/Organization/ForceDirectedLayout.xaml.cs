using Infragistics.Samples.Framework;

namespace IGDiagram.Samples.Data
{
    public partial class ForceDirectedLayout : SampleContainer
    {
        public ForceDirectedLayout()
        {
            InitializeComponent();
        }

        private void RefreshDiagramLayout(object sender, System.EventArgs e)
        {
            this.Diagram.RefreshLayout();
        }
    }   
}
