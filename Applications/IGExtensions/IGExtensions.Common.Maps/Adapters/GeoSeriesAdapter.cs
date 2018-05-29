using IGExtensions.Common.Maps.Imagery;
using IGExtensions.Framework;

namespace Infragistics.Controls.Maps  
{
    public static class GeoSeriesAdapter
    {
        /// <summary>
        /// Loads geo-imagery from default imagery style of OpenStreetMap 
        /// </summary>
        public static void LoadGeoImagery(this GeographicTileSeries geoSeries)
        {
            geoSeries.LoadGeoImagery(new OpenStreetMapImageryView());
        }
        /// <summary>
        /// Loads geo-imagery from specified imagery source
        /// </summary>
        public static void LoadGeoImagery(this GeographicTileSeries geoSeries, GeoImagerySource imagerySource)
        {
            if (imagerySource == GeoImagerySource.BingMapsImagery)
                geoSeries.LoadGeoImagery(IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.StreetMapStyle);            
            else if (imagerySource == GeoImagerySource.MapQuestImagery)
                geoSeries.LoadGeoImagery(MapQuestImageryStyle.StreetMapStyle);
            else if (imagerySource == GeoImagerySource.EsriMapImagery)
                geoSeries.LoadGeoImagery(EsriMapImageryStyle.WorldStreetMap);
            else if (imagerySource == GeoImagerySource.OpenStreetMapImagery)
                geoSeries.LoadGeoImagery(new OpenStreetMapImageryView());
            else
            {
                System.Diagnostics.Debug.WriteLine("WARNING: Imagery source " + imagerySource + " is not supported by IGExtensions library.");
            }
        }
        /// <summary>
        /// Loads geo-imagery from specified imagery style of BingMaps
        /// </summary>
        public static void LoadGeoImagery(this GeographicTileSeries geoSeries, IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle imageryStyle)
        {
            geoSeries.LoadGeoImagery(new BingMapsImageryView(imageryStyle));
        }
        /// <summary>
        /// Loads geo-imagery from specified imagery style of BingMaps
        /// </summary>
        public static void LoadGeoImagery(this GeographicTileSeries geoSeries, Infragistics.Controls.Maps.BingMapsImageryStyle imageryStyle)
        {
            var geoImageryStyle = (IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle)imageryStyle;
            geoSeries.LoadGeoImagery(new BingMapsImageryView(geoImageryStyle));
        }        
        /// <summary>
        /// Loads geo-imagery from specified imagery style of MapQuest
        /// </summary>
        public static void LoadGeoImagery(this GeographicTileSeries geoSeries, MapQuestImageryStyle imageryStyle)
        {
            geoSeries.LoadGeoImagery(new MapQuestImageryView(imageryStyle));
        }
        /// <summary>
        /// Loads geo-imagery from specified imagery style of ESRI
        /// </summary>
        public static void LoadGeoImagery(this GeographicTileSeries geoSeries, EsriMapImageryStyle imageryStyle)
        {
            geoSeries.LoadGeoImagery(new EsriMapImageryView(imageryStyle));
        }
        /// <summary>
        /// Loads geo-imagery from specified imagery view and preserves settings of GeographicTileSeries' TileImagery
        /// <remarks>Supported imagery views: <see cref="OpenStreetMapImageryView"/>, <see cref="BingMapsImageryView"/>, 
        /// <see cref="EsriMapImageryView"/>, and <see cref="MapQuestImageryView"/></remarks>
        /// </summary>
        public static void LoadGeoImagery(this GeographicTileSeries geoSeries, GeoImageryViewModel imageryViewModel)
        {
            if (geoSeries == null)
            {
                DebugManager.LogWarning("GeoSeriesAdapter cannot load geo-imagery when GeographicTileSeries is null"); return;
            }
            //geoSeriesImager.geoSeries = geoSeries;
            geoSeries.TileImagery.SaveMapImagerySettings();
            geoSeries.TileImagery = imageryViewModel.GetGeographicMapImagery();
            geoSeries.TileImagery.LoadMapImagerySettings();
        }


        #region Internal Methods
        //internal static GeoMapImagerySettings MapImagerySettings = new GeoMapImagerySettings();
        //internal static void SaveMapImagerySettings(this GeographicTileSeries geoSeries)
        //{
        //    if (geoSeries.TileImagery != null)
        //        MapImagerySettings = new GeoMapImagerySettings(geoSeries.TileImagery);
        //}
        //internal static void LoadMapImagerySettings(this GeographicTileSeries geoSeries)
        //{
        //    geoSeries.TileImagery.Opacity = MapImagerySettings.Opacity;
        //    geoSeries.TileImagery.Visibility = MapImagerySettings.Visibility;
        //}
        #endregion
    }
}