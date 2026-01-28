using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models.TeamForce
{
    public class CustomerListViewModel : ObservableModel
    {
        public CustomerListViewModel()
        {
        }

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }

        }

        private IList<Customer> customers;
        public IList<Customer> Customers
        {
            get
            {
                return this.customers;
            }

            set
            {
                if (this.customers != value)
                {
                    this.customers = value;
                    this.OnPropertyChanged("Customers");
                }
            }
        }
    }
}