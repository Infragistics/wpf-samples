using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGMenu.Models;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;

namespace IGMenu.Samples.Data
{
    public partial class ViewModel : SampleContainer
    {
        private DataItemCollection _viewModel = new DataItemCollection();

        public ViewModel()
        {
            InitializeComponent();
            this.DataContext = _viewModel;
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
            _dataProvider.GetXmlDataAsync("FileMenu.xml");
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

            foreach (var item in dataSource)
            {
                _viewModel.Add(item);
            }
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
