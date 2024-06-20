using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models.TeamForce
{
    public class TeamForceViewModel : ObservableModel
    {

        public TeamForceViewModel()
        {
            this.LoadCustomers();
        }

        private IList<CustomerListViewModel> customerList;
        public IList<CustomerListViewModel> CustomerList
        {
            get
            {
                return this.customerList;
            }

            set
            {
                if (this.customerList != value)
                {
                    this.customerList = value;
                    this.OnPropertyChanged("CustomerList");
                }
            }
        }

        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get
            {
                return this.selectedCustomer;
            }

            set
            {
                if (this.selectedCustomer != value)
                {
                    this.selectedCustomer = value;
                    this.OnPropertyChanged("SelectedCustomer");
                    this.LoadSalesData();
                }
            }
        }

        private SalesMonthCollection salesData;
        public SalesMonthCollection SalesData
        {
            get
            {
                return this.salesData;
            }

            set
            {
                if (this.salesData != value)
                {
                    this.salesData = value;
                    this.OnPropertyChanged("SalesData");
                }
            }
        }


        private ProductSaleCollection productSale;
        public ProductSaleCollection ProductSalesData
        {
            get
            {
                return this.productSale;
            }

            set
            {
                if (this.productSale != value)
                {
                    this.productSale = value;
                    this.OnPropertyChanged("ProductSalesData");
                }
            }
        }

        private void LoadCustomers()
        {
            CustomerRepository repository = new CustomerRepository();
            repository.Execute(CustomerData_Loaded);
        }

        private void CustomerData_Loaded(IList<Customer> items)
        {
            IList<CustomerListViewModel> customerList = new List<CustomerListViewModel>();
            customerList.Add(new CustomerListViewModel { Name = "Customers", Customers = items });
            this.CustomerList = customerList;
        }

        private void ProductSalesData_Loaded(SalesRepositoryResult result)
        {
            if (result != null)
            {
                this.SalesData = result.SalesData;
                this.ProductSalesData = result.ProductSalesData;
            }
        }

        private void LoadSalesData()
        {
            if (this.selectedCustomer != null)
            {
                SalesRepository repository = new SalesRepository();
                repository.Execute(ProductSalesData_Loaded, this.selectedCustomer.CustomerID);
            }

        }
    }
}