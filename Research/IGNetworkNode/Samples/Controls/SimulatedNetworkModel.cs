using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using IGNetworkNode.Resources;

namespace IGNetworkNode.Samples.Controls
{
    public class SimulatedNetworkModel
    {
        private static Random random = new Random();
        private DispatcherTimer timer;

        public SimulatedNetworkModel()
        {
            this.NetworkDevices = new ObservableCollection<NetworkDevice>();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        public ObservableCollection<NetworkDevice> NetworkDevices { get; set; }

        public static SimulatedNetworkModel GenerateModel(int numberOfDevices)
        {
            SimulatedNetworkModel model = new SimulatedNetworkModel();
            model.PauseUpdates();
            for (int i = 1; i <= numberOfDevices; i++)
            {
                NetworkDevice device;
                if (i < numberOfDevices * 4.0 / 100)
                {
                    device = new Server() { DeviceName = String.Format("{0} {1}", NetworkNodeStrings.XNN_Server, i) };
                }
                else if (i < numberOfDevices * 10.0 / 100)
                {
                    device = new Router() { DeviceName = String.Format("{0} {1}", NetworkNodeStrings.XNN_Router, i - numberOfDevices * 4 / 100) };
                }
                else if (i < numberOfDevices * 15.0 / 100)
                {
                    device = new Switch() { DeviceName = String.Format("{0} {1}", NetworkNodeStrings.XNN_Switch, i - numberOfDevices * 10 / 100) };
                }
                else if (i < numberOfDevices * 20.0 / 100)
                {
                    device = new WirelessRouter() { DeviceName = String.Format("{0} {1}", NetworkNodeStrings.XNN_WirelessRouter, i - numberOfDevices * 15 / 100) };
                }
                else if (i < numberOfDevices * 23.0 / 100)
                {
                    device = new Printer() { DeviceName = String.Format("{0} {1}", NetworkNodeStrings.XNN_Printer, i - numberOfDevices * 20 / 100) };
                }
                else if (i < numberOfDevices * 25.0 / 100)
                {
                    device = new Wan() { DeviceName = String.Format("{0} {1}", NetworkNodeStrings.XNN_WAN, i - numberOfDevices * 23 / 100) };
                }
                else if (i < numberOfDevices * 60.0 / 100)
                {
                    device = new DesktopPC() { DeviceName = String.Format("{0} {1}", NetworkNodeStrings.XNN_DesktopPC, i - numberOfDevices * 25 / 100) };
                }
                else
                {
                    device = new Laptop() { DeviceName = String.Format("{0} {1}", NetworkNodeStrings.XNN_Laptop, i - numberOfDevices * 60 / 100) };
                }

                device.IsWorking = random.Next(0, 100) < 70;
                model.NetworkDevices.Add(device);
            }

            for (int i = 0; i < numberOfDevices; i++)
            {
                var device = model.NetworkDevices[i];
                if (device is Laptop)
                {
                    NetworkDevice.ConnectDevices(model.GetRandomDevice<WirelessRouter>(model.NetworkDevices), device);
                    continue;
                }
                if (device is DesktopPC)
                {
                    NetworkDevice.ConnectDevices(model.GetRandomDevice<Switch>(model.NetworkDevices), device);
                    continue;
                }
                if (device is Printer)
                {
                    NetworkDevice.ConnectDevices(model.GetRandomDevice<Switch>(model.NetworkDevices), device);
                    continue;
                }
                if (device is Switch)
                {
                    NetworkDevice.ConnectDevices(model.GetRandomDevice<Router>(model.NetworkDevices), device);
                    continue;
                }
                if (device is WirelessRouter)
                {
                    NetworkDevice.ConnectDevices(model.GetRandomDevice<Router>(model.NetworkDevices), device);
                    continue;
                }
                if (device is Router)
                {
                    NetworkDevice.ConnectDevices(model.GetRandomDevice<Server>(model.NetworkDevices), device);
                    continue;
                }
                if (device is Server)
                {
                    NetworkDevice.ConnectDevices(model.GetRandomDevice<Wan>(model.NetworkDevices), device);
                    continue;
                }
            }

            // start updates
            model.PauseUpdates();
            return model;
        }

        public void PauseUpdates()
        {
            if (timer.IsEnabled)
                timer.Stop();
            else
                timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (NetworkDevices.Count == 0)
                return;

            // update at most 10 percent of the devices
            for (int i = 0; i < NetworkDevices.Count / 10; i++)
            {
                var device = NetworkDevices[random.Next(0, NetworkDevices.Count)];
                bool newValue = random.Next(0, 100) < 50;
                if (device.IsWorking != newValue)
                    device.IsWorking = newValue;
            }
        }

        private NetworkDevice GetRandomDevice<T>(IEnumerable<NetworkDevice> networkDevices)
        {
            var freeDevices = networkDevices.Where(d => d.Connections.Count == 0 && d is T);

            if (freeDevices.Count() == 0)
                freeDevices = networkDevices.Where(d => d is T);

            return freeDevices.ElementAt(random.Next(0, freeDevices.Count()));
        }
    }
}
