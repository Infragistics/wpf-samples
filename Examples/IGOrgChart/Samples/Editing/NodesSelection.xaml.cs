using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;

namespace IGOrgChart.Samples.Editing
{
    public partial class NodesSelection : SampleContainer
    {
        public NodesSelection()
        {
            InitializeComponent();
        }

        private void OrgChart_SelectedNodesCollectionChanged(object sender, OrgChartNodeSelectionEventArgs e)
        {
            //Get the selected nodes.
            ListBoxSelectedNodes.ItemsSource = e.CurrentSelectedNodes;
        }

        private void SelectAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OrgChart.SelectedNodes.Clear();
            
            var allNodes = OrgChart.RootNode.HighlightChildren(OrgChart.ActualDepth);

            foreach (var node in allNodes)
            {
                OrgChart.SelectedNodes.Add(node);
            }
        }

        private void ClearSelection_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OrgChart.SelectedNodes.Clear();
        }
    }
}
