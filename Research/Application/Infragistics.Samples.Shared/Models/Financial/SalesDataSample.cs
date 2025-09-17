using System;
using System.Collections.ObjectModel;
using Infragistics.Samples.Shared.Resources;

namespace Infragistics.Samples.Shared.Models
{
    /// <summary>
    /// Represents a collection of Sale objects with a sample of 500 radomly generated items
    /// </summary>
    public class SalesDataSample : ObservableCollection<Sale>
    {
        public SalesDataSample()
        {
            ObservableCollection<Sale> sales = SalesDataGenerator.GenerateSales(500);
            foreach (Sale sale in sales)
            {
                this.Add(sale);
            }
        }
    }
    public class SalesDataGenerator
    {
        public static Boolean ConstantUnitPrice;

        private static String[] _products;
        private static String[] _sellerNames;
        private static String[] _cities;

        private static readonly Random Random = new Random();

        public static ObservableCollection<Sale> GenerateSales(int numberOfSales)
        {
            _products = FinancialStrings.Data_ProductCategories.Split(';');
            _sellerNames = FinancialStrings.Data_SellerNames.Split(';');
            _cities = FinancialStrings.Data_Cities.Split(';');

            ObservableCollection<Sale> sales = new ObservableCollection<Sale>();
            for (double i = 0; i < numberOfSales; i++)
            {
                Sale sale = new Sale
                {
                    Quarter = "Q " + i,
                    Value =  GetRandomPrice(),
                    Date = GetRandomDate(),
                    Product = GerRandomProduct(),
                    NumberOfUnits = GetRandomNumUnits(),
                    Seller = GetRandomSeller()
                };
                sales.Add(sale);
            }
            return sales;
        }

        #region Get Random Objects
        private static Seller GetRandomSeller()
        {
            return new Seller
            {
                City = GetRandomCity(),
                Name = GetRandomSellerName()
            };
        }

        private static string GetRandomSellerName()
        {
            Random a = new Random(Random.Next());
            return _sellerNames[a.Next(_sellerNames.Length)];
        }

        private static string GetRandomCity()
        {
            Random a = new Random(Random.Next());
            return _cities[a.Next(_cities.Length)];
        }

        private static int GetRandomNumUnits()
        {
            Random a = new Random(Random.Next());
            return a.Next(1, 500);
        }

        private static Product GerRandomProduct()
        {
            return new Product
            {
                Name = GetRandomProductName(),
                UnitPrice = GetRandomPrice()
            };
        }

        private static double GetRandomPrice()
        {
            if (!ConstantUnitPrice)
            {
                Random a = new Random(Random.Next());
                return a.NextDouble() * 100;
            }
            return 50;
        }

        private static string GetRandomProductName()
        {
            Random a = new Random(Random.Next());
            return _products[a.Next(_products.Length)];
        }

        private static DateTime GetRandomDate()
        {
            Random a = new Random(Random.Next());
            int day = a.Next(1, 28);
            int month = a.Next(1, 12);
            int year = a.Next(DateTime.Today.Year - 5, DateTime.Today.Year + 1);
            return new DateTime(year, month, day);
        }
        #endregion  
    }
}