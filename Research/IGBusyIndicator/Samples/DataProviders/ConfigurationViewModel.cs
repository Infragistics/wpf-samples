using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Infragistics.Controls.Interactions;
using System.Windows;
using System.Windows.Media;
using System;
using System.Windows.Input;
using IGBusyIndicator.Assets;
using System.Windows.Controls;
using Infragistics.Controls.Interactions.Primitives;
using IGBusyIndicator.Resources;

namespace IGBusyIndicator.Samples.DataProviders
{
    public class ConfigurationViewModel : CustomersDataProvider
    {
        #region Members
        private readonly ObservableCollection<AnimationBrushProperties> _animationBrushPropertiesCollection;
        private BusyAnimation _currentAnimation;

        private bool _isIndeterminate = true;
        private Thickness _padding;
        private Color _overlayColor = Color.FromArgb(255, 192, 192, 192); //#ffc0c0c0
        private Color _backgroundColor;
        private Color _borderColor = Color.FromArgb(255, 255, 0, 0);
        private Thickness _borderThickness;
        private TimeSpan _displayAfter = (TimeSpan)XamBusyIndicator.DisplayAfterProperty.DefaultMetadata.DefaultValue;
        private int _animationSize = 0;

        private string _busyContent = string.Empty;
        private FontFamily _busyContentFontFamily = (FontFamily)Control.FontFamilyProperty.DefaultMetadata.DefaultValue;
        private double _busyContentFontSize = 12d;
        private FontWeight _busyContentFontWeight = FontWeights.Normal;
        private FontStyle _busyContentFontStyle = FontStyles.Normal;
        private Color _busyContentForgroundColor = Colors.Black;
        private DelegateCommand _exportCommand;
        #endregion //Members

        #region Constructor
        public ConfigurationViewModel()
        {
            this._currentAnimation = BusyAnimations.Azure;

            this._animationBrushPropertiesCollection = new ObservableCollection<AnimationBrushProperties>();
            this.InitializeAnimationBrushProperties();
        }
        #endregion //Constructor

        #region Public Properties
        public IEnumerable<AnimationBrushProperties> AnimationBrushPropertiesCollection
        {
            get
            {
                return this._animationBrushPropertiesCollection;
            }
        }

        public BusyAnimation CurrentAnimation
        {
            get
            {
                return this._currentAnimation;
            }
            set
            {
                if (this._currentAnimation != value)
                {
                    this._currentAnimation = value;
                    this.OnPropertyChanged("CurrentAnimation");

                    this.InitializeAnimationBrushProperties();
                    this.OnPropertyChanged("AnimationBrushPropertiesCollection");
                }
            }
        }

        public bool IsIndeterminate
        {
            get
            {
                return this._isIndeterminate;
            }
            set
            {
                if (this._isIndeterminate != value)
                {
                    this._isIndeterminate = value;
                    this.OnPropertyChanged("IsIndeterminate");
                }
            }
        }

        public Thickness Padding
        {
            get
            {
                return this._padding;
            }
            set
            {
                if (this._padding != value)
                {
                    this._padding = value;
                    this.OnPropertyChanged("Padding");
                }
            }
        }

        public Color OverlayColor
        {
            get
            {
                return this._overlayColor;
            }
            set
            {
                if (this._overlayColor != value)
                {
                    this._overlayColor = value;
                    this.OnPropertyChanged("OverlayColor");
                }
            }
        }

        public Color BackgroundColor
        {
            get
            {
                return this._backgroundColor;
            }
            set
            {
                if (this._backgroundColor != value)
                {
                    this._backgroundColor = value;
                    this.OnPropertyChanged("BackgroundColor");
                }
            }
        }

        public Color BorderColor
        {
            get
            {
                return this._borderColor;
            }
            set
            {
                if (this._borderColor != value)
                {
                    this._borderColor = value;
                    this.OnPropertyChanged("BorderColor");
                }
            }
        }

        public Thickness BorderThickness
        {
            get
            {
                return this._borderThickness;
            }
            set
            {
                if (this._borderThickness != value)
                {
                    this._borderThickness = value;
                    this.OnPropertyChanged("BorderThickness");
                }
            }
        }

        public TimeSpan DisplayAfter
        {
            get
            {
                return this._displayAfter;
            }
            set
            {
                if (this._displayAfter != value)
                {
                    this._displayAfter = value;
                    this.OnPropertyChanged("DisplayAfter");
                }
            }
        }

        public int AnimationSize
        {
            get
            {
                return this._animationSize;
            }
            set
            {
                if (this._animationSize != value)
                {
                    this._animationSize = value;
                    this.OnPropertyChanged("AnimationSize");
                }
            }
        }

        public string BusyContent
        {
            get
            {
                return this._busyContent;
            }
            set
            {
                if (this._busyContent != value)
                {
                    this._busyContent = value;
                    this.OnPropertyChanged("BusyContent");
                }
            }
        }

        public FontFamily BusyContentFontFamily
        {
            get
            {
                return this._busyContentFontFamily;
            }
            set
            {
                if (this._busyContentFontFamily != value)
                {
                    this._busyContentFontFamily = value;
                    this.OnPropertyChanged("BusyContentFontFamily");
                }
            }
        }

        public double BusyContentFontSize
        {
            get
            {
                return this._busyContentFontSize;
            }
            set
            {
                if (this._busyContentFontSize != value)
                {
                    this._busyContentFontSize = value;
                    this.OnPropertyChanged("BusyContentFontSize");
                }
            }
        }

        public FontWeight BusyContentFontWeight
        {
            get
            {
                return this._busyContentFontWeight;
            }
            set
            {
                if (this._busyContentFontWeight != value)
                {
                    this._busyContentFontWeight = value;
                    this.OnPropertyChanged("BusyContentFontWeight");
                }
            }
        }

        public FontStyle BusyContentFontStyle
        {
            get
            {
                return this._busyContentFontStyle;
            }
            set
            {
                if (this._busyContentFontStyle != value)
                {
                    this._busyContentFontStyle = value;
                    this.OnPropertyChanged("BusyContentFontStyle");
                }
            }
        }

        public Color BusyContentForegroundColor
        {
            get
            {
                return this._busyContentForgroundColor;
            }
            set
            {
                if (this._busyContentForgroundColor != value)
                {
                    this._busyContentForgroundColor = value;
                    this.OnPropertyChanged("BusyContentForegroundColor");
                }
            }
        }

        public ICommand ExportCommand
        {
            get
            {
                if (this._exportCommand == null)
                {
                    this._exportCommand = new DelegateCommand(ExportStyle);
                }

                return this._exportCommand;
            }
        }
        #endregion // Public Properties

        #region Methods

        private void ExportStyle(object param)
        {
            var presenter = param as BusyAnimationPresenter;
            var template = new ExportedStyleTemplate(this, presenter);
            var output = template.TransformText();

            Clipboard.SetText(output);

            MessageBox.Show(BusyIndicatorStrings.BusyIndicator_GeneratedStyle_MesageBox);
        }

        private void InitializeAnimationBrushProperties()
        {
            this._animationBrushPropertiesCollection.Clear();

            if (this._currentAnimation != null)
            {
                var currentAnimationType = this._currentAnimation.GetType();
                var propertyInfos = currentAnimationType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.PropertyType.Equals(typeof(System.Windows.Media.Brush)));

                foreach (var propInfo in propertyInfos)
                {
                    var propValue = propInfo.GetValue(this._currentAnimation, null) as System.Windows.Media.Brush;
                    var brushProperties = new AnimationBrushProperties(propInfo.Name, propValue);

                    this._animationBrushPropertiesCollection.Add(brushProperties);
                }
            }
        }

        #endregion //Methods
    }
}