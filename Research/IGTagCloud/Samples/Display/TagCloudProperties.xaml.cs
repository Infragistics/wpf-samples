using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGTagCloud.Samples.Display
{
    public partial class TagCloudProperties : Infragistics.Samples.Framework.SampleContainer
    {
        private TagViewModel viewModel;
        private XmlDataProvider xmlDataProvider;

        public TagCloudProperties()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = new TagViewModel();
            this.DataContext = this.viewModel;

            this.MinScale.Value = 0d;
            this.MaxScale.Value = 2d;

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

        private void MinScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.dataTagCloud.MinScale = this.MinScale.Value;
            this.dataTagCloud.UpdateLayout();

            if (this.MinScale.Value > this.MaxScale.Value)
            {
                this.MaxScale.Value = this.MinScale.Value;
            }
        }

        private void MaxScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.dataTagCloud.MaxScale = this.MaxScale.Value;
            this.dataTagCloud.UpdateLayout();

            if (this.MaxScale.Value < this.MinScale.Value)
            {
                this.MinScale.Value = this.MaxScale.Value;
            }
        }
    }
}
