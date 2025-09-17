using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Charts;

namespace IGTreemap.Samples
{
    public partial class HeatTreemap : Infragistics.Samples.Framework.SampleContainer
    {
        private TreemapNode _selectedNode = new TreemapNode();

        public HeatTreemap()
        {
            InitializeComponent();
        }

        private void NodeMouseLeftButtonDown(object sender, TreemapNodeClickEventArgs e)
        {
            ManageSelection(e.Node);
        }

        #region Selection

        private void ManageSelection(TreemapNode node)
        {
            // Checks if the new selected node is different from the current selected one
            if (_selectedNode != node)
            {
                ToggleSelection(_selectedNode, Visibility.Collapsed);
                _selectedNode = node;
                ToggleSelection(_selectedNode, Visibility.Visible);
            }
            else
            {
                ToggleSelection(_selectedNode, Visibility.Collapsed);
                // Resets the current selected node
                _selectedNode = new TreemapNode();
            }

            // Set the new data context
            InfoPanel.DataContext = _selectedNode.DataContext;
        }

        // Toggles between Selected and Unselected
        private void ToggleSelection(TreemapNode node, Visibility visibility)
        {
            if (VisualTreeHelper.GetChildrenCount(node) > 0)
            {
                var nodeBorder = VisualTreeHelper.GetChild(node, 0) as Border;
                if (nodeBorder != null)
                {
                    var selectionBorder = nodeBorder.FindName("SelectionBorder") as Border;

                    if (selectionBorder != null)
                    {
                        selectionBorder.Visibility = visibility;
                    }
                }
            }
        }

        #endregion
    }
}
