/* ----------------------------------------------------------------------------------------------------------
 * NOTE: this filw contains confidential information and should not be released with IG products or samples
 * ----------------------------------------------------------------------------------------------------------  */
using System;
using IGExtensions.Common.Maps.Imagery;

namespace Infragistics.Controls.Maps
{
    public static class GeoMapConfigOverride
    {
        static GeoMapConfigOverride()
        {
        }

        public static bool IsOverrideMapImageryConfig = false;
            
        public static void OverrideMapImageryConfigChanges()
        {
            if (IsOverrideMapImageryConfig) return;
         
            IsOverrideMapImageryConfig = true;
            // listen to event for loading map configuration
            GeoMapImagryProvider.LoadMapConfigurationCompleted += OnLoadMapConfiguration;
        }

        public static void OverrideMapImageryConfig()
        {
            // overload map imagery configuration on failed attempt to read config file
            GeoMapImagryProvider.OpenStreetMapImageryConfig = new OpenStreetMapImageryConfig { ImageryIsDefault = false, ImageryIsEnabled = true };
            GeoMapImagryProvider.BingMapImageryConfig = new BingMapImageryConfig { ImageryIsDefault = true, ImageryIsEnabled = true, ImageryKey = "AmWEO6eLT_mY5rVXY1AnnA6ZVodYHvjvDtBR25COTjWlKmwKAwNlLYltWFpQGMN8" };
            GeoMapImagryProvider.EsriMapImageryConfig = new EsriMapImageryConfig { ImageryIsDefault = true, ImageryIsEnabled = true, };
            GeoMapImagryProvider.CloudMadeMapImageryConfig = new CloudMadeMapImageryConfig { ImageryIsDefault = false, ImageryIsEnabled = true, ImageryKey = "a66373ac35ad5a2195f1b68cafb5255b" };
            GeoMapImagryProvider.MapQuestImageryConfig = new MapQuestImageryConfig { ImageryIsDefault = true, ImageryIsEnabled = true, };
        }
        static void OnLoadMapConfiguration(object sender, ResultEventArgs e)
        {
            //if(e.Error != null)
            //{
            //}
            OverrideMapImageryConfig();
           
        }
    }
}