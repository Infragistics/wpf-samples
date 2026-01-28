using System;
using System.Collections;
using Xamarin.Forms;

namespace Infragistics.Framework 
{
    /// <summary>
    /// Represents an extension for Xamarin <see cref="Picker"/> control with bindable ItemsSource property
    /// </summary>
    public class BindablePicker : Picker
    {
        public BindablePicker()
        {
            this.SelectedIndexChanged += OnSelectedIndexChanged;
            this.PropertyChanged += BindablePicker_PropertyChanged;
        }

        private void BindablePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ItemsSource")
            { 
                UpdatePickerItems(this, ItemsSource, ItemsMemberPath);
            }
        }

        //public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        //    "ItemsSource", typeof(IEnumerable), typeof(NumericLabel), default(IEnumerable), BindingMode.Default, null, OnItemsSourceChanged);
        //// <BindablePicker, IEnumerable>(o => o.ItemsSource,  default(IEnumerable), BindingMode.Default, null, OnItemsSourceChanged);
         
        public static readonly BindableProperty ItemsMemberPathProperty = BindableProperty.Create(
               "ItemsMemberPath", typeof(string), typeof(NumericLabel), null, BindingMode.Default, null, OnItemsMemberPathChanged);
      //   <BindablePicker, string>(o => o.ItemsMemberPath,
       //     null, BindingMode.Default, null, OnItemsMemberPathChanged);

        //public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        //         "SelectedItem", typeof(object), typeof(NumericLabel), default(object), BindingMode.Default, null, OnSelectedItemChanged);
        ////  <BindablePicker, object>(o => o.SelectedItem, 
        ////    default(object), BindingMode.Default, null, OnSelectedItemChanged);

        public string ItemsMemberPath
        {
            get { return (string)GetValue(ItemsMemberPathProperty); }
            set { SetValue(ItemsMemberPathProperty, value); }
        }

        //public IEnumerable ItemsSource
        //{
        //    get { return (IEnumerable)GetValue(ItemsSourceProperty); }
        //    set { SetValue(ItemsSourceProperty, value); }
        //}

        //public object SelectedItem
        //{
        //    get { return GetValue(SelectedItemProperty); }
        //    set { SetValue(SelectedItemProperty, value); }
        //}

        private static void OnItemsMemberPathChanged(BindableObject bindable,
            object oldValue, object newValue)
        {
            var owner = bindable as BindablePicker;
            if (owner == null) return;

            UpdatePickerItems(owner, owner.ItemsSource, (string)newValue);
        }

        static void OnItemsSourceChanged(BindableObject bindable, 
            object oldValue, object newValue)
        {
            var owner = bindable as BindablePicker;
            if (owner == null) return;

            UpdatePickerItems(owner, (IEnumerable)newValue, owner.ItemsMemberPath);  
        }

        private static void UpdatePickerItems(BindableObject bindable, 
            IEnumerable itemsSource, string itemsMemberPath)
        {
            var owner = bindable as BindablePicker;
            if (owner == null) return;

            if (itemsSource != null)  
            {
                owner.Items.Clear(); 

                foreach (var item in itemsSource)
                {
                    if (string.IsNullOrEmpty(itemsMemberPath))
                    {
                        owner.Items.Add(item.ToString());
                    }
                    else
                    {
                        var itemValue = item.GetPropertyValue(itemsMemberPath);
                        owner.Items.Add(itemValue.ToString());
                    }
                    // TODO get property value based on item's property named: ItemsMemberPath
                    // public static object GetPropValue(object src, string propName)
                    //{
                    //    return src.GetType().GetProperty(propName).GetValue(src, null);
                    //}
                    //var itemValue = item.Get
                    //picker.Items.Add(item.ToString());
                }
            }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = Items[SelectedIndex];
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as BindablePicker;
            if (picker != null && newvalue != null)
            {
                picker.SelectedIndex = picker.Items.IndexOf(newvalue.ToString());
            }
        }
    }

}