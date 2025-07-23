using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Organization
{
    public partial class CustomCellMerging : SampleContainer
    {

        public CustomCellMerging()
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
        // Group by Category and UnitPrice to showcase cell merging.
        private void SetDefaultGroupBy()
        {
            Column unitsInStockColumn = this.dataGrid.Columns["UnitsInStock"] as Column;
            this.dataGrid.GroupBySettings.GroupByColumns.Add(unitsInStockColumn);
        }
    }
    public class GroupByCustomComparer : IEqualityComparer<int>
    {
        #region IEqualityComparer<int> Members

        public bool Equals(int x, int y)
        {
            // Compares values for each group created by GetHashCode
            // Since no additional conditions is required, just return true
            return true;
        }

        public int GetHashCode(int obj)
        {
            // Compare value and return the same hashcode for each group
            if (obj >= 0 && obj < 10)
                return "0".GetHashCode();
            if (obj >= 10 && obj < 25)
                return "10".GetHashCode();
            if (obj >= 25 && obj < 50)
                return "25".GetHashCode();
            if (obj >= 50 && obj < 75)
                return "50".GetHashCode();
            if (obj >= 75)
                return "75".GetHashCode();
            else
                return obj.GetHashCode();
        }

        #endregion
    }

    public class GroupedValuesConverter : System.Windows.Data.IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Get value to convert
            int val = (int)value;

            // Compare value and returns a description of the value's range, displayed in each Groupby row
            if (val >= 0 && val < 10)
                return GridStrings.XG_Under10;
            else if (val >= 10 && val < 25)
                return GridStrings.XG_Between10And25;
            else if (val >= 25 && val < 50)
                return GridStrings.XG_Between25And50;
            else if (val >= 50 && val < 75)
                return GridStrings.XG_Between50And75;
            else if (val >= 75)
                return GridStrings.XG_GreaterThan75;
            else
                return val.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
