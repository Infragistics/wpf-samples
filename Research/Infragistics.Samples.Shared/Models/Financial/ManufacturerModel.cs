using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.DataProviders;

namespace Infragistics.Samples.Shared.Models
{
    public class ManufacturerModel : ObservableModel
    {
        /// <summary>
        /// The constructor for the ManufacturerModel class.
        /// </summary>
        public ManufacturerModel()
        {
            var dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Manufacturers.xml");
        }

        /// <summary>
        /// Handles the OnGetXmlDataCompleted event and parses the XML data for the manufacturers.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A GetXmlDataCompletedEventArgs object.</param>
        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if(e.Error != null) return;

            XDocument document = e.Result; 

            Manufacturers = document
                .Element("Manufacturers")
                .Elements("Manufacturer")
                .Select(manufacturer => new Manufacturer
                    (
                        manufacturer.Attribute("Name").Value,
                        manufacturer.Attribute("Revenue").Value,
                        manufacturer
                            .Elements("Product")
                            .Select(product => new Product
                            (
                                product.Attribute("Name").Value,
                                product.Attribute("StandardCost").Value,
                                product
                                    .Elements("InventoryEntry")
                                    .Select(entry => new InventoryEntry
                                    (
                                        entry.Attribute("Shelf").Value,
                                        entry.Attribute("Quantity").Value
                                    )).ToList()
                            )).ToList()
                    ));

            OnPropertyChanged("Manufacturers");
        }

        /// <summary>
        /// A collection with Manufacturers.
        /// </summary>
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
    }

    public class Manufacturer
    {
        /// <summary>
        /// A parameterised constructor for the Manufacturer class.
        /// </summary>
        /// <param name="name">The name of the manufacturer.</param>
        /// <param name="revenue">The manufacturer's revenue.</param>
        /// <param name="products">The products produced by the manufacturer.</param>
        public Manufacturer(string name, string revenue, IList<Product> products)
        {
            Name = name;
            Revenue = Int32.Parse(revenue);
            Products = products;
        }

        /// <summary>
        /// The name of the manufacturer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The revenue of the manufacturer.
        /// </summary>
        public int Revenue { get; set; }

        /// <summary>
        /// The products produced by the manufacturer.
        /// </summary>
        public IList<Product> Products { get; set; }

        /// <summary>
        /// The number of products.
        /// </summary>
        public int ProductsCount { get { return Products.Count; } }
    }

    public class InventoryEntry
    {
        /// <summary>
        /// A parameterised constructor for the InventoryEntry class.
        /// </summary>
        /// <param name="shelf">The name of the shelf.</param>
        /// <param name="quantity">The number of products on the shelf.</param>
        public InventoryEntry(string shelf, string quantity)
        {
            Shelf = shelf;
            Quantity = Int32.Parse(quantity);
        }

        /// <summary>
        /// The name of the shelf.
        /// </summary>
        public string Shelf { get; set; }

        /// <summary>
        /// The number of products on the shelf.
        /// </summary>
        public int Quantity { get; set; }
    }
}
