using Infragistics.Controls.Maps;

namespace IGGeographicMap.Extensions
{
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