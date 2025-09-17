using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models.General;

namespace IGTree.Samples.Organization
{
    public partial class NodeSelection : SampleContainer
    {
        public NodeSelection()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            lbNodeSelectionType.ItemsSource = EnumValuesProvider.GetEnumValues<ItemSelectionType>();
            lbNodeSelectionType.SelectedIndex = (int)dataTree.SelectionType;

            this.DownloadDataSource();
        }

        private void dataTree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedNodes.Items.Clear();
            if (this.dataTree.SelectedItems != null && this.dataTree.SelectedItems.Count > 0)
            {
                foreach (object item in this.dataTree.SelectedItems)
                {
                    DataItem selectedItem = item as DataItem;
                    this.SelectedNodes.Items.Add(new ListBoxItem { Content = selectedItem.Name });
                }
            }
        }

        private void lbNodeSelectionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ItemSelectionType)e.AddedItems[0];
            this.dataTree.SelectionType = selectedItem;
        }

        private XmlDataProvider xmlDataProvider;
        /// <summary>
        /// This method is used for downloading the sample's data source. To support localization we created a custom helper class. 
        /// </summary>
        void DownloadDataSource()
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("RssFeeds.xml"); // xml file name
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            if (doc != null)
            {
                var dataSource = (from d in doc.Descendants("Group")
                                  select new DataItem
                                  {
                                      Name = d.Attribute("Name").Value,
                                      ImagePath = ImageFilePathProvider.GetImageLocalPath(d.Attribute("ImagePath").Value),
                                      Children = this.GetChildren(d)
                                  });

                dataTree.ItemsSource = dataSource.ToList();
                dataTree.XamTreeItems[0].IsExpanded = true;
            }
        }

        private DataItemCollection GetChildren(XElement parent)
        {
            return new DataItemCollection((from d in parent.Descendants("Feed")
                                           select new DataItem
                                           {
                                               Name = d.Attribute("Name").Value,
                                               ImagePath = ImageFilePathProvider.GetImageLocalPath(d.Attribute("ImagePath").Value),
                                               Url = d.Attribute("URL").Value,
                                           }).ToList<DataItem>());
        }
    }
}
