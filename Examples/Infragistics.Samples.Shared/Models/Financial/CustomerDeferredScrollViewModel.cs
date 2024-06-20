using System;
using System.Windows.Media.Imaging;
using Infragistics.Samples.Shared.DataProviders;

namespace Infragistics.Samples.Shared.Models
{
    public class CustomerDeferredScrollViewModel
    {
        public static string ContactImagePath = ImageFilePathProvider.GetImageLocalPath() + "People/";
   
		private readonly Customer _customer;
        public CustomerDeferredScrollViewModel(Customer customer)
		{
            _customer = customer;
        }

        public string Company
        {
            get
            {
                return _customer.Company;
            }
        }

        public string ContactName
        {
            get
            {
                return _customer.ContactName;
            }
        }

        public string ContactTitle
        {
            get
            {
                return _customer.ContactTitle;
            }
        }

        public string ContactPhone
        {
            get
            {
                return _customer.Phone;
            }
        }

        public string ContactFax
        {
            get
            {
                return _customer.Fax;
            }
        }

        public string ImagePath
        {
            get
            {
                return CustomerDeferredScrollViewModel.ContactImagePath + _customer.ImageResourcePath;
            }
        }

        public BitmapImage ImageSource
        {
            get
            {
                BitmapImage result = new BitmapImage(new Uri(CustomerDeferredScrollViewModel.ContactImagePath + _customer.ImageResourcePath));
                return result;
            }
        }
		       
        public string Country
        {
            get
            {
                return _customer.Country;
            }
        }
	}
}
