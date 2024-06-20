using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Models
{
    /// <summary>
    /// Represents an EarthQuake view model in geographic context
    /// </summary>
    public class EarthQuakeViewModel : Infragistics.Controls.Maps.ShapefileRecord
    {
        public EarthQuakeViewModel()
        {
            this.EarthQuakeData = new EarthQuake();
        }
        public EarthQuake EarthQuakeData { get; set; }
    }
 
}