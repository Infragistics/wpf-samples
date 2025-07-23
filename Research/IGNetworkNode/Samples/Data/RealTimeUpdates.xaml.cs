using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IGNetworkNode.Resources;
using IGNetworkNode.Samples.Controls;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;
using System.Windows.Shapes;
using System.Windows.Media;

namespace IGNetworkNode.Samples.Data
{
    public partial class RealTimeUpdates : SampleContainer
    {
        private SimulatedNetworkModel networkModel;
        private Style RedCapStyle;
        private Style GreenCapStyle;

        public RealTimeUpdates()
        {
            InitializeComponent();
            networkModel = SimulatedNetworkModel.GenerateModel(50);
            xnn.DataContext = networkModel;
            RedCapStyle = Resources["RedCapStyle"] as Style;
            GreenCapStyle = Resources["GreenCapStyle"] as Style;

            foreach (var device in networkModel.NetworkDevices)
            {
                // Subscribe for property changed
                device.PropertyChanged += Device_IsWorkingChanged;
            }
            xnn.NodeControlAttachedEvent += Xnn_NodeControlAttached;
        }

        private void Xnn_NodeControlAttached(object sender, Infragistics.Controls.Maps.NetworkNodeEventArgs e)
        {
            var device = e.NodeControl.Content as NetworkDevice;
            if (device != null)
            {
                // Initialize connections' cap styles
                ApplyStylingToDeviceNode(device);
            }
        }

        private void Device_IsWorkingChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsWorking")
            {
                if (xnn.Nodes.Count() == 0) return;

                var device = (NetworkDevice)sender;
                ApplyStylingToDeviceNode(device);

                var historyItem = new HistoryItem(device, device.IsWorking ?
                    string.Format(NetworkNodeStrings.XNN_HistoryItem_Resumed, DateTime.Now.ToLongTimeString(), device.DeviceName)
                    : string.Format(NetworkNodeStrings.XNN_HistoryItem_Stopped, DateTime.Now.ToLongTimeString(), device.DeviceName));

                history.Items.Insert(0, historyItem);
                if (history.SelectedItem != null)
                    history.ScrollIntoView(history.SelectedItem);
            }
        }

        private void ApplyStylingToDeviceNode(NetworkDevice device)
        {

            NetworkNodeNode node = xnn.Search<NetworkDevice>(d => d == device).FirstOrDefault();

            foreach (var connection in node.IncomingConnections)
            {
                connection.LineEndCapStyle = device.IsWorking ? GreenCapStyle : RedCapStyle;
            }
            if (node.Data is WirelessRouter)
            {
                foreach (var connection in node.OutgoingConnections)
                {
                    connection.LineStartCapStyle = device.IsWorking ? GreenCapStyle : RedCapStyle;
                    if (connection.LineStyle == null)
                    {
                        connection.LineStyle = Resources["DottedConnection"] as Style;
                    }
                }
            }
            else
            {
                foreach (var connection in node.OutgoingConnections)
                {
                    connection.LineStartCapStyle = device.IsWorking ? GreenCapStyle : RedCapStyle;
                }
            }
        }

        private void Pause_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            networkModel.PauseUpdates();
            if (pauseButton.Content.ToString().Equals(NetworkNodeStrings.XNN_Pause_Updates))
                pauseButton.Content = NetworkNodeStrings.XNN_Resume_Updates;
            else
                pauseButton.Content = NetworkNodeStrings.XNN_Pause_Updates;
        }

        private void History_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NetworkDevice device = ((HistoryItem)e.AddedItems[0]).Device;
            xnn.SelectedNodes.Clear();
            xnn.SelectedNodes.Add(xnn.Search<NetworkDevice>(d => d == device).FirstOrDefault());
            xnn.ZoomSelectedNodes();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            foreach (var device in networkModel.NetworkDevices)
            {
                device.PropertyChanged -= Device_IsWorkingChanged;
            }
            base.OnNavigatingFrom(e);
        }
    }

    public class HistoryItem
    {
        public HistoryItem(NetworkDevice d, string m)
        {
            Device = d;
            Message = m;
        }

        public NetworkDevice Device { get; set; }
        public string Message { get; set; }
    }
}
