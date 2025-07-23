using System;
using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Maps;
using IGNetworkNode.Resources;

namespace IGNetworkNode.Samples.Editing
{
    public partial class Tooltips : Infragistics.Samples.Framework.SampleContainer
    {
        public Tooltips()
        {
            InitializeComponent();
        }

        private void XamMap_Loaded(object sender, RoutedEventArgs e)
        {
            XamMap map = sender as XamMap;

            if (map == null)
            {
                return;
            }

            if (!map.WorldRect.IsEmpty)
            {
                return; // map already initialized
            }

            #region initialize map
            
            Point mapLocation = new Point(-74, 40); // coords for New York City

            // define world dimensions 
            Point worldTopLeft = new Point(mapLocation.X - 30.0, mapLocation.Y + 20.0);
            Point worldBottomRight = new Point(mapLocation.X + 30.0, mapLocation.Y - 30.0);

            // convert geodetic to cartesian coordinates
            Point winTopLeft = map.MapProjection.ProjectToMap(worldTopLeft);
            Point winBottomRight = map.MapProjection.ProjectToMap(worldBottomRight);

            // define the map control's window rect & world rect
            Rect winRect = new Rect()
            {
                X = System.Math.Min(winTopLeft.X, winBottomRight.X),
                Y = System.Math.Min(winTopLeft.Y, winBottomRight.Y),
                Width = System.Math.Abs(winTopLeft.X - winBottomRight.X),
                Height = System.Math.Abs(winTopLeft.Y - winBottomRight.Y)
            };
            map.IsAutoWorldRect = false;
            map.WindowZoomMaximum = 80;

            // set the map control's window rect & world rect
            map.WorldRect = winRect;
            map.WindowRect = winRect;
            #endregion

            #region add marker to map
            {
                MapLayer symbolLayer = map.Layers[0];

                // Get worldLocation using a projection from Cartesian to Geodetic coordinates 
                Point worldLocation = mapLocation;

                String elemCaption = Environment.NewLine + Environment.NewLine +
                         Environment.NewLine + Environment.NewLine +
                         String.Format(NetworkNodeStrings.Tooltips_Long + " {0:0.00}", worldLocation.X) +
                         Environment.NewLine +
                         String.Format(NetworkNodeStrings.Tooltips_Lat + " {0:0.00}", worldLocation.Y);

                Point elemLocation = map.MapProjection.ProjectToMap(worldLocation);

                // Create Symbol Element
                SymbolElement marker = new SymbolElement()
                {
                    Name = "Location",
                    Caption = elemCaption,
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Colors.Blue),
                    Stroke = new SolidColorBrush(Colors.Blue),
                    SymbolOrigin = elemLocation,
                    SymbolType = MapSymbolType.Pentagram,
                    SymbolSize = 4
                };

                // Add the Symbol Element to the map control
                symbolLayer.WorldRect = map.WorldRect;
                symbolLayer.Elements.Add(marker);
            }
            #endregion
        }
    }
}
