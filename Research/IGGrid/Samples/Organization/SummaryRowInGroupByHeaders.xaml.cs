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
    public partial class SummaryRowInGroupByHeaders : SampleContainer
    {
        public SummaryRowInGroupByHeaders()
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
            _dataProvider.GetXmlDataAsync("Products.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Products")
                              select new Product
                              {
                                  SKU = d.Element("ProductID").GetString(),
                                  Name = d.Element("ProductName").GetString(),
                                  Category = d.Element("Category").GetString(),
                                  Supplier = d.Element("Supplier").GetString(),
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  UnitsOnOrder = d.Element("UnitsOnOrder").GetInt(),
                                  QuantityPerUnit = d.Element("QuantityPerUnit").GetString()
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();
            this.SetDefaultGroupBy();
        }

        private void SetDefaultGroupBy()
        {
            Column unitPriceColumn = this.dataGrid.Columns["UnitPrice"] as Column;
            this.dataGrid.GroupBySettings.GroupByColumns.Add(unitPriceColumn);
        }
    }
}
