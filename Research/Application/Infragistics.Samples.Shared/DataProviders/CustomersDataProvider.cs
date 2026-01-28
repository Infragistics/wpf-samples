using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for Customer objects loaded from XML file.
    /// </summary>
    public class CustomersDataProvider
    {
        public CustomersDataProvider()
        {
            _xmlProvider = new XmlDataProvider();
        }
        private readonly XmlDataProvider _xmlProvider;
        /// <summary>
        /// Occurs when loading of is Product objects from xml file is completed
        /// </summary>
        public event EventHandler<LoadCustomersCompletedEventArgs> LoadProductsCompleted;

        public event EventHandler<LoadCustomersCountCompletedEventArgs> LoadProductsCountComplete;

        public event EventHandler<LoadCustomersRangeCompletedEventArgs> LoadProductsRangeComplete;
        
        /// <summary>
        /// Loads Customer objects from XML file called: "Customers.xml"
        /// </summary>
        public void LoadProductsAsync()
        {
            _xmlProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _xmlProvider.GetXmlDataAsync("CustomersOrders.xml");
        }
        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnLoadProductsCompleted(new LoadCustomersCompletedEventArgs(e.Error));
            }
            else
            {
                XDocument doc = e.Result;  
                List<Customer> customers = (from d in doc.Descendants("Customers")
                    select new Customer
                    {
                        CustomerID = d.Element("CustomerID").GetString(),
                        Company = d.Element("CompanyName").GetString(),
                        ContactName = d.Element("ContactName").GetString(),
                        ContactTitle = d.Element("ContactTitle").GetString(),
                        AddressOne = d.Element("Address").GetString(),
                        City = d.Element("City").GetString(),
                        Country = d.Element("Country").GetString(),
                        Orders = d.GetOrders()
                    }).ToList<Customer>();

                OnLoadProductsCompleted(new LoadCustomersCompletedEventArgs(customers));
            }
        }

        private void OnLoadProductsCompleted(LoadCustomersCompletedEventArgs eventArgs)
        {
            if (this.LoadProductsCompleted != null)
            {
                this.LoadProductsCompleted(this, eventArgs);
            }
        }

        /// <summary>
        /// Loads Customer objects count from XML file called: "Customers.xml"
        /// </summary>
        public void LoadProductsCountAsync()
        {
            _xmlProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCountCompleted;
            _xmlProvider.GetXmlDataAsync("Customers.xml");
        }
        private void OnDataProviderGetXmlDataCountCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnLoadProductsCountCompleted(new LoadCustomersCountCompletedEventArgs(e.Error));
            }
            else
            {
                XDocument doc = e.Result;
                int count = doc.Descendants("Customers").Count<XElement>();
                OnLoadProductsCountCompleted(new LoadCustomersCountCompletedEventArgs(count));
            }
        }

        private void OnLoadProductsCountCompleted(LoadCustomersCountCompletedEventArgs eventArgs)
        {
            if (this.LoadProductsCountComplete != null)
            {
                this.LoadProductsCountComplete(this, eventArgs);
            }
        }

        /// <summary>
        /// Loads Customer objects range from XML file called: "Customers.xml"
        /// </summary>
        public void LoadProductsRangeAsync(int start, int count)
        {
            _xmlProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataRangeCompleted;
            _xmlProvider.GetXmlDataAsync("CustomersOrders.xml", new RangeData() { Start = start, Count = count });
        }
        private void OnDataProviderGetXmlDataRangeCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnLoadProductsRangeCompleted(new LoadCustomersRangeCompletedEventArgs(e.Error));
            }
            else
            {
                RangeData rd = e.UserState as RangeData;
                if (rd != null)
                {
                    XDocument doc = e.Result;
                    List<Customer> customers = (from d in doc.Descendants("Customers").Skip<XElement>(rd.Start).Take<XElement>(rd.Count)
                        select new Customer
                        {
                            CustomerID = d.Element("CustomerID").GetString(),
                            Company = d.Element("CompanyName").GetString(),
                            ContactName = d.Element("ContactName").GetString(),
                            ContactTitle = d.Element("ContactTitle").GetString(),
                            AddressOne = d.Element("Address").GetString(),
                            City = d.Element("City").GetString(),
                            Country = d.Element("Country").GetString(),
                            Orders = d.GetOrders()
                        }).ToList<Customer>();

                    OnLoadProductsRangeCompleted(new LoadCustomersRangeCompletedEventArgs(rd.Start, customers));
                }
            }
        }

        private void OnLoadProductsRangeCompleted(LoadCustomersRangeCompletedEventArgs eventArgs)
        {
            if (this.LoadProductsRangeComplete != null)
            {
                this.LoadProductsRangeComplete(this, eventArgs);
            }
        }
    }

    public class LoadCustomersCompletedEventArgs : EventArgs
    {

        public LoadCustomersCompletedEventArgs(List<Customer> data)
        {
            this.Result = data;
            this.Error = null;
        }

        public LoadCustomersCompletedEventArgs(Exception error)
        {
            this.Result = null;
            this.Error = error;
        }
        public List<Customer> Result { get; set; }
        public Exception Error { get; private set; }

    }

    public class LoadCustomersCountCompletedEventArgs : EventArgs
    {

        public LoadCustomersCountCompletedEventArgs(int count)
        {
            this.Result = count;
            this.Error = null;
        }

        public LoadCustomersCountCompletedEventArgs(Exception error)
        {
            this.Result = 0;
            this.Error = error;
        }
        public int Result { get; set; }
        public Exception Error { get; private set; }

    }

    public class LoadCustomersRangeCompletedEventArgs : EventArgs
    {
    
        public LoadCustomersRangeCompletedEventArgs(int startIndex, List<Customer> data)
        {
            this.startIndex = startIndex;
            this.Result = data;
            this.Error = null;
        }

        public LoadCustomersRangeCompletedEventArgs(Exception error)
        {
            this.Result = null;
            this.Error = error;
        }

        public int startIndex;
        public List<Customer> Result { get; set; }
        public Exception Error { get; private set; }

    }

    public class RangeData
    {
        public int Start { get; set; }
        public int Count { get; set; }
    }

    internal static class OrderXElementExtension
    {
        internal static IEnumerable<OrderDetail> GetOrderDetails(this XElement parent)
        {
            return (from d in parent.Descendants("OrderDetails")
                    select new OrderDetail
                    {
                        ProductName = d.Element("ProductName").GetString(),
                        Quantity = d.Element("Quantity").GetInt(),
                        Discount = d.Element("Discount").GetDecimal()
                    }).ToList<OrderDetail>();
        }

        internal static IEnumerable<Order> GetOrders(this XElement parent)
        {
            return (from d in parent.Descendants("Orders")
                    select new Order
                    {
                        OrderDate = d.Element("OrderDate").GetDateTime(),
                        ShippedDate = d.Element("ShippedDate").GetDateTime(),
                        Freight = d.Element("Freight").GetDecimal(),
                        ShipName = d.Element("ShipName").GetString(),
                        ShipAddress = d.Element("ShipAddress").GetString(),
                        ShipCity = d.Element("ShipCity").GetString(),
                        ShipPostalCode = d.Element("ShipPostalCode").GetString(),
                        ShipCountry = d.Element("ShipCountry").GetString(),
                        OrderDetails = d.GetOrderDetails()
                    }).ToList<Order>();
        }
    }
}