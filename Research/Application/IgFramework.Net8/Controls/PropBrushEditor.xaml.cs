using System;
using System.Collections.Generic;
using System.Linq; 
using System.Windows; 

namespace Infragistics.Framework
{
    public partial class PropBrushEditor : PropEditor
    {
        public PropBrushEditor()
        {
            InitializeComponent();
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex", typeof(int), typeof(PropBrushEditor), new PropertyMetadata(-1));

        //public object SelectedItem
        //{
        //    get { return (object)GetValue(SelectedItemProperty); }
        //    set { SetValue(SelectedItemProperty, value); }
        //}
        //public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
        //    "SelectedItem", typeof(object), typeof(PropBrushEditor), new PropertyMetadata(null));

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof(object), typeof(PropBrushEditor),
            new PropertyMetadata(double.NaN, (sender, e) =>
            {
                ((PropBrushEditor)sender).RaisePropertyChanged("SelectedItem", e.OldValue, e.NewValue);
            }));

        protected void RaisePropertyChanged(string propertyName, object oldValue, object newValue)
        {
            //System.Diagnostics.Debug.WriteLine("PropBrushEditor " + propertyName);

        }
    }
}
