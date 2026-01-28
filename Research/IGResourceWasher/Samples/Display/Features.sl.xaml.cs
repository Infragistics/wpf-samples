using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGResourceWasher.Samples.Display
{
    public partial class Features : SampleContainer
    {
        public Features()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
            cboWashMode.SelectedIndex = 1;
        }

        private void expandRows(RowCollection rows)
        {
            foreach (Row row in rows)
            {
                row.IsExpanded = true;
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            resWasher1.WashColor = (System.Windows.Media.Color)e.NewValue;
        }

        private void cboWashMode_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            resWasher1.WashMode = (WashMode)cboWashMode.SelectedIndex;
        }

        private XmlDataProvider xmlDataProvider;
        private void DownloadDataSource()
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Products.xml");
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

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

            this.xamGrid.ItemsSource = dataSource.ToList();
            this.expandRows(this.xamGrid.Rows);
        }
    }
}