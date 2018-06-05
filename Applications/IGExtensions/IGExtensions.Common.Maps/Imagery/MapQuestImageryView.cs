using System;
using IGExtensions.Common.Maps.Assets.Resources;

namespace IGExtensions.Common.Maps.Imagery // Infragistics.Controls.Maps 
{
    #region MapQuest Imagery View
    /// <summary>
    /// Represents a map view for the MapQuest geo-imagery. 
    /// </summary>
    public class MapQuestImageryView : GeoImageryViewModel
    {
        public MapQuestImageryView()
            : this(MapQuestImageryStyle.StreetMapStyle)
        {
        }
        public MapQuestImageryView(MapQuestImageryStyle imageryStyle)
        {
            this.ImagerySource = GeoImagerySource.MapQuestImagery;
            this.ImageryStyle = imageryStyle;
            //this.ImageryDisplayName = "Map Quest";
            if (imageryStyle == MapQuestImageryStyle.SatelliteMapStyle)
                this.ImageryDisplayName = ImageryStrings.Imagery_MapQuestSatellite;  
            if (imageryStyle == MapQuestImageryStyle.StreetMapStyle)
                this.ImageryDisplayName = ImageryStrings.Imagery_MapQuestStreet;
        }
        #region Properties
        /// <summary>
        /// Gets the style name of the MapQuest geo-imagery. 
        /// </summary>
        public MapQuestImageryStyle ImageryStyle
        {
            get { return _imageryStyle; }
            set { if (_imageryStyle == value) return; _imageryStyle = value; OnPropertyChanged("ImageryStyle"); }
        }
        private MapQuestImageryStyle _imageryStyle = MapQuestImageryStyle.StreetMapStyle;

        /// <summary>
        /// Gets the style name of the MapQuest geo-imagery. 
        /// </summary>
        public override string ImageryName
        {
            get
            {
                return this.ImagerySource.ToString().Replace("Imagery", "") +
                       this.ImageryStyle.ToString().Replace("MapStyle", "") + "Imagery";
            }
        }
        /// <summary>
        /// Gets the file name of the MapQuest geo-imagery. 
        /// </summary>
        public override string ImageryFileName { get { return this.ImageryName + ".png"; } }
        #endregion
        #region Methods

        /// <summary>
        /// check if this and the other MapQuestImageryView object are equal
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            bool result = false;
            var that = other as MapQuestImageryView;
            if (that != null)
                result = (this.ImageryStyle == that.ImageryStyle) &&
                         (this.ImageryName == that.ImageryName);
            return result;
        }
        /// <summary>
        /// check if left-hand-side and right-hand-side MapQuestImageryView objects have the same properties
        /// </summary>
        public static bool operator ==(MapQuestImageryView left, MapQuestImageryView right)
        {
            return Object.Equals(left, right);
        }
        /// <summary>
        /// check if left-hand-side and right-hand-side MapQuestImageryView objects have different properties
        /// </summary>
        public static bool operator !=(MapQuestImageryView left, MapQuestImageryView right)
        {
            return !Object.Equals(left, right);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        } 
        #endregion
    }
    /// <summary>
    /// Represents a derived imagery view from <see cref="MapQuestImageryView"/> with Street Map Style
    /// </summary>
    public class MapQuestStreetImageryView : MapQuestImageryView
    {
        public MapQuestStreetImageryView()
            : base(MapQuestImageryStyle.StreetMapStyle)
        { 
        }
    }
    /// <summary>
    /// Represents a derived imagery view from <see cref="MapQuestImageryView"/> with Satellite Map Style
    /// </summary>
    public class MapQuestSatelliteImageryView : MapQuestImageryView
    {
        public MapQuestSatelliteImageryView()
            : base(MapQuestImageryStyle.SatelliteMapStyle)
        {
        }
    }
    /// <summary>
    /// Represents static class with all known map styles for the MapQuest geo-imagery 
    /// </summary>
    public static class MapQuestImageryViews
    {
        public static MapQuestImageryView MapQuestStreetImageryView
        {
            get { return new MapQuestImageryView(MapQuestImageryStyle.StreetMapStyle); }
        }
        public static MapQuestImageryView MapQuestSatelliteImageryView
        {
            get { return new MapQuestImageryView(MapQuestImageryStyle.SatelliteMapStyle); }
        }
    }
    

    #endregion
}