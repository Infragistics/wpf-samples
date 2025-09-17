using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
    public class CustomerDataSource : ObservableModel
    {

        public CustomerDataSource()
        {
        }

        private Customer _activeCustomer;
        public Customer ActiveCustomer
        {
            get
            {
                return this._activeCustomer;
            }
            set
            {
                if (this._activeCustomer != value)
                {
                    this._activeCustomer = value;
                    this.OnPropertyChanged("ActiveCustomer");
                }
            }
        }
        private IEnumerable<Customer> _customers;
        public IEnumerable<Customer> Customers
        {
            get
            {
                return this._customers;
            }
            set
            {
                if (this._customers != value)
                {
                    this._customers = value;
                    this.OnPropertyChanged("Customers");
                }
            }
        }
    }
}
