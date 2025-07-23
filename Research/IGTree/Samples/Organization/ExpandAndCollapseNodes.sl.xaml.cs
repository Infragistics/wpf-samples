using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models.General;

namespace IGTree.Samples.Organization
{
    public partial class ExpandAndCollapseNodes : SampleContainer
    {
        public ExpandAndCollapseNodes()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }


        private void ExpandAll_Checked(object sender, RoutedEventArgs e)
        {
            this.SetNodeExpandedState(this.dataTree.XamTreeItems, true);
        }

        private void CollapseAll_Checked(object sender, RoutedEventArgs e)
        {
            if (this.dataTree != null)
            {
                this.SetNodeExpandedState(this.dataTree.XamTreeItems, false);
            }
        }

        private void SetNodeExpandedState(XamTreeItemsCollection nodes, bool expandNode)
        {
            foreach (XamTreeItem item in nodes)
            {
                item.IsExpanded = expandNode;
                this.SetNodeExpandedState(item.XamTreeItems, expandNode);
            }
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
                var dataSource = GetGroups(doc.Root, "Countries");
                dataTree.ItemsSource = dataSource.ToList();
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
                                               Children = this.GetItems(d)
                                           }).ToList<DataItem>());
        }
    }
}