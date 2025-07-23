using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;


namespace IGGrid.Samples.Display
{
    public partial class MultiColumnComboEditorColumn : Infragistics.Samples.Framework.SampleContainer
    {
        private XmlDataProvider xmlDataProvider;

        public MultiColumnComboEditorColumn()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;

        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // create xml data provider that will load data from xml file
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            xmlDataProvider.GetXmlDataAsync("CustomersOrders.xml"); // xml file name
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }


            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Orders")
                              select new Order
                              {
                                  ShipCity = d.Element("ShipCity").Value,
                                  ShipName = d.Element("ShipName").Value,
                                  CustomerId = d.Parent.Element("CustomerID").Value,
                                  ShipAddress = d.Element("ShipAddress").Value,
                                  Product = d.Element("ProductName").Value,
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();
            var customerDataSource = (from d in doc.Descendants("Customers")
                                      select new Customer
                                      {
                                          CustomerID = d.Element("CustomerID").Value,
                                          Company = d.Element("CompanyName").Value,
                                          ContactName = d.Element("ContactName").Value,
                                          ImageResourcePath = d.Element("ImageResourcePath").Value
                                      });

            CustomerList customerList = this.Resources["customerList"] as CustomerList;
            foreach (var item in customerDataSource)
            {
                customerList.Add(item);
            }
        }
    }
    public class CustomerList : ObservableCollection<Customer>
    {
    }
}


