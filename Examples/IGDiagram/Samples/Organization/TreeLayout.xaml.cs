using Infragistics.Samples.Framework;

namespace IGDiagram.Samples.Data
{
    public partial class TreeLayout : SampleContainer
    {
        public TreeLayout()
        {
            InitializeComponent();
        }

        private void RefreshDiagramLayout(object sender, System.EventArgs e)
        {
            this.Diagram.RefreshLayout(); 
        }
    }
}
