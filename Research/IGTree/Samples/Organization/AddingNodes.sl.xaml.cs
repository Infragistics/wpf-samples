using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGTree.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Models.General;

namespace IGTree.Samples.Organization
{
    public partial class AddingNodes : SampleContainer
    {
        public AddingNodes()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void dataTree_Loaded(object sender, RoutedEventArgs e)
        {
            XamTree tree = (XamTree)sender;

            if (tree.XamTreeItems.Count() > 0)
            {
                tree.XamTreeItems[0].IsExpanded = true;
            }
        }

        private AddNewDataSource viewModel;
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = new AddNewDataSource();
            this.DataContext = this.viewModel;

            this.ParentList.ItemsSource = this.viewModel.GroupItems;
            this.ParentList.SelectedIndex = 0;

            this.DownloadDataSource();
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
                                      Id = d.Attribute("Id").Value,
                                      Name = d.Attribute("Name").Value,
                                      ImagePath = ImageFilePathProvider.GetImageLocalPath(d.Attribute("ImagePath").Value),
                                      Children = this.GetChildren(d)
                                  });

                this.viewModel.DataItems = new DataItemCollection(dataSource.ToList());
            }

            if (this.dataTree.XamTreeItems.Count() > 0)
            {
                this.dataTree.XamTreeItems[0].IsExpanded = true;
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

        private void AddNode_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.NodeName.Text))
            {
                Group selectedParent = this.ParentList.SelectedItem as Group;
                this.viewModel.AddNode(selectedParent, this.NodeName.Text);
                foreach (XamTreeItem item in this.dataTree.XamTreeItems)
                {
                    if ((item.Data as DataItem).Id == selectedParent.Id)
                    {
                        item.IsExpanded = true;
                    }
                }
                this.NodeName.Text = string.Empty;
            }
        }
    }

    public class Group
    {
        public Group()
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class AddNewDataSource : ObservableModel
    {

        public AddNewDataSource()
        {
            this.groupsItems = new List<Group>();
            this.groupsItems.Add(new Group { Id = "News", Name = TreeStrings.XWT_AddNode_ItemHeaderNews, Value = "10" });
            this.groupsItems.Add(new Group { Id = "Programming", Name = TreeStrings.XWT_AddNode_ItemHeaderProgramming, Value = "20" });
            this.groupsItems.Add(new Group { Id = "Technology", Name = TreeStrings.XWT_AddNode_ItemHeaderTechnology, Value = "30" });
        }

        private DataItemCollection dataItems;
        public DataItemCollection DataItems
        {
            get
            {
                return this.dataItems;
            }
            set
            {
                if (this.dataItems != value)
                {
                    this.dataItems = value;
                    this.OnPropertyChanged("DataItems");
                }
            }
        }

        private List<Group> groupsItems;
        public List<Group> GroupItems
        {
            get
            {
                return this.groupsItems;
            }
            set
            {
                if (this.groupsItems != value)
                {
                    this.groupsItems = value;
                    this.OnPropertyChanged("GroupItems");
                }
            }
        }

        public void AddNode(Group parent, string name)
        {
            DataItem parentNode = this.dataItems.First(d => d.Id == parent.Id);
            parentNode.Children.Add(new DataItem { Name = name, ImagePath = ImageFilePathProvider.GetImageLocalPath("Tree/RSSFeed.png") });

            this.OnPropertyChanged("DataItems");
        }
    }
}