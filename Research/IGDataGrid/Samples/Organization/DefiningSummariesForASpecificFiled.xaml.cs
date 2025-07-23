using System.Data;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for DefiningSummariesForASpecificFiled.xaml
    /// </summary>
    public partial class DefiningSummariesForASpecificFiled : SampleContainer
    {
        public DefiningSummariesForASpecificFiled()
        {
            InitializeComponent();

            DataTable ordersTable = NWindDataSource.GetTable(NWindTable.Orders, true);
            ordersTable.DefaultView.RowFilter = "CustomerID <= 'ANTON'";

            this.XamDataGrid1.DataSource = ordersTable.DefaultView;
        }
    }
}