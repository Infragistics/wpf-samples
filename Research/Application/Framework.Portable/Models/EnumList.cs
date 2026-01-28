using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Infragistics.Framework
{
    public static class EnumEx
    {
        [Obsolete("Use GetValues() instead of GetList()")]
        public static List<TEnum> GetList<TEnum>()
            where TEnum : struct, IComparable, IFormattable 
        {
            var enumType = typeof(TEnum);
              
            return Enum.GetValues(enumType).Cast<TEnum>().ToList();
        }

        public static List<TEnum> GetValues<TEnum>()
            where TEnum : struct, IComparable, IFormattable
        {
            var enumType = typeof(TEnum); 
            return Enum.GetValues(enumType).Cast<TEnum>().ToList();
        }
        public static List<object> GetValues(Type enumType)
        {
            return Enum.GetValues(enumType).Cast<object>().ToList(); 
        }
        //public static IEnumerable<T> Values<T>(this T theEnum) where T : struct, IComparable, IFormattable//, IConvertible
        //{
        //    var enumValues = new List<T>();

        //    if (!(theEnum is Enum))
        //        throw new ArgumentException("must me an enum");

        //    return Enum.GetValues(typeof(T)).Cast<T>();
        //}

    }
    /// <summary>
    /// Represents populated list with values of specified enum 
    /// </summary> 
    public class EnumList<T> : List<T>
    {
        public EnumList()
        {
            var items = Enum.GetValues(typeof(T));
            foreach (var item in items)
            {
                this.Add((T)item);
            }
        }
    }

    public class EnumCollection<T> : ObservableCollection<EnumModel<T>>
    {
        public EnumCollection()
        {
            //ItemsSource = new List<T>();

            var enums = Enum.GetValues(typeof(T));
            if (enums != null && enums.Length > 0)
            {
                foreach (var item in enums)
                { 
                    this.Add(CreateModelFor((T)item));
                }
               // ItemsSource = this.Select(m => m.Item).ToList();
            }

            this.CollectionChanged += OnCollectionChanged;
        }


        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //ItemsSource = this.Select(m => m.Item).ToList();
        }

        public EnumModel<T> CreateModelFor(T itemValue, string itemName = null)
        {
            return new EnumModel<T>(this) { Item = itemValue };
        }

        public void Add(T itemValue, string itemName = null)
        {
            this.Add(CreateModelFor(itemValue, itemName));
        }
         

        //public List<T> Items { get; set; }

        //private T _selectedItem;
        /// <summary> Gets or sets SelectedItem </summary>
        //public T SelectedItem
        //{
        //    get { return _selectedItem; }
        //    //set { if (_selectedItem.Equals(value)) return; _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        //}

        //private List<T> _itemsSource;
        /// <summary> Gets or sets Items </summary>
        //public List<T> ItemsSource
        //{
        //    get { return _itemsSource; }
        //    //set { if (_itemsSource == value) return; _itemsSource = value; OnPropertyChanged("ItemsSource"); }
        //} 
    }

    public class EnumModel<T> : ObservableModel 
    {
        public EnumModel(EnumCollection<T> container)
        {
            Container = container;
        }

        public T Item { get; set; } 

        public EnumCollection<T> Container { get; private set; }

        private bool _isSelected = false;
        /// <summary> Gets or sets IsSelected </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set { if (_isSelected == value) return; _isSelected = value; OnPropertyChanged("IsSelected"); }
        }

        private string _name;
        /// <summary> Gets or sets Name </summary>
        public string Name
        {
            get { return string.IsNullOrEmpty(_name) ? Item.ToString() : _name; }
            set { if (_name == value) return; _name = value; OnPropertyChanged("Name"); }
        }


    }
      
   
}
