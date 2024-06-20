using System;
using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
    public class Order : ObservableModel
	{
        private string customerId;
        public string CustomerId
        {
            get
            {
                return this.customerId;
            }
            set
            {
                if (this.customerId != value)
                {
                    this.customerId = value;
                    this.OnPropertyChanged("CustomerId");
                }
            }
        }

        private string _orderNumber = string.Empty;
        public string OrderNumber
        {
            get
            {
                return _orderNumber;
            }
            set
            {
                if (_orderNumber != value)
                {
                    _orderNumber = value;
                    this.OnPropertyChanged("OrderNumber");
                }
            }
        }

        private string _product = string.Empty;
        public string Product
        {
            get
            {
                return _product;
            }
            set
            {
                if (_product != value)
                {
                    _product = value;
                    this.OnPropertyChanged("Product");
                }
            }
        }

        private double _salesPrice = 0d;
        public double SalesPrice
        {
            get
            {
                return _salesPrice;
            }
            set
            {
                if (_salesPrice != value)
                {
                    _salesPrice = value;
                    this.OnPropertyChanged("SalesPrice");
                }
            }
        }

        private double _quantity = 0d;
        public double Quantity
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

        public double Total
        {
            get
            {
                return _salesPrice * _quantity;
            }
        }

		private string _orderId;
		public string OrderId
		{
			get
			{
				return _orderId;
			}
			set
			{
				if (_orderId != value)
				{
					_orderId = value;
					this.OnPropertyChanged("OrderId");
				}
			}
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

		private DateTime _shippedDate;
		public DateTime ShippedDate
		{
			get
			{
				return _shippedDate;
			}
			set
			{
				if (_shippedDate != value)
				{
					_shippedDate = value;
					this.OnPropertyChanged("ShippedDate");
				}
			}
		}

		private IEnumerable<OrderDetail> _orderDetails;
		public IEnumerable<OrderDetail> OrderDetails
		{
			get
			{
				return _orderDetails;
			}
			set
			{
				if (_orderDetails != value)
				{
					_orderDetails = value;
					this.OnPropertyChanged("OrderDetails");
				}
			}
		}
	
		private decimal _freight;
		public decimal Freight
		{
			get
			{
				return _freight;
			}
			set
			{
				if (_freight != value)
				{
					_freight = value;
					this.OnPropertyChanged("Freight");
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
		
		private string _shipAddress;
		public string ShipAddress
		{
			get
			{
				return _shipAddress;
			}
			set
			{
				if (_shipAddress != value)
				{
					_shipAddress = value;
					this.OnPropertyChanged("ShipAddress");
				}
			}
		}
		
		private string _shipCity;
		public string ShipCity
		{
			get
			{
				return _shipCity;
			}
			set
			{
				if (_shipCity != value)
				{
					_shipCity = value;
					this.OnPropertyChanged("ShipCity");
				}
			}
		}
		
		private string _shipCountry;
		public string ShipCountry
		{
			get
			{
				return _shipCountry;
			}
			set
			{
				if (_shipCountry != value)
				{
					_shipCountry = value;
					this.OnPropertyChanged("ShipCountry");
				}
			}
		}
	
		private string _shipPostalCode;
		public string ShipPostalCode
		{
			get
			{
				return _shipPostalCode;
			}
			set
			{
				if (_shipPostalCode != value)
				{
					_shipPostalCode = value;
					this.OnPropertyChanged("ShipPostalCode");
				}
			}
		}

        private decimal _costPerUnit;
        public decimal CostPerUnit
        {
            get
            {
                return _costPerUnit;
            }
            set
            {
                if (_costPerUnit != value)
                {
                    _costPerUnit = value;
                    this.OnPropertyChanged("CostPerUnit");
                }
            }
        }

        private int _discount;
        public int Discount
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

        private decimal _shipAndHandle;
        public decimal ShipAndHandle
        {
            get
            {
                return _shipAndHandle;
            }
            set
            {
                if (_shipAndHandle != value)
                {
                    _shipAndHandle = value;
                    this.OnPropertyChanged("ShipAndHandle");
                }
            }
        }
	}
}
