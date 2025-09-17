using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGGrid.Models;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;

namespace IGGrid.Samples.Organization
{
    public partial class FormattingData : SampleContainer
    {

        public FormattingData()
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
            _dataProvider.GetXmlDataAsync("Properties.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Property")
                              select new Property
                              {
                                  MLS = d.Attribute("MLS").GetString(),
                                  ListingDate = d.Attribute("ListingDate").GetDateTime(),
                                  NumberOfBedRooms = d.Attribute("NumberOfBedRooms").GetInt(),
                                  NumberOfBathRooms = d.Attribute("NumberOfBathRooms").GetInt(),
                                  SqFeet = d.Attribute("SqFeet").GetDecimal(),
                                  Price = d.Attribute("Price").GetDouble(),
                                  City = d.Attribute("City").GetString(),
                                  State = d.Attribute("State").GetString(),
                                  PostalCode = d.Attribute("PostalCode").GetString()
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();
        }
    }
}
