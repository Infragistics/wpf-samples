using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;               // SampleContainer
using Infragistics.Samples.Shared.DataProviders;      // XmlDataProvider
using Infragistics.Samples.Shared.Extensions;         // XElementExtension.GetString
using Infragistics.Samples.Shared.Models;             // Customer

namespace IGGrid.Samples.Data
{
    public partial class HierarchicalData : SampleContainer
    {
        public HierarchicalData()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("CustomersOrders.xml");
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
                                             AddressOne = d.Element("Address").GetString(),
                                             City = d.Element("City").GetString(),
                                             Region = d.Element("Region").GetString(),
                                             Country = d.Element("Country").GetString(),
                                             Orders = d.GetOrders()
                                         }).ToList<Customer>();

            this.dataGrid.ItemsSource = dataSource;
        }
    }
}
