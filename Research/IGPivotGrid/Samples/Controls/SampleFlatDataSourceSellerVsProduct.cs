using Infragistics.Olap.Xmla;
using Infragistics.Samples.Shared.Models;

namespace IGPivotGrid.Samples.Controls
{
    public class SampleFlatDataSourceSellerVsProduct : SampleFlatDataSourceBase
    {
        public override void SelectItems()
        {
            SalesDataGenerator.ConstantUnitPrice = false;
            this.Rows = XmlaDataSource.GenerateInitialItems("[Seller].[Seller]");
            this.Columns = XmlaDataSource.GenerateInitialItems("[Product].[Product]");
            this.Measures = XmlaDataSource.GenerateInitialItems("AmountOfSale");
        }
    }
}
