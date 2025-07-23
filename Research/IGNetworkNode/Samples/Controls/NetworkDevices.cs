using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IGNetworkNode.Samples.Controls
{
    public abstract class NetworkDevice : INotifyPropertyChanged
    {
        private ObservableCollection<NetworkConnection> connections;
        private bool isWorking;

        public NetworkDevice()
        {
            this.Connections = new ObservableCollection<NetworkConnection>();
        }

        public string DeviceName { get; set; }

        public ObservableCollection<NetworkConnection> Connections
        {
            get
            {
                return connections;
            }
            set
            {
                if (connections == value) return;

                connections = value;
                RaisePropertyChanged("Connections");
            }
        }

        public bool IsWorking
        {
            get
            {
                return isWorking;
            }
            set
            {
                if (value == isWorking) return;

                isWorking = value;
                RaisePropertyChanged("IsWorking");
            }
        }

        public static bool ConnectDevices(NetworkDevice deviceA, NetworkDevice deviceB)
        {
            return deviceA.ConnectTo(deviceB) || deviceB.ConnectTo(deviceA);
        }

        protected virtual bool ConnectTo(NetworkDevice targetDevice)
        {
            NetworkConnection newConnection;

            if (this is Laptop || targetDevice is Laptop)
                newConnection = new NetworkConnection(targetDevice, NetworkConnectionType.Wireless);
            else
                newConnection = new NetworkConnection(targetDevice, NetworkConnectionType.Wired);

            if (!this.Connections.Contains(newConnection))
            {
                this.Connections.Add(newConnection);
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return DeviceName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class NetworkConnection : IEquatable<NetworkConnection>
    {
        public NetworkConnection(NetworkDevice target, NetworkConnectionType type)
        {
            this.Target = target;
            this.Type = type;
        }

        public NetworkDevice Target { get; set; }
        public NetworkConnectionType Type { get; set; }

        #region IEquatable<NetworkConnection> Members

        public bool Equals(NetworkConnection other)
        {
            return this.Target.Equals(other.Target);
        }

        #endregion
    }

    public enum NetworkConnectionType
    {
        Wired = 1,
        Wireless = 2
    }

    public class Server : NetworkDevice { }
    public class Router : NetworkDevice { }
    public class WirelessRouter : NetworkDevice { }
    public class Switch : NetworkDevice { }
    public class Laptop : NetworkDevice { }
    public class DesktopPC : NetworkDevice { }
    public class Printer : NetworkDevice { }
    public class Wan : NetworkDevice { }
}
