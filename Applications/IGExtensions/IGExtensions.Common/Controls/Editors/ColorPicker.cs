using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IGExtensions.Framework;
using IGExtensions.Framework.Controls;
using Infragistics.Controls.Editors;

// FrameworkElement.ApplyStyle(), ToBitmap(), 

// requires reference to the IGExtensions.Framework assembly which provides system extensions: ApplyStyle(), ToBitmap(), 

namespace IGExtensions.Common.Controls
{
    /// <summary>
    /// Represents a control for selecting a brush from color palette 
    /// </summary>
    [TemplatePart(Name = "ColorSamplerSource", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "ColorSamplerCircleSelector", Type = typeof(RadioButton))]
    [TemplatePart(Name = "ColorSamplerHexagonSelector", Type = typeof(RadioButton))]
    [TemplatePart(Name = "ColorSamplerRectSelector", Type = typeof(RadioButton))]
    [TemplatePart(Name = "ColorSamplerOpacitySlider", Type = typeof(XamNumericSlider))]
    public class ColorPicker : ObservableControl
	{
        protected FrameworkElement ColorSamplerSource;
        protected RadioButton ColorSamplerCircleSelector;
        protected RadioButton ColorSamplerHexagonSelector;
        protected RadioButton ColorSamplerRectSelector;
        protected XamNumericSlider ColorSamplerOpacitySlider;
        protected WriteableBitmap ColorSamplerSourceBitmap;
        protected bool IsTemplateApplied;
        private bool _isMouseDrag;

        public static Color InitialColor = NamedColors.White.Color; // Colors.Transparent;

        /// <summary>
        /// Constructs an instance of the ColorPicker 
        /// </summary>
		public ColorPicker()
		{
            //this.SelectedBrush = new SolidColorBrush(ColorPicker.InitialColor);
            
            // add the generic style to the control's resources
            this.DefaultStyleKey = typeof(ColorPicker);
            //const string stylePath = "/IGExtensions.Common;component/Controls/Editors/ColorPicker.xaml";
            //this.ApplyStyle(stylePath); 
            //this.PropertyChanged += OnPropertyChanged;

            this.Loaded += OnColorPickerLoaded;
        }

        private void OnColorPickerLoaded(object sender, RoutedEventArgs e)
        {
            if (this.SelectedBrush != null)
                InitializeSelection(this.SelectedBrush.Color);
         
        }

     
        public new void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case "ColorSamplerMode":
                    {
                        if (!IsTemplateApplied) return;
                        //if (this.ColorSamplerMode == ColorSamplerMode.Circle)
                        //    this.colorSamplerCircleSelector.IsChecked = true;
                        //else if (this.ColorSamplerMode == ColorSamplerMode.Hexagon)
                        //    this.colorSamplerHexagonSelector.IsChecked = true;
                        //else if (this.ColorSamplerMode == ColorSamplerMode.Rectangle)
                        //    this.colorSamplerRectSelector.IsChecked = true;
                        ColorSamplerCircleSelector.IsChecked = this.ColorSamplerMode == ColorSamplerMode.Circle;
                        ColorSamplerHexagonSelector.IsChecked = this.ColorSamplerMode == ColorSamplerMode.Hexagon;
                        ColorSamplerRectSelector.IsChecked = this.ColorSamplerMode == ColorSamplerMode.Rectangle;

                        break;
                    }
            }
        }


        /// <summary>
        /// Determines which color, white or black, is ideal to contrast the parameter color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns>Color</returns>
        private Color ContrastColor(Color color)
        {
            byte colorValue = 0;

            // Formula gives weighting to colors based off of how the human eye percieves color.  
            // Green is weighted the most and blue is weighted the least.
            // See http://en.wikipedia.org/wiki/Luminance_(relative)
            double relativeLuminance = 1 - ( ( (0.2126 * color.R) + (0.7152 * color.G) + (0.0722 * color.B) ) / 255);

            // Sets the color value to either black or white based on how "bright" the color appears to a human.
            if (relativeLuminance < 0.5)
            {
                colorValue = 0;
            }
            else
            {
                colorValue = 255;
            }

            // Creates a color that is either black or white whith an alpha value of #FF.
            return Color.FromArgb(255, colorValue, colorValue, colorValue);  
        }

        #region Overrides
		public override void OnApplyTemplate()
		{
            base.OnApplyTemplate();

            ColorSamplerSource = base.GetTemplateChild("ColorSamplerSource") as FrameworkElement;
			if (ColorSamplerSource != null)
			{
                //_colorSamplerSourceBitmap = new WriteableBitmap(_colorSamplerSource, null);
                ColorSamplerSource.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseLeftButtonDown);
                ColorSamplerSource.MouseLeftButtonUp += new MouseButtonEventHandler(OnMouseLeftButtonUp);
                ColorSamplerSource.MouseMove += new MouseEventHandler(OnMouseMove);
                ColorSamplerSource.MouseEnter += new MouseEventHandler(OnMouseEnter);
                ColorSamplerSource.MouseLeave += new MouseEventHandler(OnMouseLeave);
			}
            ColorSamplerCircleSelector = base.GetTemplateChild("ColorSamplerCircleSelector") as RadioButton;
            if (ColorSamplerCircleSelector != null)
            {
                ColorSamplerCircleSelector.Checked += OnColorSamplerSelectorChecked;
                ColorSamplerCircleSelector.IsChecked = this.ColorSamplerMode == ColorSamplerMode.Circle;
            }
            ColorSamplerHexagonSelector = base.GetTemplateChild("ColorSamplerHexagonSelector") as RadioButton;
            if (ColorSamplerHexagonSelector != null)
            {
                ColorSamplerHexagonSelector.Checked += OnColorSamplerSelectorChecked;
                ColorSamplerHexagonSelector.IsChecked = this.ColorSamplerMode == ColorSamplerMode.Hexagon;
            }
            ColorSamplerRectSelector = base.GetTemplateChild("ColorSamplerRectSelector") as RadioButton;
            if (ColorSamplerRectSelector != null)
            {
                ColorSamplerRectSelector.Checked += OnColorSamplerSelectorChecked;
                ColorSamplerRectSelector.IsChecked = this.ColorSamplerMode == ColorSamplerMode.Rectangle;
            }

            ColorSamplerOpacitySlider = base.GetTemplateChild("ColorSamplerOpacitySlider") as XamNumericSlider;
            if (ColorSamplerOpacitySlider != null)
            {
                ColorSamplerOpacitySlider.ThumbValueChanged += OnColorSamplerOpacitySliderChanged;
            }
		    InitializeSelection(this.SelectedBrush);
            //InitializeColorSelection(InitialColor);
            //this.SelectedColor = ColorPicker.InitialColor;
            //this.SelectedBrush = new SolidColorBrush(ColorPicker.InitialColor);
		    this.IsTemplateApplied = true;
		}

        
        void OnColorSamplerOpacitySliderChanged(object sender, ThumbValueChangedEventArgs<double> e)
        {
            if (this.IsUpdating) return; 
            var color = this.SelectedBrush.Color;
            color.A = (byte)(255 * this.ColorSamplerOpacitySlider.Value);
            //this.SelectedColor = color;
            this.SelectedColorName = color.ToString();
            this.SelectedBrush = new SolidColorBrush(color);
            this.ContrastSelectedBrush = new SolidColorBrush(ContrastColor(color));
      
        }

        private bool _forceUpdateColorSamplerBitmap;

        private void OnColorSamplerSelectorChecked(object sender, RoutedEventArgs e)
        {
            _forceUpdateColorSamplerBitmap = true;
            //UpdateColorSamplerBitmap(true);
           
        }
     
		#endregion Overrides

        private void UpdateColorAtLocation(Point location)
        {
            UpdateColorSamplerBitmap(_forceUpdateColorSamplerBitmap);
            _forceUpdateColorSamplerBitmap = false;

            var color = ColorSamplerSourceBitmap.GetPixelColorAt(location);
            color.A = (byte) (255 * this.ColorSamplerOpacitySlider.Value);
            //var b = new SolidColorBrush(this.SelectedColor) {Opacity = this.ColorSamplerOpacitySlider.Value};

            //int index = (((int)location.Y - 1) * _colorSamplerSourceBitmap.PixelWidth) + (int)location.X;
            //if (index >= 0 && index < _colorSamplerSourceBitmap.Pixels.Length)
            //{
            //    int pixel = _colorSamplerSourceBitmap.Pixels[index];

            //    var B = (byte)(pixel & 0xFF); pixel >>= 8;
            //    var G = (byte)(pixel & 0xFF); pixel >>= 8;
            //    var R = (byte)(pixel & 0xFF); pixel >>= 8;
            //    var A = (byte)pixel; // alpha

            //    var color = Color.FromArgb(A, R, G, B);
            // }
            //this.SelectedColor = color;
            this.SelectedColorName = color.ToString();
            this.SelectedBrush = new SolidColorBrush(color);
            this.ContrastSelectedBrush = new SolidColorBrush(ContrastColor(color));
        }

        protected bool IsUpdating;

        public void InitializeSelection(Color color)
        {
            //if (color == null) return;
            
            IsUpdating = true;
            //this.SelectedColor = color;
            this.SelectedColorName = color.ToString();
            this.SelectedBrush = new SolidColorBrush(color);
            this.ContrastSelectedBrush = new SolidColorBrush(ContrastColor(color));
            if (this.ColorSamplerOpacitySlider != null)
                this.ColorSamplerOpacitySlider.Value = (color.A / 255.0);
            IsUpdating = false;
        }
        public void InitializeSelection(SolidColorBrush brush)
        {
            if (brush != null)
               InitializeSelection(brush.Color);
        }
        public void UpdateColorSamplerBitmap(bool forceUpdate = false)
        {
            if (ColorSamplerSource != null)
            {
                //     _colorSamplerSourceBitmap..Pixels.Length == 0
                if (ColorSamplerSourceBitmap == null || forceUpdate ||
                    ColorSamplerSourceBitmap.PixelHeight == 0 ||
                    ColorSamplerSourceBitmap.PixelWidth == 0)
                {
                    DebugManager.Log("ColorPicker.SamplerBitmap changed.");
                    ColorSamplerSourceBitmap = ColorSamplerSource.ToBitmap();
                    //_colorSamplerSourceBitmap = new WriteableBitmap(_colorSamplerSource, null);

                }
                
              
            }

        }
		#region Events
		public event EventHandler SelectedBrushChanged;
        public event EventHandler SelectedColorChanged;
        #endregion Events

		#region Event Handlers
        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            _isMouseDrag = false;
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            _isMouseDrag = false;
        }
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (ColorSamplerSource != null && _isMouseDrag)
            {
                UpdateColorAtLocation(e.GetPosition(ColorSamplerSource));
            }
        }
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ColorSamplerSource != null)
            {
                _isMouseDrag = true;
            }
        }
		private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
            if (ColorSamplerSource != null)
			{
                _isMouseDrag = false;
                UpdateColorAtLocation(e.GetPosition(ColorSamplerSource));
             }
        
		}
		#endregion Event Handlers

		#region Properties
        //private ColorSamplerMode _colorSamplerMode = ColorSamplerMode.Circle;
        ///// <summary>
        ///// Gets or sets ColorSamplerMode property 
        ///// </summary>
        //public ColorSamplerMode ColorSamplerMode
        //{
        //    get {  return _colorSamplerMode; }
        //    set { if (_colorSamplerMode == value) return; _colorSamplerMode = value; OnPropertyChanged("ColorSamplerMode"); }
        //}
        public const string ColorSamplerModePropertyName = "ColorSamplerMode";
        public static readonly DependencyProperty ColorSamplerModeProperty = DependencyProperty.Register(
            ColorSamplerModePropertyName, typeof(ColorSamplerMode), typeof(ColorPicker), 
            new PropertyMetadata(ColorSamplerMode.Circle, (sender, e) =>
        {
            (sender as ColorPicker).OnPropertyChanged(new PropertyChangedEventArgs(ColorSamplerModePropertyName));
        })); 
        /// <summary>
        /// Gets or sets the ColorSamplerMode property 
        /// </summary>
        public ColorSamplerMode ColorSamplerMode
        {
            get { return (ColorSamplerMode)GetValue(ColorSamplerModeProperty); }
            set { SetValue(ColorSamplerModeProperty, value); }
        }
        public const string ColorSamplerBackgroundPropertyName = "ColorSamplerBackground";
        public static readonly DependencyProperty ColorSamplerBackgroundProperty = DependencyProperty.Register(
            ColorSamplerBackgroundPropertyName, typeof(Brush), typeof(ColorPicker), 
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent), (sender, e) =>
            {
                (sender as ColorPicker).OnPropertyChanged(new PropertyChangedEventArgs(ColorSamplerBackgroundPropertyName));
            }));
        /// <summary>
        /// Gets or sets the ColorSamplerBackground property 
        /// </summary>
        public Brush ColorSamplerBackground
        {
            get { return (Brush)GetValue(ColorSamplerBackgroundProperty); }
            set { SetValue(ColorSamplerBackgroundProperty, value); }
        }

        //public const string ColorSamplerPreviewVisibilityPropertyName = "ColorSamplerPreviewVisibility";
        //public static readonly DependencyProperty ColorSamplerPreviewVisibilityProperty = DependencyProperty.Register(
        //    ColorSamplerPreviewVisibilityPropertyName, typeof(Visibility), typeof(ColorPicker), new PropertyMetadata(Visibility.Visible, (sender, e) =>
        //    {
        //        (sender as ColorPicker).OnPropertyChanged(new PropertyChangedEventArgs(ColorSamplerPreviewVisibilityPropertyName));
        //    })); 

        public const string ColorSamplerToggleVisibilityPropertyName = "ColorSamplerToggleVisibility";
        public static readonly DependencyProperty ColorSamplerToggleVisibilityProperty = DependencyProperty.Register(
            ColorSamplerToggleVisibilityPropertyName, typeof(Visibility), typeof(ColorPicker), 
            new PropertyMetadata(Visibility.Visible, (sender, e) =>
        {
            (sender as ColorPicker).OnPropertyChanged(new PropertyChangedEventArgs(ColorSamplerToggleVisibilityPropertyName));
        })); 
        /// <summary>
        /// Gets or sets the ColorSamplerToggleVisibility property 
        /// </summary>
        public Visibility ColorSamplerToggleVisibility
        {
            get { return (Visibility)GetValue(ColorSamplerToggleVisibilityProperty); }
            set { SetValue(ColorSamplerToggleVisibilityProperty, value); }
        }
        public const string ColorSamplerPreviewVisibilityPropertyName = "ColorSamplerPreviewVisibility";
        public static readonly DependencyProperty ColorSamplerPreviewVisibilityProperty = DependencyProperty.Register(
            ColorSamplerPreviewVisibilityPropertyName, typeof(Visibility), typeof(ColorPicker),
            new PropertyMetadata(Visibility.Visible, (sender, e) =>
        {
            (sender as ColorPicker).OnPropertyChanged(new PropertyChangedEventArgs(ColorSamplerPreviewVisibilityPropertyName));
        }));

        /// <summary>
        /// Gets or sets the ColorSamplerPreviewVisibility property 
        /// </summary>
        public Visibility ColorSamplerPreviewVisibility
        {
            get { return (Visibility)GetValue(ColorSamplerPreviewVisibilityProperty); }
            set { SetValue(ColorSamplerPreviewVisibilityProperty, value); }
        }

        public const string ColorOpacitySliderVisibilityPropertyName = "ColorOpacitySliderVisibility";
        public static readonly DependencyProperty ColorOpacitySliderVisibilityProperty = DependencyProperty.Register(
            ColorOpacitySliderVisibilityPropertyName, typeof(Visibility), typeof(ColorPicker),
            new PropertyMetadata(Visibility.Collapsed, (sender, e) =>
            {
                (sender as ColorPicker).OnPropertyChanged(new PropertyChangedEventArgs(ColorOpacitySliderVisibilityPropertyName));
            }));

        /// <summary>
        /// Gets or sets the ColorOpacitySliderVisibility property 
        /// </summary>
        public Visibility ColorOpacitySliderVisibility
        {
            get { return (Visibility)GetValue(ColorOpacitySliderVisibilityProperty); }
            set { SetValue(ColorOpacitySliderVisibilityProperty, value); }
        }


        //public static readonly DependencyProperty IsSelectedCircleColorSamplerProperty =
        //    DependencyProperty.Register("IsSelectedCircleColorSampler", typeof(bool), typeof(ColorPicker),
        //      new PropertyMetadata(true, new PropertyChangedCallback(IsSelectedCircleColorSampler_Changed)));

        //public bool IsSelectedCircleColorSampler
        //{
        //    get { return (bool)GetValue(IsSelectedCircleColorSamplerProperty); }
        //    set { SetValue(IsSelectedCircleColorSamplerProperty, value); }
        //}
        //private static void IsSelectedCircleColorSampler_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        //{
        //    var owner = (ColorPicker)obj;
        //    //owner.UpdateColorSamplerBitmap(true);
        //    owner.OnPropertyChanged("IsSelectedCircleColorSampler");
        //}
        //public static readonly DependencyProperty IsSelectedHexagonColorSamplerProperty =
        //    DependencyProperty.Register("IsSelectedHexagonColorSampler", typeof(bool), typeof(ColorPicker),
        //      new PropertyMetadata(false, new PropertyChangedCallback(IsSelectedHexagonColorSampler_Changed)));

        //public bool IsSelectedHexagonColorSampler
        //{
        //    get { return (bool)GetValue(IsSelectedHexagonColorSamplerProperty); }
        //    set { SetValue(IsSelectedHexagonColorSamplerProperty, value); }
        //}
        //private static void IsSelectedHexagonColorSampler_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        //{
        //    var owner = (ColorPicker)obj;
        //    //owner.UpdateColorSamplerBitmap(true);
        //    owner.OnPropertyChanged("IsSelectedHexagonColorSampler");
        //}
        //public static readonly DependencyProperty IsSelectedRectangleColorSamplerProperty =
        //    DependencyProperty.Register("IsSelectedRectangleColorSampler", typeof(bool), typeof(ColorPicker),
        //      new PropertyMetadata(false, new PropertyChangedCallback(IsSelectedRectangleColorSamplerProperty_Changed)));

        //public bool IsSelectedRectangleColorSampler
        //{
        //    get { return (bool)GetValue(IsSelectedRectangleColorSamplerProperty); }
        //    set { SetValue(IsSelectedRectangleColorSamplerProperty, value); }
        //}
        //private static void IsSelectedRectangleColorSamplerProperty_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        //{
        //    var owner = (ColorPicker)obj;
        //    //owner.UpdateColorSamplerBitmap(true);
        //    owner.OnPropertyChanged("IsSelectedRectangleColorSampler");
        //}

        //public static readonly DependencyProperty SelectedColorSamplerProperty =
        //    DependencyProperty.Register("SelectedColorSampler", typeof(ColorSamplerType), typeof(ColorPicker),
        //    new PropertyMetadata(ColorSamplerType.Circle, new PropertyChangedCallback(SelectedColorSampler_Changed)));

        //public ColorSamplerType SelectedColorSampler 
        //{
        //    get { return (ColorSamplerType)GetValue(SelectedColorSamplerProperty); }
        //    set { SetValue(SelectedColorSamplerProperty, value); }
        //}
        //private static void SelectedColorSampler_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        //{
        //    var owner = (ColorPicker)obj;
        //    owner.OnPropertyChanged("SelectedColorSampler");
        //}


        //public static readonly DependencyProperty SelectedColorProperty =
        //DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker),
        //       new PropertyMetadata(ColorPicker.InitialColor, new PropertyChangedCallback(SelectedColor_Changed)));

        //public Color SelectedColor
        //{
        //    get { return (Color)GetValue(SelectedColorProperty); }
        //    set { SetValue(SelectedColorProperty, value); }
        //}
        //private static void SelectedColor_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        //{
        //    var owner = (ColorPicker)obj;
        //    owner.OnPropertyChanged("SelectedColor");
        //    owner.SelectedColorName = owner.SelectedColor.ToString();

        //    if (owner.IsUpdating) return;
        //    if (owner.SelectedColorChanged != null)
        //    {
        //        owner.SelectedColorChanged(owner, EventArgs.Empty);
        //    }
        //}
        public static readonly DependencyProperty SelectedColorNameProperty =
        DependencyProperty.Register("SelectedColorName", typeof(string), typeof(ColorPicker),
              new PropertyMetadata(new PropertyChangedCallback(SelectedColorName_Changed)));

        public string SelectedColorName
        {
            get { return (string)GetValue(SelectedColorNameProperty); }
            set { SetValue(SelectedColorNameProperty, value); }
        }
        private static void SelectedColorName_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var owner = (ColorPicker)obj;
            owner.OnPropertyChanged("SelectedColor");
        }

		#region SelectedBrush  
        //public const string SelectedBrushPropertyName = "SelectedBrush";
        //public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.Register(
        //    SelectedBrushPropertyName, typeof(SolidColorBrush), typeof(ColorPicker),
        //    new PropertyMetadata(NamedColors.DodgerBlue.Brush, (sender, e) =>
        //    {
        //        (sender as ColorPicker).OnPropertyChanged(new PropertyChangedEventArgs(SelectedBrushPropertyName));
        //    }));

        ///// <summary>
        ///// Gets or sets the SelectedBrush property 
        ///// </summary>
        //public SolidColorBrush SelectedBrush
        //{
        //    get { return (SolidColorBrush)GetValue(SelectedBrushProperty); }
        //    set { SetValue(SelectedBrushProperty, value); }
        //}
         /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedBrush", typeof(SolidColorBrush), typeof(ColorPicker),
                   new PropertyMetadata(new PropertyChangedCallback(SelectedBrush_Changed)));

        //TODO changed to SelectedColor and type to Color
        /// <summary>
        /// 
        /// </summary>
        public SolidColorBrush SelectedBrush
        {
            get { return (SolidColorBrush)GetValue(SelectedBrushProperty); }
            set { SetValue(SelectedBrushProperty, value); }
        }

		private static void SelectedBrush_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
            var owner = (ColorPicker)obj;
            owner.OnPropertyChanged("SelectedBrush");
            
            if (owner.IsUpdating || !owner.IsTemplateApplied) return;
            var brush = e.NewValue as SolidColorBrush;
            //owner.InitializeBrushSelection(brush);
            
            if (owner.SelectedBrushChanged != null)
			{
                owner.SelectedBrushChanged(owner, EventArgs.Empty);
			}
            if (owner.SelectedColorChanged != null)
            {
                owner.SelectedColorChanged(owner, EventArgs.Empty);
            }
        }

		#endregion
		#endregion  

        #region ContrastSelectedBrush

        /// <summary>
        /// Gets or sets the ContrastSelectedBrush property.
        /// </summary>
        public static readonly DependencyProperty ContrastSelectedBrushProperty =
           DependencyProperty.Register("ContrastSelectedBrush", typeof(SolidColorBrush), typeof(ColorPicker),
                  new PropertyMetadata(new PropertyChangedCallback(SelectedBrush_Changed)));
        
        public SolidColorBrush ContrastSelectedBrush
        {
            get { return (SolidColorBrush)GetValue(ContrastSelectedBrushProperty); }
            set { SetValue(ContrastSelectedBrushProperty, value); }
        }

        private static void ConstrastSelectedBrush_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var owner = (ColorPicker)obj;
            owner.OnPropertyChanged("ContrastSelectedBrush");
        }
        #endregion
        //#region INotifyPropertyChanged Members

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void OnPropertyChanged(string propertyName)
        //{
        //    if (this.PropertyChanged != null)
        //    {
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        //#endregion
	}
    public enum ColorSamplerMode
    {
        Circle,
        Hexagon,
        Rectangle
    }
    //public class ColorSamplerTypeConverter : IValueConverter
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var type = (ColorSamplerType)value;
    //        return visibility ? Visibility.Visible : Visibility.Collapsed;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var visibility = (Visibility)value;
    //        return (visibility == Visibility.Visible);
    //    }
    //}
}
