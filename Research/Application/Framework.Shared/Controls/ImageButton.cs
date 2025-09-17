using System; 
using Xamarin.Forms;

namespace Infragistics.Framework
{ 
    public class ImageView : Image
    {  
        public static readonly BindableProperty SourcePathProperty = BindableProperty.Create(
            "SourcePath", typeof(string), typeof(ImageView), null);
         
        public string SourcePath
        {
            get { return (string)GetValue(SourcePathProperty); }
            set { SetValue(SourcePathProperty, value); }
        }
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "SourcePath")
            {
                this.Source = StringToImageResource.GetImageSource(this.SourcePath);
            }
        } 
    }

    /// <summary>
    /// Represents a button control with image and label as a content of the button.
    /// </summary>
    public class ImageButton : Grid
    {  
        public ImageButton()
        {            
            this.Padding = new Thickness(0);
            this.Margin = new Thickness(0);
            this.RowSpacing = 0;
            this.ColumnSpacing = 0; 
            this.BackgroundColor = Colors.Transparent;

            IsClickable = true; 
            Label = new Label { Text = "" };
            Label.HorizontalOptions = LayoutOptions.CenterAndExpand;
            Label.VerticalOptions = LayoutOptions.Center;

            Image = new Image();
            Image.SetBinding(Image.HorizontalOptionsProperty, new Binding { Source = this, Path = "ImageHorizontalOptions" });
            Image.SetBinding(Image.VerticalOptionsProperty, new Binding { Source = this, Path = "ImageVerticalOptions" });
            Image.BackgroundColor = Colors.Transparent;

            Selector = new Grid();
            Selector.SetBinding(Grid.BackgroundColorProperty, new Binding { Source = this, Path = "SelectionBarColor" });
            Selector.SetBinding(Grid.HeightRequestProperty, new Binding { Source = this, Path = "SelectionBarSize" });
            Selector.IsVisible = false;
            Selector.VerticalOptions = LayoutOptions.Start;
            Selector.HorizontalOptions = LayoutOptions.FillAndExpand;

            this.Children.Add(Image);
            this.Children.Add(Label);
            this.Children.Add(Selector);

            // JM 01-27-15 TFS187929 - Moved this call here from above since the method now expects
            // the Label and Image to be created.
            UpdateTapGesture();
                        
            Label.SetBinding(Label.TextProperty, new Binding { Source = this, Path = "Text" });
            Label.SetBinding(Label.TextColorProperty, new Binding { Source = this, Path = "TextColor" }); 
            Label.SetBinding(Label.FontAttributesProperty, new Binding { Source = this, Path = "FontAttributes" });
            Label.SetBinding(Label.FontFamilyProperty, new Binding { Source = this, Path = "FontFamily" });
            Label.SetBinding(Label.FontSizeProperty, new Binding { Source = this, Path = "FontSize" });
            Label.SetBinding(Label.MarginProperty, new Binding { Source = this, Path = "TextPadding" });
            Label.LineBreakMode = LineBreakMode.WordWrap;
            Label.HorizontalTextAlignment = TextAlignment.Center;
        }

        protected internal void UpdateTapGesture()
        {
            this.GestureRecognizers.Clear();
            this.Label.GestureRecognizers.Clear();
            this.Image.GestureRecognizers.Clear();
            this.Selector.GestureRecognizers.Clear();
            
            if (UseTapGesture)
            {
                var gesture = new TapGestureRecognizer();
                gesture.NumberOfTapsRequired = 1;
                gesture.Tapped += OnViewTapped;

                this.GestureRecognizers.Add(gesture); 
            }
		}
        public event EventHandler<EventArgs> Tapped;
        public event EventHandler Clicked;

        private void ForEachSibiling(Action<ImageButton> action)
        {
            var layout = this.Parent as Layout<View>;
            if (layout != null)
            {
                // deselect siblings of this button
                foreach (var child in layout.Children)
                {
                    var button = child as ImageButton;
                    if (button != null && button.Id != this.Id && button.IsSelectable)
                    {
                        action(button);
                    }
                }
            }
        }
        public void Select()
        { 
            // deselect siblings of this button
            ForEachSibiling((button) =>
            {
                button.IsSelectionActive = false;
            });

            this.IsSelectionActive = true; 
        }
           
        public virtual void OnViewTapped(object sender, EventArgs eventArgs)
        {
            if (this.IsSelectable && this.IsSelectionActive)
            {
                return;
            }

            if (!this.IsClickable)
            { 
                return;  
            }  
            
            if (this.IsSelectable)
            {
                this.IsClickable = false;
                ForEachSibiling((button) =>
                {
                    button.IsClickable = false;
                    button.IsSelectionActive = false;
                });

                this.IsSelectionActive = true;
            }
             
            Logs.All(this, " detecting tap action... ");

            if (UseBackgroundTapColor)
            {
                this.BackgroundBeforeTapColor = this.BackgroundColor;
                this.BackgroundColor = this.BackgroundTapColor;

                StartTimer();
            }
           
            if (Clicked != null)
                Clicked(this, EventArgs.Empty);

            if (Tapped != null)
                Tapped(this, EventArgs.Empty);

            if (this.Command != null &&
                this.Command.CanExecute(this.CommandParameter))
            {
                Logs.Trace(this, " executing command... ");
                this.Command.Execute(this.CommandParameter);
            }
             
            this.IsClickable = true; 
            
            if (this.IsSelectable)
            {
                // allow clicks on all siblings of this button 
                ForEachSibiling((button) =>
                {
                    button.IsClickable = true;
                });
            }          
        }
        protected bool IsTimerRunning = false;
        private void StartTimer()
        {
            if (!IsTimerRunning)
            {
                IsTimerRunning = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(80), () =>
                {
                    OnRemovalTimerTick(this, new EventArgs());
                    return IsTimerRunning;
                });
            }
        }
        private void OnRemovalTimerTick(object sender, EventArgs e)
        {
            if (!IsTimerRunning) return;
            this.BackgroundColor = this.BackgroundBeforeTapColor;
            IsTimerRunning = false;
        }
       
        /// <summary> Gets or sets IsClickable </summary>
        public bool IsClickable { get; set; }

        protected bool IsUpdatingProperties = false;
       
        protected Image Image;
        protected Label Label;
        protected Grid Selector; 

        #region Tap Properties
        public static readonly BindableProperty UseTapGestureProperty = BindableProperty.Create(
                "UseTapGesture", typeof(bool), typeof(ImageButton), true, BindingMode.Default, null, OnUseTapGestureChanged);
        public bool UseTapGesture
        {
            get { return (bool)GetValue(UseTapGestureProperty); }
            set { SetValue(UseTapGestureProperty, value); }
        }
        private static void OnUseTapGestureChanged(
            BindableObject bindable, object oldValue, object newValue)
        {
            var owner = bindable as ImageButton;
            if (owner == null) return;

            owner.UpdateTapGesture();
        }

        public static readonly BindableProperty UseBackgroundTapColorProperty = BindableProperty.Create(
                "UseBackgroundTapColor", typeof(bool), typeof(ImageButton), true);    
        public bool UseBackgroundTapColor
        {
            get { return (bool)GetValue(UseBackgroundTapColorProperty); }
            set { SetValue(UseBackgroundTapColorProperty, value); }
        }

        protected Color BackgroundBeforeTapColor;
        
        public static readonly BindableProperty BackgroundTapColorProperty = BindableProperty.Create(
                "BackgroundTapColor", typeof(Color), typeof(ImageButton), Colors.DodgerBlue);     
        public Color BackgroundTapColor
        {
            get { return (Color)GetValue(BackgroundTapColorProperty); }
            set { SetValue(BackgroundTapColorProperty, value); }
        } 
        #endregion

        #region Image Properties
        public static readonly BindableProperty ImagePathProperty = BindableProperty.Create(
            "ImagePath", typeof(string), typeof(ImageButton), null);
        //"Image", typeof(string), typeof(ImageButton), null, BindingMode.Default, null, OnImageSourceChanged);
        //"Image", typeof(ImageSource), typeof(ImageButton), null);
        //  BindableProperty.Create<ImageButton, FileImageSource>(o => o.Image,
        //     default(FileImageSource), BindingMode.Default);
        //ImageSource

        //[TypeConverter(typeof(StringToImageResource))]
        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }
          
        public static readonly BindableProperty ImageAspectProperty = BindableProperty.Create(
                "ImageAspect", typeof(Aspect), typeof(ImageButton), default(Aspect));
        public Aspect ImageAspect
        {
            get { return (Aspect)GetValue(ImageAspectProperty); }
            set { SetValue(ImageAspectProperty, value); }
        }
        public static readonly BindableProperty ImageHorizontalOptionsProperty = BindableProperty.Create(
                "ImageHorizontalOptions", typeof(LayoutOptions), typeof(ImageButton), LayoutOptions.Center);
        public LayoutOptions ImageHorizontalOptions
        {
            get { return (LayoutOptions)GetValue(ImageHorizontalOptionsProperty); }
            set { SetValue(ImageHorizontalOptionsProperty, value); }
        }

        public static readonly BindableProperty ImageVerticalOptionsProperty = BindableProperty.Create(
                "ImageVerticalOptions", typeof(LayoutOptions), typeof(ImageButton), LayoutOptions.Center);
        public LayoutOptions ImageVerticalOptions
        {
            get { return (LayoutOptions)GetValue(ImageVerticalOptionsProperty); }
            set { SetValue(ImageVerticalOptionsProperty, value); }
        }


        protected static Thickness ImagePaddingDefault = new Thickness(5, 5, 5, 5);
        public static readonly BindableProperty ImagePaddingProperty = BindableProperty.Create(
            "ImagePadding", typeof(Thickness), typeof(ImageButton), ImagePaddingDefault);
        public Thickness ImagePadding
        {
            get { return (Thickness)GetValue(ImagePaddingProperty); }
            set { SetValue(ImagePaddingProperty, value); }
        }

        #endregion

        public static readonly BindableProperty TagProperty = BindableProperty.Create(
                "Tag", typeof(object), typeof(ImageButton), "Tag");   
        public object Tag
        {
            get { return GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }

        #region Selection Properties
        #endregion

        #region Selection Properties
        public static readonly BindableProperty SelectionBarColorProperty = BindableProperty.Create(
           "SelectionBarColor", typeof(Color), typeof(ImageButton), Color.Blue);
        public Color SelectionBarColor
        {
            get { return (Color)GetValue(SelectionBarColorProperty); }
            set { SetValue(SelectionBarColorProperty, value); }
        }

        public static readonly BindableProperty SelectionBarSizeProperty = BindableProperty.Create(
           "SelectionBarSize", typeof(double), typeof(ImageButton), 5.0);
        public double SelectionBarSize
        {
            get { return (double)GetValue(SelectionBarSizeProperty); }
            set { SetValue(SelectionBarSizeProperty, value); }
        }

        public static readonly BindableProperty SelectionFontAttributesProperty = BindableProperty.Create(
           "SelectionFontAttributes", typeof(FontAttributes), typeof(ImageButton), FontAttributes.Bold);
        public FontAttributes SelectionFontAttributes
        {
            get { return (FontAttributes)GetValue(SelectionFontAttributesProperty); }
            set { SetValue(SelectionFontAttributesProperty, value); }
        }

        public static readonly BindableProperty IsSelectionActiveProperty = BindableProperty.Create(
           "IsSelectionActive", typeof(bool), typeof(ImageButton), false);
        public bool IsSelectionActive
        {
            get { return (bool)GetValue(IsSelectionActiveProperty); }
            set { SetValue(IsSelectionActiveProperty, value); }
        }

        public static readonly BindableProperty IsSelectableProperty = BindableProperty.Create(
          "IsSelectable", typeof(bool), typeof(ImageButton), false);
        public bool IsSelectable
        {
            get { return (bool)GetValue(IsSelectableProperty); }
            set { SetValue(IsSelectableProperty, value); }
        }  
        #endregion

        #region Label Properties

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            "Text", typeof(string), typeof(ImageButton), "ImageButton");      
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            "TextColor", typeof(Color), typeof(ImageButton), Color.Black);
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
         
        protected static Thickness TextPaddingDefault = new Thickness(5, 10, 5, 10);
        public static readonly BindableProperty TextPaddingProperty = BindableProperty.Create( 
            "TextPadding", typeof(Thickness), typeof(ImageButton), TextPaddingDefault);
        public Thickness TextPadding
        {
            get { return (Thickness)GetValue(TextPaddingProperty); }
            set { SetValue(TextPaddingProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            "FontFamily", typeof(string), typeof(ImageButton), null);
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
            "FontAttributes", typeof(FontAttributes), typeof(ImageButton), FontAttributes.None);
        public FontAttributes FontAttributes
        {
            get { return (FontAttributes)GetValue(FontAttributesProperty); }
            set { SetValue(FontAttributesProperty, value); }
        } 

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            "FontSize", typeof(double), typeof(ImageButton), AppFonts.SizeDefault);
        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        #endregion

        #region Command Properties
        public static BindableProperty CommandParameterProperty = BindableProperty.Create(
                "CommandParameter", typeof(object), typeof(ImageButton), null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
         
        public static BindableProperty CommandProperty = BindableProperty.Create(
                "Command", typeof(Command<string>), typeof(ImageButton), null);
        public Command<string> Command
        {
            get { return (Command<string>)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }


        #endregion

        protected void UpdateSelection()
        {
            var labelBinding = new Binding { Source = this };

            if (this.IsSelectionActive)
                labelBinding.Path = "SelectionFontAttributes";
            else
                labelBinding.Path = "FontAttributes"; 

            this.Label.SetBinding(Label.FontAttributesProperty, labelBinding); 
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            //Logs.Trace(this.GetType() + ".OnPropertyChanged '" + propertyName + "'");
            base.OnPropertyChanged(propertyName);

            this.IsUpdatingProperties = true;
                    
            if (this.Label != null)
            {
                if (propertyName == "Text")
                {
                    this.Label.Text = this.Text;
                    this.Label.IsVisible = !string.IsNullOrEmpty(this.Text) && string.IsNullOrEmpty(this.ImagePath);
                }
                else if (propertyName == "IsSelectionActive")
                {
                    UpdateSelection();
                }
                //else if (propertyName == "TextColor")                
                //    this.Label.TextColor = this.TextColor;
                //else if (propertyName == "TextBackground")
                //    this.Label.BackgroundColor = this.TextBackground;
                //else if (propertyName == "TextPadding")                
                //     this.Label.Margin = this.TextPadding;                
                //else if (propertyName == "FontSize")                
                //    this.Label.FontSize = this.FontSize;                
                //else if (propertyName == "FontAttributes")                
                //    this.Label.FontAttributes = this.FontAttributes;                
                //else if (propertyName == "FontFamily")                
                //    this.Label.FontFamily = this.FontFamily;                
            }

            if (this.Image != null)
            {
                if (propertyName == "ImagePath")
                {
                    //Logs.Trace(this.GetType() + ".OnPropertyChanged '" + propertyName + "'");
                    this.Image.Source = StringToImageResource.GetImageSource(this.ImagePath); 
                    this.Image.IsVisible = !string.IsNullOrEmpty(this.ImagePath);
                    this.Label.IsVisible =  string.IsNullOrEmpty(this.ImagePath) && !string.IsNullOrEmpty(this.Text);
                }
                else if (propertyName == "ImageAspect")
                    this.Image.Aspect = this.ImageAspect;
                else if (propertyName == "ImagePadding")
                    this.Image.Margin = this.ImagePadding;
            }
            
            if (this.Selector != null)
            {
                if (propertyName == "IsSelectable") 
                {
                    if (!this.IsSelectable)
                    {
                        this.Selector.IsVisible = false;
                        this.Label.SetBinding(Label.FontAttributesProperty, new Binding { Source = this, Path = "FontAttributes" });
                    }
                    else
                    {
                        this.Selector.SetBinding(Grid.IsVisibleProperty, new Binding { Source = this, Path = "IsSelectionActive" });

                        UpdateSelection();
                    }
                } 
            }

            this.IsUpdatingProperties = false;
        }

    }
}