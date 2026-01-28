using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using System.Windows.Controls;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Charts;

namespace IGGrid.Samples.Display
{
    public partial class SparklineColumns : SampleContainer
    {
        public SparklineColumns()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
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

        /// <summary>
        /// Ths methods' XLinq Query uses special extention methods for converting data. 
        /// The extention methods can be found in the Infragistics.Samples.Shared.Extensions\XElementExtension class. 
        /// </summary>
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

            this.dataGrid.ItemsSource = dataSource.ToList();
        }

       

    }
   
}
