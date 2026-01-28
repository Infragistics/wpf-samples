using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Infragistics.Framework
{
	public partial class ListPopup : Grid
    {
		public ListPopup ()
		{
			InitializeComponent(); 
             
            this.ContentGird.BindingContext = this;

            var closingViews = new List<View> { this, CancelButton };
            closingViews.Add(new Command(() =>
            {
                SelectedItem = OrignalItem;
                AcceptedItem = OrignalItem;  
                Close();
            }));

            AcceptButton.Add(new Command(() =>
            {
                OrignalItem = SelectedItem;
                AcceptedItem = SelectedItem; 
                Close();
            })); 
        }

        protected object OrignalItem = null; 
        public void Show(VisualElement popup = null)
        {
            OrignalItem = SelectedItem;
            if (OrignalItem != null && IsValid(OrignalItem)) 
                ListView.ScrollTo(OrignalItem, ScrollToPosition.Center, true);
            if (popup == null) popup = this;

            popup.TranslateTo(0, 0, 250, Easing.CubicIn);
        }
        public void Close(VisualElement popup = null)
        {
            if (popup == null) popup = this;

            popup.TranslateTo(0, Height, 250, Easing.CubicIn);
        }

        #region Properties

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
                  "ItemsSource", typeof(IEnumerable), typeof(ListPopup), null);
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
                 "ItemTemplate", typeof(DataTemplate), typeof(ListPopup), null);
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
                 "SelectedItem", typeof(object), typeof(ListPopup), null);
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { if (!IsValid(value)) return; SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty AcceptedItemProperty = BindableProperty.Create(
                "AcceptedItem", typeof(object), typeof(ListPopup), null);
        public object AcceptedItem
        {
            get { return GetValue(AcceptedItemProperty); }
            set { SetValue(AcceptedItemProperty, value); }
        }

        public bool IsValid(object targetItem)
        {
            if (targetItem == null) return true;
            if (targetItem.ToString().Equals(""))
            { 
                return false;
            }
            if (this.ItemsSource == null) return false;

            foreach (var item in this.ItemsSource)
            {
                if (item.Equals(targetItem))
                    return true;
            }
            return false; 
        }


        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
                "HeaderText", typeof(string), typeof(ListPopup), "Select Item");
        public object HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public static readonly BindableProperty CancelTextProperty = BindableProperty.Create(
                "CancelText", typeof(string), typeof(ListPopup), "Cancel");
        public object CancelText
        {
            get { return (string)GetValue(CancelTextProperty); }
            set { SetValue(CancelTextProperty, value); }
        }

        public static readonly BindableProperty AcceptTextProperty = BindableProperty.Create(
                "AcceptText", typeof(string), typeof(ListPopup), "OK");
        public object AcceptText
        {
            get { return (string)GetValue(AcceptTextProperty); }
            set { SetValue(AcceptTextProperty, value); }
        }
        #endregion

        
    }


}
