using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Editing
{
    public partial class SelectAllRows : SampleContainer
    {
 
        public SelectAllRows()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
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

        private void SelectAllGrid_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.Rows.SelectAll();
        }

        private void SelectAllChildBands_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<ChildBand> childBands;
            if (dataGrid.Rows[0].RowType == RowType.GroupByRow)
                childBands = (this.dataGrid.Rows.SelectMany(t => ((GroupByRow)t).Rows)).Select(t => t.ChildBands[0]);
            else
                childBands = this.dataGrid.Rows.Select(t => t.ChildBands[0]);

            foreach (var item in childBands)
            {
                item.Rows.SelectAll();
            }
        }

        private void SelectAllGroupedBy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGrid.Rows.Count; i++)
                {
                    ((GroupByRow)dataGrid.Rows[i]).Rows.SelectAll();
                }
            }
            catch
            {
                MessageBox.Show(GridStrings.XG_Warning);
            }
        }
    }
}
