using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows; 

namespace Infragistics.Samples.Framework
{        
    public class EnumComboBox : ComboBox, INotifyPropertyChanged  
    {   
        public EnumComboBox()
		{ 
            this.Margin = new Thickness(3);
            this.MinWidth = 120;
            this.VerticalContentAlignment = VerticalAlignment.Center;
		}

        private Type _ItemType;
        /// <summary> Gets or sets ItemType </summary>
        public Type ItemType
        {
            get { return _ItemType; }
            set { if (_ItemType == value) return; _ItemType = value;
                Populate(); OnPropertyChanged("ItemType"); }
        }

        public void Populate()
        {
            this.ItemsSource = Enum.GetValues(ItemType);
        }
  
        public event PropertyChangedEventHandler PropertyChanged;         
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));  
        }
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
        
    /// <summary>
    /// Represents a ComboBox with values of TEnum as ItemsSource and converts SelectedItem to TColl<TEnum>
    /// </summary> 
    public class EnumComboBox<TEnum, TColl> : EnumComboBox<TEnum>
        where TColl : ObservableCollection<TEnum>, new() 
        where TEnum : struct, IComparable, IFormattable 
    {  
        public TColl SelectedItemAsCollection
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
            OnPropertyChanged("SelectedItemAsCollection");
        }
    }
     
    public class BooleanComboBox : EnumComboBox 
    {
        public BooleanComboBox()
        {
            this.ItemsSource = new List<bool> { false, true };
        }
    }
    

//    public class EnumSourceExtension : MarkupExtension
//{
//    private Type _enumType;
//    public Type EnumType
//    {
//        get { return _enumType; }
//        set
//        {
//            if (value != _enumType)
//            {
//                if (null != value)
//                {
//                    var enumType = Nullable.GetUnderlyingType(value) ?? value;
//                    if (!enumType.IsEnum)
//                        throw new ArgumentException("Type must be for an Enum.");
//                } 
//                _enumType = value;
//            }
//        }
//    }

//    public EnumSourceExtension() { }

//    public EnumSourceExtension(Type enumType)
//    {
//        this.EnumType = enumType;
//    }

//    public override object ProvideValue(IServiceProvider serviceProvider)
//    {
//        if (_enumType == null)
//            throw new InvalidOperationException("The EnumType must be specified.");

//        var actualEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
//        var enumValues = Enum.GetValues(actualEnumType);

//        if (actualEnumType == _enumType)
//            return enumValues;

//        var tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
//        enumValues.CopyTo(tempArray, 1);
//        return tempArray;
//    }
//}

}
