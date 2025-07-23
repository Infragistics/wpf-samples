using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infragistics.Samples.Shared.Models
{
    public class OrderViewModel : ObservableModel
    {

        public OrderViewModel()
        {
        }

        private int index;
        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                if (this.index != value)
                {
                    this.index = value;
                    this.OnPropertyChanged("Index");
                }
            }
        }

        private string orderNumber = string.Empty;
        public string OrderNumber
        {
            get
            {
                return this.orderNumber;
            }
            set
            {
                if (this.orderNumber != value)
                {
                    this.orderNumber = value;
                    this.OnPropertyChanged("OrderNumber");
                }
            }
        }

        private string product = string.Empty;
        public string Product
        {
            get
            {
                return this.product;
            }
            set
            {
                if (this.product != value)
                {
                    this.product = value;
                    this.OnPropertyChanged("Product");
                }
            }
        }

        private double salesPrice = 0d;
        public double SalesPrice
        {
            get
            {
                return this.salesPrice;
            }
            set
            {
                if (this.salesPrice != value)
                {
                    this.salesPrice = value;
                    this.OnPropertyChanged("SalesPrice");
                }
            }
        }

        private double quantity = 0d;
        public double Quantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                if (this.quantity != value)
                {
                    this.quantity = value;
                    this.OnPropertyChanged("Quantity");
                }
            }
        }

        public double Total
        {
            get
            {
                return this.salesPrice * this.quantity;
            }
        }
    }
}
