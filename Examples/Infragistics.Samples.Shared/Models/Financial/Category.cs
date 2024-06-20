using System.Collections.Generic;
using Infragistics.Samples.Shared.DataProviders;

namespace Infragistics.Samples.Shared.Models
{ 
    public class Category : ObservableModel
	{
        public static string ImagePath = ImageFilePathProvider.GetImageLocalPath() ;
		public Category()
		{
		}

		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (_name != value)
				{
					_name = value;
					this.OnPropertyChanged("Name");
				}

			}
		}
		
		private string _imageUrl;
		public string ImageUrl
		{
			get
			{
                if (string.IsNullOrWhiteSpace(this._imageUrl))
                    return this._imageUrl;
                else
                    return ImagePath + this._imageUrl;
			}
			set
			{
				if (_imageUrl != value)
				{
					_imageUrl = value;
					this.OnPropertyChanged("ImageUrl");
				}

			}
		}

		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				if (_description != value)
				{
					_description = value;
					this.OnPropertyChanged("Description");
				}

			}
		}

		private string _price;
		public string Price
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

		private IEnumerable<Category> _categories;
		public IEnumerable<Category> Categories
		{
			get
			{
				return _categories;
			}
			set
			{
				if (_categories != value)
				{
					_categories = value;
					this.OnPropertyChanged("Categories");
				}
			}
		}

		private IEnumerable<Product> _products;
		public IEnumerable<Product> Products
		{
			get
			{
				return _products;
			}
			set
			{
				if (_products != value)
				{
					_products = value;
					this.OnPropertyChanged("Products");
				}
			}
		}
	}
}
