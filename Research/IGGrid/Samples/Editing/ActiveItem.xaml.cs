using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;

namespace IGGrid.Samples.Editing
{
    public partial class ActiveItem : SampleContainer
    {
        private CustomerDataSource viewModel;

        public ActiveItem()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            viewModel = new CustomerDataSource();
            this.DataContext = viewModel;
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

            this.viewModel.Customers = dataSource.ToList<Customer>();
        }
    }
}
