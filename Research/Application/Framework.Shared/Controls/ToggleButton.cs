using System;  
using Xamarin.Forms; 

namespace Infragistics.Framework
{
    public class ToggleButton : ImageButton
    {
        public ToggleButton()
        {
            UseBackgroundTapColor = false;

            this.BackgroundUnToggleColor = Colors.Transparent;
            this.BackgroundToggleColor = Color.FromHex("#FF292829");

            this.Text = ""; 
        }

        public override void OnViewTapped(object sender, EventArgs eventArgs)
        {
            base.OnViewTapped(sender, eventArgs);

            if (this.IsTogglable)
                this.IsToggled = !this.IsToggled;

            //this.Text = this.IsToggled + " " + ++ClickCount;
            
        }

        protected internal bool IsUpdatingToggle = false;
         
        protected override void OnPropertyChanged(string propertyName = null)
        {
#if !__IOS__
            // TFS 211508. Seems setting BackgroundColor in parent rises an exception in Xamarin
            if (propertyName != "BackgroundColor")
            {
                base.OnPropertyChanged(propertyName);
            }
#endif

            // handle user-setting for BackgroundColor
            if (propertyName == "BackgroundColor" && !this.IsUpdatingToggle)
                this.BackgroundUnToggleColor = this.BackgroundColor;

            this.IsUpdatingToggle = true;
         
            if (propertyName == "IsToggled" ||
                propertyName == "BackgroundToggleColor" ||
                propertyName == "BackgroundUnToggleColor")
            {
                if (this.IsToggled)
                {
                    this.BackgroundColor = this.BackgroundToggleColor;
                }
                else
                {
                    this.BackgroundColor = this.BackgroundUnToggleColor;
                }
            }

            this.IsUpdatingToggle = false;    
        }
     

        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(
                "IsToggled", typeof(bool), typeof(ToggleButton), false);
      //  BindableProperty.Create<ToggleButton, bool>(o => o.IsToggled,
       //     false, BindingMode.Default);

        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }

        public static readonly BindableProperty IsTogglableProperty = BindableProperty.Create(
            nameof(IsTogglable), typeof(bool), typeof(ToggleButton), true);

       // BindableProperty.Create<ToggleButton, bool>(o => o.IsTogglable,
       //     true, BindingMode.Default);

        public bool IsTogglable
        {
            get { return (bool)GetValue(IsTogglableProperty); }
            set { SetValue(IsTogglableProperty, value); }
        }

        protected internal Color BackgroundUnToggleColor;
        
        public static readonly BindableProperty ToggleColorBackgroundProperty = BindableProperty.Create(
                "BackgroundToggleColor", typeof(Color), typeof(ToggleButton), Colors.Red);
      //  BindableProperty.Create<ToggleButton, Color>(o => o.BackgroundToggleColor,
         //    Colors.Red, BindingMode.Default);

        public Color BackgroundToggleColor
        {
            get { return (Color)GetValue(ToggleColorBackgroundProperty); }
            set { SetValue(ToggleColorBackgroundProperty, value); }
        }

       


    }
}