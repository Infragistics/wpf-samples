using CascadingComboBox.Models;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CascadingComboBox.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<string> _cities = new ObservableCollection<string>();
        public ObservableCollection<string> Cities
        {
            get { return _cities; }
            set { SetProperty(ref _cities, value); }
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }

        public MainWindowViewModel()
        {
            GenerateCitiesData();
            Customers = new ObservableCollection<Customer>(GenerateData());
        }

        private void GenerateCitiesData()
        {
            for (int x = 0; x < 5; x++)
            {
                Cities.Add($"City: {x}");
            }
        }

        private IList<Customer> GenerateData()
        {
            var list = new List<Customer>();
            for (int x = 0; x < 5; x++)
            {
                var customer = new Customer();
                customer.Name = $"Name: {x}";
                customer.City = $"City: {x}";
                customer.ZipCode = $"ZipCode: {x}.{x}";
                list.Add(customer);
            }
            return list;
        }
    }
}
