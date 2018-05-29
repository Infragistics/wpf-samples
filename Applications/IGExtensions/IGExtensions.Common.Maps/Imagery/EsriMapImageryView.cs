using System;
using IGExtensions.Common.Maps.Assets.Resources;
using IGExtensions.Framework;

namespace IGExtensions.Common.Maps.Imagery
{
    public class EsriMapImageryView : GeoImageryViewModel
    {
        public EsriMapImageryView()
            //: this(EsriMapImageryViews.WorldStreetMap)
        {
            this.ImagerySource = GeoImagerySource.EsriMapImagery;
        }
        public EsriMapImageryView(EsriMapImageryStyle imageryStyle)
            : this(GetImageryView(imageryStyle))
        {
            //this.ImagerySource = GeoImagerySource.EsriMapImagery;
            //this.ImageryStyle = imageryStyle;
            //this.ImageryDisplayName = "ESRI Map";
        }
        public EsriMapImageryView(EsriMapImageryView imageryView)
        {
            this.ImagerySource = GeoImagerySource.EsriMapImagery;
            this.ImageryStyle = imageryView.ImageryStyle;
            this.ImageryDisplayName = "ESRI" + imageryView.ImageryDisplayName + " Imagery";
            this.ImageryDescription = imageryView.ImageryDescription;
            this.ImageryServer = imageryView.ImageryServer;
        }
        #region Properties
        private EsriMapImageryStyle _imageryStyle;
        public EsriMapImageryStyle ImageryStyle
        {
            get { return _imageryStyle; }
            set { _imageryStyle = value; OnPropertyChanged("ImageryStyle"); }
        }
        public string ImageryServer { get; set; }

        /// <summary>
        /// Gets the style name of the ESRI geo-imagery. 
        /// </summary>
        public override string ImageryName
        {
            get
            {
                return this.ImagerySource.ToString().Remove("MapImagery") +
                       this.ImageryStyle.ToString().Replace("Map", "Imagery");
            }
        }
        /// <summary>
        /// Gets the file name of the MapQuest geo-imagery. 
        /// </summary>
        public override string ImageryFileName { get { return this.ImageryName + ".png"; } } 
        #endregion
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
                    ImageryDisplayName = ImageryStrings.WorldStreetMap,
                    ImageryDescription = ImageryStrings.WorldStreetMapInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldTopographicMap,
                    ImageryDescription = ImageryStrings.WorldTopographicMapInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldImageryMap,
                    ImageryDescription = ImageryStrings.WorldImageryMapInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldOceansMap,
                    ImageryDescription = ImageryStrings.WorldOceansMapInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Ocean_Basemap/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldNationalGeographicMap,
                    ImageryDescription = ImageryStrings.WorldNationalGeographicMapInfo,
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
                    ImageryDisplayName = ImageryStrings.WorldTerrainMap,
                    ImageryDescription = ImageryStrings.WorldTerrainMapInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/World_Terrain_Base/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldDeLormesMap,
                    ImageryDescription = ImageryStrings.WorldDeLormesMapInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Specialty/DeLorme_World_Base_Map/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldLightGrayMap,
                    ImageryDescription = ImageryStrings.WorldLightGrayMapInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Canvas/World_Light_Gray_Base/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldShadedReliefMap,
                    ImageryDescription = ImageryStrings.WorldShadedReliefMapInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/World_Shaded_Relief/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldAdministrativeOverlay,
                    ImageryDescription = ImageryStrings.WorldAdministrativeOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Reference/World_Reference_Overlay/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldPhysicalMap,
                    ImageryDescription = ImageryStrings.WorldPhysicalMapInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/World_Physical_Map/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldTransportationOverlay,
                    ImageryDescription = ImageryStrings.WorldTransportationOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Reference/World_Transportation/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldBoundariesDarkOverlay,
                    ImageryDescription = ImageryStrings.WorldBoundariesDarkOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldBoundariesLightOverlay,
                    ImageryDescription = ImageryStrings.WorldBoundariesLightOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places_Alternate/MapServer"
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
                    ImageryDisplayName = ImageryStrings.WorldLightGrayOverlay,
                    ImageryDescription = ImageryStrings.WorldLightGrayOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Canvas/World_Light_Gray_Reference/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaOwnerOccupiedHousingOverlay,
                    ImageryDescription = ImageryStrings.UsaOwnerOccupiedHousingOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Owner_Occupied_Housing/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaSoilSurveyOverlay,
                    ImageryDescription = ImageryStrings.UsaSoilSurveyOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Specialty/Soil_Survey_Map/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaPopulationOlderThanAge64Overlay,
                    ImageryDescription = ImageryStrings.UsaPopulationOlderThanAge64OverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Percent_Over_64/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaPopulationYoungerThan18Overlay,
                    ImageryDescription = ImageryStrings.UsaPopulationYoungerThan18OverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Percent_Under_18/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaPopulationGrowth2015Overlay,
                    ImageryDescription = ImageryStrings.UsaPopulationGrowth2015OverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Projected_Population_Change/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaRailNetworkOverlay,
                    ImageryDescription = ImageryStrings.UsaRailNetworkOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Reference/World_Reference_Overlay/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaUnemploymentRateOverlay,
                    ImageryDescription = ImageryStrings.UsaUnemploymentRateOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Unemployment_Rate/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaSocialVulnerabilityOverlay,
                    ImageryDescription = ImageryStrings.UsaSocialVulnerabilityOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Social_Vulnerability_Index/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaRetailSpendingPotentialOverlay,
                    ImageryDescription = ImageryStrings.UsaRetailSpendingPotentialOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Retail_Spending_Potential/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaPopulationChange2010Overlay,
                    ImageryDescription = ImageryStrings.UsaPopulationChange2010OverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Recent_Population_Change/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaPopulationChange2000Overlay,
                    ImageryDescription = ImageryStrings.UsaPopulationChange2000OverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_1990-2000_Population_Change/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaPopulationDensityOverlay,
                    ImageryDescription = ImageryStrings.UsaPopulationDensityOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Population_Density/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaPopulationByGenderOverlay,
                    ImageryDescription = ImageryStrings.UsaPopulationByGenderOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Population_by_Sex/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaMedianHouseholdIncomeOverlay,
                    ImageryDescription = ImageryStrings.UsaMedianHouseholdIncomeOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Median_Household_Income/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaMedianNetWorthOverlay,
                    ImageryDescription = ImageryStrings.UsaMedianNetWorthOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Median_Net_Worth/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaMedianHomeValueOverlay,
                    ImageryDescription = ImageryStrings.UsaMedianHomeValueOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Median_Home_Value/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaMedianAgeOverlay,
                    ImageryDescription = ImageryStrings.UsaMedianAgeOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Median_Age/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaLaborForceParticipationOverlay,
                    ImageryDescription = ImageryStrings.UsaLaborForceParticipationOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Labor_Force_Participation_Rate/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaAverageHouseholdSizeOverlay,
                    ImageryDescription = ImageryStrings.UsaAverageHouseholdSizeOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Average_Household_Size/MapServer"
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
                    ImageryDisplayName = ImageryStrings.UsaDiversityIndexOverlay,
                    ImageryDescription = ImageryStrings.UsaDiversityIndexOverlayInfo,
                    ImageryServer = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Diversity_Index/MapServer"
                };
            }
        } 
        #endregion
    }
   
}