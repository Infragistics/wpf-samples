using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models.General;

namespace IGDataTree.Samples.Editing
{
    public partial class NodeSelection : SampleContainer
    {
        public NodeSelection()
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
            xmlDataProvider.GetXmlDataAsync("Contacts.xml"); // xml file name
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            if (doc != null)
            {
                var dataSource = (from d in doc.Descendants("Contacts")
                                  select new Contact
                                  {
                                      Name = d.Element("Name").Value,
                                      ContactDetails = this.GetContactDetails(d)
                                  });


                this.MyTree.ItemsSource = dataSource.ToList();
            }
        }

        private IEnumerable<ContactDetail> GetContactDetails(XElement parent)
        {
            return (from d in parent.Descendants("ContactDetails")
                    select new ContactDetail
                    {
                        Name = d.Element("Name").Value,
                    }).ToList<ContactDetail>();
        }
    }
}