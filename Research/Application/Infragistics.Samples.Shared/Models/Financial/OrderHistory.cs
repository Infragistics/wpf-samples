using System;

namespace Infragistics.Samples.Shared.Models
{
    public class OrderHistory : ObservableModel
	{

 		private T SetValue<T>(bool updateValue, T currentValue, T value, string propertyName)
		{
			T result = currentValue;

			if (updateValue)
			{
				result = value;
				this.OnPropertyChanged(propertyName);
			}

			return result;
		}

		private DateTime _orderDate;
		public DateTime OrderDate
		{
			get
			{
				return _orderDate;
			}
			set
			{
				if (_orderDate != value)
				{
					_orderDate = value;
					this.OnPropertyChanged("OrderDate");
				}
			}
		}

		private string _shipName;
		public string ShipName
		{
			get
			{
				return _shipName;
			}
			set
			{
				if (_shipName != value)
				{
					_shipName = value;
					this.OnPropertyChanged("ShipName");
				}
			}
		}
		
		private double _price;
		public double Price
		{
			get
			{
				return _price;
			}
			set
			{
				if (_price != value)
				{
					_price = value;
					this.OnPropertyChanged("Price");
				}
			}
		}

		private string _categoryName;
		public string CategoryName
		{
			get
			{
				return _categoryName;
			}
			set
			{
				if (_categoryName != value)
				{
					_categoryName = value;
					this.OnPropertyChanged("CategoryName");
				}
			}
		}

		private string _productName;
		public string ProductName
		{
			get
			{
				return _productName;
			}
			set
			{
				if (_productName != value)
				{
					_productName = value;
					this.OnPropertyChanged("ProductName");
				}
			}
		}

		private string _supplier;
		public string Supplier
		{
			get
			{
				return _supplier;
			}
			set
			{
				if (_supplier != value)
				{
					_supplier = value;
					this.OnPropertyChanged("Supplier");
				}
			}
		}

	}
}
