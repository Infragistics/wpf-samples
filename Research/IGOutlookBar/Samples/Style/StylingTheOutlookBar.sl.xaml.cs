using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGOutlookBar.Samples.Style
{
    public partial class StylingTheOutlookBar : SampleContainer
    {
        private readonly XmlDataProvider dataProvider = new XmlDataProvider();

        public StylingTheOutlookBar()
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
            dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("CustomersOrders.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

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
                                            Region = d.Element("Region").GetString(),
                                            Country = d.Element("Country").GetString(),
                                            ImageResourcePath = "People/" + d.Element("ImageResourcePath").GetString()
                                        }).ToList();

            List<Customer> customersSubset = new List<Customer>();

            for (int i = 0; i <= 5 && i < customers.Count; i++)
            {
                customersSubset.Add(customers[i]);
            }
            this.xwobClients.GroupsSource = customersSubset;

            // Select First Group
            this.xwobClients.Groups[0].IsSelected = true;
        }
    }
}
