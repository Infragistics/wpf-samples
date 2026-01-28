using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.ChartData.ChartSeries
{
    /// <summary>
    /// Represents a custom type of ContourAreaSeries for XamDataChart control.
    /// </summary>
    public class ContourAreaSeries : Series
    {
        public ContourAreaSeries()
        {
            this.DefaultStyleKey = typeof(ContourAreaSeries);
        }

        #region Property - XAxis
        public const string XAxisPropertyName = "XAxis";
        public static readonly DependencyProperty XAxisProperty =
            DependencyProperty.Register(XAxisPropertyName, typeof(NumericXAxis),
            typeof(ContourAreaSeries), new PropertyMetadata(null, (sender, e) =>
            {
                var series = (ContourAreaSeries)sender;
                series.RaisePropertyChanged(XAxisPropertyName, e.OldValue, e.NewValue);
            }));
        public NumericXAxis XAxis
        {
            get
            {
                return this.GetValue(XAxisProperty) as NumericXAxis;
            }
            set
            {
                this.SetValue(XAxisProperty, value);
            }
        }
        #endregion
        #region Property - YAxis
        public const string YAxisPropertyName = "YAxis";
        public static readonly DependencyProperty YAxisProperty =
            DependencyProperty.Register(YAxisPropertyName, typeof(NumericYAxis),
            typeof(ContourAreaSeries), new PropertyMetadata(null, (sender, e) =>
            {
                var series = (ContourAreaSeries)sender;
                series.RaisePropertyChanged(YAxisPropertyName, e.OldValue, e.NewValue);
            }));
        public NumericYAxis YAxis
        {
            get
            {
                return this.GetValue(YAxisProperty) as NumericYAxis;
            }
            set
            {
                this.SetValue(YAxisProperty, value);
            }
        }
        #endregion

        public BrushCollection ActualContourBrushes { get; private set; }
        public BrushCollection ActualContourOutlines { get; private set; }

        #region Brush Methods
        private Brush GetContourPathFill(int conturIndex)
        {
            return GetValidBrush(conturIndex, ((XamDataChart)this.SeriesViewer).Brushes);
        }
        private Brush GetContourPathStroke(int conturIndex)
        {
            return GetValidBrush(conturIndex, ((XamDataChart)this.SeriesViewer).MarkerOutlines);
        }
        private Brush GetContourMarkerOutline(int conturIndex)
        {
            return GetValidBrush(conturIndex, ((XamDataChart)this.SeriesViewer).MarkerOutlines);
        }
        private Brush GetContourMarkerFill(int conturIndex)
        {
            return GetValidBrush(conturIndex, ((XamDataChart)this.SeriesViewer).MarkerBrushes);
        }
        private static Brush GetValidBrush(int conturIndex, BrushCollection brushes)
        {
            if (brushes == null || brushes.Count == 0)
            {
                return new SolidColorBrush(Colors.Black);
            }
            if (conturIndex >= 0 && conturIndex < brushes.Count)
            {
                return brushes[conturIndex];
            }
            conturIndex = conturIndex % brushes.Count;
            return brushes[conturIndex];
        }
        #endregion
        #region Series Overriden Methods

        /// <summary>
        /// Renders the Custom Contour Area Series
        /// </summary>
        /// <param name="animate"></param>
        protected override void RenderSeriesOverride(bool animate)
        {
            // disables series rendering with transitions (Motion Framework)
            base.RenderSeriesOverride(animate);
            // check if the series can be rendered:
            // - the Viewport (the bounds rectangle for the series) is not empty, 
            // - the RootCanvas (the container for the custom graphics) is not null.  
            // - the Axes are not null.  
            // - the ItemsSource is not null.  
            if (this.Viewport.IsEmpty || this.RootCanvas == null ||
                this.XAxis == null || this.YAxis == null ||
                this.ItemsSource == null)
            {
                return;
            }
            // clears the RootCanvas on every render of the series
            this.RootCanvas.Children.Clear();

            // create data structure for contours based on values of items in the source of this series
            var data = (ContourData)this.ItemsSource;
            var dataContours = new Dictionary<double, PointCollection>();
            foreach (ContourDataPoint dataPoint in data)
            {
                bool isInverted = this.XAxis.IsInverted;
                var param = new ScalerParams(this.SeriesViewer.WindowRect, this.Viewport, isInverted);
                // scale x-locations of data point to the series' viewport
                double x = this.XAxis.GetScaledValue(dataPoint.X, param);

                isInverted = this.YAxis.IsInverted;
                param = new ScalerParams(this.SeriesViewer.WindowRect, this.Viewport, isInverted);
                // scale y-location of data point to the series' viewport
                double y = this.YAxis.GetScaledValue(dataPoint.Y, param);

                // store scaled locations of data point based on the Value property of data points
                double key = dataPoint.Value;
                if (dataContours.ContainsKey(key))
                {
                    dataContours[key].Add(new Point(x, y));
                }
                else
                {
                    var dataPoints = new PointCollection { new Point(x, y) };
                    dataContours.Add(key, dataPoints);
                }
            }
            // sort contours data based on contout 
            var sortedContours = from item in dataContours
                                 orderby item.Key ascending
                                 select item;

            // re-use chart's brushes and outlines for actual contour's brushes and outlines 
            this.ActualContourBrushes = ((XamDataChart)this.SeriesViewer).Brushes;
            this.ActualContourOutlines = ((XamDataChart)this.SeriesViewer).MarkerOutlines;

            // create elements of contours based on contours data structure
            int conturIndex = 0;
            foreach (KeyValuePair<double, PointCollection> contour in sortedContours) //dataContours)
            {
                foreach (Point point in contour.Value)
                {
                    // get parameters of a contour marker
                    double contourMarkerValue = contour.Key;
                    double contourMarkerSize = 25;
                    double contourMarkerLocationLeft = point.X - contourMarkerSize / 2;
                    double contourMarkerLocationTop = point.Y - contourMarkerSize / 2;

                    // create element for shape of a contour marker
                    var contourMarker = new Ellipse();
                    contourMarker.Fill = GetContourMarkerFill(conturIndex);
                    contourMarker.Stroke = GetContourMarkerOutline(conturIndex);
                    contourMarker.StrokeThickness = 1.0;
                    contourMarker.Width = contourMarkerSize;
                    contourMarker.Height = contourMarkerSize;

                    // create element for value of a contour marker
                    var markerValueBlock = new TextBlock();
                    markerValueBlock.Text = contourMarkerValue.ToString();
                    markerValueBlock.Foreground = new SolidColorBrush(Colors.White);
                    markerValueBlock.VerticalAlignment = VerticalAlignment.Center;
                    markerValueBlock.HorizontalAlignment = HorizontalAlignment.Center;

                    // create element to hold elements of a contour marker 
                    var markerGrid = new Grid();
                    markerGrid.Children.Add(contourMarker);
                    markerGrid.Children.Add(markerValueBlock);
                    Canvas.SetLeft(markerGrid, contourMarkerLocationLeft);
                    Canvas.SetTop(markerGrid, contourMarkerLocationTop);
                    Canvas.SetZIndex(markerGrid, conturIndex + 11);

                    // render the marker of the current contour on the canvas of this series
                    this.RootCanvas.Children.Add(markerGrid);
                }

                PointCollection contourPoints = contour.Value;

                // create curve from points of a contour
                PathFigure contourFigure = ShapeBuilder.GetBezierSegments(contourPoints, 1.0, true);
                contourFigure.IsClosed = true;

                // create a new PathGeometry for a contour
                var contourGeo = new PathGeometry();
                contourGeo.Figures.Add(contourFigure);
                // create a new Path for a contour
                var contourShape = new Path();
                contourShape.Data = contourGeo;
                contourShape.Stroke = GetContourPathStroke(conturIndex);
                contourShape.StrokeThickness = this.Thickness;
                contourShape.Fill = GetContourPathFill(conturIndex);
                Canvas.SetZIndex(contourShape, conturIndex + 10);

                // render shape of the current contour on the canvas of this series
                this.RootCanvas.Children.Add(contourShape);
                conturIndex++;
            }
        }

        /// <summary>
        /// Redraw series any time the Viewport rect changes
        /// </summary>
        /// <param name="oldViewportRect"></param>
        /// <param name="newViewportRect"></param>
        protected override void ViewportRectChangedOverride(Rect oldViewportRect, Rect newViewportRect)
        {
            base.ViewportRectChangedOverride(oldViewportRect, newViewportRect);
            this.RenderSeries(false);
        }
        protected override void WindowRectChangedOverride(Rect oldWindowRect, Rect newWindowRect)
        {
            base.WindowRectChangedOverride(oldWindowRect, newWindowRect);
            this.RenderSeries(false);
        }

        /// <summary>
        /// Overrides property update of this series
        /// </summary>
        protected override void PropertyUpdatedOverride(object sender, string propertyName, object oldValue, object newValue)
        {
            base.PropertyUpdatedOverride(sender, propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case ItemsSourcePropertyName:
                    this.RenderSeries(false);
                    if (this.XAxis != null)
                    {
                        this.XAxis.UpdateRange();
                    }
                    if (this.YAxis != null)
                    {
                        this.YAxis.UpdateRange();
                    }
                    break;

                case XAxisPropertyName:
                    if (oldValue != null)
                    {
                        ((Infragistics.Controls.Charts.Axis)oldValue).DeregisterSeries(this);
                    }

                    if (newValue != null)
                    {
                        ((Infragistics.Controls.Charts.Axis)newValue).RegisterSeries(this);
                    }

                    if ((XAxis != null && !XAxis.UpdateRange()) ||
                        (newValue == null && oldValue != null))
                    {
                        RenderSeries(false);
                    }
                    break;

                case YAxisPropertyName:
                    if (oldValue != null)
                    {
                        ((Infragistics.Controls.Charts.Axis)oldValue).DeregisterSeries(this);
                    }

                    if (newValue != null)
                    {
                        ((Infragistics.Controls.Charts.Axis)newValue).RegisterSeries(this);
                    }
                    if ((YAxis != null && !YAxis.UpdateRange()) ||
                        (newValue == null && oldValue != null))
                    {
                        RenderSeries(false);
                    }
                    break;
            }
        }

        /// <summary>
        /// Calculates range of a given axis based on  X/Y values of data items
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        protected override AxisRange GetRange(Infragistics.Controls.Charts.Axis axis)
        {
            var myData = this.ItemsSource as ContourData;
            if (myData == null)
            {
                return base.GetRange(axis);
            }
            if (axis == this.XAxis)
            {
                double min = double.MaxValue;
                double max = double.MinValue;
                foreach (ContourDataPoint dataPoint in myData)
                {
                    min = System.Math.Min(min, dataPoint.X);
                    max = System.Math.Max(max, dataPoint.X);
                }
                return new AxisRange(min, max);
            }
            else if (axis == this.YAxis)
            {
                double min = double.MaxValue;
                double max = double.MinValue;
                foreach (ContourDataPoint dataPoint in myData)
                {
                    min = System.Math.Min(min, dataPoint.Y);
                    max = System.Math.Max(max, dataPoint.Y);
                }
                return new AxisRange(min, max);
            }
            else
            {
                return base.GetRange(axis);
            }

        }

        /// <summary>
        /// Gets the item item associated with the specified world position 
        /// when a tooltip must be displayed on the series' marker
        /// </summary>
        public override object GetItem(Point worldPoint)
        {
            var param = new ScalerParams(this.SeriesViewer.ActualWindowRect, this.Viewport, false);

            var cursorPoint = new Point(this.SeriesViewer.ViewportRect.Width * worldPoint.X,
                                          this.SeriesViewer.ViewportRect.Height * worldPoint.Y);
            var data = (ContourData)this.ItemsSource;
            foreach (ContourDataPoint dataPoint in data)
            {
                // scale locations of data point to the series' viewport
                param.IsInverted = this.XAxis.IsInverted;
                double x = this.XAxis.GetScaledValue(dataPoint.X, param);
                param.IsInverted = this.YAxis.IsInverted;
                double y = this.YAxis.GetScaledValue(dataPoint.Y, param);

                double size = 25;
                double left = x - size / 2;
                double top = y - size / 2;

                var itemBounds = new Rect(left, top, size, size);
                if (itemBounds.Contains(cursorPoint))
                {
                    return dataPoint;
                }
            }

            return null;
        }

        #endregion
    }

    public class ShapeBuilder
    {
        /// <summary>
        /// Returns PathFigure representing a Bézier curve that passes through specified points
        /// </summary>
        /// <param name="points"></param>
        /// <param name="tension"></param>
        /// <param name="isClosed"></param>
        /// <returns></returns>
        public static PathFigure GetBezierSegments(PointCollection points, double tension, bool isClosed = false)
        {
            var ret = new PathFigure();
            ret.Segments.Clear();
            ret.IsClosed = false;

            if (isClosed)
            {
                Point first = points[0];
                Point last = points[points.Count - 1];

                if (first.X != last.X || first.Y != last.Y)
                {
                    points.Add(first);
                }
            }
            var bzPoints = GetBezierPoints(points, tension);
            // First point is the starting point.
            ret.StartPoint = bzPoints[0];

            for (int i = 1; i < bzPoints.Count; i += 3)
            {
                ret.Segments.Add(new BezierSegment
                {
                    Point1 = bzPoints[i],       // B1 control point.
                    Point2 = bzPoints[i + 1],   // B2 control point.
                    Point3 = bzPoints[i + 2]    // P2 start / end point.
                });
            }

            return ret;
        }

        #region Bezier Methods
        // some of the logic for calculating is based on:
        // http://www.codeproject.com/KB/silverlight/MapBezier.aspx
        /// <summary>
        /// Returns points of a Bézier curve passing through specified points
        /// </summary>
        /// <param name="points"></param>
        /// <param name="tension"></param>
        /// <returns></returns>
        public static PointCollection GetBezierPoints(PointCollection points, double tension)
        {
            PointCollection ret = new PointCollection();

            for (int i = 0; i < points.Count; i++)
            {
                // for first point append as is.
                if (i == 0)
                {
                    ret.Add(points[0]);
                    continue;
                }

                // for each point except first and last get B1, B2. next point. 
                // Last point do not have a next point.
                ret.Add(GetBezierControlPoint1(points, i - 1, tension));
                ret.Add(GetBezierControlPoint2(points, i - 1, tension));
                ret.Add(points[i]);
            }

            return ret;
        }
        /// <summary>
        /// Returns first control point of a Bézier curve.
        /// </summary>
        /// <param name="points">Points on the curve.</param>
        /// <param name="i">Point no to calculate control point for.</param>
        /// <param name="tension">Tension</param>
        /// <returns></returns>
        /// <remarks>Formula: B1i = Pi + Pi' / 3</remarks>
        public static Point GetBezierControlPoint1(PointCollection points, int i, double tension)
        {
            var drv = GetBezierDerivative(points, i, tension);
            return new Point(points[i].X + drv.X / 3, points[i].Y + drv.Y / 3);
        }

        /// <summary>
        /// Returns second control point of a Bézier curve.
        /// </summary>
        /// <param name="points">Points on the curve.</param>
        /// <param name="i">Point no to calculate control point for.</param>
        /// <param name="tension">Tension</param>
        /// <returns></returns>
        /// <remarks>Formula: B2i = P[i + 1] - P'[i + 1] / 3</remarks>
        public static Point GetBezierControlPoint2(PointCollection points, int i, double tension)
        {
            var drv = GetBezierDerivative(points, i + 1, tension);
            return new Point(points[i + 1].X - drv.X / 3, points[i + 1].Y - drv.Y / 3);
        }
        /// <summary>
        /// Returns scaled derivative of a point in a point collection.
        /// </summary>
        /// <param name="points">Points on the curve.</param>
        /// <param name="i">Point no to calculate control point for.</param>
        /// <param name="tension">Tension</param>        
        /// <returns></returns>
        public static Point GetBezierDerivative(PointCollection points, int i, double tension)
        {
            if (points.Count < 2)
                throw new System.ArgumentOutOfRangeException("points", "PointCollection must contain at least two points.");

            double x, y;
            if (i == 0)
            {
                // First point.
                x = (points[1].X - points[0].X) / tension;
                y = (points[1].Y - points[0].Y) / tension;
                return new Point(x, y);
            }
            if (i == points.Count - 1)
            {
                // Last point.
                x = (points[i].X - points[i - 1].X) / tension;
                y = (points[i].Y - points[i - 1].Y) / tension;
                return new Point(x, y);
            }
            x = (points[i + 1].X - points[i - 1].X) / tension;
            y = (points[i + 1].Y - points[i - 1].Y) / tension;

            return new Point(x, y);
        }

        #endregion
        /// <summary>
        /// Returns string representing Path.Data for a Bézier curve.
        /// This is intended for inspecting curves in Blend. Not used in the code.
        /// </summary>
        /// <returns>Path.Data that can be used in XAML.</returns>
        public static string GetBezierPathDataString(PointCollection points, double tension)
        {
            var bzPoints = GetBezierPoints(points, tension);
            var sbRet = new System.Text.StringBuilder();
            // M 0,0 C 1.66, 1.66 6.66,11.66 10,10 
            for (int i = 0; i < bzPoints.Count; i++)
            {
                if (i == 0)
                {
                    sbRet.AppendFormat("M {0},{1} ", bzPoints[i].X, bzPoints[i].Y);
                    continue;
                }
                if (i % 3 == 1)
                {
                    sbRet.Append("C ");
                }
                sbRet.AppendFormat("{0},{1} ", bzPoints[i].X, bzPoints[i].Y);
            }
            return sbRet.ToString();
        }

    }
    
}