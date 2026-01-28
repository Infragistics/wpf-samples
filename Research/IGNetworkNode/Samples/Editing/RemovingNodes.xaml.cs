using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using IGNetworkNode.Resources;

namespace IGNetworkNode.Samples.Editing
{
    public partial class RemovingNodes : Infragistics.Samples.Framework.SampleContainer
    {
        public RemovingNodes()
        {
            InitializeComponent();
        }

        // Node Selection Changed Event
        private void xnn_SelectedNodesCollectionChanged(object sender, Infragistics.Controls.Maps.NetworkNodeSelectionEventArgs e)
        {
            for (int i = 0; i < xnn.SelectedNodes.Count; i++)
            {
                this.tb_DisaplayNode.Text = ((NodeModel)xnn.SelectedNodes[i].Data).Label;
            }
        }

        private void bth_RemoveNodeFromView_Click(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.SelectedNodes)
            {
                xnn.SelectedNodes[0].Visibility = System.Windows.Visibility.Collapsed;
                this.tb_DisaplayNode.Text = string.Empty;
            }
        }

        // Remove Nodes from the data source
        private void btn_RemoveNodeFromSource_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<NodeModel> nModel = xnn.ItemsSource as ObservableCollection<NodeModel>;
            if (xnn.SelectedNodes.Count > 0)
            {
                if (xnn.SelectedNodes[0].IncomingConnections.Count() < 1)
                {
                    nModel.Remove((NodeModel)xnn.SelectedNodes[0].Data);
                    this.tb_DisaplayNode.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show(NetworkNodeStrings.XNN_Cannot_Remove_Node);
                }
            }
        }

        private void btn_RestoreNodeIntoView_Click(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                if (node.Visibility == Visibility.Collapsed)
                    node.Visibility = Visibility.Visible;
            }
        }
    }
}
