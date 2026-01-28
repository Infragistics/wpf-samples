using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using IGNetworkNode.Resources;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;

namespace IGNetworkNode.Samples.Display
{
    public partial class ShowHideNodes : Infragistics.Samples.Framework.SampleContainer
    {
        public ShowHideNodes()
        {
            InitializeComponent();
        }

        private void btn_ShowHide_Click(object sender, RoutedEventArgs e)
        {
            // Message counter to make sure the message is displayed only once
            int msgCtr = 0; 

            // Hold one or more selected child node to unselect
            var nodesToUnselect = new List<NetworkNodeNode>();

            if (xnn.SelectedNodes.Count > 0)
            {
                foreach (NetworkNodeNode node in xnn.SelectedNodes)
                {
                    if (node.OutgoingConnections.ToString().Length > 0)
                    {
                        if (node.IsExpanded)
                        {
                            // Checks to see it the node has child nodes that it is connected to.
                            node.IsExpanded = false;
                        }
                        else
                        {
                            node.IsExpanded = true;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("2"); 
                        nodesToUnselect.Add(node);
                    }
                }

                // Unselect the child nodes that are not to be shown / hidden
                foreach (NetworkNodeNode childNodeList in nodesToUnselect)
                {
                    if (msgCtr == 0)
                        MessageBox.Show(NetworkNodeStrings.XNN_ChildNodeNotFound);

                    msgCtr++;
                    childNodeList.IsSelected = false;
                }
            }
            else
            {
                MessageBox.Show(NetworkNodeStrings.XNN_SelectNode);
            }
        }

        private void cb_EnableExpansion_Checked(object sender, RoutedEventArgs e)
        {
            xnn.ExpansionIndicatorVisibility = Visibility.Visible;
            xnn.SelectedNodes.Clear();
        }

        private void cb_EnableExpansion_Unchecked(object sender, RoutedEventArgs e)
        {
            xnn.ExpansionIndicatorVisibility = Visibility.Collapsed;
        }

    }

    public class SampleTestData
    {
        public ObservableCollection<NodeModel> Nodes { get; set; }

        private const int K = 3; // number of connections per node (maximum)
        private const int NUM_NODES = 18; // number of nodes in the graph

        public SampleTestData()
        {
            Nodes = new ObservableCollection<NodeModel>();

            // add NUM_NODES node objects to the collection
            for (int i = 0; i < NUM_NODES; i++)
            {
                NodeModel node = new NodeModel();
                node.Label = i.ToString();
                node.ToolTip = "ToolTip for " + node.Label;
                Nodes.Add(node);
            }

            int root = 0;
            int first = 1;
            int last = K;
            while (first < Nodes.Count)
            {
                Nodes[root].Connections = new ObservableCollection<NetworkConnection>();
                for (int i = first; i <= last; i++)
                {
                    if (i >= Nodes.Count)
                    {
                        break;
                    }
                    Nodes[root].Connections.Add(new NetworkConnection { Target = Nodes[i] });
                }
                root++;
                first = last + 1;
                last += K;
            }
        }
    }

}
