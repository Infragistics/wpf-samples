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
            List<XElement> xElems = (parent.Descendants("Account").Where(d => d.Attribute("ParentID").Value == parentId).ToList());

            List<Account> accounts = new List<Account>();

            foreach (XElement elem in xElems)
            {
                int number = int.Parse(elem.Attribute("AccountNumber").Value);
                string name = elem.Attribute("Name").Value;
                string balance = elem.Attribute("Balance").Value;

                decimal? actualBalance = 0;

                if (string.IsNullOrEmpty(balance))
                {
                    actualBalance = null;
                }
                else
                {
                    actualBalance = decimal.Parse(balance);
                }

                Account account = new Account()
                {
                    Number = number,
                    Name = name,
                    Balance = actualBalance
                };

                account.Accounts = this.GetAccounts(elem, elem.Attribute("AccountNumber").Value);

                accounts.Add(account);
            }

            return accounts;
        }
    }
}
