using System;
using System.Collections.ObjectModel;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Resources;
using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
    public class NetworkNodeAttendanceData
    {
        public ObservableCollection<NodeModel> Nodes { get; set; }
        private List<NodeModel> _companyNodes;
        private NodeModel _rootNode;

        private static readonly String[] Men = ModelStrings.XWM_NetNodes_Men.Split(';');
        private static readonly String[] Women = ModelStrings.XWM_NetNodes_Women.Split(';');
        private static readonly String[] Titles = ModelStrings.XWM_NetNodes_Titles.Split(';');
        private static readonly String[] Companies = ModelStrings.XWM_NetNodes_Companies.Split(';');

        public NetworkNodeAttendanceData()
        {
            PopulateNodes();
        }

        private void PopulateNodes()
        {
            Nodes = new ObservableCollection<NodeModel>();
            _companyNodes = new List<NodeModel>();

            _rootNode = new NodeModel();
            _rootNode.Label = ModelStrings.XWM_NetNodes_UICON;
            _rootNode.ToolTip = ModelStrings.XWM_NetNodes_FirstAnnualUIConvention;
            _rootNode.NodeType = NodeModel.NodeModelType.Convention;
            _rootNode.Description = ModelStrings.XWM_NetNodes_Unite;
            _rootNode.ImagePath = ImageFilePathProvider.GetImageLocalPath() + "Texture/Texture01.jpg";
            Nodes.Add(_rootNode);

            for (int i = 0; i < Companies.Length; i++)
            {
                var node = new NodeModel();
                node.Label = Companies[i];
                node.ToolTip = Companies[i];
                node.Connections = new ObservableCollection<NetworkConnection>();
                node.Connections.Add(new NetworkConnection { Target = _rootNode, Weight = 1 });
                node.NodeType = NodeModel.NodeModelType.Company;
                node.ImagePath = ImageFilePathProvider.GetImageLocalPath() + "Texture/Texture" + (i + 2).ToString("00") + ".jpg";
                _companyNodes.Add(node);
                Nodes.Add(node);
            }

            List<string> names = new List<string>();
            names.AddRange(Men);
            names.AddRange(Women);

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
                node.Connections.Add(new NetworkConnection { Target = _companyNodes[i % Companies.Length], Weight = 1 });
                node.Description = Titles[i % Titles.Length];

                if (i % 7 == 0)
                {
                    node.NodeType = NodeModel.NodeModelType.Speaker;
                }
                else
                {
                    node.NodeType = NodeModel.NodeModelType.Attendee;
                }

                if (i < Men.Length)
                {
                    node.ImagePath = ImageFilePathProvider.GetImageLocalPath() + "People/GUY" + (i + 1).ToString("00") + ".jpg";
                }
                else
                {
                    node.ImagePath = ImageFilePathProvider.GetImageLocalPath() + "People/GIRL" + (i - Men.Length + 1).ToString("00") + ".jpg";
                }

                Nodes.Add(node);
            }
        }
    }
}
