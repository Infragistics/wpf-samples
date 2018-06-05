using System;
using System.Collections.Generic;
using Infragistics.Controls.Maps;

namespace IGExtensions.Common.Maps.Imagery // Infragistics.Controls.Maps 
{
    /// <summary>
    /// Represents tile source for geographic imagery from the Map Quest service
    /// </summary>
    public class MapQuestTileSource : MapTileSource
    {
        public MapQuestTileSource()
            : base(134217728, 134217728, 256, 256, 0)
        { }

        #region TileMapStyle

        /// <summary>
        /// Gets or sets map style for the tile source of MapQuest geographic imagery
        /// </summary>
        public MapQuestImageryStyle TileMapStyle { get; set; }

        #endregion
        //private const string TileStreetPath = "http://otile4.mqcdn.com/tiles/1.0.0/osm/{Z}/{X}/{Y}.png";
        //private const string TileAerialPath = "http://oatile3.mqcdn.com/naip/{Z}/{X}/{Y}.png";
        private const string TileStreetPath = "http://otile1.mqcdn.com/tiles/1.0.0/osm/{Z}/{X}/{Y}.png";
        private const string TileAerialPath = "http://otile1.mqcdn.com/tiles/1.0.0/sat/{Z}/{X}/{Y}.png";
        /// <summary>
        /// Gets path for the type of a geographic imagery tile
        /// </summary>
        /// <returns></returns>
        private string GetTileType()
        {
            return this.TileMapStyle == MapQuestImageryStyle.SatelliteMapStyle ? TileAerialPath : TileStreetPath;
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
                uriString = uriString.Replace("{Z}", zoom.ToString());
                uriString = uriString.Replace("{X}", tilePositionX.ToString());
                uriString = uriString.Replace("{Y}", tilePositionY.ToString());
                tileImageLayerSources.Add(new Uri(uriString));
            }
        }
    }
    /// <summary>
    /// Represents geographic imagery with street map style from the Map Quest service
    /// </summary>
    public class MapQuestStreetImagery : GeographicMapImagery
    {
        public MapQuestStreetImagery()
            : base(new MapQuestTileSource { TileMapStyle = MapQuestImageryStyle.StreetMapStyle })
        { }
    }

    /// <summary>
    /// Represents geographic imagery with satellite map style from the Map Quest service
    /// </summary>
    public class MapQuestSatelliteImagery : GeographicMapImagery
    {
        public MapQuestSatelliteImagery()
            : base(new MapQuestTileSource { TileMapStyle = MapQuestImageryStyle.SatelliteMapStyle })
        { }

    }

   
}