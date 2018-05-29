using System;
using System.Collections.Generic;
using IGExtensions.Common.Assets.Resources;
using IGExtensions.Common.Models;
using IGExtensions.Framework;
using Infragistics.Controls.Maps;

namespace IGExtensions.Common.Maps.Imagery // Infragistics.Controls.Maps 
{
    #region GeoImagery
    /// <summary>
    /// Represents a base class for all geo-imagery map views
    /// </summary>
    public abstract class GeoImageryViewModel : ObservableModel
    {
        protected GeoImageryViewModel()
        {
            //ImageryFileName = "OpenStreetMapImagery.png";
        }
        public bool IsMultiImagrySupported { get { return this.ImagerySource != GeoImagerySource.OpenStreetMapImagery;  } }

        public GeoImagerySource ImagerySource { get; protected set; }
        public abstract string ImageryName { get; }
        public string ImageryDisplayName { get; set; }
        public string ImageryDescription { get; set; }
     
        public string ImagerySourceTrademark { get { return GetSourceTrademark(); } }
        //public string ImageryLabel { get { return this.ImageryDisplayName + Environment.NewLine + this.ImageryDescription; } }

        //public abstract string ImageryPreviewPath { get; set; }
        public abstract string ImageryFileName { get; }

        public GeographicMapImagery GetGeographicMapImagery()
        {
            var geoImagery = GeoMapImagryProvider.GetGeographicMapImagery(this);
            return geoImagery;
        }
   
        public string GetSourceTrademark()
        {
            var result = string.Empty;
            if (this.ImagerySource == GeoImagerySource.BingMapsImagery)
                result = CommonStrings.SourceImagery_BingMaps;
            if (this.ImagerySource == GeoImagerySource.EsriMapImagery)
                result = CommonStrings.SourceImagery_EsriMaps;
            if (this.ImagerySource == GeoImagerySource.MapQuestImagery)
                result = CommonStrings.SourceImagery_MapQuest;
            if (this.ImagerySource == GeoImagerySource.OpenStreetMapImagery)
                result = CommonStrings.SourceImagery_OpenStreetMap;            
            return result;
        }
        public override string ToString()
        {
            return this.ImageryName;
        }
    }

    public class GeoImageryPreview
    {
        public void GetMapImageryPreviewImage(GeoImageryViewModel geoImageryViewModel)
        {
            
            //TODO add logic to get imagery preview images based on type of GeoImageryViewModel
            //var path = "/Assets/Images/";

            switch (geoImageryViewModel.ImagerySource)
            {
                case GeoImagerySource.MapQuestImagery:
                    {

                        break;
                    }
                default:
                    //
                    break;
            }
        }
        public void GetMapThemePreviewImage(GeoImageryViewModel geoImageryViewModel)
        {
            //TODO add logic to get imagery preview images based on type of theme

        }
    }


    /// <summary>
    /// Represents a list of <see cref="GeoImageryViewModel"/>s
    /// </summary>
    public class GeoImageryViewList : List<GeoImageryViewModel>
    { }
    public class GeoImageryWorldViews : GeoImageryViewList
    {
        public GeoImageryWorldViews()
        {
            this.AddRange(GeoImageryViews.WorldImageryViews);
        }
    }
    public class GeoImageryKnownViews : GeoImageryViewList
    {
        public GeoImageryKnownViews()
        {
            this.AddRange(GeoImageryViews.KnownImageryViews);
        }
    }
    /// <summary>
    /// Represents a group of <see cref="GeoImageryViewModel"/>s
    /// </summary>
    public class GeoImageryGroup
    {
        public string Name { get; set; }
        public GeoImageryViewList ImageryViews { get; set; }
    }
    /// <summary>
    /// Represents a list of <see cref="GeoImageryGroup"/>s
    /// </summary>
    public class GeoImageryGroupList : List<GeoImageryGroup>
    {
        public GeoImageryGroupList()
        {
            //if (GeoMapImagryProvider.OpenStreetMapImageryConfig.ImageryIsEnabled)
            //    this.Add(new GeoImageryGroup { Name = "Open Street Map Tiles", ImageryViews = GeoImageryViews.OpenStreetMapViews });

            //if (GeoMapImagryProvider.BingMapImagery.ImageryIsEnabled)
            //    this.Add(new GeoImageryGroup { Name = "Bing Maps Tiles", ImageryViews = GeoImageryViews.BingMapViews });

            //if (GeoMapImagryProvider.EsriMapImagery.ImageryIsEnabled)
            //{
            //    this.Add(new GeoImageryGroup { Name = "ESRI World Map Tiles", ImageryViews = GeoImageryViews.EsriWorldMapViews });
            //    this.Add(new GeoImageryGroup { Name = "ESRI World Overlay Tiles", ImageryViews = GeoImageryViews.EsriWorldOverlayViews });
            //    this.Add(new GeoImageryGroup { Name = "ESRI USA Overlay Tiles", ImageryViews = GeoImageryViews.EsriUsaOverlayViews });
            //}          

            //if (GeoMapImagryProvider.MapQuestImagery.ImageryIsEnabled)
            //    this.Add(new GeoImageryGroup { Name = "Map Quest Tiles", ImageryViews = GeoImageryViews.MapQuestViews });
       
        }
    }
    /// <summary>
    /// Represents a list of <see cref="GeoImageryViewModel"/>s with some known sources of imagery tiles
    /// </summary>
    public class GeoImageryViews : GeoImageryViewList
    {
        #region Lists
        public static GeoImageryViewList KnownImageryViews
        {
            get
            {
                var list = new GeoImageryViewList();
                list.AddRange(OpenStreetMapViews);
                list.AddRange(BingMapViews);
                list.AddRange(EsriWorldMapViews);
                list.AddRange(EsriWorldOverlayViews);
                list.AddRange(EsriUsaOverlayViews);
                return list;
            }
        }
        public static GeoImageryViewList WorldImageryViews
        {
            get
            {
                var list = new GeoImageryViewList();
                list.AddRange(OpenStreetMapViews);
                list.AddRange(BingMapViews);               
                list.AddRange(EsriWorldMapViews);
                return list;
            }
        }
        public static GeoImageryViewList OpenStreetMapViews
        {
            get
            {
                var list = new GeoImageryViewList { OpenStreetMap.DefaultImageryView };
                return list;
            }
        }
        public static GeoImageryViewList BingMapViews
        {
            get
            {
                var list = new GeoImageryViewList();
                list.Add(BingMaps.StreetImageryView);
                list.Add(BingMaps.SatelliteImageryView);
                list.Add(BingMaps.SatelliteNoLabelsImageryView);
                return list;
            }
        }
        public static GeoImageryViewList MapQuestViews
        {
            get
            {
                var list = new GeoImageryViewList();
                list.Add(MapQuest.StreetImageryView);
                list.Add(MapQuest.SatelliteImageryView);
                return list;
            }
        }
                
        public static GeoImageryViewList EsriWorldMapViews
        {
            get
            {
                var list = new GeoImageryViewList();
                list.Add(Esri.WorldStreetMap);
                list.Add(Esri.WorldTopographicMap);
                list.Add(Esri.WorldImageryMap);
                list.Add(Esri.WorldOceansMap);
                list.Add(Esri.WorldNationalGeographicMap);
                list.Add(Esri.WorldPhysicalMap);
                list.Add(Esri.WorldTerrainMap);
                list.Add(Esri.WorldDeLormesMap);
                list.Add(Esri.WorldLightGrayMap);
                list.Add(Esri.WorldShadedReliefMap);
                return list;
            }
        }
        public static GeoImageryViewList EsriWorldOverlayViews
        {
            get
            {
                var list = new GeoImageryViewList();
                list.Add(Esri.WorldAdministrativeOverlay);
                list.Add(Esri.WorldTransportationOverlay);
                list.Add(Esri.WorldBoundariesDarkOverlay);
                list.Add(Esri.WorldBoundariesLightOverlay);
                list.Add(Esri.WorldLightGrayOverlay);
                return list;
            }
        }
        public static GeoImageryViewList EsriUsaOverlayViews
        {
            get
            {
                var list = new GeoImageryViewList();
                list.Add(Esri.UsaPopulationGrowth2015Overlay);
                list.Add(Esri.UsaPopulationChange2010Overlay);
                list.Add(Esri.UsaPopulationChange2000Overlay);
                list.Add(Esri.UsaPopulationDensityOverlay);
                list.Add(Esri.UsaPopulationByGenderOverlay);
                list.Add(Esri.UsaPopulationYoungerThan18Overlay);
                list.Add(Esri.UsaPopulationOlderThanAge64Overlay);
                list.Add(Esri.UsaMedianAgeOverlay);
                list.Add(Esri.UsaAverageHouseholdSizeOverlay);
                list.Add(Esri.UsaMedianHouseholdIncomeOverlay);
                list.Add(Esri.UsaMedianNetWorthOverlay);
                list.Add(Esri.UsaMedianHomeValueOverlay);
                list.Add(Esri.UsaOwnerOccupiedHousingOverlay);
                list.Add(Esri.UsaRetailSpendingPotentialOverlay);
                list.Add(Esri.UsaSocialVulnerabilityOverlay);
                list.Add(Esri.UsaUnemploymentRateOverlay);
                list.Add(Esri.UsaDiversityIndexOverlay);
                list.Add(Esri.UsaLaborForceParticipationOverlay);
                list.Add(Esri.UsaSoilSurveyOverlay);
                list.Add(Esri.UsaRailNetworkOverlay);
                return list;
            }
        }
        #endregion 
        #region OpenStreetMap
        public static class OpenStreetMap
        {
            public static OpenStreetMapImageryView DefaultImageryView
            {
                get { return new OpenStreetMapImageryView(); }
            }
        } 
        #endregion
        #region BingMaps
        public static class BingMaps
        {
            public static BingMapsImageryView StreetImageryView
            {
                get { return BingMapsImageryViews.BingMapsStreetImageryView; }
            }
            public static BingMapsImageryView SatelliteImageryView
            {
                get { return BingMapsImageryViews.BingMapsSatelliteImageryView; }
            }
            public static BingMapsImageryView SatelliteNoLabelsImageryView
            {
                get { return BingMapsImageryViews.BingMapsSatelliteNoLabelsImageryView; }
            }
        }
        #endregion
        #region MapQuest
        public static class MapQuest
        {
            public static MapQuestImageryView StreetImageryView
            {
                get { return MapQuestImageryViews.MapQuestStreetImageryView; }
            }
            public static MapQuestImageryView SatelliteImageryView
            {
                get { return MapQuestImageryViews.MapQuestSatelliteImageryView; }
            } 
        }
        #endregion
        #region ESRI
        public static class Esri
        {
            #region World
            public static EsriMapImageryView WorldStreetMap
            {
                get { return EsriMapImageryViews.WorldStreetMap; }
            }
            public static EsriMapImageryView WorldTopographicMap
            {
                get { return EsriMapImageryViews.WorldTopographicMap; }
            }
            public static EsriMapImageryView WorldImageryMap
            {
                get { return EsriMapImageryViews.WorldImageryMap; }
            }
            public static EsriMapImageryView WorldOceansMap
            {
                get { return EsriMapImageryViews.WorldOceansMap; }
            }
            public static EsriMapImageryView WorldNationalGeographicMap
            {
                get { return EsriMapImageryViews.WorldNationalGeographicMap; }
            }
            public static EsriMapImageryView WorldPhysicalMap
            {
                get { return EsriMapImageryViews.WorldPhysicalMap; }
            }
            public static EsriMapImageryView WorldTerrainMap
            {
                get { return EsriMapImageryViews.WorldTerrainMap; }
            }
            public static EsriMapImageryView WorldDeLormesMap
            {
                get { return EsriMapImageryViews.WorldDeLormesMap; }
            }
            public static EsriMapImageryView WorldLightGrayMap
            {
                get { return EsriMapImageryViews.WorldLightGrayMap; }
            }
            public static EsriMapImageryView WorldShadedReliefMap
            {
                get { return EsriMapImageryViews.WorldShadedReliefMap; }
            }
            public static EsriMapImageryView WorldAdministrativeOverlay
            {
                get { return EsriMapImageryViews.WorldAdministrativeOverlay; }
            }
            public static EsriMapImageryView WorldTransportationOverlay
            {
                get { return EsriMapImageryViews.WorldTransportationOverlay; }
            }
            public static EsriMapImageryView WorldBoundariesDarkOverlay
            {
                get { return EsriMapImageryViews.WorldBoundariesDarkOverlay; }
            }
            public static EsriMapImageryView WorldBoundariesLightOverlay
            {
                get { return EsriMapImageryViews.WorldBoundariesLightOverlay; }
            }
            public static EsriMapImageryView WorldLightGrayOverlay
            {
                get { return EsriMapImageryViews.WorldLightGrayOverlay; }
            }

            #endregion
            #region USA
            public static EsriMapImageryView UsaSoilSurveyOverlay
            {
                get { return EsriMapImageryViews.UsaSoilSurveyOverlay; }
            }
            public static EsriMapImageryView UsaPopulationYoungerThan18Overlay
            {
                get { return EsriMapImageryViews.UsaPopulationYoungerThan18Overlay; }
            }
            public static EsriMapImageryView UsaPopulationOlderThanAge64Overlay
            {
                get { return EsriMapImageryViews.UsaPopulationOlderThanAge64Overlay; }
            }
            public static EsriMapImageryView UsaOwnerOccupiedHousingOverlay
            {
                get { return EsriMapImageryViews.UsaOwnerOccupiedHousingOverlay; }
            }

            public static EsriMapImageryView UsaRailNetworkOverlay
            {
                get { return EsriMapImageryViews.UsaRailNetworkOverlay; }
            }
            public static EsriMapImageryView UsaPopulationGrowth2015Overlay
            {
                get { return EsriMapImageryViews.UsaPopulationGrowth2015Overlay; }
            }
            public static EsriMapImageryView UsaUnemploymentRateOverlay
            {
                get { return EsriMapImageryViews.UsaUnemploymentRateOverlay; }
            }
            public static EsriMapImageryView UsaSocialVulnerabilityOverlay
            {
                get { return EsriMapImageryViews.UsaSocialVulnerabilityOverlay; }
            }
            public static EsriMapImageryView UsaRetailSpendingPotentialOverlay
            {
                get { return EsriMapImageryViews.UsaRetailSpendingPotentialOverlay; }
            }
            public static EsriMapImageryView UsaPopulationChange2010Overlay
            {
                get { return EsriMapImageryViews.UsaPopulationChange2010Overlay; }
            }
            public static EsriMapImageryView UsaPopulationChange2000Overlay
            {
                get { return EsriMapImageryViews.UsaPopulationChange2000Overlay; }
            }
            public static EsriMapImageryView UsaPopulationDensityOverlay
            {
                get { return EsriMapImageryViews.UsaPopulationDensityOverlay; }
            }
            public static EsriMapImageryView UsaPopulationByGenderOverlay
            {
                get { return EsriMapImageryViews.UsaPopulationByGenderOverlay; }
            }
            public static EsriMapImageryView UsaMedianHouseholdIncomeOverlay
            {
                get { return EsriMapImageryViews.UsaMedianHouseholdIncomeOverlay; }
            }
            public static EsriMapImageryView UsaMedianNetWorthOverlay
            {
                get { return EsriMapImageryViews.UsaMedianNetWorthOverlay; }
            }
            public static EsriMapImageryView UsaMedianHomeValueOverlay
            {
                get { return EsriMapImageryViews.UsaMedianHomeValueOverlay; }
            }
            public static EsriMapImageryView UsaMedianAgeOverlay
            {
                get { return EsriMapImageryViews.UsaMedianAgeOverlay; }
            }
            public static EsriMapImageryView UsaLaborForceParticipationOverlay
            {
                get { return EsriMapImageryViews.UsaLaborForceParticipationOverlay; }
            }
            public static EsriMapImageryView UsaAverageHouseholdSizeOverlay
            {
                get { return EsriMapImageryViews.UsaAverageHouseholdSizeOverlay; }
            }
            public static EsriMapImageryView UsaDiversityIndexOverlay
            {
                get { return EsriMapImageryViews.UsaDiversityIndexOverlay; }
            } 
            #endregion
        }
        
        #endregion                                      
        #region ESRI
        //TODO add GeoImageryViews for ESRI
        #endregion
    }


    #endregion
}