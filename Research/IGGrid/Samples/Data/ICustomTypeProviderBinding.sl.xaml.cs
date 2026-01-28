using IGGrid.Resources;
using IGGrid.Samples.Models;
using IGGrid.Samples.ViewModels;
using Infragistics.Samples.Framework;
using System.Collections.ObjectModel;
using System.Windows;

namespace IGGrid.Samples.Data
{
    public partial class ICustomTypeProviderBinding : SampleContainer
    {
        private ObservableCollection<Customer> customers = null;
        private CustomTypeProviderBindingViewModel ctpbvm = null; 

        public ICustomTypeProviderBinding()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Customer.CleanPropertyList();
        }

        private void OnLoaded(object sender, RoutedEventArgs rea)
        {
            customers = new ObservableCollection<Customer>();
            ctpbvm = new CustomTypeProviderBindingViewModel();

            DataContext = ctpbvm;

            Customer.AddProperty("Age", typeof(int));
            Customer.AddProperty("Married", typeof(bool));

            Customer cust01 = new Customer { FirstName = GridStrings.ictp_Mary, LastName = GridStrings.ictp_Smith }; // { FirstName = "Mary", LastName = "Smith" };
            cust01.SetPropertyValue("Age", 40);
            cust01.SetPropertyValue("Married", true);

            Customer cust02 = new Customer { FirstName = GridStrings.ictp_John, LastName = GridStrings.ictp_Smith }; // { FirstName = "John", LastName = "Smith" };
            cust02.SetPropertyValue("Age", 45);
            cust02.SetPropertyValue("Married", true);

            customers.Add(cust01);
            customers.Add(cust02);

            RebindGrid();
        }

        private void AddProperty(object sender, RoutedEventArgs rea)
        {
            // Add custom property here
            if (ctpbvm.PropertyName == null)
            {
                MessageBox.Show(GridStrings.XG_ICTPBinding_NullInputMsg); // "Enter a property name!"
            }
            else
            {
                if (Customer.CheckIfNameExists(ctpbvm.PropertyName))
                {
                    MessageBox.Show(GridStrings.ictp_PropertyExists); // "Property with the given name already exists!"
                }
                else
                {
                    Customer.AddProperty(ctpbvm.PropertyName, ctpbvm.SelectedPropertyTypeInfo.PropertyType);
                    foreach (var customer in customers)
                    {
                        customer.SetPropertyValue(
                            ctpbvm.PropertyName,
                            ctpbvm.SelectedPropertyTypeInfo.DefaultValue);
                    }

                    RebindGrid();
                }
            }   
        }

        private void RebindGrid()
        {
            if (xamGridCustomers.ItemsSource != null)
            {
                xamGridCustomers.ItemsSource = null;
            }
            xamGridCustomers.ItemsSource = customers;
        }
    }
}