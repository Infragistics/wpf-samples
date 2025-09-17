using System;
using System.Collections.Generic;
using Infragistics.Controls.Maps;

namespace IGGeographicMap.Extensions
{
    #region GeoImagery
    /// <summary>
    /// Represents a base class for all geo-imagery map views.
    /// </summary>
    public abstract class GeoImageryView
    {
        public GeoImagerySource ImagerySource { get; protected set; }
    }
    /// <summary>
    /// Defines type of geo-imagery source
    /// </summary>
    public enum GeoImagerySource
    {
        AzureMapsImagery,
        BingMapsImagery,
        MapQuestImagery,
        OpenStreetMapImagery, 
        EsriMapImagery,
    }
    /// <summary>
    /// Represents a list of GeoImageryView objects.
    /// </summary>
    public class GeoImageryViews : List<GeoImageryView>
    { } 
    #endregion

    #region OpenStreetMap Imagery View

    /// <summary>
    /// Represents a map view for the OpenStreetMap geo-imagery.
    /// </summary>
    
    public class AzureMapImageryView : GeoImageryView 
    {
        public AzureMapImageryView()
        {
            this.ImagerySource = GeoImagerySource.AzureMapsImagery;
            this.ImageryStyle = AzureMapsImageryStyle.Satellite;
        }
        public AzureMapsImageryStyle ImageryStyle { get; set; }
        public string ImageryName { get { return this.ImagerySource + " (" + this.ImageryStyle + ")"; } }
        public override string ToString()
        {
            return this.ImageryName;
        }
    }
    
    public class OpenStreetMapImageryView : GeoImageryView
    {
        public OpenStreetMapImageryView()
        {
            this.ImagerySource = GeoImagerySource.OpenStreetMapImagery;
        }
        /// <summary>
        /// Gets the style name of the OpenStreetMap geo-imagery. 
        /// </summary>
        public string ImageryName { get { return this.ImagerySource.ToString(); } }
        public override string ToString()
        {
 	         return this.ImageryName;
        }
    } 
    #endregion

    #region EsriMap Imagery View

    /// <summary>
    /// Represents a map view for the ArcGISOnlineMap geo-imagery.
    /// </summary>
    public class EsriMapImageryView : GeoImageryView
    {
        public EsriMapImageryView() 
        {
            this.ImagerySource = GeoImagerySource.EsriMapImagery;
            this.ImageryStyle = EsriMapImageryStyle.WorldStreetMap;
            //http://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer
            this.ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer";
        }
        public EsriMapImageryView(EsriMapImageryStyle imageryStyle)
            : this(GetImageryView(imageryStyle))
        { 
        }
        public EsriMapImageryView(EsriMapImageryView imageryView)
        {
            this.ImagerySource = GeoImagerySource.EsriMapImagery;
            this.ImageryStyle = imageryView.ImageryStyle; 
            this.ImageryServer = imageryView.ImageryServer;
        }
        
        public EsriMapImageryStyle ImageryStyle { get; set; }
        public string ImageryServer { get; set; }

        /// <summary>
        /// Gets the style name of the ArcGISOnlineMap geo-imagery. 
        /// </summary>
        public string ImageryName { get { return this.ImagerySource + " (" + this.ImageryStyle + ")"; } }
        public override string ToString()
        {
            return this.ImageryName;
        }
        public static EsriMapImageryView GetImageryView(EsriMapImageryStyle imageryStyle)
        {
            #region World Maps
            if (imageryStyle == EsriMapImageryStyle.WorldStreetMap)
                return EsriMapImageryViews.WorldStreetMap;
            if (imageryStyle == EsriMapImageryStyle.WorldTopographicMap)
                return EsriMapImageryViews.WorldTopographicMap;
            if (imageryStyle == EsriMapImageryStyle.WorldImageryMap)
                return EsriMapImageryViews.WorldImageryMap;
            if (imageryStyle == EsriMapImageryStyle.WorldOceansMap)
                return EsriMapImageryViews.WorldOceansMap;
            if (imageryStyle == EsriMapImageryStyle.WorldNationalGeographicMap)
                return EsriMapImageryViews.WorldNationalGeographicMap;
            if (imageryStyle == EsriMapImageryStyle.WorldPhysicalMap)
                return EsriMapImageryViews.WorldPhysicalMap;
            if (imageryStyle == EsriMapImageryStyle.WorldDeLormesMap)
                return EsriMapImageryViews.WorldDeLormesMap;
            if (imageryStyle == EsriMapImageryStyle.WorldLightGrayMap)
                return EsriMapImageryViews.WorldLightGrayMap;
            if (imageryStyle == EsriMapImageryStyle.WorldShadedReliefMap)
                return EsriMapImageryViews.WorldShadedReliefMap;
            #endregion
            #region World Overlay
            if (imageryStyle == EsriMapImageryStyle.WorldAdministrativeOverlay)
                return EsriMapImageryViews.WorldAdministrativeOverlay;
            if (imageryStyle == EsriMapImageryStyle.WorldTransportationOverlay)
                return EsriMapImageryViews.WorldTransportationOverlay;
            if (imageryStyle == EsriMapImageryStyle.WorldBoundariesDarkOverlay)
                return EsriMapImageryViews.WorldBoundariesDarkOverlay;
            if (imageryStyle == EsriMapImageryStyle.WorldBoundariesLightOverlay)
                return EsriMapImageryViews.WorldBoundariesLightOverlay;
            if (imageryStyle == EsriMapImageryStyle.WorldLightGrayOverlay)
                return EsriMapImageryViews.WorldLightGrayOverlay;
            #endregion
            #region USA Thematic
            if (imageryStyle == EsriMapImageryStyle.UsaPopulationGrowth2015Overlay)
                return EsriMapImageryViews.UsaPopulationGrowth2015Overlay;
            if (imageryStyle == EsriMapImageryStyle.UsaUnemploymentRateOverlay)
                return EsriMapImageryViews.UsaUnemploymentRateOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaSocialVulnerabilityOverlay)
                return EsriMapImageryViews.UsaSocialVulnerabilityOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaRetailSpendingPotentialOverlay)
                return EsriMapImageryViews.UsaRetailSpendingPotentialOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaPopulationChange2010Overlay)
                return EsriMapImageryViews.UsaPopulationChange2010Overlay;
            if (imageryStyle == EsriMapImageryStyle.UsaPopulationChange2010Overlay)
                return EsriMapImageryViews.UsaPopulationChange2010Overlay;
            if (imageryStyle == EsriMapImageryStyle.UsaPopulationChange2000Overlay)
                return EsriMapImageryViews.UsaPopulationChange2000Overlay;
            if (imageryStyle == EsriMapImageryStyle.UsaPopulationDensityOverlay)
                return EsriMapImageryViews.UsaPopulationDensityOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaPopulationByGenderOverlay)
                return EsriMapImageryViews.UsaPopulationByGenderOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaMedianHouseholdIncomeOverlay)
                return EsriMapImageryViews.UsaMedianHouseholdIncomeOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaMedianNetWorthOverlay)
                return EsriMapImageryViews.UsaMedianNetWorthOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaMedianHomeValueOverlay)
                return EsriMapImageryViews.UsaMedianHomeValueOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaMedianAgeOverlay)
                return EsriMapImageryViews.UsaMedianAgeOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaLaborForceParticipationOverlay)
                return EsriMapImageryViews.UsaLaborForceParticipationOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaAverageHouseholdSizeOverlay)
                return EsriMapImageryViews.UsaAverageHouseholdSizeOverlay;
            if (imageryStyle == EsriMapImageryStyle.UsaDiversityIndexOverlay)
                return EsriMapImageryViews.UsaDiversityIndexOverlay;

            #endregion
            return EsriMapImageryViews.WorldStreetMap;
        }
  
    }
    public enum EsriMapImageryStyle
    {
        WorldStreetMap,
        WorldTopographicMap,
        WorldImageryMap,
        WorldOceansMap,
        WorldNationalGeographicMap,
        WorldPhysicalMap,
        WorldTerrainMap,
        WorldDeLormesMap,
        WorldLightGrayMap,
        WorldShadedReliefMap,
        WorldAdministrativeOverlay,
        WorldTransportationOverlay,
        WorldBoundariesDarkOverlay,
        WorldBoundariesLightOverlay,
        WorldLightGrayOverlay,
        UsaRailNetworkOverlay,
        UsaPopulationGrowth2015Overlay,
        UsaUnemploymentRateOverlay,
        UsaSocialVulnerabilityOverlay,
        UsaRetailSpendingPotentialOverlay,
        UsaPopulationChange2010Overlay,
        UsaPopulationChange2000Overlay,
        UsaPopulationDensityOverlay,
        UsaPopulationByGenderOverlay,
        UsaMedianHouseholdIncomeOverlay,
        UsaMedianNetWorthOverlay,
        UsaMedianHomeValueOverlay,
        UsaMedianAgeOverlay,
        UsaLaborForceParticipationOverlay,
        UsaAverageHouseholdSizeOverlay,
        UsaDiversityIndexOverlay,
        UsaSoilSurveyOverlay,
        UsaPopulationYoungerThan18Overlay,
        UsaPopulationOlderThanAge64Overlay,
        UsaOwnerOccupiedHousingOverlay,
    }

    public static class EsriMapImageryViews
    {
        #region World maps
        public static EsriMapImageryView WorldStreetMap
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldStreetMap,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldTopographicMap
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldTopographicMap,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldImageryMap
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldImageryMap,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldOceansMap
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldOceansMap,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Ocean/World_Ocean_Base/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldNationalGeographicMap
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldNationalGeographicMap,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/NatGeo_World_Map/MapServer"
                };
            }
        }

        public static EsriMapImageryView WorldTerrainMap
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldTerrainMap,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Terrain_Base/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldDeLormesMap
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldDeLormesMap,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Specialty/DeLorme_World_Base_Map/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldLightGrayMap
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldLightGrayMap,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Canvas/World_Light_Gray_Base/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldShadedReliefMap
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldShadedReliefMap,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Shaded_Relief/MapServer"
                };
            }
        }
        #endregion
        #region World Overlay
        public static EsriMapImageryView WorldAdministrativeOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldAdministrativeOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Reference_Overlay/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldPhysicalMap
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldPhysicalMap,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Physical_Map/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldTransportationOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldTransportationOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Transportation/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldBoundariesDarkOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldBoundariesDarkOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldBoundariesLightOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldBoundariesLightOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places_Alternate/MapServer"
                };
            }
        }
        public static EsriMapImageryView WorldLightGrayOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.WorldLightGrayOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Canvas/World_Light_Gray_Reference/MapServer"
                };
            }
        }
        #endregion
        #region USA Thematic
        public static EsriMapImageryView UsaOwnerOccupiedHousingOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaOwnerOccupiedHousingOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Owner_Occupied_Housing/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaSoilSurveyOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaSoilSurveyOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Specialty/Soil_Survey_Map/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaPopulationOlderThanAge64Overlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaPopulationOlderThanAge64Overlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Percent_Over_64/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaPopulationYoungerThan18Overlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaPopulationYoungerThan18Overlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Percent_Under_18/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaPopulationGrowth2015Overlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaPopulationGrowth2015Overlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Projected_Population_Change/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaRailNetworkOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaRailNetworkOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Reference_Overlay/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaUnemploymentRateOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaUnemploymentRateOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Unemployment_Rate/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaSocialVulnerabilityOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaSocialVulnerabilityOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Social_Vulnerability_Index/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaRetailSpendingPotentialOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaRetailSpendingPotentialOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Retail_Spending_Potential/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaPopulationChange2010Overlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaPopulationChange2010Overlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Recent_Population_Change/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaPopulationChange2000Overlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaPopulationChange2000Overlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_1990-2000_Population_Change/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaPopulationDensityOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaPopulationDensityOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Population_Density/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaPopulationByGenderOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaPopulationByGenderOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Population_by_Sex/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaMedianHouseholdIncomeOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaMedianHouseholdIncomeOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Median_Household_Income/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaMedianNetWorthOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaMedianNetWorthOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Median_Net_Worth/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaMedianHomeValueOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaMedianHomeValueOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Median_Home_Value/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaMedianAgeOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaMedianAgeOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Median_Age/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaLaborForceParticipationOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaLaborForceParticipationOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Labor_Force_Participation_Rate/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaAverageHouseholdSizeOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaAverageHouseholdSizeOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Average_Household_Size/MapServer"
                };
            }
        }
        public static EsriMapImageryView UsaDiversityIndexOverlay
        {
            get
            {
                return new EsriMapImageryView
                {
                    ImageryStyle = EsriMapImageryStyle.UsaDiversityIndexOverlay,
                    ImageryServer = "http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Diversity_Index/MapServer"
                };
            }
        }
        #endregion
    }
    #endregion
    
    #region BingMap Imagery View
    /// <summary>
    /// Represents a map view for the BingsMap geo-imagery. 
    /// </summary>
    public class BingMapsImageryView : GeoImageryView
    {
        public BingMapsImageryView()
        {
            this.ImagerySource = GeoImagerySource.BingMapsImagery;
            this.ImageryStyle = BingMapsImageryStyle.Road;
        }
        public Infragistics.Controls.Maps.BingMapsImageryStyle ImageryStyle { get; set; }
        public string ImageryName { get { return this.ImagerySource + " (" + this.ImageryStyle + ")"; } }
        public override string ToString()
        {
            return this.ImageryName;
        }
    } 
    #endregion

    #region MapQuest Imagery View
    /// <summary>
    /// Represents a map view for the MapQuest geo-imagery. 
    /// </summary>
    public class MapQuestImageryView : GeoImageryView
    {
        public MapQuestImageryView()
        {
            this.ImagerySource = GeoImagerySource.MapQuestImagery;
            this.ImageryStyle = MapQuestImageryStyle.StreetMapStyle;
        }
        public MapQuestImageryStyle ImageryStyle { get; set; }
        /// <summary>
        /// Gets the style name of the MapQuest geo-imagery. 
        /// </summary>
        public string ImageryName { get { return this.ImagerySource + " (" + this.ImageryStyle + ")"; } }
        public override string ToString()
        {
            return this.ImageryName;
        }
    }
    #endregion

    public class OSM_Imagery : Infragistics.Controls.Maps.GeographicMapImagery
    {
        public OSM_Imagery()
            : base(new OSM_TileSource())
        { }
    }

    public class OSM_TileSource : Infragistics.Controls.Maps.MapTileSource
    {
        public OSM_TileSource()
            : base(134217728, 134217728, 256, 256, 0)
        { }

        private const string CustomPath = "http://tile.openstreetmap.org/{z}/{x}/{y}.png";
        //private const string CustomPath = "http://c.tile.opentopomap.org/{z}/{x}/{y}.png";
        /// <summary>
        /// Gets path for the type of a geographic imagery tile
        /// </summary>
        /// <returns></returns>
        private string GetTileType()
        {
            return CustomPath;
        }
        /// <summary>
        /// Overridden method for getting a geographic imagery tile at specific position of the map
        /// </summary>
        protected override void GetTileLayers(int tileLevel, int tilePositionX, int tilePositionY, IList<object> tileImageLayerSources)
        {
            var tilePath = GetTileType();
            int zoom = tileLevel - 8;
            if (zoom > 0)
            {
                string uriString = tilePath;
                uriString = uriString.Replace("{z}", zoom.ToString());
                uriString = uriString.Replace("{x}", tilePositionX.ToString());
                uriString = uriString.Replace("{y}", tilePositionY.ToString());
                //System.Diagnostics.Debug.WriteLine(uriString);
                tileImageLayerSources.Add(new Uri(uriString));
            }
        }
    }

}