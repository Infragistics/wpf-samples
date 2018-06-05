using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace IGExtensions.Common.Maps.Behaviors
{
    /// <summary>
    /// Represents a marker element in a geographic series
    /// </summary>
    public class GeoMarker : ContentControl
    {
        #region constructor and initialisation
        public GeoMarker()
        {
            this.FillScale = new GeoMarkerBrushScale();
            this.RadiusScale = new GeoMarkerSizeScale();
            //this.FillScale.PropertyChanged += OnFillScalePropertyChanged;
            //this.FillValue = 1.0;

            DefaultStyleKey = typeof(GeoMarker);

            // VisualStateManager.GoToState(this, IsSelected ? SelectedVisualStateName : NotSelectedVisualStateName, true);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //TextBlock textBlock = GetTemplateChild("TextBlock") as TextBlock;

            //SetBinding(BackgroundProperty, new Binding("Series.Brush") { Source = DataContext });
            //textBlock.SetBinding(TextBlock.TextProperty, new Binding("Item.Country.Code") { Source = DataContext });

            //AdjustSize();
        }
        #endregion

        #region FillValue Dependency Property

        public const string FillValuePropertyName = "FillValue";

        /// <summary>
        /// Identifies the FillValue dependency property.
        /// </summary>
        public static readonly DependencyProperty FillValueProperty =
            DependencyProperty.Register(FillValuePropertyName, typeof(double), typeof(GeoMarker),
            new PropertyMetadata(1.0, (sender, e) =>
            {
                (sender as GeoMarker).OnPropertyChanged(new PropertyChangedEventArgs(FillValuePropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public double FillValue
        {
            get
            {
                return (double)GetValue(FillValueProperty);
            }
            set
            {
                SetValue(FillValueProperty, value);
            }
        }
        #endregion
        #region FillScale Dependency Property
        internal const string FillScalePropertyName = "FillScale";

        /// <summary>
        /// Identifies the FillScale dependency property.
        /// </summary>
        public static readonly DependencyProperty FillScaleProperty =
            DependencyProperty.Register(FillScalePropertyName,
            typeof(GeoMarkerBrushScale), typeof(GeoMarker), new PropertyMetadata((sender, e) =>
            {

                (sender as GeoMarker).OnPropertyChanged(new PropertyChangedEventArgs(FillScalePropertyName));
            }));

        /// <summary>
        /// Gets or sets the brush scale for the marker brush.
        /// </summary>
        public GeoMarkerBrushScale FillScale
        {
            get
            {
                return (GeoMarkerBrushScale)GetValue(FillScaleProperty);
            }
            set
            {
                SetValue(FillScaleProperty, value);
            }
        }
        #endregion

        #region RadiusValue Dependency Property

        public const string RadiusPropertyName = "Radius";

        /// <summary>
        /// Identifies the Radius dependency property.
        /// </summary>
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(RadiusPropertyName, typeof(double), typeof(GeoMarker),
            new PropertyMetadata(20.0, (sender, e) =>
            {
                (sender as GeoMarker).OnPropertyChanged(new PropertyChangedEventArgs(RadiusPropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill  for this scale.
        /// </summary>
        public double Radius
        {
            get
            {
                return (double)GetValue(RadiusProperty);
            }
            set
            {
                SetValue(RadiusProperty, value);
            }
        }


        public const string RadiusValuePropertyName = "RadiusValue";

        /// <summary>
        /// Identifies the RadiusValue dependency property.
        /// </summary>
        public static readonly DependencyProperty RadiusValueProperty =
            DependencyProperty.Register(RadiusValuePropertyName, typeof(double), typeof(GeoMarker),
            new PropertyMetadata(50.0, (sender, e) =>
            {
                (sender as GeoMarker).OnPropertyChanged(new PropertyChangedEventArgs(RadiusValuePropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public double RadiusValue
        {
            get
            {
                return (double)GetValue(RadiusValueProperty);
            }
            set
            {
                SetValue(RadiusValueProperty, value);
            }
        }

        #endregion
        #region RadiusScale Dependency Property
        public const string RadiusScalePropertyName = "RadiusScale";

        /// <summary>
        /// Identifies the RadiusScale dependency property.
        /// </summary>
        public static readonly DependencyProperty RadiusScaleProperty =
            DependencyProperty.Register(RadiusScalePropertyName,
            typeof(GeoMarkerSizeScale), typeof(GeoMarker), new PropertyMetadata((sender, e) =>
            {

                (sender as GeoMarker).OnPropertyChanged(new PropertyChangedEventArgs(RadiusScalePropertyName));
            }));

        /// <summary>
        /// Gets or sets the size scale for the marker size.
        /// </summary>
        public GeoMarkerSizeScale RadiusScale
        {
            get
            {
                return (GeoMarkerSizeScale)GetValue(RadiusScaleProperty);
            }
            set
            {
                SetValue(RadiusScaleProperty, value);
            }
        }
        #endregion

        internal void UpdateMarkerBackground()
        {
            if (this.FillScale != null)
            {
                this.Background = this.FillScale.GetBrushByValue(this.FillValue);
            }
        }
        internal void UpdateMarkerRadius()
        {
            if (this.RadiusScale != null)
            {
                this.Visibility = Visibility.Collapsed;
                double radius = this.RadiusScale.GetScaledSize(this.RadiusValue);
                this.Height = radius;
                this.Width = radius;
                this.Visibility = Visibility.Visible;
            }
        }
        public void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case RadiusValuePropertyName:
                case RadiusScalePropertyName:
                    {
                        UpdateMarkerRadius();
                        break;
                    }
                case FillScalePropertyName:
                    {

                        UpdateMarkerBackground();
                        break;
                    }

            }
        }

        void OnRadiusScalePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }
        void OnFillScalePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(FillScalePropertyName));
        }
    }

  
}