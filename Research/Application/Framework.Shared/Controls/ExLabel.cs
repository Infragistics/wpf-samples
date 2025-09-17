using System;
using Xamarin.Forms;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents an extension for Xamarin <see cref="Label"/> control with option to set custom font
    /// </summary>
    public class ExLabel : Label, ITapGesture
    {
        public ExLabel()
        {
            this.BackgroundColor = Colors.Transparent;
        }
        /// <summary> Occurs when a user tap gesture on this control </summary>
        public event EventHandler<EventArgs> Tapped;
        public virtual void OnViewTapped(object sender, EventArgs e)
        {
            if (Tapped != null)
                Tapped(this, e);
        }

        #region Property - UseTapGesture

        public static readonly BindableProperty UseTapGestureProperty = BindableProperty.Create(
                "UseTapGesture", typeof(bool), typeof(ExLabel), false, BindingMode.Default, null, OnUseTapGestureChanged);
       // BindableProperty.Create<ExLabel, bool>(o => o.UseTapGesture,
       //     false, BindingMode.Default, null, OnUseTapGestureChanged);
         
        public bool UseTapGesture
        {
            get { return (bool)GetValue(UseTapGestureProperty); }
            set { SetValue(UseTapGestureProperty, value); }
        }
        private static void OnUseTapGestureChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var owner = bindable as ExLabel;
            if (owner == null) return;

            owner.UpdateGestures();
        }
        protected void UpdateGestures()
        {
            this.GestureRecognizers.Clear();
            if (UseTapGesture)
            {
                var gesture = new TapGestureRecognizer();
                gesture.Tapped += OnViewTapped;
                this.GestureRecognizers.Add(gesture);
            }
        }
        #endregion

        #region Property - FontCustom
        public static readonly BindableProperty FontCustomProperty = BindableProperty.Create(
                "FontCustom", typeof(FontCustom), typeof(ExLabel), null);
      //  BindableProperty.Create<ExLabel, FontCustom>(o => o.FontCustom,
      //      null, BindingMode.Default, null, null);

        /// <summary> Gets or sets a custom font 
        /// <para>Note that the font file must be place in \Assets\Fonts folder under app projects:  </para>
        /// <para>App.iOS - with BoundleResource build action and "Do not copy" </para>
        /// <para>App.Android - with AndroidAsset build action and "Do not copy" </para> 
        /// <para>App.WinPhone - with Content build action and "Copy if newer" </para> </summary>
        public FontCustom FontCustom
        {
            get { return (FontCustom)GetValue(FontCustomProperty); }
            set { SetValue(FontCustomProperty, value); }
        }
        #endregion
    }

    /// <summary>
    /// Represents a class for storing information about custom font
    /// </summary>
    public class FontCustom
    {
        /// <summary> Gets or sets a file name of custom font with .ttf file extension
        /// <para>Note that the font file must be place under app projects:  </para>
        /// <para>App.iOS -  Resources/Fonts with BoundleResource build action and "Do not copy" </para>
        /// <para>App.Android - with AndroidAsset build action and "Do not copy" </para> 
        /// <para>App.WinPhone - Assets/Fonts with Content build action and "Copy if newer" </para> </summary>
        public string File { get; set; }

        /// <summary>
        /// Gets or sets 'preferred family' of custom font. This is required WinPhone platform only 
        /// <para>Note that preferred family is not always the same as title </para>
        /// <para> and you can find preferred family using the Font Viewer application</para>
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets 'postscript name' of custom font. This is required for iOS platform only 
        /// <para>Note that postscript name is not always the same as font title </para>
        /// <para> and you can find postscript name using the Font Viewer application</para>
        /// </summary>
        public string Script { get; set; }
    }
     
    public interface ITapGesture
    {
        event EventHandler<EventArgs> Tapped;
        void OnViewTapped(object sender, EventArgs e);
    }
}