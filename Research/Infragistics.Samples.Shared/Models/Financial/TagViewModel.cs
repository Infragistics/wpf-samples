using System.Linq;
using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
	public enum CurrentFilter
	{
		None,
		CategoryFilter,
		ProductFilter
	}

    public class TagViewModel : ObservableModel
	{
		
		private CurrentFilter _currentFilter = CurrentFilter.None;
		
		public TagViewModel()
		{
		}

		private string _categoryFilter = string.Empty;
		public string CategoryFilter
		{
			get
			{
				return _categoryFilter;
			}
			set
			{
				if (_categoryFilter != value)
				{
					_categoryFilter = value;
					this.OnPropertyChanged("CategoryFilter");
				}
			}
		}
		
		private string _productFilter = string.Empty;
		public string ProductFilter
		{
			get
			{
				return _productFilter;
			}
			set
			{
				if (_productFilter != value)
				{
					_productFilter = value;
					this.OnPropertyChanged("ProductFilter");
				}
			}
		}

		private IEnumerable<OrderHistory> _orderHistory;
		public IEnumerable<OrderHistory> OrderHistory
		{
			get
			{
				return this.FilterData();
			}
			set
			{
				if (_orderHistory != value)
				{
					_orderHistory = value;
					this.OnPropertyChanged("OrderHistory");
				}
			}
		}
	
		private IList<TagItem> _tags;
		public IList<TagItem> Tags
		{
			get
			{
				return _tags;
			}
			set
			{
				if (_tags != value)
				{
					_tags = value;
					this.OnPropertyChanged("Tags");
				}
			}
		}

		private IList<TagItem> _topTags;
		public void SetTagSource(IList<TagItem> items)
		{
			_topTags = items;
			this.Tags = _topTags;
		}

		public void FilterTags(TagItem selectedItem)
		{
			if (selectedItem != null && selectedItem.HasChildren)
			{
				this.Tags = selectedItem.Tags;
			}

			switch (_currentFilter)
			{
				case CurrentFilter.CategoryFilter:
					{
						_currentFilter = CurrentFilter.ProductFilter;
						this.ProductFilter = selectedItem.Content;
						break;
					}
				case CurrentFilter.ProductFilter:
					{
						this.ProductFilter = selectedItem.Content;
						break;
					}
				default:
					{
						_currentFilter = CurrentFilter.CategoryFilter;
						this.CategoryFilter = selectedItem.Content;
						break;
					}
			}

			this.OnPropertyChanged("OrderHistory");
		}

		public void ResetFiler()
		{
			this.Tags = _topTags;

			_currentFilter = CurrentFilter.None;
			this.CategoryFilter = string.Empty;
			this.ProductFilter = string.Empty;

			this.OnPropertyChanged("OrderHistory");
		}

		private IEnumerable<OrderHistory> FilterData()
		{
			IEnumerable<OrderHistory> filteredResult;

			switch (_currentFilter)
			{
				case CurrentFilter.CategoryFilter:
				{
					filteredResult = (from o in _orderHistory
										 where o.CategoryName == _categoryFilter
										 select o).ToList();

					break;
				}
				case CurrentFilter.ProductFilter:
				{
					filteredResult = (from o in _orderHistory
										 where o.ProductName == _productFilter
										 select o).ToList();
					break;
				}
				default:
				{
					filteredResult = _orderHistory;
					break;
				}
			}
			return filteredResult;
		}
	}
}
