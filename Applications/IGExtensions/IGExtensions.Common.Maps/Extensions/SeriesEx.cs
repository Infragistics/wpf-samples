using System.Linq;
using System.Windows;
using Infragistics.Controls.Charts;

namespace Infragistics.Controls.Charts
{
    public static class SeriesChartEx
    {
        public static Point GetSeriesValuePoint(this ScatterBase series, Point mousePoint)
        {
            var x = series.XAxis.UnscaleValue(mousePoint.X);
            var y = series.YAxis.UnscaleValue(mousePoint.Y);
            return new Point(x, y);
        }
        public static Point GetSeriesValuePoint(this HorizontalAnchoredCategorySeries series, Point mousePoint)
        {
            //var mousePoint = GetSeriesMousePoint(series, crossingPoint);

            var x = series.XAxis.UnscaleValue(mousePoint.X);
            var y = series.YAxis.UnscaleValue(mousePoint.Y);
            return new Point(x, y);
        }
    }
}

namespace Infragistics.Controls.Maps
{
    public static class SeriesMapEx
    {
        public static T FindByName<T>(this SeriesCollection series, string seriesName) where T : Series
        {
            return series.OfType<T>().Where(s => s.Name == seriesName).FirstOrDefault();
        }
        public static T FindByTitle<T>(this SeriesCollection series, string seriesTitle) where T : Series
        {
            return series.OfType<T>().Where(s => s.Title is string && (string)s.Title == seriesTitle).FirstOrDefault();
        }

        public static int LastIndexOf<T>(this SeriesCollection series) where T : Series
        {
            var index = -1;
            for (var i = 0; i < series.Count; i++)
            {
                if (series[i].GetType() == typeof(T))
                {
                    index = i;
                }
            }
            return index;
        }

        public static void OverlayOn<T>(this SeriesCollection series, Series item) where T : Series
        {
            if (series.Count == 0)
            {
                series.Add(item);
                return;
            }
              
            //var indexTile = series.LastIndexOf<GeographicTileSeries>();
            //var indexHd = series.LastIndexOf<GeographicHighDensityScatterSeries>();

            var index = series.LastIndexOf<GeographicTileSeries>();
            if (index < 0)
            {
                series.Insert(0, item);
            }
            else if (index == series.Count - 1)
            {
                series.Add(item);

            }
            else if (index < series.Count - 1)
            {
                //index = series.Count - 1;
                series.Insert(index, item);
            }
            else
            {
                series.Add(item);
            }

        }

        public static GeographicShapeSeries Duplicate(this GeographicShapeSeries series)
        {
            var clone = new GeographicShapeSeries();
            clone.Opacity = series.Opacity;
            clone.ItemsSource = series.ItemsSource;
            //clone.Resolution = series.Resolution;
           // clone.Visibility = series.Visibility;
           // clone.ToolTip = series.ToolTip;

            clone.ShapeMemberPath = series.ShapeMemberPath;
          //  clone.ShapeStyle = series.ShapeStyle;
            
            return clone;
        }
        public static GeographicProportionalSymbolSeries Duplicate(this GeographicProportionalSymbolSeries series)
        {
            var clone = new GeographicProportionalSymbolSeries();
            clone.Opacity = series.Opacity;
            clone.ItemsSource = series.ItemsSource;
            clone.Resolution = series.Resolution;
            clone.Visibility = series.Visibility;
            clone.ToolTip = series.ToolTip;

            clone.MaximumMarkers = series.MaximumMarkers;
            //clone.MarkerCollisionAvoidance = series.MarkerCollisionAvoidance;
            clone.MarkerType = series.MarkerType;
            clone.LongitudeMemberPath = series.LongitudeMemberPath;
            clone.LatitudeMemberPath = series.LatitudeMemberPath;
            clone.RadiusMemberPath = series.RadiusMemberPath;
            clone.RadiusScale = series.RadiusScale;
            clone.FillMemberPath = series.FillMemberPath;
            clone.FillScale = series.FillScale;
          
            return clone;
        }
    }
}