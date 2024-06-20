using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGMap.Samples.Data
{
    public partial class BindingSpatialData : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingSpatialData()
        {
            InitializeComponent();

            this.Loaded += OnSampleLoaded;
        }
        private XmlDataProvider _xmlDataProvider;
      
        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // create xml data provider that will load data from xml file
            _xmlDataProvider = new XmlDataProvider();
            _xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _xmlDataProvider.GetXmlDataAsync("MapSpatialData.xml"); // xml file name
        }
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) { MessageBox.Show("Spatial Data Not Found"); return; }

            // parse xml values 
            XDocument document = e.Result; 
            List<Dictionary<string, string>> data = this.MapSpatialData(document.Root);

            var reader = this.theMap.Layers["CountryLayer"].Reader as SqlShapeReader;
            if (reader != null)
            {
                reader.DataSource = data;
                theMap.Layers["CountryLayer"].ImportAsync();
            }
        }
        
        public List<Dictionary<string, string>> MapSpatialData(XElement mapElement)
        {
            // Load Spatial Data from XML
            var mapData = (from md in mapElement.Descendants("Map").Descendants("SpatialElement")
                           where md.Parent.Attribute("ID").Value == "Europe"
                           select new SpatialData
                           {
                               Name = md.Attribute("Name").GetString(),
                               CountryCode = md.Attribute("CountryCode").GetString(),
                               Value = md.Attribute("Value").GetString(),
                               SpatialValue = md.Attribute("SpatialValue").GetString(),
                           }).ToList<SpatialData>();

            // Convert Spatial Data into Dictionary
            var spatialData = new List<Dictionary<string, string>>();
            foreach (SpatialData item in mapData)
            {
                var valueDictionary = new Dictionary<string, string>();
                valueDictionary.Add("Name", item.Name);
                valueDictionary.Add("CountryCode", item.CountryCode);
                valueDictionary.Add("Value", item.Value);
                valueDictionary.Add("SpatialValue", item.SpatialValue);
                spatialData.Add(valueDictionary);
            }

            return spatialData;
        }
        private void theMap_ElementHover(object sender, MapElementHoverEventArgs e)
        {
            SelectedCountry.Text = e.Element.Name;
            SlectedConutryCode.Text = e.Element.Caption;
            SlectedPopulaton.Text = e.Element.Value.ToString("N0");
        }
    }
}