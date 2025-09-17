using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using IGNetworkNode.Custom;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;

namespace IGNetworkNode.Samples.Organization
{
    public partial class ReorganizeNetworkUsingDragAndDrop : Infragistics.Samples.Framework.SampleContainer
    {
        public ReorganizeNetworkUsingDragAndDrop()
        {
            InitializeComponent();

            var viewModel = (NetworkNodeGmlConnectionViewModel)this.Resources["ConnectionViewModel"];
            viewModel.NetworkNode = xnn;
        }
    }

    /// <summary>
    /// A GML node view model, which supports connecting and disconnecting nodes via drag and drop.
    /// </summary>
    public class NetworkNodeGmlConnectionViewModel : NetworkNodeConnectionViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NetworkNodeGmlConnectionViewModel"/>.
        /// This view models will support organization of the GML nodes since it inherits
        /// the base <see cref="NetworkNodeConnectionViewModel"/>.
        /// </summary>
        public NetworkNodeGmlConnectionViewModel()
        {
            string gmlFileName = "Graph3.gml";

            // create data provider for reading data from a GML file
            this.GmlDataProvider = new GmlDataProvider();
            this.GmlDataProvider.GetGmlDataCompleted += OnGetGmlDataCompleted;
            this.GmlDataProvider.GetGmlDataAsync(gmlFileName);
        }

        protected GmlDataProvider GmlDataProvider;
        void OnGetGmlDataCompleted(object sender, GetGmlDataCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != null)
            {
                // read and parse GML file content
                var stringReader = new StringReader(e.Result);
                var nodes = new GmlParser().Parse(stringReader);
                // bind the items of the view model to the GML nodes 
                this.Nodes = new ObservableCollection<GmlNode>(nodes);
                RaisePropertyChanged("Nodes");
            }
        }

        /// <summary>
        /// Connects of disconnects two GML nodes.
        /// </summary>
        /// <param name="dragSource">The node, which is being dragged.</param>
        /// <param name="dropTarget">The node, on which the drop is performed.</param>
        /// <param name="dropAction">The type of the action that will be performed.</param>
        protected override void OnNodeDrop(NetworkNodeNode dragSource, NetworkNodeNode dropTarget, DropAction dropAction)
        {
            GmlNode draggedGmlNode = (GmlNode)dragSource.Data;
            GmlNode dropTargetGmlNode = (GmlNode)dropTarget.Data;

            GmlConnection dragToDropConnection = new GmlConnection();
            dragToDropConnection.Target = dropTargetGmlNode;

            GmlConnection dropToDragConnection = new GmlConnection();
            dropToDragConnection.Target = draggedGmlNode;

            if (dropAction == DropAction.Disconnect)
            {
                draggedGmlNode.Connections.Remove(dragToDropConnection);
                dropTargetGmlNode.Connections.Remove(dropToDragConnection);
            }
            else
            {
                draggedGmlNode.Connections.Add(dragToDropConnection);
                dropTargetGmlNode.Connections.Add(dropToDragConnection);
            }
        }

        /// <summary>
        /// A collection with GML nodes.
        /// </summary>
        public ObservableCollection<GmlNode> Nodes { get; set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
