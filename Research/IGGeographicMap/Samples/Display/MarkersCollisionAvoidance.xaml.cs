using System.Collections.Generic;
using System.Collections.Specialized;
using IGGeographicMap.Models;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Samples
{
    public partial class MarkersCollisionAvoidance : Infragistics.Samples.Framework.SampleContainer
    {
        public MarkersCollisionAvoidance()
        {
            InitializeComponent();
        }

        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {            
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }
    }
    /// <summary>
    /// Represents a list of CollisionAvoidanceType enumerable values
    /// </summary>
    public class MarkerCollisionAvoidanceList : List<Infragistics.Controls.Charts.CollisionAvoidanceType>
    {
        
    }
}
