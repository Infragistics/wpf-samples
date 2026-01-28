using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Xml.Serialization;

namespace IGPivotGrid.Samples.Controls
{
    public class SalesCollection : ObservableCollection<Sale>
    {
        public SalesCollection()
        {
            // default to EN data file unless the app is running in JP culture
            var path = "/IGPivotGrid;component/Assets/EN/Sales.xml";
            if (Thread.CurrentThread.CurrentUICulture.Name == "ja-JP")
                path = "/IGPivotGrid;component/Assets/JA/Sales.xml";
             
            var uri = new Uri(path, UriKind.RelativeOrAbsolute);
            var sri = Application.GetResourceStream(uri);
            var serializer = new XmlSerializer(typeof(List<Sale>));
            (serializer.Deserialize(sri.Stream) as List<Sale>).ForEach(s => this.Add(s));
        }
    }

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
        public string Quarter 
        { 
            get 
            {
                if (Date.Month < 4) return String.Format("Q1 {0}", Date.Year);
                else if (Date.Month < 7) return String.Format("Q2 {0}", Date.Year);
                else if (Date.Month < 10) return String.Format("Q3 {0}", Date.Year);
                else return String.Format("Q4 {0}", Date.Year);
            }
        }

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

    public class Product
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }
}
