using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace IGDiagram.ViewModels
{
    public class NorthwindViewModel : ObservableModel
    {
        private ObservableCollection<SimpleEntityType> _types;
        public ObservableCollection<SimpleEntityType> Types
        {
            get
            {
                return this._types;
            }
            set
            {
                if (value != this._types)
                {
                    this._types = value;
                    this.OnPropertyChanged("Types");
                }
            }
        }

        private ObservableCollection<SimpleAssosiation> _associations;
        public ObservableCollection<SimpleAssosiation> Associations
        {
            get
            {
                return this._associations;
            }
            set
            {
                if (value != this._associations)
                {
                    this._associations = value;
                    this.OnPropertyChanged("Associations");
                }
            }
        }

        public NorthwindViewModel()
        {
            GetTypes();
            GetAssociations();
        }

        void GetTypes()
        {
            Types = new ObservableCollection<SimpleEntityType>()
            {
                new SimpleEntityType()
                {
                    Name = "CustomerDemographics",
                    Position = new Point(60,360),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "CustomerTypeID", EntityKey = true, Type = typeof(string)},
                        new SimpleProperty(){ Name = "CustomerDesc", EntityKey = false, Type = typeof(string)}
                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Customers", FromRole="CustomerDemographics", ToRole="Customers" }
                    }
                },
                new SimpleEntityType()
                {
                    Name = "Customers",
                    Position = new Point(240,280),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "CustomerID", EntityKey = true, Type = typeof(string)},
                        new SimpleProperty(){ Name = "CompanyName", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "ContractName", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "ContractTitle", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Address", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "City", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Region", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "PostalCode", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Country", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Phone", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Fax", EntityKey = false, Type = typeof(string)}
                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Orders", FromRole="Customers", ToRole="Orders" },
                        new SimpleNavigationProperty(){ Name="CustomerDemographics", FromRole="Customers", ToRole="CustomerDemographics" }
                    }
                },
                new SimpleEntityType()
                {
                    Name = "Orders",
                    Position = new Point(420,240),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "OrderID", EntityKey = true, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "CustomerID", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "EmployeeID", EntityKey = false, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "OrderDate", EntityKey = false, Type = typeof(DateTime)},
                        new SimpleProperty(){ Name = "RequiredDate", EntityKey = false, Type = typeof(DateTime)},
                        new SimpleProperty(){ Name = "ShippedDate", EntityKey = false, Type = typeof(DateTime)},
                        new SimpleProperty(){ Name = "ShipVia", EntityKey = false, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "Freight", EntityKey = false, Type = typeof(decimal)},
                        new SimpleProperty(){ Name = "ShipName", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "ShipAddress", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "ShipCity", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "ShipRegion", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "ShipPostalCode", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "ShipCountry", EntityKey = false, Type = typeof(string)},
                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Customers", FromRole="Orders", ToRole="Customers" },
                        new SimpleNavigationProperty(){ Name="Employees", FromRole="Orders", ToRole="Employees" , PointFromName="Bottom", PointToName="Left" },
                        new SimpleNavigationProperty(){ Name="Order_Details", FromRole="Orders", ToRole="Order_Details" },
                        new SimpleNavigationProperty(){ Name="Shippers", FromRole="Orders", ToRole="Shippers" }
                    }
                },
                new SimpleEntityType()
                {
                    Name = "Shippers",
                    Position = new Point(600,50),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "ShipperID", EntityKey = true, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "CompanyName", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Phone", EntityKey = false, Type = typeof(string)}                        
                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Orders", FromRole="Shippers", ToRole="Orders" }                       
                    }
                },
                new SimpleEntityType()
                {
                    Name = "Order_Details",
                    Position = new Point(600,328),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "OrderID", EntityKey = true, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "ProductID", EntityKey = true, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "UnitPrice", EntityKey = false, Type = typeof(decimal)},
                        new SimpleProperty(){ Name = "Quantity", EntityKey = false, Type = typeof(Int16)},
                        new SimpleProperty(){ Name = "Discount", EntityKey = false, Type = typeof(Single)} 
                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Orders", FromRole="Order_Details", ToRole="Orders" },
                        new SimpleNavigationProperty(){ Name="Products", FromRole="Order_Details", ToRole="Products" }      
                    }
                },
                new SimpleEntityType()
                {
                    Name = "Employees",
                    Position = new Point(600,583),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "EmployeeID", EntityKey = true, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "LastName", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "FirstName", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Title", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "TitleOfCourtesy", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "BirthDate", EntityKey = false, Type = typeof(DateTime)},
                        new SimpleProperty(){ Name = "HireDate", EntityKey = false, Type = typeof(DateTime)},
                        new SimpleProperty(){ Name = "Address", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "City", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Region", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "PostalCode", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Country", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "HomePhone", EntityKey = false, Type = typeof(string)},

                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Employees1", FromRole="Employees", ToRole="Employees", PointFromName="TopLeft", PointToName="TopRight" },
                        //new SimpleNavigationProperty(){ Name="Employees2", FromRole="Employees1", ToRole="Employees" },
                        new SimpleNavigationProperty(){ Name="Orders", FromRole="Employees", ToRole="Orders" },
                        new SimpleNavigationProperty(){ Name="Territories", FromRole="Employees", ToRole="Territories" }
                    },
                    EntityPoints = new ObservableCollection<EntityPoint>()
                    {
                        new EntityPoint(){ Name="Left", Position=new Point(0,.5)},
                        new EntityPoint(){ Name="Right",  Position=new Point(1,.5)},
                        new EntityPoint(){ Name="TopLeft", Position=new Point(.3,0)},
                        new EntityPoint(){ Name="TopRight", Position=new Point(.7,0)}   
                    }

                }, 
                new SimpleEntityType()
                {
                    Name = "Territories",
                    Position = new Point(780,671),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "TerritorieID", EntityKey = true, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "TerritorieDescription", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "RegionID", EntityKey = false, Type = typeof(Int32)}
                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Employees", FromRole="Territories", ToRole="Employees" },
                        new SimpleNavigationProperty(){ Name="Region", FromRole="Territories", ToRole="Region" }
                    }
                },
                new SimpleEntityType()
                {
                    Name = "Region",
                    Position = new Point(960,687),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "RegionID", EntityKey = true, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "RegionDescription", EntityKey = false, Type = typeof(string)},
                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Territories", FromRole="Region", ToRole="Territories" }
                    }
                },
                new SimpleEntityType()
                {
                    Name = "Products",
                    Position = new Point(780,281),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "ProductID", EntityKey = true, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "ProductName", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "SupplierID", EntityKey = false, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "CategoryID", EntityKey = false, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "QuantityPerUnit", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "UnitPrice", EntityKey = false, Type = typeof(decimal)},
                        new SimpleProperty(){ Name = "UnitsInStock", EntityKey = false, Type = typeof(Int16)},
                        new SimpleProperty(){ Name = "UnitsOnOrder", EntityKey = false, Type = typeof(Int16)},
                        new SimpleProperty(){ Name = "ReorderLevel", EntityKey = false, Type = typeof(Int16)},
                        new SimpleProperty(){ Name = "Discontinued", EntityKey = false, Type = typeof(bool)},
                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Categories", FromRole="Products", ToRole="Categories" },
                        new SimpleNavigationProperty(){ Name="Order_Details", FromRole="Products", ToRole="Order_Details" },
                        new SimpleNavigationProperty(){ Name="Suppliers", FromRole="Products", ToRole="Suppliers" }
                    }
                },
                new SimpleEntityType()
                {
                    Name = "Categories",
                    Position = new Point(960,100),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "CategoryID", EntityKey = true, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "CategoryName", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Description", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Picture", EntityKey = false, Type = typeof(byte[])}                        
                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Products", FromRole="Categories", ToRole="Products" }
                    }
                }, 
                new SimpleEntityType()
                {
                    Name = "Suppliers",
                    Position = new Point(960,289),
                    Properties = new ObservableCollection<SimpleProperty>()
                    {
                        new SimpleProperty(){ Name = "SupplierID", EntityKey = true, Type = typeof(Int32)},
                        new SimpleProperty(){ Name = "SupplierName", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "ContractName", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "ContractTitle", EntityKey = false, Type = typeof(string)},                        
                        new SimpleProperty(){ Name = "ContractTitle", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Address", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "City", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Region", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "PostalCode", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "Country", EntityKey = false, Type = typeof(string)},
                        new SimpleProperty(){ Name = "HomePage", EntityKey = false, Type = typeof(string)}
                    },
                    NavigationProperties = new ObservableCollection<SimpleNavigationProperty>()
                    {
                        new SimpleNavigationProperty(){ Name="Products", FromRole="Suppliers", ToRole="Products" }
                    }
                }

            };
        }
        void GetAssociations()
        {
            Associations = new ObservableCollection<SimpleAssosiation>();
            foreach (SimpleEntityType entity in Types)
            {
                foreach (SimpleNavigationProperty navpr in entity.NavigationProperties)
                {
                    var temp = new SimpleAssosiation() { End1Name = navpr.FromRole, End2Name = navpr.ToRole, PointFromName = navpr.PointFromName, PointToName = navpr.PointToName };
                    if (!Associations.Contains(temp))
                    {
                        Associations.Add(temp);
                    }
                }
            }
        }
    }

    public struct SimpleAssosiation : IEquatable<SimpleAssosiation>, INotifyPropertyChanged
    {
        private string _end1name;
        public string End1Name
        {
            get
            {
                return this._end1name;
            }
            set
            {
                if (value != this._end1name)
                {
                    this._end1name = value;
                    this.OnPropertyChanged("End1Name");
                }
            }
        }

        private string _end2name;
        public string End2Name
        {
            get
            {
                return this._end2name;
            }
            set
            {
                if (value != this._end2name)
                {
                    this._end2name = value;
                    this.OnPropertyChanged("End2Name");
                }
            }
        }

        private string _pointFromName;
        public string PointFromName
        {
            get
            {
                return this._pointFromName;
            }
            set
            {
                if (value != this._pointFromName)
                {
                    this._pointFromName = value;
                    this.OnPropertyChanged("PointFromName");
                }
            }
        }

        private string _pointToName;
        public string PointToName
        {
            get
            {
                return this._pointToName;
            }
            set
            {
                if (value != this._pointToName)
                {
                    this._pointToName = value;
                    this.OnPropertyChanged("PointToName");
                }
            }
        } 

        public bool Equals(SimpleAssosiation other)
        {
            if (this.End1Name == other.End1Name && this.End2Name == other.End2Name)
            {
                return true;
            }
            if (this.End1Name == other.End2Name && this.End2Name == other.End1Name)
            {
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        } 
    }

    public class SimpleEntityType : ObservableModel

    {
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private Color _fillColor = Color.FromRgb(0, 122, 204);
        public Color FillColor
        {
            get
            {
                return this._fillColor;
            }
            set
            {
                if (value != this._fillColor)
                {
                    this._fillColor = value;
                    this.OnPropertyChanged("FillColor");
                }
            }
        }

        private Point _position;
        public Point Position
        {
            get
            {
                return this._position;
            }
            set
            {
                if (value != this._position)
                {
                    this._position = value;
                    this.OnPropertyChanged("Position");
                }
            }
        }

        private ObservableCollection<SimpleProperty> _properties;
        public ObservableCollection<SimpleProperty> Properties
        {
            get
            {
                return this._properties;
            }
            set
            {
                if (value != this._properties)
                {
                    this._properties = value;
                    this.OnPropertyChanged("Properties");
                }
            }
        }

        private ObservableCollection<SimpleNavigationProperty> _navigationProperties;
        public ObservableCollection<SimpleNavigationProperty> NavigationProperties
        {
            get
            {
                return this._navigationProperties;
            }
            set
            {
                if (value != this._navigationProperties)
                {
                    this._navigationProperties = value;
                    this.OnPropertyChanged("NavigationProperties");
                }
            }
        }

        private ObservableCollection<EntityPoint> _entityPoints;
        public ObservableCollection<EntityPoint> EntityPoints
        {
            get
            {
                return this._entityPoints;
            }
            set
            {
                if (value != this._entityPoints)
                {
                    this._entityPoints = value;
                    this.OnPropertyChanged("EntityPoints");
                }
            }
        }         
    }

    public class EntityPoint : ObservableModel

    {
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private Point _position;
        public Point Position
        {
            get
            {
                return this._position;
            }
            set
            {
                if (value != this._position)
                {
                    this._position = value;
                    this.OnPropertyChanged("Position");
                }
            }
        } 
    }

    public class SimpleProperty : ObservableModel
    {
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private bool _entityKey;
        public bool EntityKey
        {
            get
            {
                return this._entityKey;
            }
            set
            {
                if (value != this._entityKey)
                {
                    this._entityKey = value;
                    this.OnPropertyChanged("EntityKey");
                }
            }
        }

        private Type _type;
        public Type Type
        {
            get
            {
                return this._type;
            }
            set
            {
                if (value != this._type)
                {
                    this._type = value;
                    this.OnPropertyChanged("Type");
                }
            }
        } 
    }

    public class SimpleNavigationProperty : ObservableModel
    {
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private string _association;
        public string Association
        {
            get
            {
                return this._association;
            }
            set
            {
                if (value != this._association)
                {
                    this._association = value;
                    this.OnPropertyChanged("Association");
                }
            }
        }

        private string _fromRole;
        public string FromRole
        {
            get
            {
                return this._fromRole;
            }
            set
            {
                if (value != this._fromRole)
                {
                    this._fromRole = value;
                    this.OnPropertyChanged("FromRole");
                }
            }
        }

        private string _toRole;
        public string ToRole
        {
            get
            {
                return this._toRole;
            }
            set
            {
                if (value != this._toRole)
                {
                    this._toRole = value;
                    this.OnPropertyChanged("ToRole");
                }
            }
        }

        private string _pointFromName;
        public string PointFromName
        {
            get
            {
                return this._pointFromName;
            }
            set
            {
                if (value != this._pointFromName)
                {
                    this._pointFromName = value;
                    this.OnPropertyChanged("PointFromName");
                }
            }
        }

        private string _pointToName;
        public string PointToName
        {
            get
            {
                return this._pointToName;
            }
            set
            {
                if (value != this._pointToName)
                {
                    this._pointToName = value;
                    this.OnPropertyChanged("PointToName");
                }
            }
        }
    }
}
