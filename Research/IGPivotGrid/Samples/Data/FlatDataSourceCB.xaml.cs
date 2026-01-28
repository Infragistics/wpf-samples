using System;
using System.ComponentModel;
using System.Windows;
using IGPivotGrid.Resources;
using IGPivotGrid.Samples.Controls;
using Infragistics.Olap.FlatData;
using Infragistics.Olap.Xmla;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Data
{
    public partial class FlatDataSourceCB : SampleContainer
    {
        public FlatDataSourceCB()
        {
            InitializeComponent();
            InitializeDataSource();
        }

        private void InitializeDataSource()
        {
            FlatDataSource flatDataSource = new FlatDataSource()
            {
                ItemsSource = new SalesCollection(),
                Cube = XmlaDataSource.GenerateInitialCube("Sale"),
                Rows = XmlaDataSource.GenerateInitialItems("[Seller].[Seller]"),
                Columns = XmlaDataSource.GenerateInitialItems("[Product].[Product]"),
                Measures = XmlaDataSource.GenerateInitialItems("AmountOfSale")
            };

            flatDataSource.PropertyChanged += DataSource_PropertyChanged;

            CubeMetadata cubeMetadata = new CubeMetadata();
            cubeMetadata.DisplayName = PivotGridStrings.XPG_Data_CubeMetadata_DisplayName;
            cubeMetadata.DataTypeFullName = typeof(Sale).FullName;
            
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Product",
                DisplayName = PivotGridStrings.XPG_Data_PropertyNames_Product
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "City",
                DisplayName = PivotGridStrings.XPG_Data_PropertyNames_City
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Seller",
                DisplayName = PivotGridStrings.XPG_Data_PropertyNames_Seller
            });

            //The following declarations are only for localization purposes.They are not needed for the data to be available.
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "AmountOfSale",
                DisplayName = PivotGridStrings.XPG_Data_AmountOfSale_DisplayName,
                DisplayFormat = PivotGridStrings.XPG_Data_AmountOfSale_DisplayFormat
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "NumberOfUnits",
                DisplayName = PivotGridStrings.XPG_Data_NumberOfUnits_DisplayName
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Date",
                DisplayName = PivotGridStrings.XPG_Data_PropertyNames_Date
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Quarter",
                DisplayName = PivotGridStrings.XPG_Data_Quarter_DisplayName
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "UnitPrice",
                DisplayName = PivotGridStrings.XPG_Data_UnitPrice_DisplayName
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Value",
                DisplayName = PivotGridStrings.XPG_Data_Value_DisplayName
            });
            flatDataSource.CubesSettings.Add(cubeMetadata);

            HierarchyDescriptor<Sale> sellerHierarchy = new HierarchyDescriptor<Sale>(p => p.Seller);
            sellerHierarchy.HierarchyDisplayName = PivotGridStrings.XPG_Data_PropertyNames_Seller;
            sellerHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Seller_All,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Seller_All
                );
            sellerHierarchy.AddLevel(p =>
                p.Seller.Name,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Seller_Name
                );
            flatDataSource.AddHierarchyDescriptor(sellerHierarchy);

            HierarchyDescriptor<Sale> productHierarchy = new HierarchyDescriptor<Sale>(p => p.Product);
            productHierarchy.HierarchyDisplayName = PivotGridStrings.XPG_Data_PropertyNames_Product;
            productHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Product_All,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Product_All
                );
            productHierarchy.AddLevel(p =>
                p.Product.Name,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Product_Name
                );
            flatDataSource.AddHierarchyDescriptor(productHierarchy);

            HierarchyDescriptor<Sale> locationHierarchy = new HierarchyDescriptor<Sale>(p => p.City);
            locationHierarchy.HierarchyDisplayName = PivotGridStrings.XPG_Data_PropertyNames_City;
            locationHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Location_All,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Location_All
                );
            locationHierarchy.AddLevel(p =>
                p.City,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Location_Name
                );
            flatDataSource.AddHierarchyDescriptor(locationHierarchy);

            //The following declarations are only for localization purposes.They are not needed for the data to be available.
            HierarchyDescriptor<Sale> quarterHierarchy = new HierarchyDescriptor<Sale>(p => p.Quarter);
            quarterHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_PropertyNames_Quarter,
                PivotGridStrings.XPG_Data_PropertyNames_Quarter
                );
            quarterHierarchy.HierarchyDisplayName = PivotGridStrings.XPG_Data_PropertyNames_Quarter;
            flatDataSource.AddHierarchyDescriptor(quarterHierarchy);

            HierarchyDescriptor<Sale> unitPriceHierarchy = new HierarchyDescriptor<Sale>(p => p.UnitPrice);
            unitPriceHierarchy.HierarchyDisplayName = PivotGridStrings.XPG_Data_UnitPrice_DisplayName;
            unitPriceHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_UnitPrice_DisplayName,
                PivotGridStrings.XPG_Data_UnitPrice_DisplayName
                );
            flatDataSource.AddHierarchyDescriptor(unitPriceHierarchy);

            HierarchyDescriptor<Sale> numberOfUnitsHierarchy = new HierarchyDescriptor<Sale>(p => p.NumberOfUnits);
            numberOfUnitsHierarchy.HierarchyDisplayName = PivotGridStrings.XPG_Data_NumberOfUnits_DisplayName;
            numberOfUnitsHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_NumberOfUnits_DisplayName,
                PivotGridStrings.XPG_Data_NumberOfUnits_DisplayName
                );
            flatDataSource.AddHierarchyDescriptor(numberOfUnitsHierarchy);

            HierarchyDescriptor<Sale> amountOfSaleHierarchy = new HierarchyDescriptor<Sale>(p => p.AmountOfSale);
            amountOfSaleHierarchy.HierarchyDisplayName = PivotGridStrings.XPG_Data_AmountOfSale_DisplayName;
            amountOfSaleHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_AmountOfSale_DisplayName,
                PivotGridStrings.XPG_Data_AmountOfSale_DisplayName
                );
            flatDataSource.AddHierarchyDescriptor(amountOfSaleHierarchy);

            HierarchyDescriptor<Sale> valueHierarchy = new HierarchyDescriptor<Sale>(p => p.Value);
            valueHierarchy.HierarchyDisplayName = PivotGridStrings.XPG_Data_Value_DisplayName;
            valueHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_Value_DisplayName,
                PivotGridStrings.XPG_Data_Value_DisplayName
                );
            flatDataSource.AddHierarchyDescriptor(valueHierarchy);
            
            this.pivotGrid.DataSource = flatDataSource;
            this.dataSelector.DataSource = flatDataSource;
        }

        void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
