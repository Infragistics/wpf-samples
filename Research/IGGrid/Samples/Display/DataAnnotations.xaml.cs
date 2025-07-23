using System;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using IGGrid.Resources;
using IGGrid.Models;

namespace IGGrid.Samples.Display
{
    public partial class DataAnnotations : SampleContainer
    {
        public DataAnnotations()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Products> products = new ObservableCollection<Products>()
            {
                new Products(){ Category = "Beverages", Name = "Soft drinks", UnitPrice = 3.25 },
                new Products(){ Category = "Beverages", Name = "Coffee", UnitPrice = 7.95 },
                new Products(){ Category = "Beverages", Name = "Tea", UnitPrice = 5.00 },
                new Products(){ Category = "Beverages", Name = "Beer", UnitPrice = 9.50 },
                new Products(){ Category = "Dairy Product", Name = "Milk", UnitPrice = 4.25 },
                new Products(){ Category = "Dairy Product", Name = "Cheese", UnitPrice = 8.99 },
                new Products(){ Category = "Seafood", Name = "Fish", UnitPrice = 12.00 },
                new Products(){ Category = "Seafood", Name = "Seaweed", UnitPrice = 9.00 },
                new Products(){ Category = "Grains/Cereals", Name = "Bread", UnitPrice = 4.50 },
                new Products(){ Category = "Grains/Cereals", Name = "Pasta", UnitPrice = 2.25 },
                new Products(){ Category = "Grains/Cereals", Name = "Ceriel", UnitPrice = 3.75 },
            };
            this.DataContext = products;
        }
    }
}
