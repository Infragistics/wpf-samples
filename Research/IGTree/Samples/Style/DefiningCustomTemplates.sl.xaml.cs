using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGTree.Samples.Style
{
    public partial class DefiningCustomTemplates : SampleContainer
    {
        public DefiningCustomTemplates()
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
            xmlDataProvider.GetXmlDataAsync("PresidentHistory.xml");
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            if (doc != null)
            {
                var dataSource = (from d in doc.Descendants("PoliticalParty")
                                  select new PoliticalParty
                                  {
                                      Name = d.Attribute("Name").Value,
                                      ImageResourcePath = ImageFilePathProvider.GetImageLocalPath(d.Attribute("ImageResourcePath").Value),
                                      Presidents = this.GetPresidents(d)
                                  });

                this.dataTree.ItemsSource = dataSource.ToList();
                this.dataTree.XamTreeItems[0].IsExpanded = true;
            }
        }

        private IEnumerable<President> GetPresidents(XElement parent)
        {
            PoliticalPartyType partyType = (PoliticalPartyType)Enum.Parse(typeof(PoliticalPartyType), parent.Attribute("Type").Value, true);

            return (from d in parent.Descendants("President")
                    select new President
                    {
                        PartyType = partyType,
                        Name = d.Attribute("Name").Value,
                        Years = d.Attribute("Years").Value,
                        Spouse = d.Attribute("Spouse").Value,
                        ImageResourcePath = ImageFilePathProvider.GetImageLocalPath(d.Attribute("ImageResourcePath").Value)
                    }).ToList<President>();
        }
    }
}