using System;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    /// <summary>
    /// Represents a collection of Product objects with a sample of 10 radomly generated items
    /// </summary>
    public class ProductDataSample : ObservableCollection<Product>
    {
        public ProductDataSample()
        {
            ObservableCollection<Product> products = ProductDataGenerator.GenerateProducts(10, 4);
            foreach (Product product in products)
            {
                this.Add(product);
            }
        }
    }
    public class ProductDataGenerator
    {
        private static readonly Random Random = new Random();

        public static ObservableCollection<Product> GenerateProducts(int productCount, int salesPerProductCount)
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            for (double i = 0; i < productCount; i++)
            {
                Product product = new Product();
                product.Name = "P" + i;
                product.Revenue = 0;
                ObservableCollection<Sale> sales = SalesDataGenerator.GenerateSales(salesPerProductCount);
                foreach (Sale sale in sales)
                {
                    product.Revenue += sale.Value;
                }
                products.Add(product);
            }
            return products;
        }
     }

}