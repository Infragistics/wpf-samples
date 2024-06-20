using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Infragistics;
using Infragistics.Controls.Maps;
using IGMap.Resources;

namespace IGMap.Samples.Navigation
{
    public partial class MapProcess : Infragistics.Samples.Framework.SampleContainer
    {
        private bool initialLayerImported;
        private string _shapefilePath;
        private bool _transitionPlaying;

        public MapProcess()
        {
            InitializeComponent();
            this.Loaded += SampleContainerLoaded;
        }

        private void SampleContainerLoaded(object sender, RoutedEventArgs e)
        {
          
        }


        private void xamMap_Loaded(object sender, RoutedEventArgs e)
        {
            xamMap.WindowRect = xamMap.WorldRect
                                = GetDefaultWindowRect();

            // Synchronize all layers
            xamMap.Layers[0].WorldRect = xamMap.WorldRect;
        }

        private Rect GetDefaultWindowRect()
        {
            return GetRectForLatitudeLongitude(-144, 80, 144, -80);
        }

        /// <summary>
        /// Gets the Rectangle for latitude longitude.
        /// </summary>
        /// <param name="topLeftX">The top left X.</param>
        /// <param name="topLeftY">The top left Y.</param>
        /// <param name="bottomRightX">The bottom right X.</param>
        /// <param name="bottomRightY">The bottom right Y.</param>
        /// <returns></returns>
        private Rect GetRectForLatitudeLongitude(double topLeftX, double topLeftY, double bottomRightX,
                                                 double bottomRightY)
        {
            Point winTopLeft = ProjectMapPoint(topLeftX, topLeftY);
            Point winBottomRight = ProjectMapPoint(bottomRightX, bottomRightY);

            return new Rect(System.Math.Min(winTopLeft.X, winBottomRight.X),
                            System.Math.Min(winTopLeft.Y, winBottomRight.Y),
                            System.Math.Abs(winTopLeft.X - winBottomRight.X),
                            System.Math.Abs(winTopLeft.Y - winBottomRight.Y));
        }

        /// <summary>
        /// Projects geo map point to the map.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        /// <returns></returns>
        private Point ProjectMapPoint(double longitude, double latitude)
        {
            return xamMap.MapProjection.ProjectToMap(new Point(longitude, latitude));
        }

        /// <summary>
        /// Handles the ElementClick event of the xamMap control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Controls.Maps.MapElementClickEventArgs"/> instance containing the event data.</param>
        private void xamMap_ElementClick(object sender, MapElementClickEventArgs e)
        {
            if (_transitionPlaying) return;

            string layerName = Convert.ToString(e.Element.GetProperty("PointTo"));
            if (!string.IsNullOrEmpty(layerName))
            {
                var layers = xamMap.Layers.FindName(layerName);
                if (layers != null && layers.Count() > 0)
                {
                    MapLayer newLayer = layers.First();
                    MapLayer oldLayer = e.Element.Layer;

                    _transitionPlaying = true;

                    double dL, dR, dB, dT;

                    // determine the change we want to see in the window rect
                    if (oldLayer.Name == "Main")
                    {
                        // target window = e.Element.WorldRect (zoom in)
                        dL = e.Element.WorldRect.Left - xamMap.WindowRect.Left;
                        dR = e.Element.WorldRect.Right - xamMap.WindowRect.Right;
                        dB = e.Element.WorldRect.Bottom - xamMap.WindowRect.Bottom;
                        dT = e.Element.WorldRect.Top - xamMap.WindowRect.Top;
                    }
                    else
                    {
                        // target window = double the current window (zoom out)
                        Point center = xamMap.WindowRect.GetCenter();
                        dL = xamMap.WindowRect.Left + (xamMap.WindowRect.Left - center.X);
                        dR = xamMap.WindowRect.Right + (xamMap.WindowRect.Right - center.X);
                        dB = xamMap.WindowRect.Top + (xamMap.WindowRect.Top - center.Y);
                        dT = xamMap.WindowRect.Bottom + (xamMap.WindowRect.Bottom - center.Y);
                    }

                    // augment the difference for better effect
                    dL *= 1.5;
                    dR *= 1.5;
                    dB *= 1.5;
                    dT *= 1.5;

                    double t1 = 0.025;
                    double t2 = 1.0;

                    // gradually grow/shrink the window rect
                    DispatcherTimer timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(t1)};
                    timer.Tick += (ob, ev) =>
                                      {
                                          xamMap.WindowRect = new Rect(new Point(xamMap.WindowRect.Left + (t1/t2)*dL,
                                                                                 xamMap.WindowRect.Bottom + (t1/t2)*dB),
                                                                       new Point(xamMap.WindowRect.Right + (t1/t2)*dR,
                                                                                 xamMap.WindowRect.Top + (t1/t2)*dT)
                                              );
                                      };

                    // set window rect to its final value; set layer visibilities
                    DispatcherTimer timer2 = new DispatcherTimer {Interval = TimeSpan.FromSeconds(t2)};
                    timer2.Tick += (ob2, ev2) =>
                                       {
                                           timer.Stop();
                                           timer2.Stop();

                                           oldLayer.IsVisible = false;
                                           xamMap.WindowRect = GetDefaultWindowRect();
                                           newLayer.IsVisible = true;
                                           _transitionPlaying = false;
                                       };


                    timer.Start();
                    timer2.Start();
                }
            }
        }

        private void Layer_Imported(object sender, MapLayerImportEventArgs e)
        {
            if (e.Action == MapLayerImportAction.End)
            {
                var layer = (MapLayer) sender;

                if (!initialLayerImported)
                {
                    if (layer.Reader != null && layer.Reader is ShapeFileReader)
                    {
                        string path = ((ShapeFileReader) (layer.Reader)).Uri;
                        //if (path.IndexOf('\\') >= 0)
                        //{
                        //    _shapefilePath = path.Substring(0, path.LastIndexOf('\\') + 1);
                        //}
                        if (path.IndexOf("/") >= 0)
                        {
                            _shapefilePath = path.Substring(0, path.LastIndexOf("/") + 1);
                        }
                        else
                        {
                            _shapefilePath = "";
                        }

                        initialLayerImported = true;
                    }
                }

                foreach (MapElement element in layer.Elements)
                {
                    StyleElement(element);
                    string pointTo = Convert.ToString(element.GetProperty("PointTo"));
                    PropertyInfo pinfo = typeof (MapStrings).GetProperty(element.Name);
                    if (pinfo != null)
                    {
                        string caption = pinfo.GetValue(null, null) as string;
                        if (caption != null)
                        {
                            element.Caption = caption;
                        }
                    }
                    if (!string.IsNullOrEmpty(pointTo))
                    {
                        var layers = xamMap.Layers.FindName(pointTo);
                        if (layers == null || layers.Count() == 0)
                        {
                            MapLayer newLayer = new MapLayer();
                            newLayer.Name = pointTo;
                            ShapeFileReader reader = new ShapeFileReader();
                            reader.Uri = _shapefilePath + pointTo;
                            reader.DataMapping =
                                (DataMapping)
                                ((new DataMapping.Converter()).ConvertFrom(
                                    "Name=CAPTION; PointTo=POINTTO; Type=TYPE; Description=DESC"));
                            newLayer.Reader = reader;

                            newLayer.IsVisible = false;
                            newLayer.Imported += Layer_Imported;
                            xamMap.Layers.Add(newLayer);
                            newLayer.ImportAsync();
                        }
                    }
                }
            }
        }

        private static void StyleElement(MapElement element)
        {
            string type = Convert.ToString(element.GetProperty("Type"));
            if (type == "StartEnd")
            {
                StyleStartEndElement(element);
            }
            else if (type == "Arrow")
            {
                StyleArrowElement(element);
            }
            else if (type == "Process")
            {
                StyleProcessElement(element);
            }
            else if (type == "Decision")
            {
                StyleDecisionElement(element);
            }
        }

        private static void StyleStartEndElement(MapElement element)
        {
            Color blueColor = Color.FromArgb(255, 24, 81, 112);
            element.Fill = new SolidColorBrush(blueColor);
        }

        private static void StyleArrowElement(MapElement element)
        {
           Color greenColor = Color.FromArgb(255, 135, 153, 34);
            element.Fill = new SolidColorBrush(greenColor);
            element.IsSensitive = false;
        }

        private static void StyleProcessElement(MapElement element)
        {
            Color redColor = Color.FromArgb(255, 198, 45, 54);
            element.Fill = new SolidColorBrush(redColor);
        }

        private static void StyleDecisionElement(MapElement element)
        {
            Color orangeColor = Color.FromArgb(255, 227, 114, 12);
            element.Fill = new SolidColorBrush(orangeColor);
        }

        /// <summary>
        /// Gets the elements rectangle.
        /// </summary>
        /// <param name="elements">The elements.</param>
        /// <returns></returns>
        private Rect GetElementsRectangle(IEnumerable<MapElement> elements)
        {
            var result = new Rect();
            foreach (MapElement element in elements)
            {
                result.Union(element.WorldRect);
            }
            double increaseX = System.Math.Abs(result.X*0.1);
            double increaseY = System.Math.Abs(result.Y*0.1);
            return new Rect(result.X - increaseX, result.Y - increaseY, result.Width + increaseX*2,
                            result.Height + increaseY*2);
        }
    }
}