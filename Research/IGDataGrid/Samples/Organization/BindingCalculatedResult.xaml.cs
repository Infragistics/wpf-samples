using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for BindingCalculatedResult.xaml
    /// </summary>
    public partial class BindingCalculatedResult : SampleContainer
    {
        public BindingCalculatedResult()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(BindingCalculatedResult_Samp_Loaded);
        }

        void BindingCalculatedResult_Samp_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable ordersTable = NWindDataSource.GetTable(NWindTable.Orders, true);
            ordersTable.DefaultView.RowFilter = "CustomerID <= 'ANTON'";

            this.XamDataGrid1.DataSource = ordersTable.DefaultView;

            foreach (Record record in this.XamDataGrid1.Records)
                record.IsExpanded = true;
        }
    }
}