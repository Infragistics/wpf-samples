using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infragistics.Samples.Shared.Models
{
    public class RetailSalesPerformance : INotifyPropertyChanged
    {
        private string category;
        private string subcategory;
        private string product;
        private int salesUnits;
        private double revenue;
        private double profit;

        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged("Category"); }
        }

        public string Subcategory
        {
            get { return subcategory; }
            set { subcategory = value; OnPropertyChanged("Subcategory"); }
        }

        public string Product
        {
            get { return product; }
            set { product = value; OnPropertyChanged("Product"); }
        }

        public int SalesUnits
        {
            get { return salesUnits; }
            set { salesUnits = value; OnPropertyChanged("SalesUnits"); }
        }

        public double Revenue
        {
            get { return revenue; }
            set { revenue = value; OnPropertyChanged("Revenue"); }
        }

        public double Profit
        {
            get { return profit; }
            set { profit = value; OnPropertyChanged("Profit"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RetailSalesPerformanceData : ObservableCollection<RetailSalesPerformance>
    {
        public RetailSalesPerformanceData()
        {

            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Tablets",
                Product = "Microsoft Surface Pro",
                SalesUnits = 44,
                Revenue = 2686.34,
                Profit = 346.39
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Women's Wear",
                Product = "H&M Top",
                SalesUnits = 36,
                Revenue = 5057.65,
                Profit = 870.78
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Refrigerators",
                Product = "LG Smart Fridge",
                SalesUnits = 25,
                Revenue = 22369.64,
                Profit = 2938.78
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Tablets",
                Product = "Samsung Galaxy Tab S7",
                SalesUnits = 5,
                Revenue = 942.23,
                Profit = 155.02
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Mobile Phones",
                Product = "Samsung Galaxy S21",
                SalesUnits = 46,
                Revenue = 36494.63,
                Profit = 8638.84
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Laptops",
                Product = "Dell XPS 13",
                SalesUnits = 56,
                Revenue = 14762.07,
                Profit = 1907.13
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Women's Wear",
                Product = "Zara Dress",
                SalesUnits = 21,
                Revenue = 10058.38,
                Profit = 2498.26
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Kids' Wear",
                Product = "Carter's Onesie",
                SalesUnits = 4,
                Revenue = 3486.34,
                Profit = 526.01
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Tablets",
                Product = "Samsung Galaxy Tab S7",
                SalesUnits = 17,
                Revenue = 7551.78,
                Profit = 2263.45
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Mobile Phones",
                Product = "Samsung Galaxy S21",
                SalesUnits = 12,
                Revenue = 9267.75,
                Profit = 1010.17
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Mobile Phones",
                Product = "OnePlus 9",
                SalesUnits = 3,
                Revenue = 1851.05,
                Profit = 218.31
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Tablets",
                Product = "Microsoft Surface Pro",
                SalesUnits = 33,
                Revenue = 6342.98,
                Profit = 1192.71
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Kids' Wear",
                Product = "Gap Kids T-Shirt",
                SalesUnits = 55,
                Revenue = 45120.38,
                Profit = 9558.0
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Laptops",
                Product = "MacBook Pro",
                SalesUnits = 95,
                Revenue = 77157.07,
                Profit = 13375.86
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Laptops",
                Product = "Dell XPS 13",
                SalesUnits = 63,
                Revenue = 26679.73,
                Profit = 4210.2
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Kids' Wear",
                Product = "Gap Kids T-Shirt",
                SalesUnits = 80,
                Revenue = 22866.9,
                Profit = 4761.49
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Washing Machines",
                Product = "Samsung Fully Automatic",
                SalesUnits = 36,
                Revenue = 7989.17,
                Profit = 2260.93
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Men's Wear",
                Product = "Adidas Sneakers",
                SalesUnits = 48,
                Revenue = 46953.86,
                Profit = 13868.42
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Washing Machines",
                Product = "LG Top Load",
                SalesUnits = 19,
                Revenue = 10575.98,
                Profit = 1347.99
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Laptops",
                Product = "HP Spectre x360",
                SalesUnits = 67,
                Revenue = 2205.36,
                Profit = 385.08
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Tablets",
                Product = "iPad Pro",
                SalesUnits = 64,
                Revenue = 53123.87,
                Profit = 12911.8
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Refrigerators",
                Product = "LG Smart Fridge",
                SalesUnits = 11,
                Revenue = 1239.56,
                Profit = 279.91
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Women's Wear",
                Product = "Zara Dress",
                SalesUnits = 60,
                Revenue = 23433.2,
                Profit = 5322.54
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Men's Wear",
                Product = "Adidas Sneakers",
                SalesUnits = 78,
                Revenue = 64205.16,
                Profit = 12184.55
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Microwaves",
                Product = "LG Convection Microwave",
                SalesUnits = 24,
                Revenue = 962.22,
                Profit = 184.46
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Microwaves",
                Product = "Samsung Solo Microwave",
                SalesUnits = 35,
                Revenue = 18080.45,
                Profit = 5011.58
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Men's Wear",
                Product = "Nike T-Shirt",
                SalesUnits = 41,
                Revenue = 7198.76,
                Profit = 981.71
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Men's Wear",
                Product = "Nike T-Shirt",
                SalesUnits = 98,
                Revenue = 54545.95,
                Profit = 8625.62
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Microwaves",
                Product = "Panasonic Microwave Oven",
                SalesUnits = 1,
                Revenue = 409.52,
                Profit = 89.21
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Microwaves",
                Product = "Panasonic Microwave Oven",
                SalesUnits = 31,
                Revenue = 30921.69,
                Profit = 8201.97
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Tablets",
                Product = "iPad Pro",
                SalesUnits = 75,
                Revenue = 68548.35,
                Profit = 8714.21
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Kids' Wear",
                Product = "Gap Kids T-Shirt",
                SalesUnits = 49,
                Revenue = 23027.4,
                Profit = 6897.77
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Kids' Wear",
                Product = "Nike Kids Shoes",
                SalesUnits = 53,
                Revenue = 30984.4,
                Profit = 6089.4
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Laptops",
                Product = "MacBook Pro",
                SalesUnits = 30,
                Revenue = 21401.23,
                Profit = 4130.24
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Refrigerators",
                Product = "Whirlpool Double Door",
                SalesUnits = 32,
                Revenue = 8917.39,
                Profit = 1474.65
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Tablets",
                Product = "Microsoft Surface Pro",
                SalesUnits = 68,
                Revenue = 64264.22,
                Profit = 15078.93
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Kids' Wear",
                Product = "Gap Kids T-Shirt",
                SalesUnits = 16,
                Revenue = 13191.47,
                Profit = 3457.83
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Laptops",
                Product = "Dell XPS 13",
                SalesUnits = 46,
                Revenue = 45698.34,
                Profit = 10143.94
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Men's Wear",
                Product = "Adidas Sneakers",
                SalesUnits = 41,
                Revenue = 4342.03,
                Profit = 1244.5
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Kids' Wear",
                Product = "Gap Kids T-Shirt",
                SalesUnits = 92,
                Revenue = 64280.4,
                Profit = 10937.31
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Washing Machines",
                Product = "Samsung Fully Automatic",
                SalesUnits = 80,
                Revenue = 64418.39,
                Profit = 19030.8
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Women's Wear",
                Product = "Gucci Handbag",
                SalesUnits = 89,
                Revenue = 7677.5,
                Profit = 1227.71
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Refrigerators",
                Product = "LG Smart Fridge",
                SalesUnits = 87,
                Revenue = 41122.73,
                Profit = 5590.62
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Refrigerators",
                Product = "Whirlpool Double Door",
                SalesUnits = 90,
                Revenue = 4531.87,
                Profit = 1224.65
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Men's Wear",
                Product = "Levi's Jeans",
                SalesUnits = 82,
                Revenue = 66655.75,
                Profit = 11283.09
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Washing Machines",
                Product = "Samsung Fully Automatic",
                SalesUnits = 4,
                Revenue = 1705.94,
                Profit = 201.78
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Home Appliances",
                Subcategory = "Refrigerators",
                Product = "LG Smart Fridge",
                SalesUnits = 50,
                Revenue = 18189.11,
                Profit = 4438.25
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Kids' Wear",
                Product = "Nike Kids Shoes",
                SalesUnits = 82,
                Revenue = 24529.51,
                Profit = 3845.9
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Clothing",
                Subcategory = "Women's Wear",
                Product = "H&M Top",
                SalesUnits = 2,
                Revenue = 535.12,
                Profit = 142.94
            });
            this.Add(new RetailSalesPerformance
            {
                Category = "Electronics",
                Subcategory = "Mobile Phones",
                Product = "Samsung Galaxy S21",
                SalesUnits = 86,
                Revenue = 83911.03,
                Profit = 18342.66
            });
        }
    }
}
