using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using IGGeographicMap.Samples.Custom;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Extensions
{
    public static class GeoMapExtensions
    {
        public static IEnumerable<DependencyObject> VisualDescendants(this DependencyObject current)
        {
            yield return current;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(current, i);
                foreach (DependencyObject sub in child.VisualDescendants())
                {
                    yield return sub;
                }
            }
        }

        #region CrosshairVisibility
        internal const string CrosshairVisibilityPropertyName = "CrosshairVisibility";

        public static readonly DependencyProperty CrosshairVisibilityProperty =
            DependencyProperty.RegisterAttached(CrosshairVisibilityPropertyName,
            typeof(GeographicMapCrosshairVisibility), typeof(GeoMapExtensions),
            new PropertyMetadata(null, (o, e) => OnGeoMapCrosshairsBehaviorChanged(o as XamGeographicMap,
                    e.OldValue as GeographicMapCrosshairVisibility,
                    e.NewValue as GeographicMapCrosshairVisibility)));

        public static GeographicMapCrosshairVisibility GetCrosshairVisibility(DependencyObject target)
        {
            return target.GetValue(CrosshairVisibilityProperty) as GeographicMapCrosshairVisibility;
        }
        public static void SetCrosshairVisibility(DependencyObject target, GeographicMapCrosshairVisibility behavior)
        {
            target.SetValue(CrosshairVisibilityProperty, behavior);
        }

        private static void OnGeoMapCrosshairsBehaviorChanged(XamGeographicMap geoMap, GeographicMapCrosshairVisibility oldValue, GeographicMapCrosshairVisibility newValue)
        {
            if (geoMap == null)
            {
                return;
            }
            if (oldValue != null)
            {
                oldValue.OnDetach(geoMap);
            }
            if (newValue != null)
            {
                newValue.PropertyChanged += new PropertyChangedEventHandler(newValue_PropertyChanged);
                newValue.OnAttach(geoMap);
            }
        }

        static void newValue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string name = e.PropertyName;
        } 

        #endregion

        #region MapLocatorSettings
        internal const string MapLocatorSettingsPropertyName = "MapLocatorSettings";

        public static readonly DependencyProperty MapLocatorSettingsProperty =
            DependencyProperty.RegisterAttached(MapLocatorSettingsPropertyName,
            typeof(GeoMapLocationPaneSettings), typeof(GeoMapExtensions),
            new PropertyMetadata(null, (o, e) => OnMapLocatorSettingsChanged(o as XamGeographicMap,
                    e.OldValue as GeoMapLocationPaneSettings,
                    e.NewValue as GeoMapLocationPaneSettings)));

        public static GeoMapLocationPaneSettings GetMapLocatorSettings(DependencyObject target)
        {
            return target.GetValue(MapLocatorSettingsProperty) as GeoMapLocationPaneSettings;
        }
        public static void SetMapLocatorSettings(DependencyObject target, GeoMapLocationPaneSettings behavior)
        {
            target.SetValue(MapLocatorSettingsProperty, behavior);
        }

        private static void OnMapLocatorSettingsChanged(XamGeographicMap geoMap, GeoMapLocationPaneSettings oldValue, GeoMapLocationPaneSettings newValue)
        {
            if (geoMap == null)
            {
                return;
            }
            if (oldValue != null)
            {
                oldValue.PropertyChanged -= OnMapLocatorSettingsPropertyChanged;
                oldValue.OnDetach(geoMap);
            }
            if (newValue != null)
            {
                newValue.PropertyChanged += OnMapLocatorSettingsPropertyChanged;
               // newValue.OnAttach(geoMap, newValue.GeoMapLocationPane);
                newValue.OnAttach(geoMap);
            }
        }

        static void OnMapLocatorSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string name = e.PropertyName;

        }

        #endregion
    }

    public class GeographicMapCrosshairVisibility : DependencyObject, INotifyPropertyChanged
    {
        public GeographicMapCrosshairVisibility()
        {
            _invisibleStyle = new Style(typeof(Line));
            _invisibleStyle.Setters.Add(new Setter(Shape.StrokeProperty, new SolidColorBrush(Colors.Transparent)));
        }

        private bool _first = true;
        //private bool _changed = false;
        private Line _vertical;
        private Line _horizontal;
        private readonly Style _invisibleStyle;
        private XamGeographicMap _geoMap;
        //public bool ShowHorizontalCrosshair { get; set; }

        #region ShowHorizontalCrosshair Dependency Property
        public const string ShowHorizontalCrosshairPropertyName = "ShowHorizontalCrosshair";

        /// <summary>
        /// Identifies the ShowHorizontalCrosshair dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowHorizontalCrosshairProperty =
            DependencyProperty.Register(ShowHorizontalCrosshairPropertyName, typeof(bool), typeof(GeographicMapCrosshairVisibility),
            new PropertyMetadata(true, (o, e) =>
            {
                (o as GeographicMapCrosshairVisibility).OnPropertyChanged(ShowHorizontalCrosshairPropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the maximum value for this scale.
        /// </summary>
        public bool ShowHorizontalCrosshair
        {
            get
            {
                return (bool)GetValue(ShowHorizontalCrosshairProperty);
            }
            set
            {
                SetValue(ShowHorizontalCrosshairProperty, value);
            }
        }
        #endregion
        #region ShowVerticalCrosshair Dependency Property
        public const string ShowVerticalCrosshairPropertyName = "ShowVerticalCrosshair";

        /// <summary>
        /// Identifies the ShowVerticalCrosshair dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowVerticalCrosshairProperty =
            DependencyProperty.Register(ShowVerticalCrosshairPropertyName, typeof(bool), typeof(GeographicMapCrosshairVisibility),
            new PropertyMetadata(true, (o, e) =>
            {
                (o as GeographicMapCrosshairVisibility).OnPropertyChanged(ShowVerticalCrosshairPropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the maximum value for this scale.
        /// </summary>
        public bool ShowVerticalCrosshair
        {
            get
            {
                return (bool)GetValue(ShowVerticalCrosshairProperty);
            }
            set
            {
                SetValue(ShowVerticalCrosshairProperty, value);
            }
        }
        #endregion
     
        #region INotifyPropertyChanged implementation
        /// <summary>
        /// Occurs when a property (including "effective" and non-dependency property) value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed and updated events.
        /// </summary>
        /// <param name="name">The name of the property being changed.</param>
        /// <param name="oldValue">The old value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        protected void OnPropertyChanged(string name, object oldValue, object newValue)
        {
            if (PropertyChanged != null)
            {
                if (name == ShowVerticalCrosshairPropertyName)
                {
                    ToggleVisibility(_geoMap, _vertical, this.ShowVerticalCrosshair);
                }
                if (name == ShowHorizontalCrosshairPropertyName)
                {
                    ToggleVisibility(_geoMap, _horizontal, this.ShowHorizontalCrosshair);
                }
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion


        public void OnAttach(XamGeographicMap geoMap)
        {
            _geoMap = geoMap;
            geoMap.MouseEnter += OngeoMapMouseEnter;
        }
        public void OnDetach(XamGeographicMap geoMap)
        {
            geoMap.MouseEnter -= OngeoMapMouseEnter;
            MakeVisible(geoMap, _vertical);
            MakeVisible(geoMap, _horizontal);
            _vertical = null;
            _horizontal = null;
            _first = true;
        }

        private void ToggleVisibility(XamGeographicMap geoMap, Line crosshairLine, bool isVisible)
        {
            if (isVisible) 
                MakeVisible(geoMap, crosshairLine); 
            else 
                MakeInvisible(geoMap, crosshairLine);
        }
        private void MakeInvisible(XamGeographicMap geoMap, Line crosshairLine)
        {
            crosshairLine.Style = _invisibleStyle;
        }
        private void MakeVisible(XamGeographicMap geoMap, Line crosshairLine)
        {
            if (geoMap == null || crosshairLine == null)
            {
                return;
            }
            crosshairLine.Style = geoMap.CrosshairLineStyle;
        }

        void OngeoMapMouseEnter(object sender, MouseEventArgs e)
        {
            var geoMap = sender as XamGeographicMap;

            //if ((_first || _changed) && geoMap != null)
            if (_first && geoMap != null)
            {
                _first = false;
                //_changed = false;
                var crosshairs =
                    from line in geoMap.VisualDescendants()
                    where line is Line && (line as Line).Style == geoMap.CrosshairLineStyle
                    select line as Line;
                List<Line> lines = crosshairs.ToList();

                if(lines.Count == 0 ) return;

                bool uninitialized = lines[0].X1 == 0.0 && lines[0].X2 == 0.0 &&
                                     lines[1].X1 == 0.0 && lines[1].X2 == 0.0;
                if (uninitialized)
                {
                    _first = true;
                    geoMap.MouseMove += OngeoMapMouseEnter;
                    return;
                }

                if (lines[0].X1 == lines[0].X2)
                {
                    _vertical = lines[0];
                    if (lines.Count >= 1) _horizontal = lines[1];
                }
                else
                {
                    if (lines.Count >= 1) _vertical = lines[1];
                    _horizontal = lines[0];
                }

                if (!this.ShowHorizontalCrosshair)
                {
                    MakeInvisible(geoMap, _horizontal);
                }
                if (!this.ShowVerticalCrosshair)
                {
                    MakeInvisible(geoMap, _vertical);
                }
                geoMap.MouseMove -= OngeoMapMouseEnter;
            }
        }
    }

    public class GeoMapLocationPaneSettings : DependencyObject, INotifyPropertyChanged
    {
        // Behavior GeographicMapLocationBehavior
        private XamGeographicMap _geoMap;

        public GeoMapLocationPaneSettings()
        {

        }
        #region Event Handlers
        //public void OnAttach(XamGeographicMap geoMap, GeoMapLocationPane mapLocator)
        //{
        //    this.GeoMapLocationPane = mapLocator;
        //    _geoMap = geoMap;
        //    _geoMap.MouseMove += OnGeoMapMouseMove;
        //}

        public void OnAttach(XamGeographicMap geoMap)
        {
            // this.GeoMapLocationPane = mapLocator;
            _geoMap = geoMap;
            _geoMap.MouseMove += OnGeoMapMouseMove;
        }

        public void OnDetach(XamGeographicMap geoMap)
        {
            this.GeoMapLocationPane = null;
            geoMap.MouseMove -= OnGeoMapMouseMove;

        }
        private void OnGeoMapMouseMove(object sender, MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(_geoMap);
            var geoLocation = GetGeoLocation(mousePosition);

            this.GeoMapLocationPane.Location = geoLocation;
            this.GeoMapLocationPane.Position = mousePosition;

        }
        #endregion
        #region Properties

        public const string GeoMapLocationPanePropertyName = "GeoMapLocationPane";
        /// <summary>
        /// Identifies the GeoMapLocationPane dependency property.
        /// </summary>
        public static readonly DependencyProperty GeoMapLocationPaneProperty =
            DependencyProperty.Register(GeoMapLocationPanePropertyName, typeof(GeoMapLocationPane), typeof(GeoMapLocationPaneSettings),
            new PropertyMetadata(null, (o, e) =>
            {
                (o as GeoMapLocationPaneSettings).OnPropertyChanged(GeoMapLocationPanePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the GeoMapLocationPane property
        /// </summary>
        public GeoMapLocationPane GeoMapLocationPane
        {
            get { return (GeoMapLocationPane)GetValue(GeoMapLocationPaneProperty); }
            set { SetValue(GeoMapLocationPaneProperty, value); }
        }
        #endregion

        private GeoLocation GetGeoLocation(Point mousePosition)
        {
            var xAxis = _geoMap.XAxis;
            var yAxis = _geoMap.YAxis;

            var viewport = new Rect(0, 0, _geoMap.ActualWidth, _geoMap.ActualHeight);
            var window = _geoMap.WindowRect;

            bool isInverted = xAxis.IsInverted;
            var param = new ScalerParams(window, viewport, isInverted);
            param.EffectiveViewportRect = _geoMap.EffectiveViewport;
            var longitude = xAxis.GetUnscaledValue(mousePosition.X, param);

            isInverted = yAxis.IsInverted;
            param = new ScalerParams(window, viewport, isInverted);
            var latitude = yAxis.GetUnscaledValue(mousePosition.Y, param);

            return new GeoLocation(longitude, latitude);
        }

        #region INotifyPropertyChanged implementation
        /// <summary>
        /// Occurs when a property (including "effective" and non-dependency property) value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed and updated events.
        /// </summary>
        /// <param name="name">The name of the property being changed.</param>
        /// <param name="oldValue">The old value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        protected void OnPropertyChanged(string name, object oldValue, object newValue)
        {

            if (name == GeoMapLocationPanePropertyName)
            {
                this.GeoMapLocationPane = newValue as GeoMapLocationPane;
                // ToggleVisibility(_geoMap, _vertical, this.ShowVerticalCrosshair);
            }
            if (PropertyChanged != null)
            {

                //if (name == ShowHorizontalCrosshairPropertyName)
                //{
                //    ToggleVisibility(_geoMap, _horizontal, this.ShowHorizontalCrosshair);
                //}
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

    }

 
}