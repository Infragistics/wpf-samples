using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;
using IGGeographicMap.Models;
using IGGeographicMap.Samples.Custom;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;    // XmlDataProvider
using Infragistics.Samples.Shared.Extensions;       // XElement extensions
using Infragistics.Samples.Shared.Models;           // CountryDataModel
using Infragistics.Samples.Shared.Resources;
using System;

namespace IGGeographicMap.Samples.Data
{
    public partial class BindingXmlFiles : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingXmlFiles()
        {
            InitializeComponent();
          
            this.WorldDataViewModel = new WorldDataViewModel();
            this.DataContext = this.WorldDataViewModel;


            var path = "/Infragistics.Samples.Services;component/shapefiles/world/";            
            Shapefile = new ShapefileConverter();
            Shapefile.ImportCompleted += OnShapefileImported;
            Shapefile.ShapefileSource = new Uri(path + "world_countries_reg.shp", UriKind.RelativeOrAbsolute);
            Shapefile.DatabaseSource  = new Uri(path + "world_countries_reg.dbf", UriKind.RelativeOrAbsolute);

            // create data provider for reading data from XML file
            this.XmlDataProvider = new XmlDataProvider();
            this.XmlDataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
            this.XmlDataProvider.GetXmlDataAsync("CountryData.xml");
        }
        protected ShapefileConverter Shapefile;
        protected WorldDataViewModel WorldDataViewModel;
        protected XmlDataProvider XmlDataProvider;
        
        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
           if(e.Error != null) return;

            List<CountryDataModel> worldData = ParseCountryData(e.Result);
            foreach (CountryDataModel countryData in worldData)
            {
                this.WorldDataViewModel.AddCountryData(countryData);
            }
            // update world data with loaded world data from XML file
            this.WorldDataViewModel.UpdateWorlData();
            this.GeoMap.Series[0].ItemsSource = this.WorldDataViewModel.WorldData;

            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
            this.GeoMap.Visibility = System.Windows.Visibility.Visible;
        }
        /// <summary>
        /// Parser XML document with country data 
        /// </summary>
        public List<CountryDataModel> ParseCountryData(XDocument xmlDocument)
        {
            List<CountryDataModel> results =
               (from d in xmlDocument.Element("NewDataSet").Elements("Table")
                select new CountryDataModel
                {   
                    // get values using XElementExtension            
                    CountryCode = d.Element("CountryCode").GetString(),
                    CountryName = d.Element("CountryName").GetString(),
                    BirthRate = d.Element("BirthRate").GetDouble(),
                    ElectricityProduction = d.Element("ElectricityProduction").GetDouble(),
                    InternetUsers = d.Element("InternetUsers").GetDouble(),
                    MedianAge = d.Element("MedianAge").GetDouble(),
                    OilProduction = d.Element("OilProduction").GetDouble(),
                    Population = d.Element("Population").GetDouble(),
                    PublicDebt = d.Element("PublicDebt").GetDouble(),
                    Televisions = d.Element("Televisions").GetDouble(),
                    UnemploymentRate = d.Element("UnemploymentRate").GetDouble()
                }).ToList();

            return results;
        }
    
        private void OnShapefileImported(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            foreach (ShapefileRecord countryShape in Shapefile)
            {
                if (countryShape.Fields != null)
                    this.WorldDataViewModel.AddCountryShapeRecord(countryShape);
            }
            // update world data with loaded world shape records from DBF file
            this.WorldDataViewModel.UpdateWorlData();
            this.GeoMap.Series[0].ItemsSource = this.WorldDataViewModel.WorldData;           
        }                        
    }
    /// <summary>
    /// Represents a view model of World Data
    /// </summary>
    public class WorldDataViewModel : ObservableModel
    {
        public WorldDataViewModel()
        {
            this.WorldDictionary = new Dictionary<string, CountryDataViewModel>();
            this.WorldData = new ObservableCollection<CountryDataViewModel>();
        }
        public Dictionary<string, CountryDataViewModel> WorldDictionary { get; set; }

        private ObservableCollection<CountryDataViewModel> _worldData;
        public ObservableCollection<CountryDataViewModel> WorldData
        {
            get { return _worldData; }
            set
            {
                _worldData = value;
                OnPropertyChanged("WorldData");
            }
        }
        #region Methods
        public void AddCountryData(CountryDataModel countryData)
        {
            if (countryData.CountryName == null) return;

            var countryKey = countryData.CountryName;

            if (this.WorldDictionary.ContainsKey(countryKey))
            {
                // add countryData to existing item in the dictionary
                this.WorldDictionary[countryKey].CountryWorldData = countryData;
            }
            else
            {
                // add countryData as new item to in the dictionary
                var countryViewModel = new CountryDataViewModel();
                countryViewModel.CountryName = countryKey;
                countryViewModel.CountryWorldData = countryData;
                this.WorldDictionary.Add(countryKey, countryViewModel);
            }
        }
        public void AddCountryShapeRecord(ShapefileRecord countryShapeRecord)
        {
            if (countryShapeRecord.Fields == null) return;
            
            var countryKey = (string) countryShapeRecord.Fields["NAME"];

            if (this.WorldDictionary.ContainsKey(countryKey))
            {
                // add countryShapeRecord to existing item in the dictionary
                this.WorldDictionary[countryKey].Fields = countryShapeRecord.Fields;
                this.WorldDictionary[countryKey].Points = countryShapeRecord.Points;
            }
            else
            {
                // add countryShapeRecord as new item to in the dictionary
                var countryViewModel = new CountryDataViewModel();
                countryViewModel.CountryName = countryKey;
                countryViewModel.Points = countryShapeRecord.Points;
                countryViewModel.Fields = countryShapeRecord.Fields;
                this.WorldDictionary.Add(countryKey, countryViewModel);
            }
        }

        public void UpdateWorlData()
        {
            this.WorldData = new ObservableCollection<CountryDataViewModel>();
            foreach (KeyValuePair<string, CountryDataViewModel> kvp in this.WorldDictionary)
            {
                if (kvp.Value.Points != null && kvp.Value.Points.Count > 0)
                    this.WorldData.Add(kvp.Value);
            }
            OnPropertyChanged("WorldData");
         
        }
        #endregion

    }

    /// <summary>
    ///  Represents a country view model that contains data loaded from XML (CountryDataModel) and DBF files (ShapefileRecord)
    /// </summary>
    public class CountryDataViewModel : ShapefileRecord
    {
        public CountryDataViewModel()
        {
            this.CountryName = string.Empty;
            this.CountryWorldData = new CountryDataModel();
        }

        /// <summary>
        /// Gets or sets Country Data
        /// </summary>
        public CountryDataModel CountryWorldData { get; set; }

        public string CountryName { get; set; }

        public new string ToString()
        {
            return this.Points.Count + " " + CountryName;
        }
    }
}
