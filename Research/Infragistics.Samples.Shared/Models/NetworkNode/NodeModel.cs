using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class NodeModel : ObservableModel
    {
        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                if (value != _label)
                {
                    _label = value;
                    OnPropertyChanged("Label");
                }
            }
        }

        private string _toolTip;
        public string ToolTip
        {
            get { return _toolTip; }
            set
            {
                if (value != _toolTip)
                {
                    _toolTip = value;
                    OnPropertyChanged("ToolTip");
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (value != _imagePath)
                {
                    _imagePath = value;
                    OnPropertyChanged("ImagePath");
                }
            }
        }

        public enum NodeModelType { Default, Attendee, Speaker, Company, Convention };
        private NodeModelType _nodeType;
        public NodeModelType NodeType
        {
            get { return _nodeType; }
            set
            {
                if (value != _nodeType)
                {
                    _nodeType = value;
                    OnPropertyChanged("NodeType");
                }
            }
        }

        private ObservableCollection<NetworkConnection> _connections;
        public ObservableCollection<NetworkConnection> Connections
        {
            get { return _connections; }
            set
            {
                if (value != _connections)
                {
                    _connections = value;
                    OnPropertyChanged("Connections");
                }
            }
        }
         
    }
}
