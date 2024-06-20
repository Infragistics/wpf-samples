using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Editing
{
    public partial class Selection : SampleContainer
    {

        public Selection()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Properties.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Property")
                              select new EstateProperty
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
