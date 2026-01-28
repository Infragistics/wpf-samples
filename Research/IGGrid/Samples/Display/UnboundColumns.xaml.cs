using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGGrid.Tools;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Display
{
    public partial class UnboundColumns : SampleContainer
    {
        private bool _isLoaded = false;
        public UnboundColumns()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (!this._isLoaded)
            {
                DownloadDataSource();
                this.MyDataGrid.Columns.DataColumns["Total Units"].SummaryColumnSettings.SummaryOperands.Add(new MySummaryOperand());

                this._isLoaded = true;
            }
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
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  UnitsOnOrder = d.Element("UnitsOnOrder").GetInt()
                              });

            this.MyDataGrid.ItemsSource = dataSource.ToList();
        }
    }
}
