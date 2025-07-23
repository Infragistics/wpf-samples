using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Display
{
    public partial class IDataErrorInfoSupport : SampleContainer
    {

        public IDataErrorInfoSupport()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Products.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Products")
                              select new ProductLocal
                              {
                                  SKU = d.Element("ProductID").GetString(),
                                  Name = d.Element("ProductName").GetString(),
                                  Category = d.Element("Category").GetString(),
                                  Supplier = d.Element("Supplier").GetString(),
                                  UnitPrice = d.Element("UnitPrice").GetDecimal(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  UnitsOnOrder = d.Element("UnitsOnOrder").GetInt(),
                                  QuantityPerUnit = d.Element("QuantityPerUnit").GetString()
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();
        }
    }

    public class ProductLocal : ObservableModel
    {
        public ProductLocal()
        {
        }

        private string sku;
        public string SKU
        {
            get
            {
                return this.sku;
            }
            set
            {
                if (this.sku != value)
                {
                    this.sku = value;
                    this.OnPropertyChanged("SKU");
                }
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                    bool valid = ValidationHandler.ValidateRule("Name", GridStrings.XWG_IDataErrorInfo,
                                                               () => (value.Length >= 5));
                }
            }
        }

        private string category;
        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                if (this.category != value)
                {
                    this.category = value;
                    this.OnPropertyChanged("Category");
                }
            }
        }

        private string designer;
        public string Designer
        {
            get
            {
                return this.designer;
            }
            set
            {
                if (this.designer != value)
                {
                    this.designer = value;
                    this.OnPropertyChanged("Designer");
                }
            }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get
            {
                return this.imageUrl;
            }
            set
            {
                if (this.imageUrl != value)
                {
                    this.imageUrl = value;
                    this.OnPropertyChanged("ImageUrl");
                }
            }
        }

        private bool inStock;
        public bool InStock
        {
            get
            {
                return this.inStock;
            }
            set
            {
                if (this.inStock != value)
                {
                    this.inStock = value;
                    this.OnPropertyChanged("InStock");
                }
            }
        }

        private bool onBackOrder;
        public bool OnBackOrder
        {
            get
            {
                return this.onBackOrder;
            }
            set
            {
                if (this.onBackOrder != value)
                {
                    this.onBackOrder = value;
                    this.OnPropertyChanged("OnBackOrder");
                }
            }
        }

        private string supplier;
        public string Supplier
        {
            get
            {
                return this.supplier;
            }
            set
            {
                if (this.supplier != value)
                {
                    this.supplier = value;
                    this.OnPropertyChanged("Supplier");
                }
            }
        }

        private decimal unitPrice;
        public decimal UnitPrice
        {
            get
            {
                return this.unitPrice;
            }
            set
            {
                if (this.unitPrice != value)
                {
                    this.unitPrice = value;
                    this.OnPropertyChanged("UnitPrice");
                }
            }
        }

        private int unitsInStock;
        public int UnitsInStock
        {
            get
            {
                return this.unitsInStock;
            }
            set
            {
                if (this.unitsInStock != value)
                {
                    this.unitsInStock = value;
                    this.OnPropertyChanged("UnitsInStock");
                }
            }
        }

        private int unitsOnOrder;
        public int UnitsOnOrder
        {
            get
            {
                return this.unitsOnOrder;
            }
            set
            {
                if (this.unitsOnOrder != value)
                {
                    this.unitsOnOrder = value;
                    this.OnPropertyChanged("UnitsOnOrder");
                }
            }
        }
        private string quantityPerUnit;
        public string QuantityPerUnit
        {
            get
            {
                return this.quantityPerUnit;
            }
            set
            {
                if (this.quantityPerUnit != value)
                {
                    this.quantityPerUnit = value;
                    this.OnPropertyChanged("QuantityPerUnit");
                }
            }
        }

        private Uri productUri;
        public Uri ProductUri
        {
            get
            {
                return this.productUri;
            }
            set
            {
                this.productUri = value;
                this.OnPropertyChanged("ProductUri");
            }
        }

    }
}

