using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace IGTreeGrid.Samples.ViewModels
{
    public class AccountsDataProvider : ObservableModel
    {
        public AccountsDataProvider()
        {
            LoadDataSource();
        }

        private ObservableCollection<Account> _accounts = null;
        public ObservableCollection<Account> Accounts
        {
            get
            {
                return this._accounts;
            }
            internal set
            {
                if (this._accounts != value)
                {
                    this._accounts = value;
                    this.OnPropertyChanged("Accounts");
                }
            }
        }

        void LoadDataSource()
        {
            var dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Accounts2.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;
            XDocument doc = e.Result;
            IEnumerable<Account> accounts = this.GetAccounts(doc.Root, "");
            this.Accounts = new ObservableCollection<Account>(accounts);
        }

        private IEnumerable<Account> GetAccounts(XElement parent, string parentId)
        {
            return (from d in parent.Descendants("Account")
                    where d.Attribute("ParentID").Value == parentId
                    select new Account
                    {
                        Number = d.Attribute("AccountNumber").Value,
                        Name = d.Attribute("Name").Value,
                        Balance = d.Attribute("Balance").Value,
                        Accounts = this.GetAccounts(d, d.Attribute("AccountNumber").Value)
                    }).ToList<Account>();
        }
    }
}
