using System.Data;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for GroupByAreaExternal.xaml
    /// </summary>
    public partial class GroupByAreaExternal : SampleContainer
    {
        public GroupByAreaExternal()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);
            this.XamDataGrid1.DataSource = dt.DefaultView;
        }

        private void cboGroupByAreaMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Since the GroupBy areas are outside of the XamDataGrid, we need to manually hide and show them when the GroupByAreaMode changes.
            //		(when the GroupBy areas are inside the XamDataGrid, the rid takes care of this for us)
            if (this.XamDataGrid1.GroupByAreaMode == Infragistics.Windows.DataPresenter.GroupByAreaMode.DefaultFieldLayoutOnly)
            {
                this.ExternalGroupByArea.Visibility = Visibility.Visible;
                this.ExternalGroupByAreaMulti.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.ExternalGroupByArea.Visibility = Visibility.Collapsed;
                this.ExternalGroupByAreaMulti.Visibility = Visibility.Visible;
            }
        }
    }
}