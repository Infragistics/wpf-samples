using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Infragistics.Controls.Maps;
using Infragistics.Controls.Charts;
using System.Collections.Generic;

namespace IGExtensions.Common.Maps
{
    public class GeographicFlightPolylineSeries
        : GeographicShapeSeriesBase
    {
        public  GeographicFlightPolylineSeries()
        {
            this.DefaultStyleKey = typeof(GeographicFlightPolylineSeries);
        }

        protected override Series CreateSeries()
        {
            return new FlightPolylineSeries();
        }
    }

    public class FlightPolylineSeries
        : ShapeSeriesBase
    {
        /// <summary>
        /// FlightPolylineSeries constructor.
        /// </summary>
        public FlightPolylineSeries()
            : base()
        {
            this.DefaultStyleKey = typeof(FlightPolylineSeries);
        }
        /// <summary>
        /// Creates the view object for this series.
        /// </summary>
        /// <returns>A PolylineSeriesView.</returns>
        protected override SeriesView CreateView()
        {
            return new FlightPolylineSeriesView(this);
        }

        protected override void OnViewCreated(SeriesView view)
        {
            base.OnViewCreated(view);

            FlightView = (FlightPolylineSeriesView)view;
        }

        protected FlightPolylineSeriesView FlightView { get; set; }

        /// <summary>
        /// Boolean indicating whether shapes in this series should be closed polygons.
        /// </summary>
        protected override bool IsClosed
        {
            get
            {
                return false;
            }
        }

        protected override void  RenderSeriesOverride(bool animate)
        {
            FlightView.ClearGeometry();

 	        base.RenderSeriesOverride(animate);

            FlightView.UpdateGeometry();
        }
    }

    public class FlightPolylineSeriesView
        : ShapeSeriesViewBase
    {
        public  FlightPolylineSeriesView(FlightPolylineSeries model)
            : base(model)
        {
            FlightModel = model;
        }

        protected FlightPolylineSeries FlightModel { get; set; }

        private List<PathGeometry> _geometries = new List<PathGeometry>();

        protected override FrameworkElement GetShapeElement(int i, object item)
        {
            return null;
        }

        protected override void ApplyData(FrameworkElement element, PathGeometry data)
        {
            _geometries.Add(data);
        }

        protected override PathGeometry GetShapeGeometry(int i, List<PointCollection> points)
        {
            if (points == null)
            {
                return null;
            }

            PathGeometry pathGeometry = new PathGeometry();

            foreach (PointCollection ring in points)
            {
                if (ring.Count == 0) 
                {
                    continue;
                }
                PolyLineSegment polylineSegment = new PolyLineSegment()
                {
                    Points = ring
                };

                PathFigure pathFigure = new PathFigure();
                pathFigure.IsFilled = false;
                pathFigure.IsClosed = false;
                pathFigure.StartPoint = ring[0];
                pathFigure.Segments.Add(polylineSegment);

                pathGeometry.Figures.Add(pathFigure);

            }
            return pathGeometry;
        }
    
        internal void ClearGeometry()
        {
 	        _geometries.Clear();
        }

        private Path _mainPath = new Path();

        public override void AttachUI(Canvas rootCanvas)
        {
            base.AttachUI(rootCanvas);

            rootCanvas.Children.Add(_mainPath);
        }

        internal void UpdateGeometry()
        {
            _mainPath.Stroke = Model.ActualBrush;
            _mainPath.StrokeThickness = Model.Thickness;

            GeometryGroup gg = new GeometryGroup();
            foreach (var pathGeometry in _geometries)
            {
                gg.Children.Add(pathGeometry);
            }

            _mainPath.Data = gg;
        }
    }

}
