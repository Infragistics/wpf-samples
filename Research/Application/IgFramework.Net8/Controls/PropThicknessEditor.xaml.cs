using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Infragistics.Framework
{ 
    public partial class PropThicknessEditor : PropSliderEditor
    {
        public PropThicknessEditor()
        {
            InitializeComponent();

            this.Minimum = 0;
            this.Maximum = 20;
        }

        public Thickness Value
        {
            get { return (Thickness)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(Thickness), typeof(PropThicknessEditor), new PropMetadata<PropThicknessEditor>(new Thickness()));


        //public double ActualValue
        //{
        //    get { return (double)GetValue(ActualValueProperty); }
        //    internal set { SetValue(ActualValueProperty, value); }
        //}
        //public static readonly DependencyProperty ActualValueProperty = DependencyProperty.Register(
        //    "ActualValue", typeof(double), typeof(PropThicknessEditor), new PropMetadata<PropThicknessEditor>(double.NaN));

        public override void RaisePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.RaisePropertyChanged(e);

            System.Diagnostics.Debug.WriteLine("PropThicknessEditor " + e.Property.Name);

            //if (e.Property.Name == "Value")
            //{
            //    ActualValue = Value == null ? double.NaN : Value.Left;

            //    FormattedValue = ActualValue.ToString("0"); //.ToString("0." + decimals);
            //}
        } 
    }

     

     


}
