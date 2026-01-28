using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Infragistics.Framework
{
    public class PropEditor : UserControl, IPropEditor
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(PropEditor), new PropertyMetadata(null));

        public virtual void RaisePropertyChanged(DependencyPropertyChangedEventArgs e)
        {

        }

        //public static DependencyProperty Reg(string name, Type propertyType, Type controlType, object defaultValue)
        //{
        //    // IPropEditor ownerType, 
        //    return DependencyProperty.Register(
        //    name, propertyType, controlType,
        //    new PropertyMetadata(defaultValue, (s, e) =>
        //    {
        //        ((IPropEditor)s).RaisePropertyChanged(e);
        //    })); 
        //}
    }

    public interface IPropEditor
    {
        void RaisePropertyChanged(DependencyPropertyChangedEventArgs e);
    }

    public class PropMetadata<T> : PropertyMetadata where T : IPropEditor
    {
        public PropMetadata(object defaultValue)
           : base(defaultValue, (s, e) =>
           {
               ((IPropEditor)s).RaisePropertyChanged(e);
           })
        {

        }
    }

}
