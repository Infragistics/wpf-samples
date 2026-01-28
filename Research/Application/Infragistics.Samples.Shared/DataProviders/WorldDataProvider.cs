using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Extensions;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for loading info about all countries in the world.
    /// </summary>
    public class WorldDataProvider
    {
        public WorldDataProvider()
        {
            this.CountriesDir = new Dictionary<string, CountryDataModel>();
            this.LoadingError = null;
        }

        #region Properties

        protected Dictionary<string, CountryDataModel> CountriesDir;

        public List<CountryDataModel> CountriesList
        {
            get { return CountriesDir.Values.Where(i => i.IsValid()).ToList(); }
        }

        protected bool IsLoadedWorldInfoData;
        protected bool IsLoadedWorldFinancialData;
        protected bool IsErrorFound;
        protected Exception LoadingError;
        #endregion
        #region Events
        /// <summary>
        /// Occurs when GetWorldDataAsync() is completed
        /// </summary>
        public event EventHandler<GetWorldDataCompletedEventArgs> GetWorldDataCompleted;

        /// <summary>
        /// Occurs when GetWorldInfoDataAsync() is completed
        /// </summary>
        public event EventHandler<GetWorldDataCompletedEventArgs> GetWorldInfoDataCompleted;

        /// <summary>
        /// Occurs when GetWorldFinancialDataAsync() is completed
        /// </summary>
        public event EventHandler<GetWorldDataCompletedEventArgs> GetWorldFinancialDataCompleted;

        #endregion

        #region Methods
        private bool IsWorldDataLoaded()
        {
            if (WorldDataManager.DataSource.Any())
            {
                this.CountriesDir = WorldDataManager.DataSource;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets World Country (Info and Financial) data from the following files using the XmlDataProvider:
        /// <para>- CountryData.xml </para> 
        /// <para>- GdpByCountry.xml </para> 
        /// </summary>
        public void GetWorldDataAsync()
        {
            if (IsWorldDataLoaded())
            {
                this.ValidateEvents(); return;
            }
            this.IsErrorFound = false;
            this.IsLoadedWorldFinancialData = false;
            this.IsLoadedWorldInfoData = false;

            this.GetWorldFinancialDataAsync();
            this.GetWorldInfoDataAsync();
        }
        /// <summary>
        /// Gets World Financial data from GdpByCountry.xml file using the XmlDataProvider
        /// </summary>
        public void GetWorldFinancialDataAsync()
        {
            if (IsWorldDataLoaded())
            {
                this.ValidateEvents(); return;
            }
         
            this.IsErrorFound = false;
            this.IsLoadedWorldFinancialData = false;

            var dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnWorldFinancialXmlDataCompleted;
            dataProvider.GetXmlDataAsync("GdpByCountry.xml");
        }

        /// <summary>
        /// Get World Info data from CountryData.xml file using the XmlDataProvider
        /// </summary>
        public void GetWorldInfoDataAsync()
        {
            if (IsWorldDataLoaded())
            {
                this.ValidateEvents(); return;
            }
         
            this.IsErrorFound = false;
            this.IsLoadedWorldInfoData = false;

            var dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnGetWorldInfoXmlDataCompleted;
            dataProvider.GetXmlDataAsync("CountryData.xml");
        }

        private void ValidateEvents()
        {
            if (this.LoadingError != null)
            {
                // notify about the loading error
                OnLoadingError(this.LoadingError);
            }
            if (IsLoadedWorldInfoData && this.GetWorldInfoDataCompleted != null)
            {
                // notify about successful loading of WorldInfoData
                this.GetWorldInfoDataCompleted(this, new GetWorldDataCompletedEventArgs(this.CountriesList));
            }
            if (IsLoadedWorldFinancialData && this.GetWorldFinancialDataCompleted != null)
            {
                this.GetWorldFinancialDataCompleted(this, new GetWorldDataCompletedEventArgs(this.CountriesList));
            }
            if (IsLoadedWorldInfoData && IsLoadedWorldFinancialData)
            {
                var items = this.CountriesList;
                if (this.GetWorldDataCompleted != null)
                {
                    this.GetWorldDataCompleted(this, new GetWorldDataCompletedEventArgs(this.CountriesList));
                }
            }
        }
        private void OnLoadingError(Exception error)
        {
            if (this.GetWorldDataCompleted != null)
            {
                this.GetWorldDataCompleted(this, new GetWorldDataCompletedEventArgs(error));
            }
            if (this.GetWorldInfoDataCompleted != null)
            {
                this.GetWorldInfoDataCompleted(this, new GetWorldDataCompletedEventArgs(error));
            }
            if (this.GetWorldFinancialDataCompleted != null)
            {
                this.GetWorldFinancialDataCompleted(this, new GetWorldDataCompletedEventArgs(error));
            }
        }
        private XDocument ReadXmlData(string xmlString)
        {
            XDocument dataSource = new XDocument();
            using (StringReader reader = new StringReader(xmlString))
            {
                string resultString = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(resultString))
                {
                    dataSource = XDocument.Parse(resultString);
                }
            }
            return dataSource;
        }
   


        #endregion

        #region Parser Methods
        public List<CountryDataModel> ParseCountryData(string xmlString)
        {
            XDocument xmlDocument = ReadXmlData(xmlString);
            return ParseCountryData(xmlDocument);
        }
        public List<CountryDataModel> ParseCountryData(XDocument xmlDocument)
        {
            var items = xmlDocument.Element("NewDataSet").Elements("Table").ToList();
            var countries = items.Select(item => new CountryDataModel
            {
                // get values using XElementExtension
                CountryCode = item.Element("CountryCode").GetString(),
                CountryName = item.Element("CountryName").GetString(),
                BirthRate = item.Element("BirthRate").GetDouble(),
                ElectricityProduction = item.Element("ElectricityProduction").GetDouble(),
                InternetUsers = item.Element("InternetUsers").GetDouble(),
                MedianAge = item.Element("MedianAge").GetDouble(),
                OilProduction = item.Element("OilProduction").GetDouble(),
                Population = item.Element("Population").GetDouble(),
                PublicDebt = item.Element("PublicDebt").GetDouble(),
                Televisions = item.Element("Televisions").GetDouble(),
                UnemploymentRate = item.Element("UnemploymentRate").GetDouble()
            }).ToList();
            
            return countries;
        }

        public List<CountryDataModel> ParseGdpByCountryData(string xmlString)
        {
            var xmlDocument = ReadXmlData(xmlString);
            return ParseGdpByCountryData(xmlDocument);
        }
        public List<CountryDataModel> ParseGdpByCountryData(XDocument xmlDocument)
        {
            var items = xmlDocument.Element("NewDataSet").Elements("GDPByCountry").ToList();
            var countries = items.Select(item => new CountryDataModel
            {
                // get values using XElementExtension
                CountryCode = item.Element("CountryCode").GetString(),
                GdpPerCapita = item.Element("GDPPerCapita").GetDouble()
            }).ToList();

            return countries;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the GetXmlDataCompleted event and parses the country data.
        /// </summary>
        private void OnGetWorldInfoXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.LoadingError = e.Error;
            }
            else
            {
                var newItems = ParseCountryData(e.Result);
                // add new world data or merged it with existing data
                foreach (var item in newItems)
                {
                    if (this.CountriesDir.ContainsKey(item.CountryCode))
                    {
                        this.CountriesDir[item.CountryCode].UpdateWith(item);
                    }
                    else
                    {
                        this.CountriesDir[item.CountryCode] = item;
                    }
                }
                //this.WorldData = this.WorldData.Count == 0 ? newWorldData : this.MergeWorldData(this.WorldData, newWorldData);
                this.IsLoadedWorldInfoData = true;
            }

            this.ValidateEvents();
        }
        /// <summary>
        /// Handles the GetXmlDataCompleted event and parses the country data.
        /// </summary>
        private void OnWorldFinancialXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.LoadingError = e.Error;
            }
            else
            {
                var newItems = ParseGdpByCountryData(e.Result);
                // add new world data or merged it with existing data
                foreach (var item in newItems)
                {
                    if (this.CountriesDir.ContainsKey(item.CountryCode))
                    {
                        this.CountriesDir[item.CountryCode].UpdateWith(item);
                    }
                    else
                    {
                        this.CountriesDir[item.CountryCode] = item;
                    }
                }
                //this.WorldData = this.WorldData.Count == 0 ? newWorldData : this.MergeWorldData(this.WorldData, newWorldData);
                this.IsLoadedWorldFinancialData = true;
            }
            this.ValidateEvents();
        }


        #endregion


    }
    public class GetWorldDataCompletedEventArgs : EventArgs
    {

        public GetWorldDataCompletedEventArgs(List<CountryDataModel> data)
        {
            this.Result = data;
            this.Error = null;
        }

        public GetWorldDataCompletedEventArgs(Exception error)
        {
            this.Result = null;
            this.Error = error;
        }
        public List<CountryDataModel> Result { get; set; }
        public Exception Error { get; private set; }

    }
    public static class WorldDataManager
    {
        static WorldDataManager()
        {
            DataSource = new Dictionary<string, CountryDataModel>();
        }
        public static Dictionary<string, CountryDataModel>  DataSource;
    
    }
}