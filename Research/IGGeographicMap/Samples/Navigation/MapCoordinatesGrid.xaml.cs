 
using IGGeographicMap.Models;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples.Navigation
{
	public partial class MapCoordinatesGrid : Infragistics.Samples.Framework.SampleContainer
	{
        public MapCoordinatesGrid()
        {
	        InitializeComponent();
            this.SampleLoaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, System.EventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }
		
	}

	
}
