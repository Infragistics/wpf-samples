using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Performance
{
    public partial class DeferredScrolling : SampleContainer
    {

        public DeferredScrolling()
        {
            InitializeComponent();
            this.DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Customers.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  CustomerID = d.Element("CustomerID").GetString(),
                                  Company = d.Element("CompanyName").GetString(),
                                  ContactName = d.Element("ContactName").GetString(),
                                  ContactTitle = d.Element("ContactTitle").GetString(),
                                  Phone = d.Element("Phone").GetString(),
                                  Fax = d.Element("Fax").GetString(),
                                  AddressOne = d.Element("Address").GetString(),
                                  City = d.Element("City").GetString(),
                                  Region = d.Element("Region").GetString(),
                                  Country = d.Element("Country").GetString(),
                                  ImageResourcePath = d.Element("ImageResourcePath").GetString()
                              });

            ObservableCollection<CustomerDeferredScrollViewModel> viewModels = new ObservableCollection<CustomerDeferredScrollViewModel>();

            foreach (Customer item in dataSource)
            {
                CustomerDeferredScrollViewModel customer = new CustomerDeferredScrollViewModel(item);
                viewModels.Add(customer);
            }

            this.DataContext = viewModels;
        }
    }
}
