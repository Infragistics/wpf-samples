using System.Linq;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;

namespace IGOrgChart.Samples.Editing
{
    public partial class IndividualNodeProperties : SampleContainer
    {
        public IndividualNodeProperties()
        {
            InitializeComponent();
        }

        private void SelectedNodesCollectionChanged(object sender, OrgChartNodeSelectionEventArgs e)
        {
            NodeOptionsPanel.DataContext = e.CurrentSelectedNodes.FirstOrDefault();
        }
    }
}
