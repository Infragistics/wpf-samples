using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using System.Data;
using System.Windows;

namespace IGDataGrid.Samples.Organization
{
    public partial class FieldGrouping : SampleContainer
    {
        public FieldGrouping()
        {
            InitializeComponent();
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DataTable ordersTable = NWindDataSource.GetTable(NWindTable.Orders, true, null, 15);
            this.xamDataGrid1.DataSource = ordersTable.DefaultView;
        }
    }
}
