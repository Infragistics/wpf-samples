using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Organization
{
    public partial class ProgrammaticallyGroupBy : SampleContainer
    {

        public ProgrammaticallyGroupBy()
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
                                  Country = d.Element("Country").Value,
                                  Orders = d.GetOrders()
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();
            this.SetDefaultGroupBy();
        }

        private void SetDefaultGroupBy()
        {
            Column countryColumn = this.dataGrid.Columns["Country"] as Column;
            this.dataGrid.GroupBySettings.GroupByColumns.Add(countryColumn);

            Column companyColumn = this.dataGrid.Columns["Company"] as Column;
            this.dataGrid.GroupBySettings.GroupByColumns.Add(companyColumn);
        }
    }
}
