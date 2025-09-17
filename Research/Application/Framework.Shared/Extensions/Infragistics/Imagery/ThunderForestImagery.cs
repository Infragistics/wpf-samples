using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Infragistics.Controls.Maps
{
    public class ThunderForestImagery : GeographicMapImagery
    { 
        public ThunderForestImagery() 
            : this(ThunderForestIStyle.OpenCycle)
        {
        }
        public ThunderForestImagery(ThunderForestIStyle style)
            : base(new ThunderForestTileSource { TileStyle = style } )
        {
        }

        //protected ThunderForestTileSource MapSource { get { return this.TileSource as ThunderForestTileSource; } }

        public ThunderForestIStyle MapStyle
        {
            get { return (ThunderForestIStyle)GetValue(MapStyleProperty); }
            set { SetValue(MapStyleProperty, value); }
        }

        public static readonly DependencyProperty MapStyleProperty =
            DependencyProperty.Register("MapStyle", typeof(ThunderForestIStyle), typeof(ThunderForestImagery),
            new PropertyMetadata(ThunderForestIStyle.Landscape, (s, e) =>
            {
                (s as ThunderForestImagery).OnPropertyChanged(e.Property.Name);
            }));

        public string MapKey
        {
            get { return (string)GetValue(MapKeyProperty); }
            set { SetValue(MapKeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapKeyProperty =
            DependencyProperty.Register("MapKey", typeof(string), typeof(ThunderForestImagery), 
            new PropertyMetadata("", (s, e) =>
            {
                (s as ThunderForestImagery).OnPropertyChanged(e.Property.Name);
            }));

        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "MapStyle")
            { 
                (TileSource as ThunderForestTileSource).TileStyle = MapStyle;
            }
            else if (propertyName == "MapKey")
            {
                (TileSource as ThunderForestTileSource).TileKey = MapKey;
            }
        }
    }

    /// <summary>
    /// Represents tile source for geographic imagery from the Map Quest service
    /// </summary>
    public class ThunderForestTileSource : Infragistics.Controls.Maps.MapTileSource
    {
        public ThunderForestTileSource()
            : base(134217728, 134217728, 256, 256, 0)
        {
             
        }   
        public ThunderForestIStyle TileStyle
        {
            get { return (ThunderForestIStyle)GetValue(TileStyleProperty); }
            set { SetValue(TileStyleProperty, value); }
        }
        public static readonly DependencyProperty TileStyleProperty =
            DependencyProperty.Register("TileStyle", 
                typeof(ThunderForestIStyle), typeof(ThunderForestTileSource), 
                new PropertyMetadata(ThunderForestIStyle.Landscape));

        public string TileKey
        {
            get { return (string)GetValue(TileKeyProperty); }
            set { SetValue(TileKeyProperty, value); }
        }
        public static readonly DependencyProperty TileKeyProperty =
            DependencyProperty.Register("TileKey",
                typeof(string), typeof(ThunderForestTileSource),
                new PropertyMetadata(""));
               
        private const string key = "4087c6a4943d412ab143fa8d0e49c28a"; // limited for DEV
        private const string uri = "https://{server}.tile.thunderforest.com/{style}/{z}/{x}/{y}.png?apikey={key}";
         

        /// <summary>
        /// Overridden method for getting a geographic imagery tile at specific position of the map
        /// </summary>
        protected override void GetTileLayers(int tileLevel, int tilePositionX, int tilePositionY, IList<object> tileImageLayerSources)
        {
            var style = GetStyle();
            int zoom = tileLevel - 8;
            if (zoom > 0)
            {
                string uriString = uri;
                uriString = uriString.Replace("{server}", "a"); // use also b and c servers
                uriString = uriString.Replace("{style}", style);
                uriString = uriString.Replace("{z}", zoom.ToString());
                uriString = uriString.Replace("{x}", tilePositionX.ToString());
                uriString = uriString.Replace("{y}", tilePositionY.ToString());
                uriString = uriString.Replace("{key}", TileKey);

                //System.Diagnostics.Debug.WriteLine("https://a.tile.thunderforest.com/cycle/1/1/1.png?apikey=4087c6a4943d412ab143fa8d0e49c28a");
                //System.Diagnostics.Debug.WriteLine(uriString);

                tileImageLayerSources.Add(new Uri(uriString));
            }
        }

        private string GetStyle()
        {
            string style;

            if (TileStyle == ThunderForestIStyle.OpenCycle)
                style = "cycle";
            else if (TileStyle == ThunderForestIStyle.TransportLight)
                style = "transport";
            else if (TileStyle == ThunderForestIStyle.Landscape)
                style = "landscape";
            else if (TileStyle == ThunderForestIStyle.Outdoors)
                style = "outdoors";
            else if (TileStyle == ThunderForestIStyle.TransportDark)
                style = "transport-dark";
            else if (TileStyle == ThunderForestIStyle.Spinal)
                style = "spinal-map";
            else if (TileStyle == ThunderForestIStyle.Pioneer)
                style = "pioneer";
            else if (TileStyle == ThunderForestIStyle.MobileAtlas)
                style = "mobile-atlas";
            else if (TileStyle == ThunderForestIStyle.Neighbourhood)
                style = "neighbourhood";
            else
                style = "cycle";

            return style;
        }
    }
    /// <summary>
    /// Defines style of the ThunderForest geographic imagery. 
    /// </summary>
    public enum ThunderForestIStyle
    {
        OpenCycle,
        Landscape,
        Outdoors,
        Spinal,
        Pioneer,
        MobileAtlas,
        Neighbourhood,
        TransportLight,
        TransportDark,


    }

    

}