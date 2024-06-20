using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Display
{
    public partial class ImageColumns : SampleContainer
    {

        public ImageColumns()
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
            _dataProvider.GetXmlDataAsync("Customers.xml");
        }

        /// <summary>
        /// Ths methods' XLinq Query uses special extention methods for converting data. 
        /// The extention methods can be found in the Common\XElementExtension class. 
        /// </summary>
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            string imagePath = ImageFilePathProvider.GetImageLocalPath() + "People/";
            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  CustomerID = d.Element("CustomerID").GetString(),
                                  Company = d.Element("CompanyName").GetString(),
                                  ContactName = d.Element("ContactName").GetString(),
                                  ContactTitle = d.Element("ContactTitle").GetString(),
                                  Country = d.Element("Country").GetString(),
                                  Phone = d.Element("Phone").GetString(),
                                  Fax = d.Element("Fax").GetString(),
                                  ImageResourcePath = imagePath + d.Element("ImageResourcePath").GetString()
                              }).ToList<Customer>();

            this.dataGrid.ItemsSource = dataSource;
        }

    }
}
