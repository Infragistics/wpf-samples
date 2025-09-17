using System;
using Xamarin.Forms;

namespace Infragistics.Framework
{ 
    /// <summary>
    /// Represents an extension for Xamarin <see cref="StackLayout"/> control with tap gesture support
    /// </summary>
    public class ExStackLayout : StackLayout, ITapGesture
    {
        public ExStackLayout()
        {
            var gesture = new TapGestureRecognizer(); 
            gesture.Tapped += OnViewTapped;
            this.GestureRecognizers.Add(gesture);
            this.BackgroundColor = Colors.Transparent;
        }

        /// <summary> Occurs when a user tap gesture on this control </summary>
        public event EventHandler<EventArgs> Tapped;
        public virtual void OnViewTapped(object sender, EventArgs e)
        {
            if (Tapped != null)
                Tapped(this, e);
        }
    }


}