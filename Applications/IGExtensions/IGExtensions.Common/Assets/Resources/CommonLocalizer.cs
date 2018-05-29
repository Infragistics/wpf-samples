using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using IGExtensions.Framework;

namespace IGExtensions.Common.Assets.Resources
{
    /// <summary>
    /// Represents common assets localizer that provides access to the <see cref="CommonStrings"/> 
    /// </summary>
    public class CommonLocalizer : ObservableModel //: CommonStrings
    {
        private static readonly CommonStrings CommonStringAssets = new CommonStrings();
        //private static readonly PropertyStrings PropertyStringsAssets = new PropertyStrings();
        private static readonly GeoStrings GeoStringsAssets = new GeoStrings();

        
        protected static List<string> DataSourceList = new List<string>();
        public CommonLocalizer()
        {
            DataSourceList  = new List<string>();
            DataSourceList.Add(CommonStrings.SourceImagery_BingMaps);
            DataSourceList.Add(CommonStrings.SourceImagery_EsriMaps);            
            DataSourceList.Add(CommonStrings.SourceImagery_MapQuest);
            DataSourceList.Add(CommonStrings.SourceImagery_OpenStreetMap);
            DataSourceList.Add(CommonStrings.SourceData_NOAA);
            DataSourceList.Add(CommonStrings.SourceData_USNA);
            DataSourceList.Add(CommonStrings.SourceData_USGS);
            DataSourceList.Add(CommonStrings.SourceData_USCB);
            DataSourceList.Add(CommonStrings.SourceData_BTS);
            DataSourceList.Add(CommonStrings.SourceData_CIA);
            DataSourceList.Add(CommonStrings.SourceData_GeoCommons);
            DataSourceList.Add(CommonStrings.SourceData_USAT);
           
            _dataSource = string.Empty;
            foreach (var source in DataSourceList)
            {
                _dataSource += source;
                if (source != DataSourceList.Last()) _dataSource += ", ";
            }
            //_dataSource = dataSources;
        }
        /// <summary>
        /// Gets common strings resource 
        /// </summary>
        public CommonStrings CommonStrings
		{
            get { return CommonStringAssets; }
		}
        ///// <summary>
        ///// Gets property strings resource 
        ///// </summary>
        //public PropertyStrings PropertyStrings
        //{
        //    get { return PropertyStringsAssets; }
        //}
        /// <summary>
        /// Gets geographic strings resource 
        /// </summary>
        public GeoStrings GeoStrings
        {
            get { return GeoStringsAssets; }
        }

        private string _dataSource;
        public string DataSources { get { return _dataSource; } }
         

        //TODO generate links based on control name and ValueConverter
        public string DataChartSamplesLink
        {
            get { return GetActualSamplesLink(CommonStrings.DataChartSamplesLinkSL); }
        }
        public string DockManagerSamplesLink
        {
            get { return GetActualSamplesLink(CommonStrings.DockManagerSamplesLinkSL); }
        }
        public string GeographicMapSamplesLink
        {
            get { return GetActualSamplesLink(CommonStrings.GeographicMapSamplesLinkSL); }
        }
        public string PivotGridSamplesLink
        {
            get { return GetActualSamplesLink(CommonStrings.PivotGridSamplesLinkSL); }
        }
        public string SliderSamplesLink
        {
            get { return GetActualSamplesLink(CommonStrings.SliderSamplesLinkSL); }
        }
        public string TileManagerSamplesLink
        {
            get { return GetActualSamplesLink(CommonStrings.TileManagerSamplesLinkSL); }
        }
        public string TreeSamplesLink
        {
            get { return GetActualSamplesLink(CommonStrings.TreeSamplesLinkSL); }
        }
   
        public string AppInfoDownloadLink
        {
            get { return GetActualSamplesLink(CommonStrings.AppInfoDownloadLinkSL); }
        }
        public string GetActualSamplesLink(string genericSamplesLink)
        {
            var link = genericSamplesLink;
#if SILVERLIGHT
            link = genericSamplesLink.Replace("wpf", "silverlight");
#else
            link = genericSamplesLink.Replace("silverlight", "wpf");
#endif
            return link;

        }
    }
}
