using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Infragistics.Framework
{
    public class ComboBoxBase : ComboBox
    {
        public ComboBoxBase()
        {
            MinHeight = 25;
            Width = 130;
            Margin = new Thickness(0, 2, 4, 2);
            Padding = new Thickness(4, 2, 4, 2);
            BorderThickness = new Thickness(1);
            VerticalAlignment = VerticalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
        }
    }

    public class EnumComboBox : ComboBoxBase, INotifyPropertyChanged  
    {   
        public EnumComboBox()
        {
            this.SelectionChanged += EnumComboBox_SelectionChanged;
        }

        private void EnumComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.SelectedItem != null && this.SelectedItem.ToString() == "NULL")
            {
                this.SelectedItem = null;
            }
        }

        private Type _ItemType;
        /// <summary> Gets or sets ItemType </summary>
        public Type ItemType
        {
            get { return _ItemType; }
            set { if (_ItemType == value) return; _ItemType = value;
                PopulateSource(); OnPropertyChanged("ItemType"); }
        }

        public void PopulateSource()
        {
            var enums = Enum.GetValues(ItemType);
            var items = new List<object>();
            foreach (var item in enums)
            {
                items.Add(item);
            }
            //items.Add("NULL");

            this.ItemsSource = items;
        }
  
        public event PropertyChangedEventHandler PropertyChanged;         
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
            }

            //if (propertyName == "DoubleIndex")
            //{
            //    SelectedIndex = (int)DoubleIndex;
            //}
        }

        #region DoubleIndex
        public double DoubleIndex
        {
            get { return (double)GetValue(DoubleIndexProperty); }
            set { SetValue(DoubleIndexProperty, value); }
        }


        public static readonly DependencyProperty DoubleIndexProperty = DependencyProperty.Register(
                "DoubleIndex", typeof(double), typeof(EnumComboBox),
                new PropertyMetadata(new PropertyChangedCallback(OnDoubleIndexChanged)));

        private static void OnDoubleIndexChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var owner = (EnumComboBox)obj;
            //owner.Refresh();
            owner.SelectedIndex = int.Parse(e.NewValue.ToString());
        }

        //public static readonly DependencyProperty DoubleIndexProperty =
        //    DependencyProperty.Register("DoubleIndex",
        //        typeof(double),
        //        typeof(EnumComboBox),
        //        new PropertyMetadata(0.0));
        #endregion

    }

    public class EnumComboBox<TEnum> : EnumComboBox 
        where TEnum : struct, IComparable, IFormattable 
    {   
        public EnumComboBox()
		{ 
            this.ItemType = typeof(TEnum); 
		} 
        public new TEnum SelectedValue
        {
            get
            {
                return (TEnum)this.SelectedItem;
            }
            set
            {
                this.SelectedItem = value;
            }
        }
    }

    public class BooleanComboBox : EnumComboBox 
    {
        public BooleanComboBox()
        {
            this.ItemsSource = new List<bool> { false, true };
        }
    }
     
    /// <summary>
    /// Represents a ComboBox with values of TEnum as ItemsSource and converts SelectedItem to TColl<TEnum>
    /// </summary> 
    public class EnumComboBox<TEnum, TColl> : EnumComboBox<TEnum>
        where TColl : ObservableCollection<TEnum>, new()
        where TEnum : struct, IComparable, IFormattable
    {  
        public TColl SelectedItemCollection
        {
            get
            {
                var ret = new TColl();
                if (this.SelectedItem != null)
                    ret.Add((TEnum)this.SelectedItem);
                return ret;
            }
        } 

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            OnPropertyChanged("SelectedItemCollection");
        } 
    }
     
    /// <summary>
    /// Represents a ComboBox with values of TEnum as ItemsSource and converts SelectedItem to TColl<TEnum>
    /// </summary> 
    public class EnumListBox<TEnum, TColl> : EnumComboBox<TEnum>
        where TColl : ObservableCollection<TEnum>, new() 
        where TEnum : struct, IComparable, IFormattable 
    {   
        public EnumListBox()
        {   
            this.SelectedItems = new TColl();
        }

        #region SelectedItems
        public TColl SelectedItems
        {
            get { return (TColl)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems",
                typeof(TColl),
                typeof(EnumListBox<TEnum, TColl>),
                new PropertyMetadata(null));
        #endregion

        #region SelectionMode
        public SelectionMode SelectionMode
        {
            get { return (SelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(SelectionMode),
                typeof(EnumListBox<TEnum, TColl>), new PropertyMetadata(SelectionMode.Multiple));

        #endregion

        #region SelectedNames
        public string SelectedNames
        {
            get { return (string)GetValue(SelectedNamesProperty); }
            set { SetValue(SelectedNamesProperty, value); }
        }

        public static readonly DependencyProperty SelectedNamesProperty =
            DependencyProperty.Register("SelectedNames", typeof(string),
                typeof(EnumListBox<TEnum, TColl>),
                new PropertyMetadata("Select Items...")); 
        #endregion
        
        protected ListBox PopupList;  
        protected Button PopupButton; 
        protected Popup PopupDialog;
        
        public virtual void SelectAll()
        {
            if (this.PopupList != null)            
                this.PopupList.SelectAll();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            this.PopupDialog = GetTemplateChild("PopupDialog") as Popup; 
             
            this.PopupButton = GetTemplateChild("PopupButton") as Button;
            if (this.PopupButton != null)
                this.PopupButton.Click += OnPopupButtonClick;
            

            this.PopupList = GetTemplateChild("PopupList") as ListBox;
            if (this.PopupList != null)            
                this.PopupList.SelectionChanged += OnPopupListSelectionChanged;
        }
        
        private void OnPopupButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.PopupDialog != null)
                this.PopupDialog.IsOpen = !this.PopupDialog.IsOpen;
        }

        private void OnPopupListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.PopupList == null) return;
            if (this.PopupList.SelectedItems == null) return;
                        
            var selectedItems = new List<TEnum>(); 
            foreach (var item in PopupList.SelectedItems)
            {                
                selectedItems.Add((TEnum)item);
            } 
            
            OnSelectedItemsChanged(selectedItems);            
            
            if (selectedItems.Count == 0)
                this.SelectedNames = "Select Items..."; 
            else
                OnSelectedNamesChanged(selectedItems);             
        }
         
        protected virtual void OnSelectedItemsChanged(List<TEnum> selectedItems)
        { 
            var selectedEnums = new TColl();  
            foreach (var item in selectedItems)
            {                
                selectedEnums.Add(item); 
            }

            this.SelectedItems = selectedEnums; 
        } 
        protected virtual void OnSelectedNamesChanged(List<TEnum> selectedItems)
        {  
            if (selectedItems.Count == 0)
                this.SelectedNames = "Select Items...";
            else if (selectedItems.Count == 1)
                this.SelectedNames = selectedItems[0].ToString(); 
            else
                this.SelectedNames = "Selected " + selectedItems.Count + " Item(s)";                 
        } 
    }

    
}
