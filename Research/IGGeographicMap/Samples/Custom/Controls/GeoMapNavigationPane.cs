using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Extensions;

namespace IGGeographicMap.Samples.Custom
{
    /// <summary>
    /// Represents navigation pane that provides a set of interactive controls for navigating or panning the <see cref="XamGeographicMap"/> control.  
    /// Panning functionality is tied closely to the location of the canvas's view center.
    /// </summary>
    [TemplatePart(Name = GeoMapNavigationPane.HorizontalRootName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = GeoMapNavigationPane.VerticalRootName, Type = typeof(FrameworkElement))]
    public class GeoMapNavigationPane : GeoMapPane
    {
        public GeoMapNavigationPane()
        {
            this.MapPaneOrientation = Orientation.Vertical;
            
            DefaultStyleKey = typeof(GeoMapNavigationPane);

            this.IsMovable = true;
        }
         
        protected const double ZoombarMinScale = 100;       // 100 percent
        protected const double ZoombarMaxScale = 10000;     // actual value must not be greater than 100.0 / GeoMapAdapter.MapWindowMinZoom
      
        #region Template Parts
        /// <summary>
        /// Sets or gets the root FrameworkElement for horizontal layout.
        /// </summary>
        public FrameworkElement HorizontalRoot
        {
            get { return _horizontalRoot; }
            private set
            {
                _horizontalRoot = value;
                UpdateParts();
            }
        }
        private const string HorizontalRootName = "HorizontalRoot";
        private FrameworkElement _horizontalRoot;

        /// <summary>
        /// Sets or gets the root FrameworkElement for vertical layout.
        /// </summary>
        public FrameworkElement VerticalRoot
        {
            get { return _verticalRoot; }
            private set
            {
                _verticalRoot = value;
                UpdateParts();
            }
        }
        private const string VerticalRootName = "VerticalRoot";
        private FrameworkElement _verticalRoot;

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call ApplyTemplate.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            HorizontalRoot = GetTemplateChild(HorizontalRootName) as FrameworkElement;
            VerticalRoot = GetTemplateChild(VerticalRootName) as FrameworkElement;

            UpdateParts();
        }
        #endregion

        #region Non-Template Parts
        private void UpdateParts()
        {
            var root = this.MapPaneOrientation == Orientation.Horizontal ? HorizontalRoot : VerticalRoot;

            string startName = this.MapPaneOrientation == Orientation.Horizontal ? "Horizontal" : "Vertical";

            PanWestButton = root != null ? root.FindName(startName + PanWestButtonName) as ButtonBase : null;
            PanSouthButton = root != null ? root.FindName(startName + PanSouthButtonName) as ButtonBase : null;
            PanEastButton = root != null ? root.FindName(startName + PanEastButtonName) as ButtonBase : null;
            PanNorthButton = root != null ? root.FindName(startName + PanNorthButtonName) as ButtonBase : null;

            BackButton = root != null ? root.FindName(startName + BackButtonName) as ButtonBase : null;
            ForwardButton = root != null ? root.FindName(startName + ForwardButtonName) as ButtonBase : null;

            FitButton = root != null ? root.FindName(startName + FitButtonName) as ButtonBase : null;
            ZoomInButton = root != null ? root.FindName(startName + ZoomInButtonName) as ButtonBase : null;
            ZoomOutButton = root != null ? root.FindName(startName + ZoomOutButtonName) as ButtonBase : null;

            ZoomRange = root != null ? root.FindName(startName + ZoomRangeName) as RangeBase : null;

            if (HorizontalRoot != null)
            {
                HorizontalRoot.Visibility = this.MapPaneOrientation == Orientation.Horizontal ? Visibility.Visible : Visibility.Collapsed;
            }

            if (VerticalRoot != null)
            {
                VerticalRoot.Visibility = this.MapPaneOrientation == Orientation.Vertical ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        
        /// <summary>
        /// XamMap component pan west button, usually but not necessarily a RepeatButton.
        /// </summary>
        public ButtonBase PanWestButton
        {
            get { return panWestButton; }
            private set
            {
                if (panWestButton != null)
                {
                    panWestButton.Click -= OnNavigationButtonClick;
                }

                panWestButton = value;

                if (panWestButton != null)
                {
                    panWestButton.Click += OnNavigationButtonClick;
                }
            }
        }
        private const string PanWestButtonName = "PanWestButton";
        private ButtonBase panWestButton;

        /// <summary>
        /// XamMap component pan south button, usually but not necessarily a RepeatButton.
        /// </summary>
        public ButtonBase PanSouthButton
        {
            get { return _panSouthButton; }
            private set
            {
                if (_panSouthButton != null)
                {
                    _panSouthButton.Click -= OnNavigationButtonClick;
                }

                _panSouthButton = value;

                if (_panSouthButton != null)
                {
                    _panSouthButton.Click += OnNavigationButtonClick;
                }
            }
        }
        private const string PanSouthButtonName = "PanSouthButton";
        ButtonBase _panSouthButton;

        /// <summary>
        /// XamMap component pan east button, usually but not necessarily a RepeatButton.
        /// </summary>
        public ButtonBase PanEastButton
        {
            get { return _panEastButton; }
            private set
            {
                if (_panEastButton != null)
                {
                    _panEastButton.Click -= OnNavigationButtonClick;
                }

                _panEastButton = value;

                if (_panEastButton != null)
                {
                    _panEastButton.Click += OnNavigationButtonClick;
                }
            }
        }
        private const string PanEastButtonName = "PanEastButton";
        private ButtonBase _panEastButton;

        /// <summary>
        /// XamMap component pan north button, usually but not necessarily a RepeatButton.
        /// </summary>
        public ButtonBase PanNorthButton
        {
            get { return _panNorthButton; }
            private set
            {
                if (_panNorthButton != null)
                {
                    _panNorthButton.Click -= OnNavigationButtonClick;
                }

                _panNorthButton = value;

                if (_panNorthButton != null)
                {
                    _panNorthButton.Click += OnNavigationButtonClick;
                }
            }
        }
        private const string PanNorthButtonName = "PanNorthButton";
        private ButtonBase _panNorthButton;

        /// <summary>
        /// XamMap component window back button, usually but not necessarily a Button.
        /// </summary>
        public ButtonBase BackButton
        {
            get { return _backButton; }
            private set
            {
                if (_backButton != null)
                {
                    _backButton.Click -= OnNavigationButtonClick;
                }

                _backButton = value;

                if (_backButton != null)
                {
                    _backButton.Click += OnNavigationButtonClick;
                }
            }
        }
        private const string BackButtonName = "BackButton";
        private ButtonBase _backButton;

        /// <summary>
        /// XamMap component window back button, usually but not necessarily a Button.
        /// </summary>
        public ButtonBase ForwardButton
        {
            get { return _forwardButton; }
            private set
            {
                if (_forwardButton != null)
                {
                    _forwardButton.Click -= OnNavigationButtonClick;
                }

                _forwardButton = value;

                if (_forwardButton != null)
                {
                    _forwardButton.Click += OnNavigationButtonClick;
                }
            }
        }
        private const string ForwardButtonName = "ForwardButton";
        private ButtonBase _forwardButton;

        /// <summary>
        /// Sets or gets the zoom in button, usually but not necessarily a RepeatButton.
        /// </summary>
        public ButtonBase ZoomInButton
        {
            get { return _zoomInButton; }
            private set
            {
                if (_zoomInButton != null)
                {
                    _zoomInButton.Click -= OnNavigationButtonClick;
                }

                _zoomInButton = value;

                if (_zoomInButton != null)
                {
                    _zoomInButton.Click += OnNavigationButtonClick;
                }
            }
        }
        private const string ZoomInButtonName = "ZoomInButton";
        private ButtonBase _zoomInButton;

        /// <summary>
        /// Sets or gets the zoom out button, usually but not necessarily a RepeatButton.
        /// </summary>
        public ButtonBase ZoomOutButton
        {
            get { return _zoomOutButton; }
            private set
            {
                if (_zoomOutButton != null)
                {
                    _zoomOutButton.Click -= OnNavigationButtonClick;
                }

                _zoomOutButton = value;

                if (_zoomOutButton != null)
                {
                    _zoomOutButton.Click += OnNavigationButtonClick;
                }
            }
        }
        private const string ZoomOutButtonName = "ZoomOutButton";
        private ButtonBase _zoomOutButton;

        /// <summary>
        /// Sets or gets the zoom fit button, usually but not necessarily a Button.
        /// </summary>
        public ButtonBase FitButton
        {
            get { return _fitButton; }
            private set
            {
                if (_fitButton != null)
                {
                    _fitButton.Click -= OnNavigationButtonClick;
                }

                _fitButton = value;

                if (_fitButton != null)
                {
                    _fitButton.Click += OnNavigationButtonClick;
                }
            }
        }
        private const string FitButtonName = "FitButton";
        private ButtonBase _fitButton;

        /// <summary>
        /// Sets or gets the zoom RangeBase control, usually but not necessarily a Slider.
        /// </summary>
        public RangeBase ZoomRange
        {
            get { return _zoomRange; }
            private set
            {
                if (_zoomRange != null)
                {
                    _zoomRange.ValueChanged -= OnZoomRangeValueChanged;
                }

                _zoomRange = value;
                if (_zoomRange != null)
                {
                    _zoomRange.ValueChanged += OnZoomRangeValueChanged;
                    Update();
                }
            }
        }
        private const string ZoomRangeName = "ZoomRange";
        private RangeBase _zoomRange;
        #endregion
        
        #region Properties
       
        public const string ZoomLevelDisplayTextPropertyName = "ZoomLevelDisplayText";
        /// <summary>
        /// Identifies the ZoomLevelDisplayText dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoomLevelDisplayTextProperty =
            DependencyProperty.Register(ZoomLevelDisplayTextPropertyName, typeof(string), typeof(GeoMapNavigationPane),
             new PropertyMetadata("100%", (sender, e) =>
             {
                 (sender as GeoMapNavigationPane).OnPropertyChanged(new PropertyChangedEventArgs(ZoomLevelDisplayTextPropertyName));
             })); 
        /// <summary>
        /// Gets or sets display mode of theZoomLevelDisplayText
        /// </summary>
        public string ZoomLevelDisplayText
        {
            get { return (string)GetValue(ZoomLevelDisplayTextProperty); }
            set { SetValue(ZoomLevelDisplayTextProperty, value); }
        }
        #endregion
        
        public override void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
             // if (this.IsUpdating) return;

            switch (ea.PropertyName)
            {
                case GeoMapPane.GeographicMapPropertyName:
                    {
                        if (this.GeographicMap != null)
                            this.GeographicMap.WindowRectChanged += OnGeographicMapWindowRectChanged;

                        break;
                    }
                case GeoMapPane.MapPaneOrientationPropertyName:
                    {
                        UpdateParts();
                        break;
                        
                    }
            }
            base.OnPropertyChanged(ea);

        }

        private void OnGeographicMapWindowRectChanged(object sender, Infragistics.RectChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("WindowRectChanged: " + e.NewRect.FormatToString("0.00000"));
            Update();
        }
        private void OnZoomRangeValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.GeographicMap == null || _ignoreRangeValueChanged) return;

            if (sender == this.ZoomRange && Math.Round(e.OldValue) != Math.Round(e.NewValue))
            {
                Rect window = this.GeographicMap.WindowRect;
                Point windowCenter = this.GeographicMap.GetWindowCenterPoint();     // provided by GeoMapAdapter
                var zoombarScaleRatio = e.NewValue / 100.0;
                var windowZoomScale = 1 / zoombarScaleRatio;
                var windowOffset = windowZoomScale / 2;

                window.Width = System.Math.Max(GeoMapAdapter.MapWindowMinZoom, windowZoomScale);
                window.Height = System.Math.Max(GeoMapAdapter.MapWindowMinZoom, windowZoomScale);
                window.X = System.Math.Max(GeoMapAdapter.MapWindowMinPosition, windowCenter.X - windowOffset);
                window.Y = System.Math.Max(GeoMapAdapter.MapWindowMinPosition, windowCenter.Y - windowOffset);

                this.ZoomLevelDisplayText = (e.NewValue).ToString() + "%";
                this.GeographicMap.NavigateTo(window);      // provided by GeoMapAdapter
            }
        }
        private void OnNavigationButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.GeographicMap == null) return;

            // Note: navigation methods are provided by the GeoMapAdapter
            if (sender == PanNorthButton) { this.GeographicMap.NavigateNorth(); }
            if (sender == PanSouthButton) { this.GeographicMap.NavigateSouth(); }
            if (sender == PanWestButton) { this.GeographicMap.NavigateWest(); }
            if (sender == PanEastButton) { this.GeographicMap.NavigateEast(); }

            if (sender == ZoomInButton) { this.GeographicMap.ZoomIn(); }
            if (sender == ZoomOutButton) { this.GeographicMap.ZoomOut(); }

            if (sender == FitButton) { this.GeographicMap.NavigateToFullView(); }
             
        }

        private bool _ignoreRangeValueChanged = false;
        private void Update()
        {
            if (ZoomRange == null || this.GeographicMap == null) return;

            Rect window = this.GeographicMap.WindowRect;

            if (!Double.IsNaN(window.Width) && !Double.IsNaN(window.Height))
            {
                _ignoreRangeValueChanged = true;

                var winZoomScale = 1.0 / System.Math.Min(window.Width, window.Height);
                var winZoomProc = winZoomScale * 100.0;

                System.Diagnostics.Debug.WriteLine("ZoomRange.Value " + winZoomScale + " " + winZoomProc);

                ZoomRange.SmallChange = 50;
                ZoomRange.Minimum = GeoMapNavigationPane.ZoombarMinScale;
                ZoomRange.Maximum = GeoMapNavigationPane.ZoombarMaxScale;
                ZoomRange.Value = System.Math.Min(winZoomProc, ZoomRange.Maximum);

                this.ZoomLevelDisplayText = (winZoomProc).ToString() + "%";

                _ignoreRangeValueChanged = false;
            }
        }
       
    }
}