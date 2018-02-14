using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using IGShowcase.EarthQuake.Resources;

namespace IGShowcase.EarthQuake.ViewModels
{
	public sealed class FilterViewModel : IEnumerable<FilterViewModel.FilterItem>
	{
		#region Private Fields
		private readonly IEnumerable<FilterItem> _items;
        private List<string> _selectedItems;
		#endregion

        public void ToggleItemsSelecttion(bool isSelected)
        {
            foreach (var item in this)
            {
                item.IsSelected = isSelected;
            }
        }
		#region Constructors
		/// <summary>
		/// Creates an instance of FilterViewModel
		/// </summary>
		/// <param name="items">All items in the filter</param>
		/// <param name="selectedItems">The selected items in the filter</param>
		public FilterViewModel(IEnumerable<string> items, IEnumerable<string> selectedItems)
		{
			_selectedItems = selectedItems.ToList();
			_items = items.Select(x => new FilterItem(this, x, selectedItems.Contains(x))).ToList();
            AllowPropertyUpdates = true;
		}
		#endregion

		#region Public Properties
        public bool AllowPropertyUpdates { get; set; }
        
		/// <summary>
		/// Gets or sets the selected items.
		/// </summary>
		/// <value>The selected items.</value>
		public List<string> SelectedItems
		{
			get { return _selectedItems; }
			private set
			{
				if (_selectedItems != value)
				{
					_selectedItems = value;
                    if (AllowPropertyUpdates) OnFilterChanged();
				}
			}
		}

		/// <summary>
		/// Occurs when any of the filter items is changed.
		/// </summary>
		public event EventHandler FilterChanged;
		#endregion Pubblic Properties

		#region IEnumerable<FilterItem> Members
		public IEnumerator<FilterItem> GetEnumerator()
		{
			return _items.GetEnumerator();
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}
		#endregion

		/// <summary>
		/// This method gets called if any filter item is clicked.
		/// </summary>
		private void OnFilterItemChanged()
		{
			SelectedItems = this.Where(x => x.IsSelected).Select(x => x.Name).ToList();
		}

		public void OnFilterChanged()
		{
			if (FilterChanged != null)
			{
                if (AllowPropertyUpdates) FilterChanged(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// This class represents one item for in the filter.
		/// </summary>
		public class FilterItem : INotifyPropertyChanged
		{
			#region Member Variables
			private readonly FilterViewModel _vm;
			private bool _isSelected;
			#endregion Member Variables

			/// <summary>
			/// Initializes a new instance of the <see cref="FilterItem"/> class.
			/// </summary>
			/// <param name="vm">The parent FilterViewModel.</param>
			/// <param name="name">The name of the item.</param>
			/// <param name="initialState">The initial selection state.</param>
			public FilterItem(FilterViewModel vm, string name, bool initialState)
			{
				_vm = vm;
				_isSelected = initialState;
				Name = name;
				DisplayName = AppStrings.ResourceManager.GetString(name);
			}

			#region Public Properties
			/// <summary>
			/// Gets the name of the item.
			/// </summary>
			/// <value>The name.</value>
			public string Name { get; private set; }

			/// <summary>
			/// Gets the localized name of the item.
			/// </summary>
			/// <value>The name.</value>
			public string DisplayName { get; private set; }

			/// <summary>
			/// Gets or sets a value indicating whether this item is selected.
			/// </summary>
			/// <value>
			/// 	<c>true</c> if this item is selected; otherwise, <c>false</c>.
			/// </value>
			public bool IsSelected
			{
				get { return _isSelected; }
				set
				{
					if (_isSelected != value)
					{
						_isSelected = value;
						OnPropertyChanged("IsSelected");
						_vm.OnFilterItemChanged();
					}
				}
			}
			#endregion Public Properties

			#region INotifyPropertyChanged Members
			public event PropertyChangedEventHandler PropertyChanged;

			private void OnPropertyChanged(string propertyName)
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				}
			}
			#endregion
		}
	}
}