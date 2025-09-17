using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGTree.Samples.Editing
{
    public partial class TreeDataFiltering : SampleContainer
    {
        public TreeDataFiltering()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
            dataTree.Loaded += new RoutedEventHandler(dataTree_Loaded);
        }

        void dataTree_Loaded(object sender, RoutedEventArgs e)
        {
            XamTree tree = (XamTree)sender;

            if (tree.XamTreeItems.Count() > 0)
            {
                tree.XamTreeItems[0].IsExpanded = true;
            }
        }

        private DataFilteringViewModel viewModel;
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = new DataFilteringViewModel();
            this.DataContext = this.viewModel;

            this.DownloadDataSource();
        }

        private void dataTree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Customer selectedCustomer = e.AddedItems[0] as Customer;
                this.viewModel.SetSelectedCustomer(selectedCustomer);
            }
        }

        private XmlDataProvider xmlDataProvider;
        /// <summary>
        /// This method is used for downloading the sample's data source. To support localization we created a custom helper class. 
        /// </summary>
        void DownloadDataSource()
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("CustomersOrders.xml"); // xml file name
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            if (doc != null)
            {
                var dataSource = (from d in doc.Descendants("Customers")
                                  select new Customer
                                  {
                                      CustomerID = d.Element("CustomerID").Value,
                                      Company = d.Element("CompanyName").Value,
                                      ContactName = d.Element("ContactName").Value,
                                      ContactTitle = d.Element("ContactTitle").Value,
                                      AddressOne = d.Element("Address").Value,
                                      City = d.Element("City").Value,
                                      Region = d.Element("Region").GetString(),
                                      Country = d.Element("Country").Value,
                                      Orders = this.GetOrders(d)
                                  });

                List<Customer> customers = dataSource.ToList();
                this.viewModel.Customers = customers;

                if (customers.Count > 0)
                {
                    this.viewModel.SetSelectedCustomer(customers[0]);
                }
            }
        }

        private IEnumerable<Order> GetOrders(XElement parent)
        {
            return (from d in parent.Descendants("Orders")
                    select new Order
                    {
                        OrderDate = d.Element("OrderDate").GetDateTime(),
                        ShippedDate = d.Element("ShippedDate").GetDateTime(),
                        Freight = d.Element("Freight").GetDecimal(),
                        ShipName = d.Element("ShipName").Value,
                        ShipAddress = d.Element("ShipAddress").Value,
                        ShipCity = d.Element("ShipCity").Value,
                        ShipPostalCode = d.Element("ShipPostalCode").GetString(),
                        ShipCountry = d.Element("ShipCountry").Value,
                        OrderDetails = this.GetOrderDetails(d)
                    }).ToList<Order>();
        }

        private IEnumerable<OrderDetail> GetOrderDetails(XElement parent)
        {
            return (from d in parent.Descendants("OrderDetails")
                    select new OrderDetail
                    {
                        ProductName = d.Element("ProductName").Value,
                        Quantity = d.Element("Quantity").GetInt(),
                        Discount = d.Element("Discount").GetDecimal()
                    }).ToList<OrderDetail>();
        }
    }

    public class DataFilteringViewModel : ObservableModel
    {

        public DataFilteringViewModel()
        {
        }

        private IEnumerable<Customer> customers;
        public IEnumerable<Customer> Customers
        {
            get
            {
                return this.customers;
            }
            set
            {
                if (this.customers != value)
                {
                    this.customers = value;
                    this.OnPropertyChanged("Customers");
                }
            }
        }

        public IEnumerable<Order> Orders
        {
            get
            {
                IEnumerable<Order> selectedCustomerOrders = null;
                if (this.selectedCustomer != null)
                {
                    selectedCustomerOrders = this.selectedCustomer.Orders;
                }
                return selectedCustomerOrders;
            }
        }

        private Customer selectedCustomer;
        public void SetSelectedCustomer(Customer selected)
        {
            this.selectedCustomer = selected;
            this.OnPropertyChanged("Orders");
        }
    }
}