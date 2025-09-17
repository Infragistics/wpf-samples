using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGTagCloud.Samples.Styling
{
    public partial class TagCloudStyling : SampleContainer
    {
        private XmlDataProvider xmlDataProvider;

        public TagCloudStyling()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
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
                var result = this.GetItems(docRoot, "CarSales");
                this.dataTagCloud.ItemsSource = result;
            }
        }

        private IEnumerable<TagItem> GetItems(XElement parent, string filter)
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
    }
}