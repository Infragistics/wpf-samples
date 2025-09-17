using System;
using Xamarin.Forms;

namespace Infragistics.Framework
{
    public class NumericLabel : Label
    {

        public static readonly BindableProperty TextValueProperty = BindableProperty.Create(
            nameof(TextValue), typeof(double), typeof(NumericLabel), 0.0,
            BindingMode.Default, null, OnTextValuePropertyChanged);
        // <NumericLabel, double>(o => o.TextValue, 0.0, BindingMode.Default, null, OnTextValuePropertyChanged);
         

        /// <summary> Gets or sets Data </summary>
        public double TextValue
        {
            get { return (double)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }

        private static void OnTextValuePropertyChanged(BindableObject bindable,
                                        object oldVal, object newVal)
        {
            var owner = bindable as NumericLabel;
            if (owner == null) return;

            var oldValue = (double)oldVal;
            var newValue = (double)newVal;

            if (double.IsNaN(newValue) || double.IsInfinity(newValue))
            {
                owner.Text = "NaN"; return;
            }
            if (double.IsNaN(oldValue) || double.IsInfinity(oldValue))
            {
                owner.Text = "NaN"; return;
            }
            var span = newValue - oldValue;
            var interval = span / 2;

            Logs.Trace(owner, "changing value to: " + newValue.ToStringShort("."));

            for (var i = oldValue; i < newValue; i += interval)
            {
                var text = i.ToStringShort(".");

                Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
                {
                    Logs.Trace(owner, "updating text  to: " + text);
                    owner.Text = text;
                    owner.InvalidateMeasure();

                    //stop timer 
                    return false;
                });
            }
        }


    }
	
}