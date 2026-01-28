using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGMenu.Models;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;

namespace IGMenu.Samples.Display
{
    public partial class CheckableMenuItems : SampleContainer
    {

        public CheckableMenuItems()
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
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("FilterMenu.xml");
        }

        /// <summary>
        /// Ths methods' XLinq Query uses special extention methods for converting data. 
        /// The extention methods can be found in the Infragistics.Samples.Shared.Extensions\XElementExtension class. 
        /// </summary>
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = GetItems(doc.Root);

            this.MainMenu.ItemsSource = dataSource;
        }
        private DataItemCollection GetItems(XElement parent)
        {
            return new DataItemCollection((from d in parent.Elements("Menu")
                                           select new DataItem
                                           {
                                               Name = d.Attribute("Value").GetString(),
                                               Children = this.GetItems(d)
                                           }).ToList<DataItem>());
        }
    }
}