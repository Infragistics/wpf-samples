using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class NetworkNodeLargeData
    {
        private const int NodesToDisplay = 500;
        public ObservableCollection<NodeModel> Nodes { get; set; }
        private List<NodeModel> _level0;
        private List<NodeModel> _level1;
        private List<NodeModel> _level2;

        public NetworkNodeLargeData()
        {
            PopulateNodes();
        }

        private void PopulateNodes()
        {
            Nodes = new ObservableCollection<NodeModel>();
            int curIndex = 0;
            _level0 = new List<NodeModel>();
            _level1 = new List<NodeModel>();
            _level2 = new List<NodeModel>();

            for (int i = 0; i < 3; i++)
            {
                var node = new NodeModel();
                node.Label = curIndex.ToString();
                node.ToolTip = node.Label;
                node.Connections = new ObservableCollection<NetworkConnection>();
                _level0.Add(node);
                if (i > 0)
                {
                    node.Connections.Add(new NetworkConnection { Target = _level0[i - 1], Weight = 1 });
                }
                Nodes.Add(node);
                curIndex++;
            }
            
            for (int i = 0; i < 100; i++)
            {
                var node = new NodeModel();
                node.Label = curIndex.ToString();
                node.ToolTip = node.Label;
                node.Connections = new ObservableCollection<NetworkConnection>();
                node.Connections.Add(new NetworkConnection { Target = _level0[i % _level0.Count], Weight = 1 });
                _level1.Add(node);
                Nodes.Add(node);
                curIndex++;
            }

            int n = Nodes.Count;

            for (int i = 0; i < NodesToDisplay - n; i++) // bring the number of nodes to NODES_TO_DISPLAY
            {
                var node = new NodeModel();
                node.Label = curIndex.ToString();
                node.ToolTip = node.Label;
                node.Connections = new ObservableCollection<NetworkConnection>();
                node.Connections.Add(new NetworkConnection { Target = _level1[i % _level1.Count], Weight = 1 });
                _level2.Add(node);
                Nodes.Add(node);
                curIndex++;
            }
        }
    }
}
