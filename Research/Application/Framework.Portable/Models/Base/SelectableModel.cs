using System;
using System.Collections.Generic; 

namespace Infragistics.Framework 
{  
    public class SelectableViewModel : ObservableModel
    {
        public SelectableViewModel()
        {
            ItemsSource = new List<SelectableObject>();
        }
        public void Add(object itemValue)
        {
            this.ItemsSource.Add(new SelectableObject(itemValue));
        }
        public void Add(object itemValue, string itemDisplay)
        {
            this.ItemsSource.Add(new SelectableObject(itemValue, itemDisplay));
        }
        /// <summary> Gets or sets  </summary>
        public List<SelectableObject> ItemsSource { get; set; }

        private SelectableObject _selectedItem;
        /// <summary> Gets or sets Selected Item </summary>
        public SelectableObject SelectedItem
        {
            get { return _selectedItem; }
            set { if (_selectedItem == value) return; _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { if (_selectedIndex == value) return; _selectedIndex = value; OnPropertyChanged("SelectedIndex"); }
        }

        protected override void OnPropertyChanged(string propertyName)
        {           
            if (propertyName == "SelectedItem")
            {
                for (int i = 0; i < ItemsSource.Count; i++)
                {
                    ItemsSource[i].IsSelected = ItemsSource[i] == SelectedItem;
                    if (ItemsSource[i].IsSelected)
                        _selectedIndex = i;
                }
            }
            else if (propertyName == "SelectedIndex")
            {
                SelectedItem = ItemsSource[SelectedIndex];
            }

            base.OnPropertyChanged(propertyName);
        }
    }

    public class SelectableObject : ObservableModel
    {
        public SelectableObject()
        {
        }

        public SelectableObject(object value)
            : this(value, value.ToString())
        {

        }
        public SelectableObject(object value, string name)
        {
            Value = value;
            Name = name;
        }
        /// <summary> Gets or sets Value </summary>
        public object Value { get; set; }

        /// <summary> Gets or sets Name </summary>
        public string Name { get; set; }

        private bool _isSelected;
        /// <summary> Gets or sets IsSelected </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set { if (_isSelected == value) return; _isSelected = value; OnPropertyChanged("IsSelected"); }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

}
