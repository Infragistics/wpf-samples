using System.Collections.ObjectModel;
using System.Windows;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents distance in geographic context and provides conversion between kilometers, miles, and degrees
    /// </summary>
    public class GeoDistance //: ObservableModel
    {
        public GeoDistance() : this(0)
        { }
        public GeoDistance(double distance)
        {
            this.Kilometers = distance;
        }
        public GeoDistance(Point origin, Point destination)
            : this(new GeoPoint(origin), new GeoPoint(destination))
        { }
        public GeoDistance(IGeoLocatable origin, IGeoLocatable destination)
        {
            this.Kilometers = GeoCalculator.GetDistance(origin, destination).Kilometers;
        }
     
        #region Properties
        private double _kilometers;
        /// <summary>
        /// Gets or sets the distance in kilometers
        /// </summary>
        public double Kilometers 
        { 
            get { return _kilometers;}
            set 
            { 
                _kilometers = value;
                _miles = _kilometers * GeoGlobal.Converters.KilometersToMiles;
                _degrees = _kilometers * GeoGlobal.Converters.KilometersToDegrees;
                //OnPropertyChanged("Kilometers");
            }  
        }

        private double _miles;
        /// <summary>
        /// Gets or sets the distance in nautical miles 
        /// </summary>
        public double Miles
        {
            get { return _miles; }
            set
            {
                _miles = value;
                _kilometers = _miles * GeoGlobal.Converters.MilesToKilometers;
                _degrees = _kilometers * GeoGlobal.Converters.KilometersToDegrees;
                //OnPropertyChanged("Miles");
            }
        }

        private double _degrees;
        /// <summary>
        /// Gets or sets the distance in geographic degrees 
        /// </summary>
        public double Degrees
        {
            get { return _degrees; }
            set
            {
                _degrees = value;
                _kilometers = _degrees * GeoGlobal.Converters.DegreesToKilometers;
                _miles = _kilometers * GeoGlobal.Converters.KilometersToMiles;
                //OnPropertyChanged("Degrees");
            }
        }
        

	#endregion
    }
   
}
