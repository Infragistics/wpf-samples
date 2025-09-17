using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq; 
using System.Windows;

namespace Infragistics.Controls.Maps
{
    /// <summary>
    /// Represents map imagery control with Esri tiles
    /// <para>Overview http://services.arcgisonline.com/arcgis/rest/services </para>
    /// <para>License http://downloads2.esri.com/ArcGISOnline/docs/tou_summary.pdf </para>
    /// </summary>
    public class EsriMapImagery : ArcGISOnlineMapImagery
    {
        public EsriMapImagery() : this(EsriImageryStyle.WorldTerrainBase)
        {
        }
        public EsriMapImagery(EsriImageryStyle style)
        {
            this.PropertyChanged += OnPropertyChanged;
            UpdateMapImagery(style);
        }
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MapStyle")
            {
                UpdateMapImagery(MapStyle);
            }
        }
        protected void UpdateMapImagery(EsriImageryStyle newStyle)
        {
            if (MapStyle != newStyle)
            {
                MapStyle = newStyle;
            }
            this.MapServerUri = EsriMapUtil.GetServerUri(newStyle);
        }

        public EsriImageryStyle MapStyle
        {
            get { return (EsriImageryStyle)GetValue(MapStyleProperty); }
            set { SetValue(MapStyleProperty, value); }
        }

        public static readonly DependencyProperty MapStyleProperty =
            DependencyProperty.Register("MapStyle", typeof(EsriImageryStyle), typeof(EsriMapImagery),
                new PropertyMetadata(EsriImageryStyle.WorldTerrainBase));
    }

    /// <summary>
    /// Determines style of Esri geo-imagery
    /// </summary>
    public enum EsriImageryStyle
    {
        WorldSatelliteMap,
        WorldStreetMap,
        WorldTopographicMap,
        WorldOceansMap,
        WorldOceansBase,
        WorldNationalGeoMap,
        WorldPhysicalMap,
        WorldDeLormesMap,
        WorldTerrainBase,
        WorldElevationHillsOverlay,
        WorldShadedReliefMap,
        WorldNavigationMap,
        WorldLightGrayMap,
        WorldLightGrayOverlay,
        WorldDarkGrayMap,
        WorldDarkGrayOverlay,
        WorldSoilSurveyLand,
        /// <summary> Specifies world administrative imagery overlay that is visible from 300% zoom </summary>
        WorldAdministrativeOverlay,
        /// <summary> Specifies world transportation imagery overlay that is visible from 300% zoom </summary>
        WorldTransportationOverlay,
        WorldBoundariesDarkOverlay,
        WorldBoundariesLightOverlay,

        PolarAntarcticImagery,
        PolarArcticImagery,
        PolarArcticOceanBase,
        PolarArcticOceanOverlay,

        //USA Demographics
        UsaPopulationNow,
        UsaPopulationProjection,
        UsaPopulationChange2010,
        UsaPopulationChange2000,
        UsaPopulationDensity,
        UsaPopulationByGender,
        UsaPopulationMedianAge,
        UsaPopulationDiversityIndex,
        UsaPopulationAgeUnder18,
        UsaPopulationAgeOver64,
        UsaPopulationLaborForce,
        UsaPopulationUnemployment,
        /// <summary>
        /// Specifies USA overlay with the dominant lifestyle segment in an area in 2012, based on 65 segments including their socioeconomic and demographic compositions
        /// </summary>
        UsaPopulationTapestry,
        //USA Households
        UsaHouseholdMedianIncome,
        UsaHouseholdMedianWorth,
        UsaHouseholdMedianValue,
        UsaHouseholdAverageSize,
        UsaHouseholdOwnership,
        //USA other
        UsaSocialVulnerability,
        UsaRetailSpendingPotential,
        UsaRailNetwork,
    }

    public static class EsriMapUtil
    {
        static EsriMapUtil()
        {
            InitalizeServers();
        }
        public static string GetServerUri(EsriImageryStyle style)
        {
            if (Servers.ContainsKey(style))
            {
                return Servers[style];
            }
            throw new NotSupportedException("Esri does not support " + style + " imagery");
        }
        internal static Dictionary<EsriImageryStyle, string> _servers = new Dictionary<EsriImageryStyle, string>();
        public static Dictionary<EsriImageryStyle, string> Servers
        {
            get
            {
                if (_servers == null || _servers.Count == 0)
                {
                    InitalizeServers();
                }
                return _servers;
            }
        }
        internal static void Add(EsriImageryStyle style, string serverName)
        {
            if (_servers.ContainsKey(style))
            {
                Debug.WriteLine("Duplicated server name for " + style + " " + _servers[style]);
            }
            else
            {
                _servers.Add(style, serverName);
            }
        }
        internal static void InitalizeServers()
        {
            _servers = new Dictionary<EsriImageryStyle, string>();
            // world maps
            Add(EsriImageryStyle.WorldStreetMap, "World_Street_Map");
            Add(EsriImageryStyle.WorldTopographicMap, "World_Topo_Map");
            Add(EsriImageryStyle.WorldSatelliteMap, "World_Imagery");
            Add(EsriImageryStyle.WorldNationalGeoMap, "NatGeo_World_Map");
            Add(EsriImageryStyle.WorldTerrainBase, "World_Terrain_Base");
            Add(EsriImageryStyle.WorldShadedReliefMap, "World_Shaded_Relief");
            Add(EsriImageryStyle.WorldPhysicalMap, "World_Physical_Map");
            Add(EsriImageryStyle.WorldElevationHillsOverlay, "Elevation/World_Hillshade");

            Add(EsriImageryStyle.WorldOceansMap, "Ocean_Basemap");
            Add(EsriImageryStyle.WorldOceansBase, "Ocean/World_Ocean_Base");

            Add(EsriImageryStyle.WorldLightGrayMap, "Canvas/World_Light_Gray_Base");
            Add(EsriImageryStyle.WorldLightGrayOverlay, "Canvas/World_Light_Gray_Reference");
            Add(EsriImageryStyle.WorldDarkGrayMap, "Canvas/World_Light_Gray_Base");
            Add(EsriImageryStyle.WorldDarkGrayOverlay, "Canvas/World_Light_Gray_Reference");

            // world other
            Add(EsriImageryStyle.WorldNavigationMap, "Specialty/World_Navigation_Charts");
            Add(EsriImageryStyle.WorldSoilSurveyLand, "Specialty/Soil_Survey_Map");
            Add(EsriImageryStyle.WorldDeLormesMap, "Specialty/DeLorme_World_Base_Map");
            Add(EsriImageryStyle.WorldBoundariesLightOverlay, "World_Boundaries_and_Places_Alternate");
            Add(EsriImageryStyle.WorldAdministrativeOverlay, "Reference/World_Reference_Overlay");
            Add(EsriImageryStyle.WorldTransportationOverlay, "Reference/World_Transportation");
            Add(EsriImageryStyle.WorldBoundariesDarkOverlay, "Reference/World_Boundaries_and_Places");

            // polar maps
            Add(EsriImageryStyle.PolarAntarcticImagery, "Polar/Antarctic_Imagery");
            Add(EsriImageryStyle.PolarArcticImagery, "Polar/Arctic_Imagery");
            Add(EsriImageryStyle.PolarArcticOceanBase, "Polar/Arctic_Ocean_Base");
            Add(EsriImageryStyle.PolarArcticOceanOverlay, "Polar/Arctic_Ocean_Reference");

            // USA Demographics
            Add(EsriImageryStyle.UsaPopulationAgeOver64, "Demographics/USA_Percent_Over_64");
            Add(EsriImageryStyle.UsaPopulationAgeUnder18, "Demographics/USA_Percent_Under_18");
            Add(EsriImageryStyle.UsaPopulationProjection, "Demographics/USA_Projected_Population_Change");
            Add(EsriImageryStyle.UsaPopulationNow, "Demographics/USA_Recent_Population_Change");
            Add(EsriImageryStyle.UsaPopulationChange2010, "Demographics/USA_1990-2000_Population_Change");
            Add(EsriImageryStyle.UsaPopulationChange2000, "Demographics/USA_2000-2010_Population_Change");
            Add(EsriImageryStyle.UsaPopulationDensity, "Demographics/USA_Population_Density");
            Add(EsriImageryStyle.UsaPopulationByGender, "Demographics/USA_Population_by_Sex");
            Add(EsriImageryStyle.UsaPopulationDiversityIndex, "Demographics/USA_Diversity_Index");
            Add(EsriImageryStyle.UsaPopulationUnemployment, "Demographics/USA_Unemployment_Rate");
            Add(EsriImageryStyle.UsaPopulationMedianAge, "Demographics/USA_Median_Age");
            Add(EsriImageryStyle.UsaPopulationLaborForce, "Demographics/USA_Labor_Force_Participation_Rate");
            Add(EsriImageryStyle.UsaPopulationTapestry, "Demographics/USA_Tapestry ");
            Add(EsriImageryStyle.UsaSocialVulnerability, "Demographics/USA_Social_Vulnerability_Index");
            Add(EsriImageryStyle.UsaRetailSpendingPotential, "Demographics/USA_Retail_Spending_Potential");
            // USA Household 
            Add(EsriImageryStyle.UsaHouseholdMedianIncome, "Demographics/USA_Median_Household_Income");
            Add(EsriImageryStyle.UsaHouseholdMedianWorth, "Demographics/USA_Median_Net_Worth");
            Add(EsriImageryStyle.UsaHouseholdMedianValue, "Demographics/USA_Median_Home_Value");
            Add(EsriImageryStyle.UsaHouseholdAverageSize, "Demographics/USA_Average_Household_Size");
            Add(EsriImageryStyle.UsaHouseholdOwnership, "Demographics/USA_Owner_Occupied_Housing");
            // USA other 
            Add(EsriImageryStyle.UsaRailNetwork, "Reference/World_Reference_Overlay");

            var uri = "http://services.arcgisonline.com/ArcGIS/rest/services/";

            foreach (var style in _servers.Keys.ToArray())
            {
                _servers[style] = uri + _servers[style] + "/MapServer";
            }
        }
    }

}
