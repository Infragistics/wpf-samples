using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Infragistics.Framework
{
    public class PropSliderEditor : PropEditor
    {
        #region Properties
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", typeof(double), typeof(PropSliderEditor), new PropertyMetadata(double.NaN));

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof(double), typeof(PropSliderEditor), new PropertyMetadata(double.NaN));

        public string FormattedValue
        {
            get { return (string)GetValue(FormattedValueProperty); }
            internal set { SetValue(FormattedValueProperty, value); }
        }
        public static readonly DependencyProperty FormattedValueProperty = DependencyProperty.Register(
            "FormattedValue", typeof(string), typeof(PropSliderEditor), new PropertyMetadata("NaN"));
        
        //public int Interval
        //{
        //    get { return (int)GetValue(IntervalProperty); }
        //    set { SetValue(IntervalProperty, value); }
        //}
        //public static readonly DependencyProperty IntervalProperty = DependencyProperty.Register(
        //    "Interval", typeof(int), typeof(PropSliderEditor), new PropertyMetadata(-1));

        #endregion
    }

}
