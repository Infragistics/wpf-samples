using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;

namespace IGMap.Samples.Navigation
{
    public partial class MapLayerVisibility : Infragistics.Samples.Framework.SampleContainer
    {
        public MapLayerVisibility()
        {
            InitializeComponent();
            this.loaderMessage.Visibility = Visibility.Visible;
        }

        private int totalLayers;
        private int loadedLayers;
        private double totalProgress;

        private void OnMapLayerImported(object sender, MapLayerImportEventArgs e)
        {
            totalLayers = this.shapeMap.Layers.Count;
            if (e.Action == MapLayerImportAction.End)
            {
                loadedLayers += 1;
                totalProgress = (double)loadedLayers / totalLayers * 100;
                UpdatePorgressBar();

                #region Remove Duplicate Labels

                MapLayer layer = sender as MapLayer;
                MapElementCollection elements = new MapElementCollection();

                foreach (MapElement element in layer.Elements)
                {
                    SurfaceElement surface = element as SurfaceElement;
                    if (surface != null && surface.Polylines.Count > 1)
                    {
                        Rect largestBounds = new Rect(0, 0, 0, 0);
                        int largestIndex = -1;
                        for (int i = 0; i < surface.Polylines.Count; i++)
                        {
                            Rect currentBounds = this.GetBounds(surface.Polylines[i]);
                            if (currentBounds.Height * currentBounds.Width > largestBounds.Height * largestBounds.Width)
                            {
                                largestIndex = i;
                                largestBounds = currentBounds;
                            }
                        }
                        MapPolylineCollection polylinesToRemove = new MapPolylineCollection();
                        for (int i = 0; i < surface.Polylines.Count; i++)
                        {
                            if (i != largestIndex)
                            {
                                polylinesToRemove.Add(surface.Polylines[i]);
                            }
                        }
                        SurfaceElement unlabeledElement = new SurfaceElement();
                        foreach (MapPolyline pL in polylinesToRemove)
                        {
                            surface.Polylines.Remove(pL);
                        }
                        unlabeledElement.Polylines = polylinesToRemove;
                        unlabeledElement.WorldRect = surface.WorldRect;
                        unlabeledElement.Fill = surface.ActualFill;

                        elements.Add(unlabeledElement);
                    }
                }
                foreach (MapElement element in elements)
                {
                    layer.Elements.Add(element);
                }

                #endregion
            }
        }

        private void UpdatePorgressBar()
        {
            loaderProgress.Value = totalProgress;
            txtProgress.Text = String.Format("... {0:0.00} %", totalProgress);
            if (loadedLayers == totalLayers)
            {
                ((Storyboard)this.Resources["sbFadeOutLoaderMessage"]).Begin();
                this.ZoomToLocation(new GeoLocation { Name = "Washington", Latitude = 38.8, Longitude = -77.0 });
            }
        }

        private void ZoomToLocation(GeoLocation location)
        {
            int rectBound = 10;
            Rect currentWindowRect = this.shapeMap.WindowRect;
            Rect geoRect = this.GetGeoLocationRect(location.Longitude - rectBound,
                                                   location.Latitude + rectBound,
                                                   location.Longitude + rectBound,
                                                   location.Latitude - rectBound);

            currentWindowRect.Union(geoRect);
            this.shapeMap.WindowRect = currentWindowRect;
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.0) };
            timer.Tick += (o, e) =>
                              {
                                  this.shapeMap.WindowRect = geoRect;
                                  ((DispatcherTimer)o).Stop();
                              };
            timer.Start();
        }

        private Rect GetGeoLocationRect(double topLeftX, double topLeftY, double bottomRightX, double bottomRightY)
        {
            Point winTopLeft = this.ProjectMapPoint(topLeftX, topLeftY);
            Point winBottomRight = this.ProjectMapPoint(bottomRightX, bottomRightY);

            return new Rect(System.Math.Min(winTopLeft.X, winBottomRight.X),
                            System.Math.Min(winTopLeft.Y, winBottomRight.Y),
                            System.Math.Abs(winTopLeft.X - winBottomRight.X),
                            System.Math.Abs(winTopLeft.Y - winBottomRight.Y));
        }

        // Convert Geodetic to Cartesian coordinates
        private Point ProjectMapPoint(double longitude, double latitude)
        {
            return this.shapeMap.MapProjection.ProjectToMap(new Point(longitude, latitude));
        }

        private Rect GetBounds(IEnumerable<Point> points)
        {
            double xmin = Double.PositiveInfinity;
            double ymin = Double.PositiveInfinity;
            double xmax = Double.NegativeInfinity;
            double ymax = Double.NegativeInfinity;

            foreach (Point point in points)
            {
                xmin = System.Math.Min(xmin, point.X);
                ymin = System.Math.Min(ymin, point.Y);

                xmax = System.Math.Max(xmax, point.X);
                ymax = System.Math.Max(ymax, point.Y);
            }

            if (Double.IsInfinity(xmin) || Double.IsInfinity(ymin) || Double.IsInfinity(ymin) || Double.IsInfinity(ymax))
            {
                return Rect.Empty;
            }

            return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
        }

        private void OnWindowRectChanged(object sender, MapWindowRectChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("WindowScale is " + this.shapeMap.WindowScale);

            if (this.shapeMap.WindowScale >= 400)
            {
                if (this.shapeMap.MapTileSource == null)
                {
                    System.Diagnostics.Debug.WriteLine("Changed MapTileSource at WindowScale " + this.shapeMap.WindowScale);
                    this.shapeMap.MapTileSource = new OpenStreetMapTileSource();
                }
            }
            else
            {
                if (this.shapeMap.MapTileSource != null) this.shapeMap.MapTileSource = null;
            }

        }


    }
}