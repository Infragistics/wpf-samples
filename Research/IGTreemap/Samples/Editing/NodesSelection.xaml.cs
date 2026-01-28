using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Charts;

namespace IGTreemap.Samples
{
    public partial class NodesSelection : Infragistics.Samples.Framework.SampleContainer
    {
        private ObservableCollection<TreemapNode> _selectedNodes = new ObservableCollection<TreemapNode>();

        public NodesSelection()
        {
            InitializeComponent();
        }

        private void Treemap_NodeMouseLeftButtonDown(object sender, TreemapNodeClickEventArgs e)
        {
            ManageSelection(e.Node);
        }

        #region Selection

        private void ManageSelection(TreemapNode node)
        {
            //Add your selection logic here
            //Curently there will be multiple selection

            if (_selectedNodes.Contains(node))
            {
                _selectedNodes.Remove(node);
                ToggleSelection(node, Visibility.Collapsed);
            }
            else
            {
                _selectedNodes.Add(node);
                ToggleSelection(node, Visibility.Visible);
            }

            ListBoxSelectedNodes.ItemsSource = _selectedNodes.Select(x => x.Text);
        }

        //Toggles between Selected and Unselected
        private void ToggleSelection(TreemapNode node, Visibility visibility)
        {
            if (VisualTreeHelper.GetChildrenCount(node) > 0)
            {
                Border nodeBorder = VisualTreeHelper.GetChild(node, 0) as Border;
                Border selectionBorder = nodeBorder.FindName("SelectionBorder") as Border;

                if (selectionBorder != null)
                {
                    selectionBorder.Visibility = visibility;
                }
            }
        }

        #endregion
    }
}
