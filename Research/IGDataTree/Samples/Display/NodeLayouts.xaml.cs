using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models.General;

namespace IGDataTree.Samples.Display
{
    public partial class NodeLayouts : SampleContainer
    {
        public NodeLayouts()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
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
            xmlDataProvider.GetXmlDataAsync("Families.xml"); // xml file name
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            if (doc != null)
            {
                var dataSource = (from d in doc.Descendants("Families")
                                  select new Family
                                  {
                                      Name = d.Element("Name").Value,
                                      Parents = GetParents(d),
                                      Children = GetChildren(d),
                                      CarLayouts = GetCars(d),
                                  });

                MyTree.ItemsSource = dataSource.ToList();
                MyTree.Nodes[0].IsExpanded = true;
                MyTree.Nodes[0].Nodes[0].IsExpanded = true;
                MyTree.Nodes[0].Nodes[1].IsExpanded = true;
                MyTree.Nodes[0].Nodes[2].IsExpanded = true;
            }
        }

        private IEnumerable<Parent> GetParents(XElement parent)
        {
            return (from d in parent.Descendants("Parents")
                    select new Parent
                    {
                        Name = d.Element("Name").Value,
                    }).ToList<Parent>();
        }


        private IEnumerable<Child> GetChildren(XElement parent)
        {
            return (from d in parent.Descendants("Children")
                    select new Child
                    {
                        Name = d.Element("Name").Value,
                    }).ToList<Child>();
        }

        private IEnumerable<CarLayout> GetCars(XElement parent)
        {
            return (from d in parent.Descendants("Cars")
                    select new CarLayout
                    {
                        Name = d.Element("Name").Value,
                    }).ToList<CarLayout>();
        }
    }
}