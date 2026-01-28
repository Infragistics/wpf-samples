using System.Collections.Generic;
using System.Windows;
using Infragistics.Samples.Shared.Models.Navigation;

namespace Infragistics.Samples.Shared.Models
{
    public class GeoRegion : ObservableModel
    {
        #region Constructors
        public GeoRegion()
        {
            this.Origin = new GeoLocation { Longitude = -180, Latitude = 90 };
            this.Ending = new GeoLocation { Longitude = 180, Latitude = -90 };
        }
        public GeoRegion(Point origin, Point ending)
        {
            this.Origin = GeoLocation.FromPoint(origin);
            this.Ending = GeoLocation.FromPoint(ending);
        }
        public GeoRegion(GeoLocation origin, GeoLocation ending)
        {
            this.Origin = origin;
            this.Ending = ending;
        }

        public GeoRegion(GeoLocation center, double width, double height)
            : this(new GeoLocation { Longitude = center.Longitude - width, Latitude = center.Latitude + height },
                   new GeoLocation { Longitude = center.Longitude + width, Latitude = center.Latitude - height })
        { }

        public GeoRegion(GeoLocation center, double dimension)
            : this(center, dimension, dimension)
        { }

        public GeoRegion(Rect mapRect)
            : this(new GeoLocation { Longitude = mapRect.X, Latitude = mapRect.Y },
                   new GeoLocation { Longitude = mapRect.X + mapRect.Width, Latitude = mapRect.Y + mapRect.Height })
        { }

        /// <summary>
        /// Creates a GeoRegion from geo-location (south-west) and geo-dimension (height and width)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public GeoRegion(double x, double y, double width, double height)
            : this(new Rect(x, y, width, height))
        { }

        public GeoRegion(Point origin, double width, double height)
            : this(new Rect(origin.X, origin.Y, width, height))
        { }
        /// <summary>
        /// Creates a GeoRegion from geo-location (south-west) and geo-dimension (height and width)
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dimension"></param>
        public GeoRegion(Point origin, double dimension)
            : this(new Rect(origin.X, origin.Y, dimension, dimension))
        { }

        #endregion
        #region Properties

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        private string _name;
        
        /// <summary>
        /// Gets or Sets the top-left corner (Origin) of GeoRegion
        /// </summary>
        public GeoLocation Origin { get { return _origins; } set { _origins = value; OnPropertyChanged("Origin"); } }
        private GeoLocation _origins;
      
        /// <summary>
        /// Gets or Sets the bottom-right corner (Ending) of GeoRegion
        /// </summary>
        public GeoLocation Ending { get { return _ending; } set { _ending = value; OnPropertyChanged("Ending"); } }
        private GeoLocation _ending;
        
        /// <summary>
        /// Gets or Sets the center of GeoRegion
        /// </summary>
        public GeoLocation Center { get { return GetCenter(); } set { this.SetCenter(value); OnPropertyChanged("Center"); } }
        private GeoLocation _center;

        public GeoLocation NorthWest 
        {
            get { return new GeoLocation(this.GeLongitudeMin(), this.GetLatitudeMax()); }
        }
        public GeoLocation NorthEast
        {
            get { return new GeoLocation(this.GeLongitudeMax(), this.GetLatitudeMax()); }
        }
        public GeoLocation SouthWest
        {
            get { return new GeoLocation(this.GeLongitudeMin(), this.GetLatitudeMin()); }
        }
        public GeoLocation SouthEast
        {
            get { return new GeoLocation(this.GeLongitudeMax(), this.GetLatitudeMin()); }
        }

        public double Width
        {
            get { return System.Math.Abs(this.Origin.Longitude - this.Ending.Longitude); }
        }
        public double Height
        {
            get { return System.Math.Abs(this.Origin.Latitude - this.Ending.Latitude); }
        }

        #endregion

        #region Methods
        private double GetLatitudeMin()
        {
            return System.Math.Min(this.Origin.Latitude, this.Ending.Latitude);
        }
        private double GeLongitudeMin()
        {
            return System.Math.Min(this.Origin.Longitude, this.Ending.Longitude);
        }
        private double GetLatitudeMax()
        {
            return System.Math.Max(this.Origin.Latitude, this.Ending.Latitude);
        }
        private double GeLongitudeMax()
        {
            return System.Math.Max(this.Origin.Longitude, this.Ending.Longitude);
        }

        private GeoLocation GetCenter()
        {
            _center = new GeoLocation
            {
                Longitude = this.Origin.Longitude + Width / 2.0,
                Latitude = this.Origin.Latitude - Height / 2.0
            };
            return _center;
        }
        private void SetCenter(GeoLocation newCenter)
        {
            _center = newCenter;
            double widthHalf = Width / 2.0;
            double heightHalf = Height / 2.0;
            _origins = new GeoLocation
            {
                Longitude = _center.Longitude - widthHalf,
                Latitude = _center.Latitude + heightHalf
            };
            _ending = new GeoLocation
            {
                Longitude = _center.Longitude + widthHalf,
                Latitude = _center.Latitude - heightHalf
            };
        }
        #endregion
        #region Converters

        /// <summary>
        /// Convert GeoRegion to Rect object
        /// </summary>
        /// <returns></returns>
        public Rect ToRect()
        {
            //double x = System.Math.Min(Origin.Longitude, Ending.Longitude);
            //double y = System.Math.Max(Origin.Latitude, Ending.Latitude);
            //double w = System.Math.Abs(Origin.Longitude - Ending.Longitude);
            //double h = System.Math.Abs(Origin.Latitude - Ending.Latitude);
            //return new Rect(x, y, w,h);
            return new Rect
            {
                X = Origin.Longitude,
                Y = Origin.Latitude,
                //X = System.Math.Min(Origin.Longitude, Ending.Longitude),
                //Y = System.Math.Min(Origin.Latitude, Ending.Latitude),
                Width = System.Math.Abs(Origin.Longitude - Ending.Longitude),
                Height = System.Math.Abs(Ending.Latitude - Origin.Latitude)
                //Height = System.Math.Abs(Origin.Latitude - Ending.Latitude)
            };
        }
        /// <summary>
        /// Converts to Geographic Rect where bottom-left of the rectangle is southEast and top-right is northWest coordinate
        /// </summary>
        /// <returns></returns>
        public Rect ToGeoRect()
        {
            double xMin = System.Math.Min(this.Origin.Longitude, this.Ending.Longitude);
            double yMin = System.Math.Min(this.Origin.Latitude, this.Ending.Latitude);
            double xMax = System.Math.Max(this.Origin.Longitude, this.Ending.Longitude);
            double yMax = System.Math.Max(this.Origin.Latitude, this.Ending.Latitude);
            double w = System.Math.Abs(xMax - xMin);
            double h = System.Math.Abs(yMax - yMin);
            return new Rect(xMin, yMin, w, h);
        }
        public Rect ToWindowRect()
        {
            double xMin = System.Math.Min(this.Origin.Longitude, this.Ending.Longitude);
            double yMin = System.Math.Min(this.Origin.Latitude, this.Ending.Latitude);
            double xMax = System.Math.Max(this.Origin.Longitude, this.Ending.Longitude);
            double yMax = System.Math.Max(this.Origin.Latitude, this.Ending.Latitude);

            
            double w = System.Math.Abs(xMax - xMin);
            double h = System.Math.Abs(yMax - yMin);

            var x = GeoCalculator.GetWindowCoordinateX(xMin);
            var y = GeoCalculator.GetWindowCoordinateX(yMin);

            w = GeoCalculator.GetWindowCoordinateX(xMax); // -x;
            h = GeoCalculator.GetWindowCoordinateX(yMax); // - y;

            var size = System.Math.Max(w, h);

            return new Rect(x, y, size, size);
            //return new Rect(x, y, w, h);
        }
        public new string ToString()
        {
            return this.ToGeoRect().ToString();
        }
        public static GeoRegion FromRect(Rect mapRect)
        {
            return new GeoRegion(mapRect);
        }
        #endregion
    }

    public static class GeoRegions
    {

        public static List<GeoRegion> KnownRegions
        {
            get
            {
                var list = new List<GeoRegion>
                {
                    WorldFullRegion, NorthAmericaRegion, SouthAmericaRegion, 
                    AfricaRegion, EuropeRegion, AsiaRegion, OceaniaRegion
                };
                return list;
            }
        }
        /// <summary>
        /// Finds a geo-region in the list of known geo-regions or returns geo-region of the world
        /// <remarks> </remarks>
        /// </summary>
        /// <param name="regionIdentifier"></param>
        public static GeoRegion FindGeoRegion(string regionIdentifier)
        {
            foreach (GeoRegion region in GeoRegions.KnownRegions)
            {
                if(region.Name.Equals(regionIdentifier))
                {
                    return region;
                }
            }
            return GeoRegions.WorldFullRegion;
        }
        /// <summary>
        /// GeoRegion of the whole world
        /// </summary>
        public static GeoRegion WorldFullRegion = new GeoRegion
        {
            Name = "World",
            Origin = new GeoLocation { Longitude = -180, Latitude = 90 },
            Ending = new GeoLocation { Longitude = 180, Latitude = -90 }
        };
        /// <summary>
        /// GeoRegion of the world without polar regions
        /// </summary>
        public static GeoRegion WorldNonPolarRegion = new GeoRegion
        {
            Name = "WorldNonPolar",
            Origin = new GeoLocation { Longitude = -180, Latitude = 60 },
            Ending = new GeoLocation { Longitude = 180, Latitude = -50 }
        };
        /// <summary>
        /// GeoRegion of the world without Antarctic continent
        /// </summary>
        public static GeoRegion WorldNonAntarcticRegion = new GeoRegion
        {
            Name = "WorldNonAntarcticRegion",
            Origin = new GeoLocation { Longitude = -116, Latitude = 84 },
            Ending = new GeoLocation { Longitude = 132, Latitude = -56 }
        };
        /// <summary>
        /// GeoRegion of the world with six main continents
        /// </summary>
        public static GeoRegion WorldSixMainContinents = new GeoRegion
        {
            Name = "WorldSixMainContinents",
            Origin = new GeoLocation { Longitude = -116, Latitude = 85 },
            Ending = new GeoLocation { Longitude = 132, Latitude = -60 }
        };
        /// <summary>
        /// GeoRegion of the world with seven continents
        /// </summary>
        public static GeoRegion WorldSevenContinents = new GeoRegion
        {
            Name = "WorldSevenContinents",
            Origin = new GeoLocation { Longitude = -116, Latitude = 70 },
            Ending = new GeoLocation { Longitude = 132, Latitude = -85 }
        };
        /// <summary>
        /// GeoRegion of the world with North America continent
        /// </summary>
        public static GeoRegion NorthAmericaRegion = new GeoRegion
        {
            Name = "North America",
            Origin = new GeoLocation { Longitude = -180, Latitude = 70 },
            Ending = new GeoLocation { Longitude = -50, Latitude = 5 }
        };
        /// <summary>
        /// GeoRegion of the world with South America continent
        /// </summary>
        public static GeoRegion SouthAmericaRegion = new GeoRegion
        {
            Name = "South America",
            Origin = new GeoLocation { Longitude = -130, Latitude = 15 },
            Ending = new GeoLocation { Longitude = 15, Latitude = -60 }
        };
        /// <summary>
        /// GeoRegion of the world with Africa continent
        /// </summary>
        public static GeoRegion AfricaRegion = new GeoRegion
        {
            Name = "Africa",
            Origin = new GeoLocation { Longitude = -20, Latitude = 40 },
            Ending = new GeoLocation { Longitude = 55, Latitude = -40 }
        };
        /// <summary>
        /// GeoRegion of the world with Europe continent
        /// </summary>
        public static GeoRegion EuropeRegion = new GeoRegion
        {
            Name = "Europe",
            Origin = new GeoLocation { Longitude = -25, Latitude = 70 },
            Ending = new GeoLocation { Longitude = 45, Latitude = 30 }
        };
        /// <summary>
        /// GeoRegion of the world with Asia continent 
        /// </summary>
        public static GeoRegion AsiaRegion = new GeoRegion
        {
            Name = "Asia",
            Origin = new GeoLocation { Longitude = 30, Latitude = 70 },
            Ending = new GeoLocation { Longitude = 180, Latitude = -10 }
        };
        /// <summary>
        /// GeoRegion of the world with Oceania continent
        /// </summary>
        public static GeoRegion OceaniaRegion = new GeoRegion
        {
            Name = "Oceania",
            Origin = new GeoLocation { Longitude = 110, Latitude = -10 },
            Ending = new GeoLocation { Longitude = 180, Latitude = -50 }
        };
        /// <summary>
        /// GeoRegion of the world with Australia continent
        /// </summary>
        public static GeoRegion AustraliaRegion = new GeoRegion
        {
            Name = "Australia",
            Origin = new GeoLocation { Longitude = 108, Latitude = 6 },
            Ending = new GeoLocation { Longitude = 154, Latitude = -46 }
        };
        /// <summary>
        /// GeoRegion of the world with United States
        /// </summary>
        public static GeoRegion UnitedStatesRegion = new GeoRegion
        {
            Name = "United States",
            Origin = new GeoLocation { Longitude = -130, Latitude = 50 },
            Ending = new GeoLocation { Longitude = -50, Latitude = 20 }
        };
        /// <summary>
        /// GeoRegion of the world with United States (lower 48 states)
        /// </summary>
        public static GeoRegion UnitedStatesLower48Region = new GeoRegion
        {
            Name = "United States Lower 48",
            Origin = new GeoLocation { Longitude = -115, Latitude = 55 },
            Ending = new GeoLocation { Longitude = -75, Latitude = 15 }
        };
    }
}
