using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Infragistics.Framework
{ 
    public partial class PropValueEditor : PropSliderEditor
    {
        public PropValueEditor()
        {
            InitializeComponent();
        } 
        public int Precision
        {
            get { return (int)GetValue(PrecisionProperty); }
            set { SetValue(PrecisionProperty, value); }
        }
        public static readonly DependencyProperty PrecisionProperty = DependencyProperty.Register(
            "Precision", typeof(int), typeof(PropValueEditor), new PropMetadata<PropValueEditor>(1));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(PropValueEditor), new PropMetadata<PropValueEditor>(double.NaN));

        public bool IsInteger
        {
            get { return (bool)GetValue(IsIntegerProperty); }
            set { SetValue(IsIntegerProperty, value); }
        }
        public static readonly DependencyProperty IsIntegerProperty = DependencyProperty.Register(
            "IsInteger", typeof(bool), typeof(PropValueEditor), new PropertyMetadata(false));

        //public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        //    "Value", typeof(double), typeof(PropValueEditor),
        //    new PropertyMetadata(double.NaN, (s, e) =>
        //    {
        //        ((PropValueEditor)s).RaisePropertyChanged(e);
        //    }));

        public override void RaisePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.RaisePropertyChanged(e);

            System.Diagnostics.Debug.WriteLine("PropValueEditor " + e.Property.Name);

            if (e.Property.Name == "Value" || e.Property.Name == "Precision")
            {
                var decimals = string.Concat(System.Linq.Enumerable.Repeat("0", Precision));
                FormattedValue = Value.ToString("0." + decimals);
            }

            if (e.Property.Name == "Interval")
            {
              //  IsInteger = Interval == 1;
            }

            if (e.Property.Name == "IsInteger")
            {
                if (IsInteger)
                {

                }
                else
                {

                }
                //  IsInteger = Interval == 1;
            }
        } 
    }

    public class PropIntegerEditor : PropValueEditor
    {
        public PropIntegerEditor()
        { 
            IsInteger = true;
        }
    }





}
