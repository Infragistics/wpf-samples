namespace Infragistics.Samples.Shared.Models
{
    public class OrderDetail : ObservableModel
	{

		private string _productId;
		public string ProductID
		{
			get
			{
				return _productId;
			}
			set
			{
				if (_productId != value)
				{
					_productId = value;
					this.OnPropertyChanged("ProductID");
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

		private decimal _discount;
		public decimal Discount
		{
			get
			{
				return _discount;
			}
			set
			{
				if (_discount != value)
				{
					_discount = value;
					this.OnPropertyChanged("Discount");
				}
			}
		}

		private int _quantity;
		public int Quantity
		{
			get
			{
				return _quantity;
			}
			set
			{
				if (_quantity != value)
				{
					_quantity = value;
					this.OnPropertyChanged("Quantity");
				}
			}
		}
	}
}
