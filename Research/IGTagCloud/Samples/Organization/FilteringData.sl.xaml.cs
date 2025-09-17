using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGTagCloud.Samples.Organization
{
    public partial class FilteringData : Infragistics.Samples.Framework.SampleContainer
    {
        private TagViewModel viewModel;
        private XmlDataProvider xmlDataProvider;

        public FilteringData()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = new TagViewModel();
            this.DataContext = this.viewModel;

            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("TagsData.xml"); // xml file name
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;
            if (doc != null)
            {
                XElement docRoot = doc.Root;
                var result = this.GetItems(docRoot, "ProductCategory");
                this.viewModel.SetTagSource(result);
            }
            this.GetOrderHistory();
        }

        private IList<TagItem> GetItems(XElement parent, string filter)
        {
            return ((from d in parent.Elements("TagGroup").Descendants("Tag")
                     where d.Parent.Attribute("Name").Value == filter
                     select new TagItem
                     {
                         Id = d.Attribute("ID").Value,
                         Content = d.Attribute("Content").Value,
                         Weight = d.Attribute("Weight").GetInt(),
                         NavigateUri = d.Attribute("NavigateUri").Value,
                         Tags = this.GetItems(d, "Products")
                     }).ToList<TagItem>());
        }

        private void dataTagCloud_XamTagCloudItemClicked(object sender, XamTagCloudItemEventArgs e)
        {
            TagItem selectedTag = e.XamTagCloudItem.Content as TagItem;
            if (selectedTag != null)
            {
                this.viewModel.FilterTags(selectedTag);
            }
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.ResetFiler();
        }

        private XmlDataProvider xmlDatProviderOrderHistory;
        private void GetOrderHistory()
        {
            xmlDatProviderOrderHistory = new XmlDataProvider();
            xmlDatProviderOrderHistory.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDatProviderOrderHistory_GetXmlDataCompleted);
            xmlDatProviderOrderHistory.GetXmlDataAsync("OrderHistory.xml"); // xml file name
        }

        void xmlDatProviderOrderHistory_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;
            if (doc != null)
            {
                XElement docRoot = doc.Root;
                var dataSource = (from d in doc.Descendants("Row")
                                  select new OrderHistory
                                  {
                                      OrderDate = d.Attribute("OrderDate").GetDateTime(),
                                      ShipName = d.Attribute("ShipName").Value,
                                      Price = d.Attribute("Price").GetDouble(),
                                      CategoryName = d.Attribute("CategoryName").Value,
                                      ProductName = d.Attribute("ProductName").Value,
                                      Supplier = d.Attribute("Supplier").Value
                                  });
                this.viewModel.OrderHistory = dataSource.ToList();
            }
        }
    }
}