using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Projection objects define the full public API for projecting
    /// geodetic map coordinates to/from Cartesian the internal coordinate
    /// system
    /// </summary>
    /// <remarks>
    /// The general purpose of map projections and the problems encountered
    /// have been discusses often and well in various books on cartography
    /// and map projections. Every map user and maker should have a basic
    /// understanding of projections, no matter how much computers seem
    /// to have automated the operations.
    /// <para>
    /// A map projection is a systematic representation of all or part of the
    /// surface of a round body, especially the Earth, on a plane. This usually
    /// includes delineating meridians and parallels, as required by some definitions
    /// of a map projection, but it may not, depending on the the purpose of the
    /// map. A projection is required in any case. Since this cannot be done without
    /// distortion, the cartographer must choose the characteristic which is to
    /// be shown accurately at the expense of others, or a compromise of several
    /// characteristics. If the map covers a continent or the Earth, distortion
    /// will be visually apparent. If the region is the size of a small town,
    /// distortion may be barely measurable using many projections, but it can
    /// still be serious with other projections. There is literally an infinite
    /// number of projections that can be devised, and several hundred have been
    /// published, most of which are rarely-used novelties. Most projections
    /// may be infinitely varied by choosing different points on Earth as the
    /// center or as a starting point.
    /// </para>
    /// <para>
    /// It cannot be said that there is one "best" projection for mapping. It is
    /// even risky to claim that one has found the best projection for a given
    /// application, unless the parameters chosen are artificially constricting.
    /// A carefully constructed globe is not the best map for most applications 
    /// because its scale is by necessity too small. A globe is awkward to use
    /// in general, and a straight edge cannot be satisfactorily used on one for
    /// measurement of distance.
    /// </para>
    /// <para>
    /// To identify the location points on the Earth, a graticule or network of
    /// longitude and latitude lines has been superimposed on the surface. They 
    /// are commonly referred to as meridians and parallels respectively.
    /// </para>
    /// <para>
    /// Given the North and South poles, which are approximately the ends of the 
    /// axis about which the Earth rotates, and the Equator, an imaginary line
    /// halfway between the two poles, the parallels of latitude are formed
    /// by circles surrounding the Earth and in planes parallel with that of 
    /// the equator. If the circles are drawn equally spaced along the surface
    /// of the sphere, with 90 spaces from the equator to 90 degrees North and South
    /// at the respective poles, each is called a degree of latitude.
    /// </para>
    /// <para>
    /// Meridians of longitude are formed with a series of imaginary lines, all 
    /// intersecting at both the North and South poles, and crossing each
    /// parallel of latitude at right angles but striking the equator at various 
    /// points.
    /// </para>
    /// <para>
    /// There is only one location for the equator and poles which serve as references
    /// for counting degrees of latitude, but there is no natural origin from which
    /// to count degrees of longitude, since all meridians are identical in shape and
    /// size. It thus becomes necessary to choose arbitrarily one meridian as the
    /// starting point, or prime meridian. In 1884, the International Meridian 
    /// Conference, meeting in Washington, agreed to adopt the "meridian passing through
    /// the center of the transit instrument at the Observatory of Greenwich as the
    /// initial meridian for longitude" resolving that "from this meridian longitude
    /// shall be counted in two directions up to 180 degrees, east longitude being
    /// plus and west longitude being minus"
    /// </para>
    /// </remarks>
    public abstract class GeoProjection : DependencyObject
    {
        internal GeoProjection()
        {
            UpdateConstants();
        }
        #region Propeties

        #region GeoEllipsoidType Dependency Property
        /// <summary>
        /// Sets or gets the current projections's datum
        /// </summary>
        public GeoEllipsoidType EllipsoidType
        {
            get
            {
                return (GeoEllipsoidType)GetValue(EllipsoidTypeProperty);
            }
            set
            {
                SetValue(EllipsoidTypeProperty, value);
            }
        }
        /// <summary>
        /// Identifies the GeoEllipsoidType dependency property.
        /// </summary>
        public static readonly DependencyProperty EllipsoidTypeProperty = DependencyProperty.Register(
            "EllipsoidType", typeof(GeoEllipsoidType), typeof(GeoProjection),
            new PropertyMetadata(GeoEllipsoidType.WGS84, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        public virtual double LatitudeMin
        {
            get { return GeoGlobal.WorldsMercator.LatitudeMin; }
        }

        public virtual double LatitudeMax
        {
            get { return GeoGlobal.WorldsMercator.LatitudeMax; }
        }

        public virtual double LongitudeMin
        {
            get { return GeoGlobal.WorldsMercator.LongitudeMin; }
        }

        public virtual double LongitudeMax
        {
            get { return GeoGlobal.WorldsMercator.LongitudeMax; }
        } 
        #endregion

        /// <summary>
        /// Updates the constants.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        protected static void UpdateConstants(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as GeoProjection).UpdateConstants();
        }

        /// <summary>
        /// Updates the constants.
        /// </summary>
        protected virtual void UpdateConstants()
        {
        }

        //internal static Point RealToMap(Point real)
        //{
        //    return new Point(real.X, -real.Y);
        //}

        internal static Point InverseLatitude(Point real)
        {
            return new Point(real.X, -real.Y);
        }

        #region Project -> Cartesian
        /// <summary>
        /// Projects a geodetic point to Cartesian coordinate system
        /// </summary>
        public Point ProjectToCartesian(Point geodetic)
        {
            return InverseLatitude(ConvertToCartesian(geodetic));
        }
        ///// <summary>
        ///// Projects a geodetic point to Cartesian coordinate system
        ///// </summary>
        //public GeoPoint ProjectToCartesian(IGeoLocatable geodetic)
        //{
        //    return new GeoPoint(ProjectToCartesian(geodetic.ToPoint()));
        //}
        /// <summary>
        /// Projects a list of geodetic points to Cartesian coordinate system
        /// </summary>
        public List<Point> ProjectToCartesian(IList<Point> geodetic)
        {
            var cartesian = new List<Point> { Capacity = geodetic.Count };
            for (int i = 0; i < geodetic.Count; ++i)
            {
                cartesian.Add(ProjectToCartesian(geodetic[i]));
            }
            return cartesian;
        }

        /// <summary>
        /// Projects a list of list of geodetic to Cartesian coordinate system
        /// </summary>
        public List<List<Point>> ProjectToCartesian(IList<List<Point>> geodetic)
        {
            var cartesian = new List<List<Point>> { Capacity = geodetic.Count };
            foreach (var mapPolyline in geodetic)
            {
                cartesian.Add(ProjectToCartesian(mapPolyline));
            }
            return cartesian;
        } 
        #endregion

        #region Project -> Geographic
        /// <summary>
        /// Projects a Cartesian point to geodetic coordinate system  
        /// </summary>
        public Point ProjectToGeographic(Point cartesian)
        {
            return ConvertToGeographic(InverseLatitude(cartesian));
        }

        /// <summary>
        /// Projects a list of Cartesian points to geodetic coordinate system  
        /// </summary>
        public List<Point> ProjectToGeographic(IList<Point> cartesian)
        {
            var geodetic = new List<Point> { Capacity = cartesian.Count };
            for (int i = 0; i < cartesian.Count; ++i)
            {
                geodetic.Add(ProjectToGeographic(cartesian[i]));
            }
            return geodetic;
        }

        /// <summary>
        /// Projects a list of list of Cartesian points to geodetic coordinate system  c.
        /// </summary>
        public List<List<Point>> ProjectToGeographic(List<List<Point>> cartesian)
        {
            var geodetic = new List<List<Point>> { Capacity = cartesian.Count };
            foreach (var mapPolyline in cartesian)
            {
                geodetic.Add(ProjectToGeographic(mapPolyline));
            }
            return geodetic;
        } 
        #endregion

        #region Projection's Overrides
        /// <summary>
        /// Projects a geodetic point to Cartesian coordinate system  
        /// </summary>
        protected abstract Point ConvertToCartesian(Point geodetic);

        /// <summary>
        /// Projects a Cartesian point to geodetic coordinate system  
        /// </summary>
        protected abstract Point ConvertToGeographic(Point cartesian);

        #endregion
        internal virtual void RenderGrid(Path path, Rect worldRect, Rect windowRect)
        {
            // clip the windowRect to the admissible window

            var pathGeometry = new PathGeometry();

            if (!worldRect.IsEmpty && !windowRect.IsEmpty)
            {
                Point lt = ProjectToGeographic(new Point(windowRect.Left, windowRect.Top));
                Point rb = ProjectToGeographic(new Point(windowRect.Right, windowRect.Bottom));

                int ntick = 10;
                //double lonrange= MathUtil.NiceCeiling(Math.Abs(rb.X - lt.X));
                //double latrange= MathUtil.NiceCeiling(Math.Abs(lt.Y - rb.Y));

                //double lonspacing = MathUtil.NiceRound(lonrange / (ntick - 1));
                //double latspacing = MathUtil.NiceRound(latrange / (ntick - 1));
                //double spacing = Math.Min(lonspacing, latspacing);

                //double lonmin = Math.Floor(lt.X / spacing) * spacing;
                //double lonmax = Math.Ceiling(rb.X / spacing) * spacing;

                //double latmin = Math.Floor(rb.Y / spacing) * spacing;
                //double latmax = Math.Ceiling(lt.Y / spacing) * spacing;

                //#region longitude (vertical lines)

                //for (double lon = lonmin; lon <= lonmax; lon += spacing)
                //{
                //    Point p0 = ProjectToCartesian(new Point(lon, latmin));
                //    Point p1 = ProjectToCartesian(new Point(lon, latmax));

                //    var pathFigure = new PathFigure() { StartPoint = p0, IsClosed = false, IsFilled = false };
                //    pathFigure.Segments.Add(new LineSegment() { Point = p1 });

                //    pathGeometry.Figures.Add(pathFigure);
                //}
                //#endregion

                //#region latitude (horizontal lines)

                //for (double lat = latmin; lat <= latmax; lat += spacing)
                //{
                //    Point p0 = ProjectToCartesian(new Point(lonmin, lat));
                //    Point p1 = ProjectToCartesian(new Point(lonmax, lat));

                //    var pathFigure = new PathFigure() { StartPoint = p0, IsClosed = false, IsFilled = false };
                //    pathFigure.Segments.Add(new LineSegment() { Point = p1 });

                //    pathGeometry.Figures.Add(pathFigure);
                //}
                //#endregion
            }
            path.Data = pathGeometry;
        }
    }

   
}
