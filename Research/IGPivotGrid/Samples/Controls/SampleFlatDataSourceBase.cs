using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using IGPivotGrid.Resources;
using Infragistics.Olap;
using Infragistics.Olap.Data;
using Infragistics.Olap.FlatData;
using Infragistics.Olap.Xmla;

namespace IGPivotGrid.Samples.Controls
{
    public abstract class SampleFlatDataSourceBase : FlatDataSource
    {
        public bool Switched = false;
        public bool IsExpanded { get; set; }

        public SampleFlatDataSourceBase()
        {
            this.IsExpanded = true;

            this.Cube = XmlaDataSource.GenerateInitialCube("Sale");

            SelectItems();

            this.ItemsSource = new SalesCollection();

            CubeMetadata cubeMetadata = new CubeMetadata();
            cubeMetadata.DisplayName = PivotGridStrings.XPG_Data_CubeMetadata_DisplayName;
            cubeMetadata.DataTypeFullName = typeof(Sale).FullName;
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "AmountOfSale",
                DisplayName = PivotGridStrings.XPG_Data_AmountOfSale_DisplayName,
                DisplayFormat = PivotGridStrings.XPG_Data_AmountOfSale_DisplayFormat,
                DimensionType = DimensionType.Measure
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "NumberOfUnits", // Units Sold
                DisplayName = PivotGridStrings.XPG_Data_NumberOfUnits_DisplayName,
                DimensionType = DimensionType.Measure
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "UnitPrice",
                DisplayName = PivotGridStrings.XPG_Data_UnitPrice_DisplayName,
                DisplayFormat = PivotGridStrings.XPG_Data_AmountOfSale_DisplayFormat,
                DimensionType = DimensionType.Measure
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Value",
                DisplayName = PivotGridStrings.XPG_Data_Value_DisplayName,
                DisplayFormat = PivotGridStrings.XPG_Data_AmountOfSale_DisplayFormat,
                DimensionType = DimensionType.Measure
            });
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
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Date",
                DisplayName = PivotGridStrings.XPG_Data_PropertyNames_Date
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Quarter",
                DisplayName = PivotGridStrings.XPG_Data_PropertyNames_Quarter
            });

            this.CubesSettings.Add(cubeMetadata);

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
            this.AddHierarchyDescriptor(sellerHierarchy);

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
            this.AddHierarchyDescriptor(productHierarchy);

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
            this.AddHierarchyDescriptor(locationHierarchy);

            //Remaining hierarchies can be autogenerated, but are added here in order for the displayed strings to be localized
            HierarchyDescriptor<Sale> quarterHierarchy = new HierarchyDescriptor<Sale>(p => p.Quarter);
            quarterHierarchy.HierarchyDisplayName = PivotGridStrings.XPG_Data_PropertyNames_Quarter;
            quarterHierarchy.AddLevel(PivotGridStrings.XPG_AllQuaters, PivotGridStrings.XPG_AllQuaters);
            quarterHierarchy.AddLevel(p => p.Quarter, PivotGridStrings.XPG_Data_PropertyNames_Quarter);
            this.AddHierarchyDescriptor(quarterHierarchy);

            HierarchyDescriptor<Sale> dateHierarchy = new HierarchyDescriptor<Sale>(p => p.Date);
            dateHierarchy.AppliesToPropertiesOfType = typeof(DateTime);
            dateHierarchy.AddLevel<DateTime>(dt => PivotGridStrings.XPG_AllPeriods, PivotGridStrings.XPG_AllPeriods);
            dateHierarchy.AddLevel<DateTime>(dt => dt.Year, PivotGridStrings.XPG_Years);
            dateHierarchy.AddLevel<DateTime>(dt => dt.SemesterShort(), PivotGridStrings.XPG_Semesters);
            dateHierarchy.AddLevel<DateTime>(dt => dt.QuarterShort(), PivotGridStrings.XPG_Quarters);
            dateHierarchy.AddLevel<DateTime>(dt => dt.MonthShort(), PivotGridStrings.XPG_Months);

            Expression<Func<DateTime, int>> expression = pd => pd.Month;
            dateHierarchy.LevelDescriptors[4].OrderByKeyExpression = expression;

            dateHierarchy.AddLevel<DateTime>(dt => dt.Date.ToShortDateString(), PivotGridStrings.XPG_Dates);
            dateHierarchy.HierarchyDisplayName = PivotGridStrings.XPG_Data_PropertyNames_Date;
            this.AddHierarchyDescriptor(dateHierarchy);
        }

        public virtual void SelectItems()
        {
        }

        protected override void OnResultChanged(ResultChangedEventArgs args)
        {
            base.OnResultChanged(args);

            if (IsExpanded == true)
            {
                //Sanity check
                if (this.Result == null || Switched == true)
                    return;
                if (this.Result.ColumnAxis == null)
                    return;
                if (this.Result.ColumnAxis.Tuples.Count <= 0)
                    return;
                if (this.Result.ColumnAxis.Tuples[0].Members.Count <= 0)
                    return;
                if (this.Result.RowAxis == null)
                    return;
                if (this.Result.RowAxis.Tuples.Count <= 0)
                    return;
                if (this.Result.RowAxis.Tuples[0].Members.Count <= 0)
                    return;
                if (this.Result.ColumnAxis.Tuples[0].Members[0].UniqueName == String.Empty)
                    return;
                if (this.Result.RowAxis.Tuples[0].Members[0].UniqueName == String.Empty)
                    return;

                Expand();
            }
        }

        private void Expand()
        {
            ReadOnlyCollection<ITuple> tuples = this.Result.ColumnAxis.Tuples;
            ITuple tuple =
                tuples.Where(t => t.Members.Where(m => m.LevelDepth == 0).Count() > 0).FirstOrDefault();
            IMember member =
                tuple.Members.Where(m => m.LevelDepth == 0).FirstOrDefault();

            IFilterMember filterMember = this.FindParentMember(this.Columns, member);
            //Not Compilabe yet - IFilterMember filterMember = this.FindFilterMember(ExecutionContext.Columns, member);

            if (filterMember != null && !filterMember.IsExpanded)
            {
                this.SwitchAxisMember(member);
            }
            else
            {
                tuples = this.Result.RowAxis.Tuples;
                tuple =
                    tuples.Where(t => t.Members.Where(m => m.LevelDepth == 0).Count() > 0).FirstOrDefault();
                member =
                    tuple.Members.Where(m => m.LevelDepth == 0).FirstOrDefault();

                this.SwitchAxisMember(member);
                this.Switched = true;
            }
        }
    }
}
