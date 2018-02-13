using Infragistics.Controls.Maps;

namespace IGShowcase.HospitalFloorPlan.ViewModels
{
    public class HospitalMapViewModel
    {
        public HospitalMapViewModel()
        {
            MapProjector = new MapCartesianProjection();
        }

        public MapCartesianProjection MapProjector { get; set; }
        public XamGeographicMap MapView { get; set; }
     
    }
}