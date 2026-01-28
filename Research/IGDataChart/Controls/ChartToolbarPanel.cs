using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Infragistics.Controls;
using Infragistics.Controls.Charts;

namespace IGDataChart.Controls
{
    public class ChartToolbarPanel : Control
    {
        #region Initialization
        public ChartToolbarPanel()
        {
            this.DefaultStyleKey = typeof(ChartToolbarPanel);

            this.ChartToolbarPanelClick = new RoutedEventHandler(OnButtonClick);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PanModeButton = GetTemplateChild(PanModeButtonElementName) as ToggleButton;
            this.PanUpButton = GetTemplateChild(PanUpButtonElementName) as Button;
            this.PanDownButton = GetTemplateChild(PanDownButtonElementName) as Button;
            this.PanLeftButton = GetTemplateChild(PanLeftButtonElementName) as Button;
            this.PanRightButton = GetTemplateChild(PanRightButtonElementName) as Button;
         
            this.ZoomModeButton = GetTemplateChild(ZoomModeButtonElementName) as ToggleButton;
            this.ZoomInButton = GetTemplateChild(ZoomInButtonElementName) as Button;
            this.ZoomOutButton = GetTemplateChild(ZoomOutButtonElementName) as Button;
         
            this.FitHorizontalButton = GetTemplateChild(FitHorizontalButtonElementName) as Button;
            this.FitVerticalButton = GetTemplateChild(FitVerticalButtonElementName) as Button;
            this.FitAllButton = GetTemplateChild(FitAllButtonElementName) as Button;

            this.CrosshairVisibilityButton = GetTemplateChild(CrosshairVisibilityButtonElementName) as ToggleButton;
            this.GridVisibilityButton = GetTemplateChild(GridVisibilityButtonElementName) as ToggleButton;
            this.LegendVisibilityButton = GetTemplateChild(LegendVisibilityButtonElementName) as ToggleButton;
     
            Refresh();
        }

        #endregion
        #region Properties - Pan Buttons
     
        public ToggleButton PanModeButton
        {
            get { return _panModeButton; }
            private set
            {
                if (PanModeButton != null)
                {
                    PanModeButton.Click -= ChartToolbarPanelClick;
                }

                _panModeButton = value;

                if (PanModeButton != null)
                {
                    PanModeButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private ToggleButton _panModeButton;
        private const string PanModeButtonElementName = "PanMode";

        public Button PanUpButton
        {
            get { return _panUpButton; }
            private set
            {
                if (PanUpButton != null)
                {
                    PanUpButton.Click -= ChartToolbarPanelClick;
                }

                _panUpButton = value;

                if (PanUpButton != null)
                {
                    PanUpButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private Button _panUpButton;
        private const string PanUpButtonElementName = "PanUp";

        public Button PanDownButton
        {
            get { return _panDownButton; }
            private set
            {
                if (PanDownButton != null)
                {
                    PanDownButton.Click -= ChartToolbarPanelClick;
                }

                _panDownButton = value;

                if (PanDownButton != null)
                {
                    PanDownButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private Button _panDownButton;
        private const string PanDownButtonElementName = "PanDown";

        public Button PanLeftButton
        {
            get { return _panLeftButton; }
            private set
            {
                if (PanLeftButton != null)
                {
                    PanLeftButton.Click -= ChartToolbarPanelClick;
                }

                _panLeftButton = value;

                if (PanLeftButton != null)
                {
                    PanLeftButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private Button _panLeftButton;
        private const string PanLeftButtonElementName = "PanLeft";

        public Button PanRightButton
        {
            get { return _panRightButton; }
            private set
            {
                if (PanRightButton != null)
                {
                    PanRightButton.Click -= ChartToolbarPanelClick;
                }

                _panRightButton = value;

                if (PanRightButton != null)
                {
                    PanRightButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private Button _panRightButton;
        private const string PanRightButtonElementName = "PanRight";

        #endregion
        #region Properties - Other Buttons

        private ToggleButton _crosshairVisibilityButton;
        private const string CrosshairVisibilityButtonElementName = "CrosshairVisibility";
        public ToggleButton CrosshairVisibilityButton
        {
            get { return _crosshairVisibilityButton; }
            private set
            {
                if (CrosshairVisibilityButton != null)
                {
                    CrosshairVisibilityButton.Click -= ChartToolbarPanelClick;
                }

                _crosshairVisibilityButton = value;

                if (CrosshairVisibilityButton != null)
                {
                    CrosshairVisibilityButton.Click += ChartToolbarPanelClick;
                }
            }
        }
       
        private ToggleButton _gridVisibilityButton;
        private const string GridVisibilityButtonElementName = "GridVisibility";
        public ToggleButton GridVisibilityButton
        {
            get { return _gridVisibilityButton; }
            private set
            {
                if (GridVisibilityButton != null)
                {
                    GridVisibilityButton.Click -= ChartToolbarPanelClick;
                }

                _gridVisibilityButton = value;

                if (GridVisibilityButton != null)
                {
                    GridVisibilityButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        
        private ToggleButton _legendVisibilityButton;
        private const string LegendVisibilityButtonElementName = "LegendVisibility";
        public ToggleButton LegendVisibilityButton
        {
            get { return _legendVisibilityButton; }
            private set
            {
                if (LegendVisibilityButton != null)
                {
                    LegendVisibilityButton.Click -= ChartToolbarPanelClick;
                }

                _legendVisibilityButton = value;

                if (LegendVisibilityButton != null)
                {
                    LegendVisibilityButton.Click += ChartToolbarPanelClick;
                }
            }
        }

        #endregion
        #region Properties - Zoom Buttons
        
        public Button ZoomInButton
        {
            get { return _zoomInButton; }
            private set
            {
                if (ZoomInButton != null)
                {
                    ZoomInButton.Click -= ChartToolbarPanelClick;
                }

                _zoomInButton = value;

                if (ZoomInButton != null)
                {
                    ZoomInButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private Button _zoomInButton;
        private const string ZoomInButtonElementName = "ZoomIn";

        public Button ZoomOutButton
        {
            get { return _zoomOutButton; }
            private set
            {
                if (ZoomOutButton != null)
                {
                    ZoomOutButton.Click -= ChartToolbarPanelClick;
                }

                _zoomOutButton = value;

                if (ZoomOutButton != null)
                {
                    ZoomOutButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private Button _zoomOutButton;
        private const string ZoomOutButtonElementName = "ZoomOut";

        public ToggleButton ZoomModeButton
        {
            get { return _zoomModeButton; }
            private set
            {
                if (ZoomModeButton != null)
                {
                    ZoomModeButton.Click -= ChartToolbarPanelClick;
                }

                _zoomModeButton = value;

                if (ZoomModeButton != null)
                {
                    ZoomModeButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private ToggleButton _zoomModeButton;
        private const string ZoomModeButtonElementName = "ZoomMode";
      
        public Button FitHorizontalButton
        {
            get { return _fitHorizontalButton; }
            private set
            {
                if (FitHorizontalButton != null)
                {
                    FitHorizontalButton.Click -= ChartToolbarPanelClick;
                }

                _fitHorizontalButton = value;

                if (FitHorizontalButton != null)
                {
                    FitHorizontalButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private Button _fitHorizontalButton;
        private const string FitHorizontalButtonElementName = "FitHorizontally";

        public Button FitVerticalButton
        {
            get { return _fitVerticalButton; }
            private set
            {
                if (FitVerticalButton != null)
                {
                    FitVerticalButton.Click -= ChartToolbarPanelClick;
                }

                _fitVerticalButton = value;

                if (FitVerticalButton != null)
                {
                    FitVerticalButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private Button _fitVerticalButton;
        private const string FitVerticalButtonElementName = "FitVertically";

        public Button FitAllButton
        {
            get { return _fitAllButton; }
            private set
            {
                if (FitAllButton != null)
                {
                    FitAllButton.Click -= ChartToolbarPanelClick;
                }

                _fitAllButton = value;

                if (FitAllButton != null)
                {
                    FitAllButton.Click += ChartToolbarPanelClick;
                }
            }
        }
        private Button _fitAllButton;
        private const string FitAllButtonElementName = "FitBoth";
        #endregion
     
        #region DataChart Dependency Property
        /// <summary>
        /// Sets or gets the DataChart property.
        /// <para>This is a dependency property.</para>
        /// </summary>
        public XamDataChart DataChart
        {
            get
            {
                return (XamDataChart)GetValue(DataChartProperty);
            }
            set
            {
                SetValue(DataChartProperty, value);
            }
        }

        internal const string DataChartPropertyName = "DataChart";
        /// <summary>
        /// Identifies the DataChart dependency property.
        /// </summary>
        public static readonly DependencyProperty DataChartProperty = DependencyProperty.Register(
                DataChartPropertyName, typeof(XamDataChart), typeof(ChartToolbarPanel), 
                new PropertyMetadata(new PropertyChangedCallback(OnDataChartChanged)));

        private static void OnDataChartChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ChartToolbarPanel owner = (ChartToolbarPanel)obj;
            owner.Refresh();
        }
        #endregion

        protected readonly RoutedEventHandler ChartToolbarPanelClick;
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataChart != null)
            {
                if (sender == this.PanModeButton && DataChart.DefaultInteraction != InteractionState.DragPan)
                {
                    DataChart.DefaultInteraction = InteractionState.DragPan;
                }

                if (sender == this.ZoomModeButton && DataChart.DefaultInteraction != InteractionState.DragZoom)
                {
                    DataChart.DefaultInteraction = InteractionState.DragZoom;
                }

                if (sender == this.CrosshairVisibilityButton)
                {
                    if (this.CrosshairVisibilityButton.IsChecked != null)
                        DataChart.CrosshairVisibility = this.CrosshairVisibilityButton.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }

                if (sender == this.GridVisibilityButton)
                {
                    if (this.GridVisibilityButton.IsChecked != null)
                        DataChart.GridMode = this.GridVisibilityButton.IsChecked.Value ? GridMode.BehindSeries : GridMode.None;
                }

                if (sender == this.LegendVisibilityButton)
                {
                    if (this.LegendVisibilityButton.IsChecked != null)
                    {
                        Visibility legendVisibility = this.LegendVisibilityButton.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                        if (DataChart.Legend != null) DataChart.Legend.Visibility = legendVisibility;
                        foreach (Series series in DataChart.Series.Where(series => series.Legend != null))
                        {
                            series.Legend.Visibility = legendVisibility;
                        }
                    }
                }

                Rect window = DataChart.WindowRect;

                #region Handle Fit Operations
                if (sender == this.FitHorizontalButton || sender == this.FitAllButton)
                {
                    window.X = 0;
                    window.Width = 1.0;
                }

                if (sender == this.FitVerticalButton || sender == this.FitAllButton)
                {
                    window.Y = 0;
                    window.Height = 1.0;
                } 
                #endregion

                #region Handle Zoom Operations
                const double zoomScale = 0.05;
                if (sender == this.ZoomInButton)
                {
                    window.Width = System.Math.Max(0.1, window.Width - (2 * zoomScale));
                    window.Height = System.Math.Max(0.1, window.Height - (2 * zoomScale));

                    if (window.Width > 0.1) window.X = System.Math.Min(1.0, window.X + zoomScale);
                    if (window.Height > 0.1) window.Y = System.Math.Min(1.0, window.Y + zoomScale);
                }

                if (sender == this.ZoomOutButton)
                {
                    window.Width = System.Math.Min(1.0, window.Width + (2 * zoomScale));
                    window.Height = System.Math.Min(1.0, window.Height + (2 * zoomScale));

                    if (window.Width < 1) window.X = System.Math.Max(0.0, window.X - zoomScale);
                    if (window.Height < 1) window.Y = System.Math.Max(0.0, window.Y - zoomScale);
                }

                #endregion

                #region Handle Pan Operations
                const double panScale = 0.05;
                if (sender == this.PanUpButton)
                {
                    window.Y = System.Math.Max(0.0, window.Y - panScale);
                }
                if (sender == this.PanDownButton)
                {
                    window.Y = System.Math.Min(1.0 - window.Height, window.Y + panScale);
                }
                if (sender == this.PanLeftButton)
                {
                    window.X = System.Math.Max(0.0, window.X - panScale);
                }
                if (sender == this.PanRightButton)
                {
                    window.X = System.Math.Min(1.0 - window.Width, window.X + panScale);
                }

                #endregion
                DataChart.WindowRect = window;
                Refresh();
            }
        }

        private void Refresh()
        {
            if (this.PanModeButton != null)
            {
                this.PanModeButton.IsChecked = DataChart != null && DataChart.DefaultInteraction == InteractionState.DragPan && (DataChart.IsHorizontalZoomEnabled || DataChart.IsVerticalZoomEnabled);
                this.PanModeButton.IsEnabled = DataChart != null && (DataChart.IsHorizontalZoomEnabled || DataChart.IsVerticalZoomEnabled);
            }

            if (this.ZoomModeButton != null)
            {
                this.ZoomModeButton.IsChecked = DataChart != null && DataChart.DefaultInteraction == InteractionState.DragZoom && (DataChart.IsHorizontalZoomEnabled || DataChart.IsVerticalZoomEnabled);
                this.ZoomModeButton.IsEnabled = DataChart != null && (DataChart.IsHorizontalZoomEnabled || DataChart.IsVerticalZoomEnabled);
            }

            if (this.CrosshairVisibilityButton != null)
            {
                this.CrosshairVisibilityButton.IsChecked = DataChart != null && DataChart.CrosshairVisibility == Visibility.Visible;
            }

            if (this.GridVisibilityButton != null)
            {
                this.GridVisibilityButton.IsChecked = DataChart != null && DataChart.GridMode != GridMode.None;
            }

            if (this.LegendVisibilityButton != null)
            {
                if (DataChart != null)
                {
                    if (DataChart.Legend != null)
                        this.LegendVisibilityButton.IsChecked = DataChart.Legend.Visibility == Visibility.Visible;
                    else
                    {
                        Visibility legendVisibility = Visibility.Collapsed;
                        foreach (Series series in DataChart.Series.Where(series => series.Legend != null))
                        {
                            legendVisibility = series.Legend.Visibility;
                        }
                        this.LegendVisibilityButton.IsChecked = legendVisibility == Visibility.Visible;
                    }
                }
            }

            if (this.FitHorizontalButton != null)
            {
                this.FitHorizontalButton.Visibility = DataChart != null && DataChart.IsHorizontalZoomEnabled ? Visibility.Visible : Visibility.Collapsed;
            }

            if (this.FitVerticalButton != null)
            {
                this.FitVerticalButton.Visibility = DataChart != null && DataChart.IsVerticalZoomEnabled ? Visibility.Visible : Visibility.Collapsed;
            }

            if (this.FitAllButton != null)
            {
                this.FitAllButton.Visibility = DataChart != null && DataChart.IsHorizontalZoomEnabled && DataChart.IsVerticalZoomEnabled ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }

}