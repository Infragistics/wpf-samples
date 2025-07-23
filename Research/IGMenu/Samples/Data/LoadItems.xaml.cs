using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using IGMenu.Models;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;

namespace IGMenu.Samples.Data
{
    public partial class LoadItems : SampleContainer
    {

        public LoadItems()
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

            if (this.MainMenu.Items.Count == 0)
                this.LoadMenu(this.MainMenu.Items, dataSource);
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

        private void LoadMenu(ItemCollection menuItems, DataItemCollection sourceItems)
        {
            foreach (DataItem item in sourceItems)
            {
                XamMenuItem menu = new XamMenuItem { Header = item.Name };
                menuItems.Add(menu);
                if (item.Children != null && item.Children.Count > 0)
                {
                    this.LoadMenu(menu.Items, item.Children);
                }
            }
        }
    }
}