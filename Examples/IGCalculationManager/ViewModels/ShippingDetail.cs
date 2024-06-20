using Infragistics.Samples.Shared.Models;

namespace IGCalculationManager.ViewModels
{
    public class ShippingDetail : ObservableModel
    {
        string _itemDescription;
        int _quantity;
        decimal _price;
        decimal _shipping;

        public ShippingDetail()
        {
        }

        public ShippingDetail(string itemDescription, int quantity, decimal price, decimal shipping)
        {
            _itemDescription = itemDescription;
            _quantity = quantity;
            _price = price;
            _shipping = shipping;
        }

        public string ItemDescription
        {
            get { return _itemDescription; }
            set
            {
                if (_itemDescription != value)
                {
                    _itemDescription = value;
                    this.OnPropertyChanged("ItemDescription");
                }
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    this.OnPropertyChanged("Price");
                }
            }
        }

        public decimal Shipping
        {
            get { return _shipping; }
            set
            {
                if (_shipping != value)
                {
                    _shipping = value;
                    this.OnPropertyChanged("Shipping");
                }
            }
        }

        public int Quantity
        {
            get { return _quantity; }
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
