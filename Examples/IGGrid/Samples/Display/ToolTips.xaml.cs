﻿using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Display
{
    public partial class ToolTips : SampleContainer
    {

        public ToolTips()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
            enumProvider.SelectedEnumIndex = 0;
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
        }

        private void enumProvider_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var selectedItem = (AllowToolTips)e.NewValue;
            for (int i = 0; i < dataGrid.Columns.DataColumns.Count; i++)
            {
                this.dataGrid.Columns.DataColumns[i].AllowToolTips = selectedItem;
            }
        }
    }
}
