using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGTagCloud.Samples.Display
{
    public partial class TagCloudOverflow : Infragistics.Samples.Framework.SampleContainer
    {
        private TagViewModel viewModel;
        private XmlDataProvider xmlDataProvider;

        public TagCloudOverflow()
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
                var result = this.GetItems(docRoot, "web20");
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

        private void SeeMoreLink_Click(object sender, RoutedEventArgs e)
        {
            this.HostControl.ColumnDefinitions[0].Width = new GridLength(700);
        }

        private void dataTagCloud_XamTagCloudClipped(object sender, XamTagCloudClippedEventArgs e)
        {
            if (e.CloudClipped)
            {
                this.SeeMoreLink.Visibility = Visibility.Visible;
            }
            else
            {
                this.SeeMoreLink.Visibility = Visibility.Collapsed;
            }
        }
    }
}
