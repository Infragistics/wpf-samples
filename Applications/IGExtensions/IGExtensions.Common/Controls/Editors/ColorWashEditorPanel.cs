using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGExtensions.Framework;
using IGExtensions.Framework.Controls;      // FrameworkElementEx.ApplyStyle()

#if SILVERLIGHT
using Infragistics;
#else // WPF
using Infragistics.Windows.Themes;
#endif

namespace IGExtensions.Common.Controls
{
    /// <summary>
    /// Represents a control for selecting settings for the <see cref="ResourceWasher"/> 
    /// </summary>
    [TemplatePart(Name = "WashModeSelector", Type = typeof(ComboBox))]
    [TemplatePart(Name = "WashColorSelector", Type = typeof(ColorPicker))]
    public class ColorWashEditorPanel : ObservableControl
    {

        //public event EventHandler WashColorChanged;
        //public event EventHandler WashModeChanged;

        public event ColorWashSettingsChangedEventHandler WashSettingsChanged;
        
        private ColorPicker _washColorSelector;
        private ComboBox _washModeSelector;
     
        /// <summary>
        /// Constructs an instance of the ColorWashEditorPanel 
        /// </summary>
        public ColorWashEditorPanel()
        {
            //// add the generic style to the control's resources
            //const string stylePath = "/IGExtensions.Common;component/Controls/Editors/ColorWashEditorPanel.xaml";
            //this.ApplyStyle(stylePath);

            this.DefaultStyleKey = typeof(ColorWashEditorPanel);
          
            this.Loaded += OnColorWashEditorPanelLoaded;
        }

        void OnColorWashEditorPanelLoaded(object sender, RoutedEventArgs e)
        {
            // initialize current wash settings using the global ColorWashManager
            this.InitializeWashSettings(ColorWashManager.WashSettings);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _washColorSelector = base.GetTemplateChild("WashColorSelector") as ColorPicker;
            if (_washColorSelector != null)
            {
                _washColorSelector.SelectedColorChanged += OnSelectedColorChanged;
            }

            _washModeSelector = base.GetTemplateChild("WashModeSelector") as ComboBox;
            if (_washModeSelector != null)
            {
                _washModeSelector.SelectionChanged += OnSelectedModeChanged;
             }

        }

        protected bool IsInitializingWashSetting;

        public void InitializeWashSettings(ColorWashSettings washSettings)
        {
            DebugManager.Log("ColorWashEditorPanel.InitializingWashSetting: " + washSettings.WashColor + " " + washSettings.WashMode);
       
            IsInitializingWashSetting = true;
            if (_washModeSelector != null) _washModeSelector.SelectedIndex = washSettings.WashMode == WashMode.SoftLightBlend ? 0 : 1;
            if (_washColorSelector != null)
            {
                _washColorSelector.InitializeSelection(washSettings.WashColor);
                //_washColorSelector.SelectedColor = washSettings.WashColor;
            }

            this.WashMode = washSettings.WashMode;
            this.WashColor = washSettings.WashColor;
            IsInitializingWashSetting = false;
        }

        #region Event Handlers
        private void OnSelectedModeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitializingWashSetting) return;
            
            // get current wash settings
            this.WashMode = (WashMode)_washModeSelector.SelectedIndex;
            this.WashSettings = new ColorWashSettings { WashColor = this.WashColor, WashMode = this.WashMode };
            // pass current wash settings to the global ColorWashManager
            ColorWashManager.WashSettings = this.WashSettings;
        }

        private void OnSelectedColorChanged(object sender, EventArgs e)
        {
            if (IsInitializingWashSetting) return;
            
            // get current wash settings
            this.WashColor = _washColorSelector.SelectedBrush.Color;
            this.WashSettings = new ColorWashSettings { WashColor = this.WashColor, WashMode = this.WashMode };
            // pass current wash settings to the global ColorWashManager
            ColorWashManager.WashSettings = this.WashSettings;
        } 
        #endregion

        #region Properties
        public static readonly DependencyProperty WashSettingsProperty =
         DependencyProperty.Register("WashSettings", typeof(ColorWashSettings), typeof(ColorWashEditorPanel),
           new PropertyMetadata(new PropertyChangedCallback(WashSettings_Changed)));

        public ColorWashSettings WashSettings
        {
            get { return (ColorWashSettings)GetValue(WashSettingsProperty); }
            set { SetValue(WashSettingsProperty, value); }
        }
        private static void WashSettings_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var owner = (ColorWashEditorPanel)obj;
            owner.OnPropertyChanged("WashSettings");

            if (owner.WashSettingsChanged != null)
            {
                owner.WashSettingsChanged(owner, new ColorWashSettingsChangedEventArgs(owner.WashSettings));
            }
        }

        public static readonly DependencyProperty WashColorProperty =
         DependencyProperty.Register("WashColor", typeof(Color), typeof(ColorWashEditorPanel),
           new PropertyMetadata(Colors.Black, new PropertyChangedCallback(WashColor_Changed)));

        public Color WashColor
        {
            get { return (Color)GetValue(WashColorProperty); }
            set { SetValue(WashColorProperty, value); }
        }
        private static void WashColor_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var owner = (ColorWashEditorPanel)obj;
            owner.OnPropertyChanged("WashColor");

            //if (owner.WashColorChanged != null)
            //{
            //    owner.WashColorChanged(owner, EventArgs.Empty);
            //}
        }

        public static readonly DependencyProperty WashModeProperty =
         DependencyProperty.Register("WashMode", typeof(WashMode), typeof(ColorWashEditorPanel),
           new PropertyMetadata(WashMode.HueSaturationReplacement, new PropertyChangedCallback(WashMode_Changed)));

        public WashMode WashMode
        {
            get { return (WashMode)GetValue(WashModeProperty); }
            set { SetValue(WashModeProperty, value); }
        }
        private static void WashMode_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var owner = (ColorWashEditorPanel)obj;
            owner.OnPropertyChanged("WashMode");

            //if (owner.WashModeChanged != null)
            //{
            //    owner.WashModeChanged(owner, EventArgs.Empty);
            //}
        }

        #endregion
    }

    /// <summary>
    /// Represents color wash settings for the <see cref="ColorWashEditorPanel"/>
    /// </summary>
    public class ColorWashSettings
    {
        /// <summary>
        /// Constructs an instance of the ColorWashSettings 
        /// </summary>
        public ColorWashSettings()
        {
            this.WashColor = Colors.Black;
            this.WashMode = WashMode.HueSaturationReplacement;
        }
        /// <summary>
        /// Gets or sets Wash Mode
        /// </summary>
        public WashMode WashMode { get; set; }
        /// <summary>
        /// Gets or sets Wash Color
        /// </summary>
        public Color WashColor { get; set; }

        public static ColorWashSettings DefaultWash { get { return WhiteWash; } }
        public static ColorWashSettings WhiteWash { get { return new ColorWashSettings { WashColor = Colors.Blue }; } }
        public static ColorWashSettings BlueWash { get { return new ColorWashSettings { WashColor = Colors.Blue }; } }
        public static ColorWashSettings BrownWash { get { return new ColorWashSettings { WashColor = Colors.Brown }; } }
        public static ColorWashSettings CyanWash { get { return new ColorWashSettings { WashColor = Colors.Cyan }; } }
        public static ColorWashSettings GreenWash { get { return new ColorWashSettings { WashColor = Colors.Green }; } }
        public static ColorWashSettings RedWash { get { return new ColorWashSettings { WashColor = Colors.Red }; } }
        public static ColorWashSettings OrangeWash { get { return new ColorWashSettings { WashColor = Colors.Orange }; } }
        public static ColorWashSettings YellowWash { get { return new ColorWashSettings { WashColor = Colors.Yellow }; } }
        public static ColorWashSettings BlackWash { get { return new ColorWashSettings { WashColor = Colors.Black }; } }
        public static ColorWashSettings GrayWash { get { return new ColorWashSettings { WashColor = Colors.Gray }; } }
    }

    public delegate void ColorWashSettingsChangedEventHandler(object sender, ColorWashSettingsChangedEventArgs e);
    
    public class ColorWashSettingsChangedEventArgs : EventArgs
    {
        public ColorWashSettingsChangedEventArgs(ColorWashSettings settings)
        {
            this.ColorWashSettings = settings;
        }
        public ColorWashSettings ColorWashSettings { get; private set; }
    }

    /// <summary>
    /// Represents color wash manager that keeps track of current wash settings 
    /// </summary>
    public static class ColorWashManager
    {
        public static event ColorWashSettingsChangedEventHandler WashSettingsChanged;
        
        public static ColorWashSettings WashSettings
        {
            get { return _washSettings; }
            set { _washSettings = value; OnColorWashSettingsChanged(); }
        }
        private static ColorWashSettings _washSettings = new ColorWashSettings();

        public static void OnColorWashSettingsChanged()
        {
            DebugManager.Log("ColorWashManager.WashSettings changed: " + WashSettings.WashColor + " " + WashSettings.WashMode);
            if (WashSettingsChanged != null)
            {
                WashSettingsChanged(null, new ColorWashSettingsChangedEventArgs(WashSettings));
            }
        }
    }
}