using System; 
using System.Collections.Generic;  
using Xamarin.Forms;

namespace Infragistics.Framework
{
    public enum SettingsAnimation
    {
        FromLeft,
        FromBottom,
        FromRight,
        FromTop,
        FromTopLeft,
        FromTopRight,
        FromBottomLeft,
        FromBottomRight,
    }

    public interface IHasSettingsPopup
    {
        SettingsPopup Popup { get; } 
    }

    public partial class SettingsPopup : Grid
    {
		public SettingsPopup()
		{
			InitializeComponent();
             
            var closingViews = new List<View> { this };
            closingViews.Add(new Command(() =>
            {
                this.IsOpen = false;
            }));

            this.IsVisible = false;
            this.PropertyChanged += SettingsPopup_PropertyChanged;
            Close(0);
        }

        private void SettingsPopup_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOpen")
            {
                if (this.IsOpen) Open(); else Close();
            }
            else if (e.PropertyName == "Style")
            {
                OnPropertyChanged("TitleStyle");
            }
        }
        #region Methods
        public void Toggle()
        {
            this.IsOpen = !this.IsOpen;
        }

        public void Open(uint miliseconds = 250)
        {
            this.IsVisible = true;
            this.TranslateTo(0, 0, miliseconds, Easing.CubicIn);
        }
        public void Close(uint miliseconds = 250)
        {
            var x = 0.0;
            var y = 0.0;

            if (Animation == SettingsAnimation.FromBottom ||
                Animation == SettingsAnimation.FromBottomLeft ||
                Animation == SettingsAnimation.FromBottomRight)
                y = this.Height * 1.25;

            if (Animation == SettingsAnimation.FromTop ||
                Animation == SettingsAnimation.FromTopLeft ||
                Animation == SettingsAnimation.FromTopRight)
                y = this.Height * -1.25;

            if (Animation == SettingsAnimation.FromLeft ||
                Animation == SettingsAnimation.FromTopLeft ||
                Animation == SettingsAnimation.FromBottomLeft)
                x = this.Width * -1.25;

            if (Animation == SettingsAnimation.FromRight ||
                Animation == SettingsAnimation.FromTopRight ||
                Animation == SettingsAnimation.FromBottomRight)
                x = this.Width * 1.25;

            this.TranslateTo(x, y, miliseconds, Easing.CubicIn);
        }
        #endregion

        #region Properties
        public static readonly BindableProperty AnimationProperty = BindableProperty.Create(
                "Animation", typeof(SettingsAnimation), typeof(SettingsPopup), SettingsAnimation.FromRight);
        public SettingsAnimation Animation
        {
            get { return (SettingsAnimation)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }

        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
                  "IsOpen", typeof(bool), typeof(SettingsPopup), false);
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static Thickness DefaultBoarder = new Thickness(2, 2, 2, 2);
        public static readonly BindableProperty BoarderThicknessProperty = BindableProperty.Create(
                "BoarderThickness", typeof(Thickness), typeof(SettingsPopup), DefaultBoarder);
        public Thickness BoarderThickness
        {
            get { return (Thickness)GetValue(BoarderThicknessProperty); }
            set { SetValue(BoarderThicknessProperty, value); }
        }
         
        public static readonly BindableProperty BoarderColorProperty = BindableProperty.Create(
                "BoarderColor", typeof(Color), typeof(SettingsPopup), Color.FromHex("#FF828181"));
        public Color BoarderColor
        {
            get { return (Color)GetValue(BoarderColorProperty); }
            set { SetValue(BoarderColorProperty, value); }
        }

        public static readonly BindableProperty ContentProperty = BindableProperty.Create(
                  "Content", typeof(View), typeof(SettingsPopup), null);
        public View Content
        {
            get { return (View)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly BindableProperty ContentHorizontalOptionsProperty = BindableProperty.Create(
                  "ContentHorizontalOptions", typeof(LayoutOptions), typeof(SettingsPopup), LayoutOptions.FillAndExpand);
        public LayoutOptions ContentHorizontalOptions
        {
            get { return (LayoutOptions)GetValue(ContentHorizontalOptionsProperty); }
            set { SetValue(ContentHorizontalOptionsProperty, value); }
        }

        public static readonly BindableProperty ContentVerticalOptionsProperty = BindableProperty.Create(
                  "ContentVerticalOptions", typeof(LayoutOptions), typeof(SettingsPopup), LayoutOptions.FillAndExpand);
        public LayoutOptions ContentVerticalOptions
        {
            get { return (LayoutOptions)GetValue(ContentVerticalOptionsProperty); }
            set { SetValue(ContentVerticalOptionsProperty, value); }
        }


        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            "Title", typeof(string), typeof(SettingsPopup), "Title");
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty TitleStyleProperty = BindableProperty.Create(
             "TitleStyle", typeof(Style), typeof(SettingsPopup), null);
        public Style TitleStyle
        {
            get { return (Style)GetValue(TitleStyleProperty); }
            set { SetValue(TitleStyleProperty, value); }
        }

         
 
        #endregion

        
    }


}
