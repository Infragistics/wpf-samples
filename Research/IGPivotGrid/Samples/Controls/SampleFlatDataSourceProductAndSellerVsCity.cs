using Infragistics.Olap.Xmla;
using Infragistics.Samples.Shared.Models;

namespace IGPivotGrid.Samples.Controls
{
    public class SampleFlatDataSourceProductAndSellerVsCity : SampleFlatDataSourceBase
    {
        public override void SelectItems()
        {
            SalesDataGenerator.ConstantUnitPrice = false;
            this.Rows = XmlaDataSource.GenerateInitialItems("[Product].[Product], [Seller].[Seller]");
            this.Columns = XmlaDataSource.GenerateInitialItems("[City].[City]");
            this.Measures = XmlaDataSource.GenerateInitialItems("AmountOfSale, NumberOfUnits");
        }
    }
}
