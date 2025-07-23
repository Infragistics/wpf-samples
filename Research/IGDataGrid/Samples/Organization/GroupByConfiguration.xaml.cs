using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for GroupByConfiguration.xaml
    /// </summary>
    public partial class GroupByConfiguration : SampleContainer
    {
        public GroupByConfiguration()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);

            this.XamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}
