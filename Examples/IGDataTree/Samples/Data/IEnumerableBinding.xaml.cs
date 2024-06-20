using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models.General;

namespace IGDataTree.Samples.Data
{
    public partial class IEnumerableBinding : SampleContainer
    {
        public IEnumerableBinding()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private XmlDataProvider xmlDataProvider;

        /// <summary>
        /// This method is used for downloading the sample's data source. To support localization we created a custom helper class. 
        /// </summary>
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Continents.xml"); // xml file name
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            if (doc != null)
            {
                var dataSource = (from d in doc.Descendants("Continents")
                                  select new Continent
                                  {
                                      Name = d.Element("Name").Value,
                                      Countries = this.GetCountries(d)
                                  });


                this.MyTree.ItemsSource = dataSource.ToList();
            }
        }

        private IEnumerable<Country> GetCountries(XElement parent)
        {
            return (from d in parent.Descendants("Countries")
                    select new Country
                    {
                        CountryName = d.Element("CountryName").Value,
                        Cities = this.GetCities(d)
                    }).ToList<Country>();
        }

        private IEnumerable<City> GetCities(XElement parent)
        {
            return (from d in parent.Descendants("Cities")
                    select new City
                    {
                        CityName = d.Element("CityName").Value,
                    }).ToList<City>();
        }
    }
}