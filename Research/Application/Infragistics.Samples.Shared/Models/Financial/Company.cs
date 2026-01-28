using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
    /// <summary>
    /// Represents a Company object 
    /// </summary>
    public class Company : ObservableModel
    {
        #region Constructor
         
        public Company()
        {
            _products = new List<Product>();
            _revenue = 0;

            //Product product;
            //for (int i = 0; i < 10; i++)
            //{
            //    product = new Product(5);
            //    product.Name = "P" + i;
            //    _revenue += product.Revenue;
            //    _products.Add(product);
            //}
        } 
        #endregion

        #region Properties
        private double _revenue;
        public double Revenue
        {
            get
            {
                return _revenue;
            }
            set
            {
                if (_revenue != value)
                {
                    _revenue = value;
                    this.OnPropertyChanged("Revenue");
                }
            }
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

        private List<Product> _products;
        public List<Product> Products
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
        #endregion
    }
}