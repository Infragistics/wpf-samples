using System;

namespace Infragistics.Samples.Shared.Models
{
    /// <summary>
    /// Represents a RealEstate Property
    /// </summary>
    public class RealEstate : ObservableModel 
	{

        public RealEstate() { }

		private string _mlsNumber;
		public string MLS
		{
			get
			{
				return _mlsNumber;
			}
			set
			{
				if (_mlsNumber != value)
				{
					_mlsNumber = value;
					this.OnPropertyChanged("MLS");
				}
			}
		}
				
		private DateTime _listingDate;
		public DateTime ListingDate
		{
			get
			{
				return _listingDate;
			}
			set
			{
				if (_listingDate != value)
				{
					_listingDate = value;
					this.OnPropertyChanged("ListingDate");
				}
			}
		}

		private bool _isSold;
		public bool IsSold
		{
			get
			{
				return _isSold;
			}
			set
			{
				if (_isSold != value)
				{
					_isSold = value;
					this.OnPropertyChanged("IsSold");
				}
			}
		}

		private int _numberOfBedRooms;
		public int NumberOfBedRooms
		{
			get
			{
				return _numberOfBedRooms;
			}
			set
			{
				if (_numberOfBedRooms != value)
				{
					_numberOfBedRooms = value;
					this.OnPropertyChanged("NumberOfBedRooms");
				}
			}
		}

		private int _numberOfBathRooms;
		public int NumberOfBathRooms
		{
			get
			{
				return _numberOfBathRooms;
			}
			set
			{
				if (_numberOfBathRooms != value)
				{
					_numberOfBathRooms = value;
					this.OnPropertyChanged("NumberOfBathRooms");
				}
			}
		}

		private decimal _sqFeet;
		public decimal SqFeet
		{
			get
			{
				return _sqFeet;
			}
			set
			{
				if (_sqFeet != value)
				{
					_sqFeet = value;
					this.OnPropertyChanged("SqFeet");
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

		private string _city;
		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				if (_city != value)
				{
					_city = value;
					this.OnPropertyChanged("City");
				}
			}
		}

		private string _state;
		public string State
		{
			get
			{
				return _state;
			}
			set
			{
				if (_state != value)
				{
					_state = value;
					this.OnPropertyChanged("State");
				}
			}
		}

		private string _postalCode;
		public string PostalCode
		{
			get
			{
				return _postalCode;
			}
			set
			{
				if (_postalCode != value)
				{
					_postalCode = value;
					this.OnPropertyChanged("PostalCode");
				}
			}
		}		
	}

}
