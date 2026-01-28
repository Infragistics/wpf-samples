using System;
using Xamarin.Forms;

namespace Infragistics.Framework
{ 
    /// <summary>
    /// Represents an extension for Xamarin <see cref="ContentView"/> control with tap gesture support
    /// </summary>
    public class ExContentView : ContentView, ITapGesture
    {
        public ExContentView()
        {
            UpdateGestures();
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
                "UseTapGesture", typeof(bool), typeof(ExContentView), true, BindingMode.Default, null, OnUseTapGestureChanged);
     //   BindableProperty.Create<ExContentView, bool>(o => o.UseTapGesture,
      //      true, BindingMode.Default, null, OnUseTapGestureChanged);

        public bool UseTapGesture
        {
            get { return (bool)GetValue(UseTapGestureProperty); }
            set { SetValue(UseTapGestureProperty, value); }
        }
        private static void OnUseTapGestureChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var owner = bindable as ExContentView;
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
    }
}