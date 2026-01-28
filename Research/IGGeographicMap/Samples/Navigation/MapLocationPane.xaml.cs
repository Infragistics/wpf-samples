using System.Windows;
using System.Windows.Controls;
using IGGeographicMap.Models;
using IGGeographicMap.Samples.Custom;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples
{
    public partial class MapLocationPane : Infragistics.Samples.Framework.SampleContainer
    {
        public MapLocationPane()
        {
            InitializeComponent();
             
            this.SampleLoaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, System.EventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }
       
        private void MapCoordinateVerticalOrientationButton_Click(object sender, RoutedEventArgs e)
        {
            this.GeoMapLocationPane.MapPaneOrientation = Orientation.Vertical;
        }
        private void MapCoordinateHorizontalOrientationButton_Click(object sender, RoutedEventArgs e)
        {
            this.GeoMapLocationPane.MapPaneOrientation = Orientation.Horizontal;
        }
        private void MapCoordinateModeButton_MWC_Click(object sender, RoutedEventArgs e)
        {
            this.GeoMapLocationPane.MapCoordinateDisplayMode = MapCoordinateDisplayMode.MapWindowCoordinates;
        }
        private void MapCoordinateModeButton_DD_Click(object sender, RoutedEventArgs e)
        {
            this.GeoMapLocationPane.MapCoordinateDisplayMode = MapCoordinateDisplayMode.GeoDecimalDegrees;
        }
        private void MapCoordinateModeButton_GMS_Click(object sender, RoutedEventArgs e)
        {
            this.GeoMapLocationPane.MapCoordinateDisplayMode = MapCoordinateDisplayMode.GeoDegreeMinuteSeconds;
        }
        private void MapCoordinateModeButton_MMP_Click(object sender, RoutedEventArgs e)
        {
            this.GeoMapLocationPane.MapCoordinateDisplayMode = MapCoordinateDisplayMode.MapMouseCoordinates;
        }
       
    }
}
