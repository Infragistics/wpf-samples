using System.Windows;
using System.Windows.Controls;
using IGGeographicMap.Models;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples
{
    public partial class MapNavigationPane : Infragistics.Samples.Framework.SampleContainer
    {
        public MapNavigationPane()
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
            this.GeoMapNavigationPane.MapPaneOrientation = Orientation.Vertical;
        }
        private void MapCoordinateHorizontalOrientationButton_Click(object sender, RoutedEventArgs e)
        {
            this.GeoMapNavigationPane.MapPaneOrientation = Orientation.Horizontal;
        }

    }
}
