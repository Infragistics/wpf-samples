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
    public partial class TagCloudInteraction : Infragistics.Samples.Framework.SampleContainer
    {
        private TagViewModel viewModel;
        private XmlDataProvider xmlDataProvider;

        public TagCloudInteraction()
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
                var result = this.GetItems(docRoot, "IGSite");
                this.viewModel.Tags = result;
            }
        }

        private IList<TagItem> GetItems(XElement parent, string filter)
        {
            return ((from d in parent.Elements("TagGroup").Descendants("Tag")
                     where d.Parent.Attribute("Name").Value == filter
                     select new TagItem
                     {
                         Content = d.Attribute("Content").Value,
                         Weight = d.Attribute("Weight").GetInt(),
                         NavigateUri = d.Attribute("NavigateUri").Value
                     }).ToList<TagItem>());
        }

        private void dataTagCloud_XamTagCloudItemClicked(object sender, XamTagCloudItemEventArgs e)
        {
            XamTagCloudItem selectedTag = e.XamTagCloudItem as XamTagCloudItem;
            if (selectedTag != null)
            {
                this.SelectedTag.Text = selectedTag.Content.ToString();
                this.SelectedTagUri.Text = selectedTag.NavigateUri.ToString();
            }
        }
    }
}