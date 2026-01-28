using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for Products objects loaded from XML file.
    /// </summary>
    public class ProductsDataProvider
    {
        public ProductsDataProvider()
        {
            _xmlProvider = new XmlDataProvider();
        }
        private readonly XmlDataProvider _xmlProvider;
        /// <summary>
        /// Occurs when loading of is Product objects from xml file is completed
        /// </summary>
        public event EventHandler<LoadProductsCompletedEventArgs> LoadProductsCompleted;
        
        /// <summary>
        /// Loads Product objects from xml file called: "Products.xml"
        /// </summary>
        public void LoadProductsAsync()
        {
            _xmlProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _xmlProvider.GetXmlDataAsync("Products.xml");
        }
        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnLoadProductsCompleted(new LoadProductsCompletedEventArgs(e.Error));
            }
            else
            {
                XDocument doc = e.Result;
                List<Product> products = (from d in doc.Descendants("Products")
                                  select new Product
                                  {
                                      SKU = d.Element("ProductID").GetString(),
                                      Name = d.Element("ProductName").GetString(),
                                      Category = d.Element("Category").GetString(),
                                      Supplier = d.Element("Supplier").GetString(),
                                      UnitPrice = d.Element("UnitPrice").GetDouble(),
                                      UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                      UnitsOnOrder = d.Element("UnitsOnOrder").GetInt(),
                                      QuantityPerUnit = d.Element("QuantityPerUnit").GetString()
                                  }).ToList<Product>();

                OnLoadProductsCompleted(new LoadProductsCompletedEventArgs(products));
            }
        }

        private void OnLoadProductsCompleted(LoadProductsCompletedEventArgs eventArgs)
        {
            if (this.LoadProductsCompleted != null)
            {
                this.LoadProductsCompleted(this, eventArgs);
            }
        }
    }

    public class LoadProductsCompletedEventArgs : EventArgs
    {

        public LoadProductsCompletedEventArgs(List<Product> data)
        {
            this.Result = data;
            this.Error = null;
        }

        public LoadProductsCompletedEventArgs(Exception error)
        {
            this.Result = null;
            this.Error = error;
        }
        public List<Product> Result { get; set; }
        public Exception Error { get; private set; }

    }
}