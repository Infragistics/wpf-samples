using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.Themes;

namespace IGResourceWasher.Samples.Display
{
    public partial class Features : Page
    {
        public Features()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private XmlDataProvider xmlDataProvider;

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();
            cboWashMode.SelectedIndex = 1;
        }

        private void DownloadDataSource()
        {
            // create xml data provider that will load data from xml file
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            xmlDataProvider.GetXmlDataAsync("Products.xml"); // xml file name
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;
            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Products")
                              select new Product
                              {
                                  SKU = d.Element("ProductID").Value,
                                  Name = d.Element("ProductName").Value,
                                  Category = d.Element("Category").Value,
                                  Supplier = d.Element("Supplier").Value,
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  UnitsOnOrder = d.Element("UnitsOnOrder").GetInt(),
                                  QuantityPerUnit = d.Element("QuantityPerUnit").Value
                              });
            this.dataGrid.DataSource = dataSource.ToList();
            this.ExpandRows(this.dataGrid.Records);
        }

        private void ExpandRows(RecordCollectionBase rows)
        {
            foreach (Record row in rows)
            {
                row.IsExpanded = true;
            }
        }

        private void cboWashMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resWasher1.WashMode = (WashMode)cboWashMode.SelectedIndex;
        }

        //TODO: comment out when the ColorPicker is present in .Shared project
        private void ColorPicker_ColorChanged(object sender, System.Windows.Media.Color e)
        {
            resWasher1.WashColor = e;
        }

        private void ColorPicker_SelectedColorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            resWasher1.WashColor = (System.Windows.Media.Color)e.NewValue;
        }
    }
}
