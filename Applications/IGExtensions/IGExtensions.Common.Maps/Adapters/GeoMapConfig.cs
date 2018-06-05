using IGExtensions.Common.Maps.Imagery;

namespace Infragistics.Controls.Maps
{
    /// <summary>
    /// Represents configuration for a geo-imagery source
    /// </summary>
    public class GeoMapImageryConfig
    {
        public GeoMapImageryConfig(GeoImagerySource imagerySource)
        {
            ImagerySource = imagerySource;
            ImageryIsEnabled = true;
            ImageryIsDefault = false;
            ImageryKey = string.Empty;
            ImagerySource = imagerySource;
        }

        static internal GeoMapImageryConfig OpenStreetMapImageryConfig = new OpenStreetMapImageryConfig { ImageryIsDefault = false };
        static internal GeoMapImageryConfig BingMapImageryConfig = new BingMapImageryConfig { ImageryIsDefault = true, ImageryIsEnabled = true };
        static internal GeoMapImageryConfig EsriMapImageryConfig = new EsriMapImageryConfig { ImageryIsDefault = true, ImageryIsEnabled = true };        
        static internal GeoMapImageryConfig MapQuestImageryConfig = new MapQuestImageryConfig { ImageryIsDefault = true };
        #region Properties

        public string ImageryKey { get; set; }
        public bool ImageryIsEnabled { get; set; }
        public bool ImageryIsDefault { get; set; }
        public GeoImagerySource ImagerySource { get; protected set; }

        #endregion
        public bool ImageryIsValid
        {
            get
            {
                if (ImagerySource == GeoImagerySource.EsriMapImagery ||
                    ImagerySource == GeoImagerySource.OpenStreetMapImagery ||
                    ImagerySource == GeoImagerySource.MapQuestImagery)
                    return true;
                // check key for GeoImagerySource.BingMapsImagery or GeoImagerySource.BingMapsImagery
                return !this.ImageryKey.Equals(string.Empty);
            }
        }

    }
    public class OpenStreetMapImageryConfig : GeoMapImageryConfig
    {
        public OpenStreetMapImageryConfig()
            : base(GeoImagerySource.OpenStreetMapImagery) 
        { }
    }
    public class BingMapImageryConfig : GeoMapImageryConfig
    {
        public BingMapImageryConfig()
            : base(GeoImagerySource.BingMapsImagery)
        { }
    }
    public class EsriMapImageryConfig : GeoMapImageryConfig
    {
        public EsriMapImageryConfig()
            : base(GeoImagerySource.EsriMapImagery)
        { }
    }
     
    public class MapQuestImageryConfig : GeoMapImageryConfig
    {
        public MapQuestImageryConfig()
            : base(GeoImagerySource.MapQuestImagery)
        { }
    }

}