using System;
using System.Collections.ObjectModel;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Resources;

namespace Infragistics.Samples.Shared.Models
{
    public class NetworkNodeSocialData
    {
        public ObservableCollection<NodeModel> Nodes { get; set; }
        private static String[] _names;
        private NodeModel _groupNode1;
        private NodeModel _groupNode2;

        public NetworkNodeSocialData()
        {
            PopulateNodes();
        }

        private void PopulateNodes()
        {
            Nodes = new ObservableCollection<NodeModel>();

            _groupNode1 = new NodeModel();
            _groupNode1.Label = ModelStrings.XWM_NetNodes_MovieClub;
            _groupNode1.ToolTip = _groupNode1.Label;
            _groupNode1.Description = ModelStrings.XWM_NetNodes_OnlineSocialGroup;
            _groupNode1.ImagePath = ImageFilePathProvider.GetImageLocalPath() + "General/BgMovie.png";
            Nodes.Add(_groupNode1);

            _groupNode2 = new NodeModel();
            _groupNode2.Label = ModelStrings.XWM_NetNodes_RecyclingClub;
            _groupNode2.ToolTip = _groupNode2.Label;
            _groupNode2.Description = ModelStrings.XWM_NetNodes_OnlineSocialGroup;
            _groupNode2.Connections = new ObservableCollection<NetworkConnection>();
            _groupNode2.Connections.Add(new NetworkConnection { Target = _groupNode1, Weight = 1 });
            _groupNode2.ImagePath = ImageFilePathProvider.GetImageLocalPath() + "General/Recycle.png";
            Nodes.Add(_groupNode2);

            _names = ModelStrings.XWM_NetNodes_Women.Split(';');
            for (int i = 0; i < 11; i++)
            {
                var node = new NodeModel();
                node.Label = _names[i];
                node.ToolTip = node.Label;
                node.Description = ModelStrings.XWM_NetNodes_GroupMember;
                node.Connections = new ObservableCollection<NetworkConnection>();
                node.ImagePath = ImageFilePathProvider.GetImageLocalPath() + "People/GIRL" + (i + 1).ToString("00") + ".jpg";
                if (i < 5)
                {
                    node.Connections.Add(new NetworkConnection { Target = _groupNode1, Weight = 1 });
                }
                else
                {
                    node.Connections.Add(new NetworkConnection { Target = _groupNode2, Weight = 1 });
                }
                Nodes.Add(node);
            }
        }
    }
}
