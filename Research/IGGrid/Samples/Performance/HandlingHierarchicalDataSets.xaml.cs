using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Performance
{
    public partial class HandlingHierarchicalDataSets : SampleContainer
    {

        public HandlingHierarchicalDataSets()
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

        private void ExpandAll_Click(object sender, RoutedEventArgs e)
        {
            this.ExpandCollapseAll1(dataGrid.Rows, true);
        }

        private void CollapseAll_Click(object sender, RoutedEventArgs e)
        {
            this.ExpandCollapseAll1(dataGrid.Rows, false);
        }

        private void ExpandCollapseAll1(IEnumerable<Row> rows, bool expand)
        {
            foreach (Row row in rows)
            {
                row.IsExpanded = expand ? true : false;
                if (null != row.ChildBands)
                {
                    foreach (ChildBand cb in row.ChildBands)
                    {
                        cb.IsExpanded = expand ? true : false;
                        this.ExpandCollapseAll1(cb.Rows, expand);
                    }
                }
            }
        }
    }
}
