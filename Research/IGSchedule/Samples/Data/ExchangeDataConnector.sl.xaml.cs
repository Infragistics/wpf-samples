using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using IGSchedule.Resources;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGSchedule.Samples.Data
{
    public partial class ExchangeDataConnector : SampleContainer
    {
        bool _connected = false;

        public ExchangeDataConnector()
        {
            InitializeComponent();
            this.dataManager.ColorScheme = new IGColorScheme();
            this.Loaded += new RoutedEventHandler(ExchangeDataConnector_Loaded);
        }

        void ExchangeDataConnector_Loaded(object sender, RoutedEventArgs e)
        {
            this.SrvType.ItemsSource = GetEnumValues(ExchangeVersion.Exchange2007_SP1);
            this.SrvType.SelectedIndex = 0;
            this.ConnectButton.Content = ScheduleStrings.Connect;
        }

        private IEnumerable<Enum> GetEnumValues(Enum e)
        {
            return from f in e.GetType().GetFields(BindingFlags.Static | BindingFlags.Public) select (Enum)f.GetValue(e);
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this._connected)
            {
                if (SrvAddr.Text != "")
                {
                    if (this.SrvType.SelectedItem != null)
                    {
                        ExchangeVersion ev = (ExchangeVersion)this.SrvType.SelectedItem;
                        if (this.tfName.Text == "")
                        {
                            MessageBox.Show(ScheduleStrings.ERR_NoUserName, ScheduleStrings.MissingInformation, MessageBoxButton.OK);
                            return;
                        }
                        if (this.tfPass.Password == "")
                        {
                            MessageBox.Show(ScheduleStrings.ERR_NoPassword, ScheduleStrings.MissingInformation, MessageBoxButton.OK);
                            return;
                        }
                        if (this.tfDomain.Text == "")
                        {
                            MessageBox.Show(ScheduleStrings.ERR_NoDomain, ScheduleStrings.MissingInformation, MessageBoxButton.OK);
                            return;
                        }
                        try
                        {
                            new Uri(SrvAddr.Text);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ScheduleStrings.ERR_SrvAddrInvalid, ScheduleStrings.MissingInformation, MessageBoxButton.OK);
                            return;
                        }
                        this.ConnectButton.IsEnabled = false;
                        List<ExchangeUser> users = new List<ExchangeUser>();
                        users.Add(new ExchangeUser(this.tfName.Text, this.tfPass.Password, this.tfDomain.Text));
                        this.exchangeConnector.Users = users;
                        this.exchangeConnector.ServerConnectionSettings = new ExchangeServerConnectionSettings();
                        this.exchangeConnector.ServerConnectionSettings.RequestedServerVersion = ev;
                        this.exchangeConnector.ServerConnectionSettings.Url = new Uri(SrvAddr.Text);
                        this.exchangeConnector.Connect();
                        this.dataManager.CurrentUserId = this.tfDomain.Text + "\\" + this.tfName.Text;
                        this.ConnectButton.Content = ScheduleStrings.Disconnect;
                        this.ConnectButton.IsEnabled = true;
                        this._connected = true;
                    }
                    else
                    {
                        MessageBox.Show(ScheduleStrings.ERR_SrvTypeNotSelected, ScheduleStrings.MissingInformation, MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show(ScheduleStrings.ERR_NoExchSrvAddress, ScheduleStrings.MissingInformation, MessageBoxButton.OK);
                }
            }
            else
            {
                this.exchangeConnector.Disconnect();
                this.exchangeConnector.Users = null;
                this.exchangeConnector.ServerConnectionSettings = null;
                this.dataManager.CurrentUserId = null;
                this.ConnectButton.Content = ScheduleStrings.Connect;
                this._connected = false;
            }
        }
    }
}
