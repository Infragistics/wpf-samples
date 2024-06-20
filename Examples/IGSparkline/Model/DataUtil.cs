using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IGSparkline.Model
{
    public class DataUtil
    {
        #region private members

        #region private fields

        private static ObservableCollection<Category> categories;
        private static ObservableCollection<Product> products;
        private static ObservableCollection<Category> categoriesAndProducts;

        #endregion

        #region private methods

        private static ObservableCollection<Category> CreateCategories()
        {
            return new ObservableCollection<Category>
            {
                new Category 
                {
                    CategoryID = 1, 
                    CategoryName = "Beverages", 
                    Description = "Soft drinks, coffees, teas, beers, and ales"
                },

                new Category 
                {
                    CategoryID = 2, 
                    CategoryName = "Condiments", 
                    Description = "Sweet and savory sauces, relishes, spreads, and seasonings"
                },
                
                new Category
                {
                    CategoryID = 3,
                    CategoryName = "Confections",
                    Description = "Desserts, candies, and sweet breads",
                },

                new Category
                {
                    CategoryID = 4,
                    CategoryName = "Dairy Product",
                    Description = "Cheeses",
                },

                new Category
                {
                    CategoryID = 5,
                    CategoryName = "Grains/Cereals",
                    Description = "Breads, crackers, pasta, and cereal",
                },

                new Category
                {
                    CategoryID = 6,
                    CategoryName = "Meat/Poultry",
                    Description = "Prepared meats",
                },

                new Category
                {
                    CategoryID = 7,
                    CategoryName = "Produce",
                    Description = "Dried fruit and bean curd",
                },

                new Category
                {
                    CategoryID = 8,
                    CategoryName = "Seafood",
                    Description = "Seaweed and fish",
                }
            };
        }

        private static ObservableCollection<Product> CreateProducts()
        {
            return new ObservableCollection<Product>
            {
                new Product
                {
                    ProductID = 1,
                    ProductName = "Chai",
                    CategoryID = 1,
                    QuantityPerUnit = "10 boxes x 20 bags",
                    UnitPrice = 18.0000m,
                    UnitsInStock = 39,
                    UnitsOnOrder = 0,
                    ReorderLevel = 10,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 2,
                    ProductName = "Chang",
                    CategoryID = 1,
                    QuantityPerUnit = "24 - 12 oz bottles",
                    UnitPrice = 19.0000m,
                    UnitsInStock = 17,
                    UnitsOnOrder = 40,
                    ReorderLevel = 25,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 24,
                    ProductName = "Guaraná Fantástica",
                    CategoryID = 1,
                    QuantityPerUnit = "12 - 355 ml cans",
                    UnitPrice = 4.5000m,
                    UnitsInStock = 20,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = true
                },

                new Product
                {
                    ProductID = 77,
                    ProductName = "Original Frankfurter grüne Soße",
                    CategoryID = 2,
                    QuantityPerUnit = "12 boxes",
                    UnitPrice = 13.0000m,
                    UnitsInStock = 32,
                    UnitsOnOrder = 0,
                    ReorderLevel = 15,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 61,
                    ProductName = "Sirop d'érable",
                    CategoryID = 2,
                    QuantityPerUnit = "24 - 500 ml bottles",
                    UnitPrice = 28.5000m,
                    UnitsInStock = 113,
                    UnitsOnOrder = 0,
                    ReorderLevel = 25,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 63,
                    ProductName = "Vegie-spread",
                    CategoryID = 2,
                    QuantityPerUnit = "15 - 625 g jars",
                    UnitPrice = 43.9000m,
                    UnitsInStock = 24,
                    UnitsOnOrder = 0,
                    ReorderLevel = 5,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 16,
                    ProductName = "Pavlova",
                    CategoryID = 3,
                    QuantityPerUnit = "32 - 500 g boxes",
                    UnitPrice = 17.4500m,
                    UnitsInStock = 29,
                    UnitsOnOrder = 0,
                    ReorderLevel = 10,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 19,
                    ProductName = "Teatime Chocolate Biscuits",
                    CategoryID = 3,
                    QuantityPerUnit = "10 boxes x 12 pieces",
                    UnitPrice = 9.2000m,
                    UnitsInStock = 25,
                    UnitsOnOrder = 0,
                    ReorderLevel = 5,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 20,
                    ProductName = "Sir Rodney's Marmalade",
                    CategoryID = 3,
                    QuantityPerUnit = "30 gift boxes",
                    UnitPrice = 81.0000m,
                    UnitsInStock = 40,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 69,
                    ProductName = "Gudbrandsdalsost",
                    CategoryID = 4,
                    QuantityPerUnit = "10 kg pkg.",
                    UnitPrice = 36.0000m,
                    UnitsInStock = 26,
                    UnitsOnOrder = 0,
                    ReorderLevel = 15,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 71,
                    ProductName = "Flotemysost",
                    CategoryID = 4,
                    QuantityPerUnit = "10 - 500 g pkgs.",
                    UnitPrice = 21.5000m,
                    UnitsInStock = 26,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 72,
                    ProductName = "Mozzarella di Giovanni",
                    CategoryID = 4,
                    QuantityPerUnit = "24 - 200 g pkgs.",
                    UnitPrice = 34.8000m,
                    UnitsInStock = 14,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false
                },
                new Product
                {
                    ProductID = 22,
                    ProductName = "Gustaf's Knäckebröd",
                    CategoryID = 5,
                    QuantityPerUnit = "24 - 500 g pkgs.",
                    UnitPrice = 21.0000m,
                    UnitsInStock = 104,
                    UnitsOnOrder = 0,
                    ReorderLevel = 25,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 23,
                    ProductName = "Tunnbröd",
                    CategoryID = 5,
                    QuantityPerUnit = "12 - 250 g pkgs.",
                    UnitPrice = 9.0000m,
                    UnitsInStock = 61,
                    UnitsOnOrder = 0,
                    ReorderLevel = 25,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 53,
                    ProductName = "Perth Pasties",
                    CategoryID = 6,
                    QuantityPerUnit = "48 pieces",
                    UnitPrice = 32.8000m,
                    UnitsInStock = 0,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = true
                },

                new Product
                {
                    ProductID = 54,
                    ProductName = "Tourtière",
                    CategoryID = 6,
                    QuantityPerUnit = "16 pies",
                    UnitPrice = 7.4500m,
                    UnitsInStock = 21,
                    UnitsOnOrder = 0,
                    ReorderLevel = 10,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 55,
                    ProductName = "Pâté chinois",
                    CategoryID = 6,
                    QuantityPerUnit = "24 boxes x 2 pies",
                    UnitPrice = 24.0000m,
                    UnitsInStock = 115,
                    UnitsOnOrder = 0,
                    ReorderLevel = 20,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 28,
                    ProductName = "Rössle Sauerkraut",
                    CategoryID = 7,
                    QuantityPerUnit = "25 - 825 g cans",
                    UnitPrice = 45.6000m,
                    UnitsInStock = 26,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = true
                },

                new Product
                {
                    ProductID = 7,
                    ProductName = "Uncle Bob's Organic Dried Pears",
                    CategoryID = 7,
                    QuantityPerUnit = "12 - 1 lb pkgs.",
                    UnitPrice = 30.0000m,
                    UnitsInStock = 15,
                    UnitsOnOrder = 0,
                    ReorderLevel = 10,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 14,
                    ProductName = "Tofu",
                    CategoryID = 7,
                    QuantityPerUnit = "40 - 100 g pkgs.",
                    UnitPrice = 23.2500m,
                    UnitsInStock = 35,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 45,
                    ProductName = "Rogede sild",
                    CategoryID = 8,
                    QuantityPerUnit = "1k pkg.",
                    UnitPrice = 9.5000m,
                    UnitsInStock = 5,
                    UnitsOnOrder = 70,
                    ReorderLevel = 15,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 46,
                    ProductName = "Spegesild",
                    CategoryID = 8,
                    QuantityPerUnit = "4 - 450 g glasses",
                    UnitPrice = 12.0000m,
                    UnitsInStock = 95,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false
                },

                new Product
                {
                    ProductID = 58,
                    ProductName = "Escargots de Bourgogne",
                    CategoryID = 8,
                    QuantityPerUnit = "24 pieces",
                    UnitPrice = 13.2500m,
                    UnitsInStock = 62,
                    UnitsOnOrder = 0,
                    ReorderLevel = 20,
                    Discontinued = false
                }
            };
        }

        private static ObservableCollection<Category> CreateCategoriesAndProducts()
        {
            ObservableCollection<Category> retVal = CreateCategories();

            ObservableCollection<Product> tempProducts = CreateProducts();

            Category parentCategory = null;

            foreach (Product p in tempProducts)
            {
                if (parentCategory == null || parentCategory.CategoryID != p.CategoryID)
                {
                    parentCategory = retVal.First<Category>(category => category.CategoryID == p.CategoryID);
                }

                parentCategory.Products.Add(p);
            }

            return retVal;
        }

        #endregion

        #endregion

        #region public members

        #region public properties
        public static ObservableCollection<Category> Categories
        {
            get
            {
                if (categories == null)
                    categories = CreateCategories();

                return categories;
            }
        }

        public static ObservableCollection<Product> Products
        {
            get
            {
                if (products == null)
                    products = CreateProducts();

                return products;
            }
        }

        public static ObservableCollection<Category> CategoriesAndProducts
        {
            get
            {
                if (categoriesAndProducts == null)
                    categoriesAndProducts = CreateCategoriesAndProducts();

                return categoriesAndProducts;
            }
        }
        #endregion

        #endregion
    }


    //____________________________________________ Category ___________________________________
    public class Category : INotifyPropertyChanged
    {
        #region private fields

        private int categoryID;
        private string categoryName;
        private string description;
        private ObservableCollection<Product> products;

        #endregion

        #region public properties
        public int CategoryID
        {
            get
            {
                return this.categoryID;
            }
            set
            {
                if (this.categoryID != value)
                {
                    this.categoryID = value;
                    OnPropertyChanged("CategoryID");
                }
            }
        }

        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
            set
            {
                if (this.categoryName != value)
                {
                    this.categoryName = value;
                    OnPropertyChanged("CategoryName");
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public ObservableCollection<Product> Products
        {
            get
            {
                if (products == null)
                {
                    products = new ObservableCollection<Product>();
                }

                return this.products;
            }
        }

        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }


    // _________________________________________ Product ______________________________________
    public class Product : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region private members

        private int productID;
        private string productName;
        private int categoryID;
        private string quantityPerUnit;
        private decimal unitPrice;
        private int unitsInStock;
        private int unitsOnOrder;
        private int reorderLevel;
        private bool discontinued;

        #endregion

        #region public properties

        public int ProductID
        {
            get
            {
                return this.productID;
            }
            set
            {
                if (this.productID != value)
                {
                    this.productID = value;
                    OnPropertyChanged("ProductID");
                }
            }
        }

        public string ProductName
        {
            get
            {
                return this.productName;
            }
            set
            {
                if (this.productName != value)
                {
                    this.productName = value;
                    OnPropertyChanged("ProductName");
                }
            }
        }

        public int CategoryID
        {
            get
            {
                return this.categoryID;
            }
            set
            {
                if (this.categoryID != value)
                {
                    this.categoryID = value;
                    OnPropertyChanged("CategoryID");
                }
            }
        }

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
                    OnPropertyChanged("QuantityPerUnit");
                }
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return this.unitPrice;
            }
            set
            {
                if (value < 0)
                    throw new Exception("Negative numbers are not allowed.");

                this.unitPrice = value;
                OnPropertyChanged("UnitPrice");
            }
        }

        public int UnitsInStock
        {
            get
            {
                return this.unitsInStock;
            }
            set
            {
                if (value < 0)
                    throw new Exception("Negative numbers are not allowed.");

                this.unitsInStock = value;
                OnPropertyChanged("UnitsInStock");
            }
        }

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
                    OnPropertyChanged("UnitsOnOrder");
                }
            }
        }

        public int ReorderLevel
        {
            get
            {
                return this.reorderLevel;
            }
            set
            {
                if (this.reorderLevel != value)
                {
                    this.reorderLevel = value;
                    OnPropertyChanged("ReorderLevel");
                }
            }
        }

        public bool Discontinued
        {
            get
            {
                return this.discontinued;
            }
            set
            {
                if (this.discontinued != value)
                {
                    this.discontinued = value;
                    OnPropertyChanged("Discontinued");
                }
            }
        }

        #endregion
    }
}
