using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models.General;

namespace IGTree.Samples.Style
{
    public partial class UsingCustomIcons : SampleContainer
    {
        public UsingCustomIcons()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
        }

        private XmlDataProvider xmlDataProvider;
        private void DownloadDataSource()
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("TreeItems.xml");
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            if (doc != null)
            {
                var dataSource = GetGroups(doc.Root, "Computer");

                dataTree.ItemsSource = dataSource.ToList();
                dataTree.XamTreeItems[0].IsExpanded = true;
            }
        }

        private DataItemCollection GetGroups(XElement parent, string filter)
        {
            return new DataItemCollection((from d in parent.Elements("Tree").Descendants("Group")
                                           where d.Parent.Attribute("ID").Value == filter
                                           select new DataItem
                                           {
                                               Name = d.Attribute("Name").Value,
                                               ImagePath = ImageFilePathProvider.GetImageLocalPath(d.Attribute("ImageUrl").GetString()),
                                               Children = this.GetItems(d)
                                           }).ToList<DataItem>());
        }

        private DataItemCollection GetItems(XElement parent)
        {
            return new DataItemCollection((from d in parent.Elements("Item")
                                           select new DataItem
                                           {
                                               Name = d.Attribute("Name").Value,
                                               ImagePath = ImageFilePathProvider.GetImageLocalPath(d.Attribute("ImageUrl").GetString()),
                                           }).ToList<DataItem>());
        }
    }
}