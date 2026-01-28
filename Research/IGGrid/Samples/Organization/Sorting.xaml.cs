using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Organization
{
    public partial class Sorting : SampleContainer
    {

        public Sorting()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
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
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();
        }

        private void EnableAllowSort_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.SortingSettings.AllowSorting = (bool)this.EnableAllowSort.IsChecked;

            // Set IsEnabled State of Additional Checkboxes based on AllowSort value
            this.EnableMultiSort.IsEnabled = this.EnableShowIndicator.IsEnabled = (bool)this.EnableAllowSort.IsChecked;
        }

        private void EnableMultiSort_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.SortingSettings.AllowMultipleColumnSorting = (bool)this.EnableMultiSort.IsChecked;
        }

        private void EnableShowIndicator_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.SortingSettings.ShowSortIndicator = (bool)this.EnableShowIndicator.IsChecked;
        }
    }
}
