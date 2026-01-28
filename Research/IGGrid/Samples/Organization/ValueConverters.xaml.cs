using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Organization
{
    public partial class ValueConverters : SampleContainer
    {
        public ValueConverters()
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
            var _dataProvider = new Infragistics.Samples.Shared.DataProviders.XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Customers.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  CustomerID = d.Element("CustomerID").GetString(),
                                  Region = d.Element("Region").GetString(),
                                  Company = d.Element("CompanyName").GetString(),
                                  ContactName = d.Element("ContactName").GetString(),
                                  ContactTitle = d.Element("ContactTitle").GetString(),
                                  AddressOne = d.Element("Address").GetString(),
                                  City = d.Element("City").GetString(),
                                  Country = d.Element("Country").GetString()
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();
        }
    }

    public class EmptyValueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strVal = value as string;

            if (string.IsNullOrEmpty(strVal))
            {
                return GridStrings.XWG_ValueConverter_NoRegionFieldSpecified;
            }
            else
                return value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
