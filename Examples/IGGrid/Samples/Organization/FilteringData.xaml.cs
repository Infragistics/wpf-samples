﻿using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;

namespace IGGrid.Samples.Organization
{
    public partial class FilteringData : SampleContainer
    {

        public FilteringData()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
            this.Unloaded += OnSampleUnloaded;
        }
        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
            ApplyThemes();
        }

        private void OnSampleUnloaded(object sender, RoutedEventArgs e)
        {
            ClearThemes();
        }

        
        private void ApplyThemes()
        { 
            ThemeLoader.SetTheme(this, ThemeType.IG); 
        }
        private void ClearThemes()
        { 
            ThemeLoader.SetTheme(this, ThemeType.Default); 
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
                                  OnBackOrder = d.Element("UnitsInStock").GetInt() == 0,
                                  QuantityPerUnit = d.Element("QuantityPerUnit").GetString()
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();
        }
    }
}
