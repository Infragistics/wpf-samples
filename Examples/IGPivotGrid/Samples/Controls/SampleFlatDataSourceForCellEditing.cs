using Infragistics.Olap.Xmla;
using Infragistics.Samples.Shared.Models;

namespace IGPivotGrid.Samples.Controls
{
    public class SampleFlatDataSourceForCellEditing : SampleFlatDataSourceBase
    {
        public override void SelectItems()
        {
            SalesDataGenerator.ConstantUnitPrice = true;
            this.Rows = XmlaDataSource.GenerateInitialItems("[City].[City]");
            this.Columns = XmlaDataSource.GenerateInitialItems("[Product].[Product]");
            this.Measures = XmlaDataSource.GenerateInitialItems("AmountOfSale, NumberOfUnits, UnitPrice");
        }
    }
}
