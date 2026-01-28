using IGDataGrid.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IGDataGrid.Samples.ViewModel
{
    public class ProductsDataProvider : ObservableModel
    {
        public ProductsDataProvider()
        {
            DownloadDataSource();
        }

        private ObservableCollection<ProductExtended> _products = null;
        public ObservableCollection<ProductExtended> Products
        {
            get
            {
                return this._products;
            }
            set
            {
                if (this._products != value)
                {
                    this._products = value;
                    this.OnPropertyChanged("Products");
                }
            }
        }

        private ObservableCollection<Role> _roles = null;
        public ObservableCollection<Role> Roles
        {
            get
            {
                return this._roles;
            }
            set
            {
                if (this._roles != value)
                {
                    this._roles = value;
                    this.OnPropertyChanged("Roles");
                }
            }
        }

        private Role _selectedRole;
        public Role SelectedRole
        {
            get
            {
                return _selectedRole;
            }
            set
            {
                if (_selectedRole != value)
                {
                    _selectedRole = value;
                    this.OnPropertyChanged("SelectedRole");
                }
            }
        }

        private void DownloadDataSource()
        {
            var dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Products.xml");

            CreateRoles();
        }

        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            var doc = e.Result;
            var dataSource = (from d in doc.Descendants("Products")
                              select new ProductExtended()
                              {
                                  SKU = d.Element("ProductID").GetString(),
                                  Name = d.Element("ProductName").GetString(),
                                  Category = d.Element("Category").GetString(),
                                  Supplier = d.Element("Supplier").GetString(),
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  UnitsOnOrder = d.Element("UnitsOnOrder").GetInt(),
                                  QuantityPerUnit = d.Element("QuantityPerUnit").GetString(),
                                  SupplyDate = DateTime.Today,
                                  ReadyForSale = true
                              });

            this.Products = new ObservableCollection<ProductExtended>(dataSource.ToList());
        }

        private const string AdminRole = "admin";
        private const string DefaultRole = "default";

        public readonly string General = "GENERAL"; 
        public readonly string Column_SKU = "SKU";
        public readonly string Column_NAME = "Name";
        public readonly string Column_UNIT_PRICE = "UnitPrice"; 


        private Dictionary<string, RoleSettings> SetRolesSettings(string roleID)
        {
            var roleSettingsList = new Dictionary<string, RoleSettings>();

            // Set settings based on user role
            if (roleID.Equals(AdminRole))
            {

                // Set general settings
                var generalSettings = new RoleSettings
                    {
                        IsAddNewRecordAllowed = true, 
                        IsDeleteAllowed = true,
                        IsColumnVisible = true
                    };

                roleSettingsList.Add(General, generalSettings);

                // Set specific settings for SKU field
                var roleSettingsSKU = new RoleSettings
                    {
                        ColumnName = DataGridStrings.DataGrid_ProductID, 
                        IsColumnReadOnly = false
                    };

                roleSettingsList.Add(Column_SKU, roleSettingsSKU);

                // Set specific settings for ProductName field
                var roleSettingsName = new RoleSettings
                    {
                        ColumnName = DataGridStrings.DataGrid_Name, 
                        IsColumnReadOnly = false
                    };

                roleSettingsList.Add(Column_NAME, roleSettingsName);

                // Set specific settings for UnitPrice field
                var roleSettingsUnitPrice = new RoleSettings
                {
                    IsEditAllowed = true
                };

                roleSettingsList.Add(Column_UNIT_PRICE, roleSettingsUnitPrice);
            }
            else if (roleID.Equals(DefaultRole))
            {
                // Set general settings
                var generalSettings = new RoleSettings
                {
                    IsAddNewRecordAllowed = false,
                    IsDeleteAllowed = false,
                    IsColumnVisible = false
                };

                roleSettingsList.Add(General, generalSettings);

                // Set specific settings for SKU field
                var roleSettingsSKU = new RoleSettings
                {
                    ColumnName = DataGridStrings.DataGrid_SKU,
                    IsColumnReadOnly = true
                };

                roleSettingsList.Add(Column_SKU, roleSettingsSKU);

                // Set specific settings for ProductName field
                var roleSettingsName = new RoleSettings
                {
                    ColumnName = DataGridStrings.DataGrid_ProductName,
                    IsColumnReadOnly = true
                };

                roleSettingsList.Add(Column_NAME, roleSettingsName);

                // Set specific settings for UnitPrice field
                var roleSettingsUnitPrice = new RoleSettings
                {
                    IsEditAllowed = false
                };

                roleSettingsList.Add(Column_UNIT_PRICE, roleSettingsUnitPrice);
            }

            return roleSettingsList;
        }

        private void CreateRoles()
        {
            Roles = new ObservableCollection<Role>
                {
                    new Role
                        {
                            RoleName = "Administrator", RoleSettings = SetRolesSettings(AdminRole)
                        }, 
                    new Role
                        {
                            RoleName = "Default User", RoleSettings = SetRolesSettings(DefaultRole)
                        }, 
                };
        }
    }

    public class OptionsList : ObservableCollection<OptionElement>
    {
        public OptionsList()
        {
            this.Add(new OptionElement() { Name = CommonStrings.CategoryProvider_Beverages });
            this.Add(new OptionElement() { Name = CommonStrings.CategoryProvider_Condiments });
            this.Add(new OptionElement() { Name = CommonStrings.CategoryProvider_Confections });
            this.Add(new OptionElement() { Name = CommonStrings.CategoryProvider_DiaryProducts });
            this.Add(new OptionElement() { Name = CommonStrings.CategoryProvider_Grains });
            this.Add(new OptionElement() { Name = CommonStrings.CategoryProvider_Meat });
            this.Add(new OptionElement() { Name = CommonStrings.CategoryProvider_Produce });
            this.Add(new OptionElement() { Name = CommonStrings.CategoryProvider_Seafood });
        }
    }

    public class ProductExtended : Product
    {
        public DateTime SupplyDate { get; set; }
        public bool ReadyForSale { get; set; }
    }
    public class OptionElement : ObservableModel
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
                if (this._name != value)
                {
                    this._name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
    }

    public class RoleSettings : ObservableModel
    {
        public string ColumnName { get; set; }
        public bool? IsColumnReadOnly { get; set; }
        public bool? IsEditAllowed { get; set; }
        public bool? IsAddNewRecordAllowed { get; set; }
        public bool? IsDeleteAllowed { get; set; }
        public bool? IsColumnVisible { get; set; }
    }

    public class Role : ObservableModel
    {
        public string RoleName { get; set; }
        public Dictionary<string, RoleSettings> RoleSettings { get; set; }
    }
}
