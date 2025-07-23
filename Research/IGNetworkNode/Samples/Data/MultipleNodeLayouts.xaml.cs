using System.Collections.ObjectModel;
using Infragistics.Samples.Shared.Models;
using IGNetworkNode.Resources;

namespace IGNetworkNode.Samples.Data
{
    public partial class MultipleNodeLayouts : Infragistics.Samples.Framework.SampleContainer
    {
        public MultipleNodeLayouts()
        {
            InitializeComponent();

            var nodes = new ObservableCollection<NodeModel>();

            var rootNode = new NodeModel();
            rootNode.Label = NetworkNodeStrings.Item + " 0 (NodeModel)";
            rootNode.Connections = new ObservableCollection<NetworkConnection>();
            nodes.Add(rootNode);

            for (int i = 1; i < 7; i++)
            {
                NodeModel node;
                if (i <= 3)
                {
                    node = new NodeModel();
                    node.Label = NetworkNodeStrings.Item + " " + i.ToString() + " (NodeModel)";
                }
                else
                {
                    node = new SpecialNodeModel();
                    node.Label = NetworkNodeStrings.Item + " " + i.ToString() + " (SpecialNodeModel)";
                    
                }
                node.Connections = new ObservableCollection<NetworkConnection>();
                node.Connections.Add(new NetworkConnection { Target = rootNode });
                nodes.Add(node);
            }
            xnn.ItemsSource = nodes;
        }
    }

    public class SpecialNodeModel : NodeModel
    {
        
    }
}
