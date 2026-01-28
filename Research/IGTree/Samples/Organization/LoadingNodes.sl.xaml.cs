using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models.General;

namespace IGTree.Samples.Organization
{
    public partial class LoadingNodes : SampleContainer
    {
        public LoadingNodes()
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
                var result = GetGroups(doc.Root, "Icons");
                LoadItems(dataTree.Items, result);
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
                                               ImagePath = d.Attribute("ImageUrl").GetString(),
                                               Children = this.GetItems(d)
                                           }).ToList<DataItem>());
        }

        private DataItemCollection GetItems(XElement parent)
        {
            return new DataItemCollection((from d in parent.Elements("Item")
                                           select new DataItem
                                           {
                                               Name = d.Attribute("Name").Value,
                                               ImagePath = d.Attribute("ImageUrl").GetString(),
                                           }).ToList<DataItem>());
        }

        private void LoadItems(ItemCollection nodes, DataItemCollection sourceItems)
        {

            foreach (DataItem item in sourceItems)
            {
                XamTreeItem node = this.CreateTreeItem(item);
                nodes.Add(node);

                if (item.Children != null && item.Children.Count > 0)
                {
                    this.LoadItems(node.Items, item.Children);
                }
            }
        }

        private XamTreeItem CreateTreeItem(DataItem item)
        {
            XamTreeItem node = new XamTreeItem();
            node.Header = item.Name;
            if (!string.IsNullOrEmpty(item.ImagePath))
            {
                node.ExpandedIcon = this.LoadImage(item.ImagePath);
                node.CollapsedIcon = this.LoadImage(item.ImagePath);
            }

            return node;
        }

        private Image LoadImage(string imagePath)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.UriSource = new Uri(ImageFilePathProvider.GetImageLocalPath(imagePath), UriKind.RelativeOrAbsolute);

            return new Image { Source = bmp, Margin = new Thickness(0, 0, 10, 0) };
        }
    }
}