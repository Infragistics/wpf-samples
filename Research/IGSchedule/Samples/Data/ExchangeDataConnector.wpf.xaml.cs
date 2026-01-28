using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using IGSchedule.Resources;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Data
{
    /// <summary>
    /// Interaction logic for ExchangeDataConnector.xaml
    /// </summary>
    public partial class ExchangeDataConnector : SampleContainer
    {
        bool _connected = false;

        public ExchangeDataConnector()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ExchangeDataConnector_Loaded);
        }
        
        void ExchangeDataConnector_Loaded(object sender, RoutedEventArgs e)
        {
            this.ConnectButton.Content = ScheduleStrings.Connect;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this._connected)
            {
                if (this.SrvAddr.Text != "")
                {
                    if (this.SrvType.Value != null)
                    {
                        ExchangeVersion ev = (ExchangeVersion)this.SrvType.Value;
                        if (this.UseDefCred.IsChecked.HasValue)
                        {
                            try
                            {
                                new Uri(SrvAddr.Text);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(ScheduleStrings.ERR_SrvAddrInvalid,
                                            ScheduleStrings.MissingInformation,
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Exclamation);
                                return;
                            }
                            if (this.UseDefCred.IsChecked.Value)
                            {
                                this.ConnectButton.IsEnabled = false;
                                this.exchangeConnector.UseDefaultCredentials = true;
                                this.exchangeConnector.Users = null;
                                this.exchangeConnector.ServerConnectionSettings = new ExchangeServerConnectionSettings();
                                this.exchangeConnector.ServerConnectionSettings.RequestedServerVersion = ev;
                                this.exchangeConnector.ServerConnectionSettings.Url = new Uri(SrvAddr.Text);
                                this.exchangeConnector.Connect();
                                string domain = Environment.UserDomainName;
                                string user = Environment.UserName;
                                this.dataManager.CurrentUserId = domain.ToLower() + char.ConvertFromUtf32(92) + user.ToLower();
                                this.ConnectButton.Content = ScheduleStrings.Disconnect;
                                this.ConnectButton.IsEnabled = true;
                                this._connected = true;
                            }
                            else
                            {
                                if (this.tfName.Text == "")
                                {
                                    MessageBox.Show(ScheduleStrings.ERR_NoUserName,
                                        ScheduleStrings.MissingInformation,
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Exclamation);
                                    return;
                                }
                                if (this.tfPass.Password == "")
                                {
                                    MessageBox.Show(ScheduleStrings.ERR_NoPassword,
                                        ScheduleStrings.MissingInformation,
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Exclamation);
                                    return;
                                }
                                if (this.tfDomain.Text == "")
                                {
                                    MessageBox.Show(ScheduleStrings.ERR_NoDomain,
                                        ScheduleStrings.MissingInformation,
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Exclamation);
                                    return;
                                }
                                this.ConnectButton.IsEnabled = false;
                                this.exchangeConnector.UseDefaultCredentials = false;
                                List<ExchangeUser> users = new List<ExchangeUser>();
                                users.Add(new ExchangeUser(this.tfName.Text, this.tfPass.Password, this.tfDomain.Text));
                                this.exchangeConnector.Users = users;
                                this.exchangeConnector.ServerConnectionSettings = new ExchangeServerConnectionSettings();
                                this.exchangeConnector.ServerConnectionSettings.RequestedServerVersion = ev;
                                this.exchangeConnector.ServerConnectionSettings.Url = new Uri(SrvAddr.Text);
                                this.exchangeConnector.Connect();
                                this.dataManager.CurrentUserId = this.tfDomain.Text + char.ConvertFromUtf32(92) + this.tfName.Text;
                                this.ConnectButton.Content = ScheduleStrings.Disconnect;
                                this.ConnectButton.IsEnabled = true;
                                this._connected = true;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(ScheduleStrings.ERR_SrvTypeNotSelected,
                            ScheduleStrings.MissingInformation,
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show(ScheduleStrings.ERR_NoExchSrvAddress,
                        ScheduleStrings.MissingInformation,
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
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

        private void UseDefCred_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null && cb.IsChecked.HasValue)
            {
                this.tName.IsEnabled = !cb.IsChecked.Value;
                this.tfName.IsEnabled = !cb.IsChecked.Value;
                this.tPass.IsEnabled = !cb.IsChecked.Value;
                this.tfPass.IsEnabled = !cb.IsChecked.Value;
                this.tDomain.IsEnabled = !cb.IsChecked.Value;
                this.tfDomain.IsEnabled = !cb.IsChecked.Value;
            }
        }
    }
}
