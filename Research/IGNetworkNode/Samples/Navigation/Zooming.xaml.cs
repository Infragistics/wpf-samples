using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using IGNetworkNode.Resources;
using Infragistics.Samples.Shared.Models;

namespace IGNetworkNode.Samples.Navigation
{
    public partial class Zooming : Infragistics.Samples.Framework.SampleContainer
    {
        public Zooming()
        {
            InitializeComponent();
            SliderZoomLevel.ValueChanged += SliderZoomLevel_ValueChanged;
            SliderZoomLevel.Value = 1;
        }

        private void ButtonScaleToFit_Click(object sender, RoutedEventArgs e)
        {
            //Scale the contents to fit the Network Node.
            xnn.ScaleToFit();
            this.SliderZoomLevel.Value = xnn.ZoomLevel;
        }

        private void ButtonZoomTo100_Click(object sender, RoutedEventArgs e)
        {
            //Zoom the nodes to 100%.
            xnn.ZoomTo100();
            this.SliderZoomLevel.Value = xnn.ZoomLevel;
        }

        private void ButtonZoomIn_Click(object sender, RoutedEventArgs e)
        {
            xnn.ZoomIn();
            this.SliderZoomLevel.Value = xnn.ZoomLevel;
        }

        private void ButtonZoomOut_Click(object sender, RoutedEventArgs e)
        {
            xnn.ZoomOut();
            this.SliderZoomLevel.Value = xnn.ZoomLevel;
            xnn.SelectedNodes.Clear();
        }

        private void SliderZoomLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            xnn.ZoomLevel = SliderZoomLevel.Value;
        }

        private void NetworkNode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ZoomLevel")
            {
                SliderZoomLevel.Value = xnn.ZoomLevel;
            }
        }

        private void xnn_SelectedNodesCollectionChanged(object sender, Infragistics.Controls.Maps.NetworkNodeSelectionEventArgs e)
        {
            if (this.rb_EnableSingleNodeZooming.IsChecked == true)
            {
                this.xnn.ZoomSelectedNodes();
                this.SliderZoomLevel.Value = xnn.ZoomLevel;
            }
        }

        private void rb_EnableSingleNodeZooming_Checked(object sender, RoutedEventArgs e)
        {
            xnn.SelectedNodes.Clear();
            xnn.SelectionType = Infragistics.Controls.Maps.NetworkNodeSelectionType.Single;
            this.tb_CharacterEntry.Text = string.Empty;
            this.tb_CharacterEntry.IsEnabled = false;
        }

        private void rb_EnableMultiNodeZooming_Checked(object sender, RoutedEventArgs e)
        {
            xnn.SelectedNodes.Clear();
            xnn.SelectionType = Infragistics.Controls.Maps.NetworkNodeSelectionType.Multiple;
            this.tb_CharacterEntry.Text = string.Empty;
            this.tb_CharacterEntry.IsEnabled = false;
        }

        private void rb_ZoomSpecificNode_Checked(object sender, RoutedEventArgs e)
        {
            this.xnn.SelectedNodes.Clear();
            this.tb_CharacterEntry.IsEnabled = true;
            this.tb_CharacterEntry.Text = NetworkNodeStrings.XNN_SearchNode;
        }

        private void tb_CharacterEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tb_CharacterEntry.Text = string.Empty;
            xnn.SelectedNodes.Clear();
        }

        private void btn_ZoomSelection_Click(object sender, RoutedEventArgs e)
        {
            if (xnn.SelectedNodes.Count > 0)
            {
                this.xnn.ZoomSelectedNodes();
                this.SliderZoomLevel.Value = xnn.ZoomLevel;
                return;
            }
            else
            {
                if (this.rb_EnableMultiNodeZooming.IsChecked == true)
                {
                    MessageBox.Show(NetworkNodeStrings.XNN_Msg_ForSelectingNodes);
                    return;
                }
            }

            if (tb_CharacterEntry.Text == string.Empty)
            {
                MessageBox.Show(NetworkNodeStrings.XNN_Msg_ForEntringCharacters);
                return;
            }

            // Search and add the retrived nodes into the selected nodes collection
            foreach (var node in xnn.Search((NodeModel item) => item.Label.StartsWith(tb_CharacterEntry.Text)))
            {
                xnn.SelectedNodes.Add(node);
            }

            // Zoom in on the selected nodes
            if (xnn.SelectedNodes.Count > 0)
            {
                xnn.ZoomNodes(xnn.SelectedNodes);
                this.SliderZoomLevel.Value = xnn.ZoomLevel;
            }
            else
            {
                MessageBox.Show(NetworkNodeStrings.XNN_Msg_NodeNotFound);
            }

            this.tb_CharacterEntry.Text = string.Empty;
        }
    }


    // Sample test data
    public class SampleTestData
    {
        public ObservableCollection<NodeModel> Nodes { get; set; }

        private const int K = 3;            // number of connections per node (maximum)
        private const int NUM_NODES = 98;   // number of nodes in the graph

        public SampleTestData()
        {
            Nodes = new ObservableCollection<NodeModel>();

            // Add NUM_NODES node objects to the collection
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
