using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Infragistics.Samples.Shared.Resources;

namespace Infragistics.Samples.Shared.Models
{
    public class NetworkNodeCompanyData
    {
        public ObservableCollection<NodeModel> Nodes { get; set; }
        private NodeModel _rootNode;

        private static readonly String[] Men = ModelStrings.XWM_NetNodes_Men.Split(';');
        private static readonly String[] Women = ModelStrings.XWM_NetNodes_Women.Split(';');
        private static readonly String[] Titles = ModelStrings.XWM_NetNodes_Titles.Split(';');
        private static readonly String[] Companies = ModelStrings.XWM_NetNodes_Companies.Split(';');

        public NetworkNodeCompanyData()
        {
            PopulateNodes();
        }

        private void PopulateNodes()
        {
            Nodes = new ObservableCollection<NodeModel>();

            _rootNode = new NodeModel();
            _rootNode.Label = Companies[0];
            _rootNode.ToolTip = _rootNode.Label;
            _rootNode.NodeType = NodeModel.NodeModelType.Company;
            Nodes.Add(_rootNode);

            List<string> names = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                names.Add(Men[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                names.Add(Women[i]);
            }

            for (int i = 0; i < names.Count; i++)
            {
                var node = new NodeModel();
                node.Label = "";
                String[] tokens = names[i].Split(' ');
                foreach (string t in tokens)
                {
                    node.Label += t[0];
                }
                node.ToolTip = names[i];
                node.Connections = new ObservableCollection<NetworkConnection>();
                node.Connections.Add(new NetworkConnection { Target = _rootNode, Weight = 1 });
                node.NodeType = NodeModel.NodeModelType.Default;
                node.Description = Titles[i % Titles.Length];
                Nodes.Add(node);
            }
        }
    }
}
