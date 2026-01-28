using System.Collections.Generic;
using Infragistics.Samples.Shared.DataProviders;

namespace Infragistics.Samples.Shared.Models
{
    public class Customer : ObservableModel
	{
        public static string ContactImagePath = ImageFilePathProvider.GetImageLocalPath() + "People/";

        private string _customerId;
		public string CustomerID
		{
			get
			{
				return _customerId;
			}
			set
			{
				if (_customerId != value)
				{
					_customerId = value;
					this.OnPropertyChanged("CustomerID");
				}
			}
		}

		private string _company;
		public string Company
		{
			get
			{
				return _company;
			}
			set
			{
				if (_company != value)
				{
					_company = value;
					this.OnPropertyChanged("Company");
				}
			}
		}

		private string _contactName;
		public string ContactName
		{
			get
			{
				return _contactName;
			}
			set
			{
				if (_contactName != value)
				{
					_contactName = value;
					this.OnPropertyChanged("ContactName");
				}
			}
		}

        public string ContactNameCompanyPhone
        {
            get
            {
                return string.Format("{0}: {1} / {2}", _company, _contactName, _phone);
            }
        }
		
		private string _contactTitle;
		public string ContactTitle
		{
			get
			{
				return _contactTitle;
			}
			set
			{
				if (_contactTitle != value)
				{
					_contactTitle = value;
					this.OnPropertyChanged("ContactTitle");
				}
			}
		}

		private string _contactEmail;
		public string ContactEmail
		{
			get
			{
				return _contactEmail;
			}
			set
			{
				if (_contactEmail != value)
				{
					_contactEmail = value;
					this.OnPropertyChanged("ContactEmail");
				}
			}
		}

		private string _addressOne;
		public string AddressOne
		{
			get
			{
				return _addressOne;
			}
			set
			{
				if (_addressOne != value)
				{
					_addressOne = value;
					this.OnPropertyChanged("AddressOne");
				}
			}
		}

		private string _addressTwo;
		public string AddressTwo
		{
			get
			{
				return _addressTwo;
			}
			set
			{
				if (_addressTwo != value)
				{
					_addressTwo = value;
					this.OnPropertyChanged("AddressTwo");
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
				
		private string _region;
		public string Region
		{
			get
			{
				return _region;
			}
			set
			{
				if (_region != value)
				{
					_region = value;
					this.OnPropertyChanged("Region");
				}
			}
		}

		public string FormatedCityState
		{
			get
			{
				return _city + ", " + _region + " " + _postalCode;
			}
		}

				
		private string _country;
		public string Country
		{
			get
			{
				return _country;
			}
			set
			{
				if (_country != value)
				{
					_country = value;
					this.OnPropertyChanged("Country");
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
				
		private string _phone;
		public string Phone
		{
			get
			{
				return _phone;
			}
			set
			{
				if (_phone != value)
				{
					_phone = value;
					this.OnPropertyChanged("Phone");
				}
			}
		}


		private string _fax;
		public string Fax
		{
			get
			{
				return _fax;
			}
			set
			{
				if (_fax != value)
				{
					_fax = value;
					this.OnPropertyChanged("Fax");
				}
			}
		}

		private string _imageResourcePath;
		public string ImageResourcePath
		{
			get
			{
				return _imageResourcePath;
			}
			set
			{
				if (_imageResourcePath != value)
				{
					_imageResourcePath = value;
					this.OnPropertyChanged("ImageResourcePath");
				}
			}
		}

		public string ImagePath
		{
			get
			{
               return ImageFilePathProvider.GetImageLocalPath("People/" + this.ImageResourcePath);				
			}
		}

		private IEnumerable<Order> _orders;
		public IEnumerable<Order> Orders
		{
			get
			{
				return _orders;
			}
			set
			{
				if (_orders != value)
				{
					_orders = value;
					this.OnPropertyChanged("Orders");
				}
			}
		}
	
	}
}
