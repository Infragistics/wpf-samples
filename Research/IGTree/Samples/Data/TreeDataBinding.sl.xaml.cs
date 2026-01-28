using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGTree.Samples.Data
{
    public partial class TreeDataBinding : SampleContainer
    {
        public TreeDataBinding()
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
            xmlDataProvider.GetXmlDataAsync("Books.xml");
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            var dataSource = (from d in doc.Descendants("book")
                              where (d.Descendants("chapter") != null && d.Descendants("chapter").Count() > 0)
                              select new Book
                              {
                                  Author = d.Attribute("Author").Value,
                                  Title = d.Attribute("Title").Value,
                                  UnitPrice = d.Attribute("UnitPrice").GetDecimal(),
                                  Url = d.Attribute("Url").Value,
                                  ReleaseDate = d.Attribute("ReleaseDate").Value,
                                  Chapters = this.GetChapters(d)
                              });

            this.dataTree.ItemsSource = dataSource.ToList();
            this.dataTree.XamTreeItems[0].IsExpanded = true;
        }

        private IEnumerable<Chapter> GetChapters(XElement parent)
        {
            return (from d in parent.Descendants("chapter")
                    select new Chapter
                    {
                        Title = d.Attribute("Title").Value
                    }).ToList<Chapter>();
        }
    }
}