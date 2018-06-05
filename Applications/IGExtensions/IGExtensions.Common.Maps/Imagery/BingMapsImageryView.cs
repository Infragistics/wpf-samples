using System;
using IGExtensions.Common.Maps.Assets.Resources;
using Infragistics.Controls.Maps;

namespace IGExtensions.Common.Maps.Imagery // Infragistics.Controls.Maps 
{
    #region BingMaps Imagery View
    /// <summary>
    /// Represents an imagery view for the BingMaps geo-imagery with default (Street) Map Style 
    /// </summary>
    public class BingMapsImageryView : GeoImageryViewModel
    {
        /// <summary>
        /// Constructs an instance of BingMapsImageryView
        /// </summary>
        public BingMapsImageryView() 
            : this(IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.StreetMapStyle)
        {
        }
        /// <summary>
        /// Constructs an instance of BingMapsImageryView
        /// </summary>
        /// <param name="imageryStyle"></param>
        public BingMapsImageryView(IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle imageryStyle)
        {
            this.ImageryKey = GeoMapImagryProvider.BingMapImageryConfig.ImageryKey;
            this.ImagerySource = GeoImagerySource.BingMapsImagery;
            this.ImageryStyle = imageryStyle;
            if (imageryStyle == BingMapsImageryStyle.SatelliteMapStyle)
                this.ImageryDisplayName = ImageryStrings.Imagery_BingMapHybrid;
            if (imageryStyle == BingMapsImageryStyle.SatelliteNoLabelsMapStyle)
                this.ImageryDisplayName = ImageryStrings.Imagery_BingMapSatellite;
            if (imageryStyle == BingMapsImageryStyle.StreetMapStyle)
                this.ImageryDisplayName = ImageryStrings.Imagery_BingMapStreet;
        }
        #region Properties

        /// <summary>
        /// Gets or sets Imagery Style for the BingMapsImageryView
        /// </summary>
        public BingMapsImageryStyle ImageryStyle
        {
            get { return _imageryStyle; }
            set { if (_imageryStyle == value) return; _imageryStyle = value; OnPropertyChanged("ImageryStyle"); }
        }
        private BingMapsImageryStyle _imageryStyle = BingMapsImageryStyle.StreetMapStyle;
       
        /// <summary>
        /// Gets or sets imagery key for the Bing Maps geo-imagery. 
        /// <para>You must get your own key from http://www.bingmapsportal.com </para>
        /// </summary>
        public string ImageryKey { get; set; }

        /// <summary>
        /// Gets Imagery Name of the BingMapsImageryView
        /// </summary>
        public override string ImageryName
        {
            get { return this.ImagerySource.ToString().Replace("Imagery", "") + this.ImageryStyle.ToString().Replace("MapStyle", "Imagery"); }
        }
        public override string ImageryFileName { get { return ImageryName + ".png"; } }
        #endregion
        ///// <summary>
        ///// Checks if the imagery key is reserved for Infragistics' sample app development  
        ///// </summary>
        ///// <returns></returns>
        //public bool IsImageryKeyReserved()
        //{
        //    return this.ImageryKey == GeoImageryViewModel.BingMapsImageryKey;
        //}
        public bool IsImageryKeyValid()
        {
            return !this.ImageryKey.Equals(string.Empty);
        }
        
    }
    #endregion
    /// <summary>
    /// Represents a derived imagery view from  <see cref="BingMapsImageryView"/> with Street Map Style
    /// </summary>
    public class BingMapsStreetImageryView : BingMapsImageryView
    {
        public BingMapsStreetImageryView()
            : base(BingMapsImageryStyle.StreetMapStyle)
        {
        }
    }
    /// <summary>
    /// Represents a derived imagery view from <see cref="BingMapsImageryView"/> with Satellite Map Style
    /// </summary>
    public class BingMapsSatelliteImageryView : BingMapsImageryView
    {
        public BingMapsSatelliteImageryView()
            : base(BingMapsImageryStyle.SatelliteMapStyle)
        {
        }
    }
    /// <summary>
    /// Represents static class with all known map styles for the BingMaps geo-imagery 
    /// </summary>
    public static class BingMapsImageryViews
    {
        public static BingMapsImageryView BingMapsStreetImageryView
        {
            get { return new BingMapsImageryView(BingMapsImageryStyle.StreetMapStyle); }
        }
        public static BingMapsImageryView BingMapsSatelliteImageryView
        {
            get { return new BingMapsImageryView(BingMapsImageryStyle.SatelliteMapStyle); }
        }
        public static BingMapsImageryView BingMapsSatelliteNoLabelsImageryView
        {
            get { return new BingMapsImageryView(BingMapsImageryStyle.SatelliteNoLabelsMapStyle); }
        }
    }
}