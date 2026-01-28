using System;
 
namespace Infragistics.Samples.Shared.Models
{
    public class Sale
    {
        public Sale()
        {
            this.Product = new Product();
            this.Seller = new Seller();
        }
        public Product Product { get; set; }
        public Seller Seller { get; set; }
        public DateTime Date { get; set; }

        public double Value { get; set; }
        public string Quarter { get; set; }
        
        public string City
        {
            get { return Seller.City; }
            set { Seller.City = value; }
        }

        public int NumberOfUnits { get; set; }
        public double AmountOfSale
        {
            get { return UnitPrice * NumberOfUnits; }
            set { Product.UnitPrice = value / NumberOfUnits; }
        }
        public double UnitPrice
        {
            get { return Product.UnitPrice; }
            set { Product.UnitPrice = value; }
        }
    }
    public class Seller
    {
        public string Name { get; set; }
        public string City { get; set; }
    }

}