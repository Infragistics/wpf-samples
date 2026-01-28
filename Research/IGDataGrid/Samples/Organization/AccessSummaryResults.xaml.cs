using System;
using System.Data;
using System.Windows;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter.Events;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for AccessSummaryResults.xaml
    /// </summary>
    public partial class AccessSummaryResults : SampleContainer
    {
        public AccessSummaryResults()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable orderDetailsTable = NWindDataSource.GetTable(NWindTable.OrderDetails);
            orderDetailsTable.DefaultView.RowFilter = "OrderID <= 10252";

            this.XamDataGrid1.DataSource = orderDetailsTable.DefaultView;
        }

        private void XamDataGrid1_SummaryResultChanged(object sender, SummaryResultChangedEventArgs e)
        {
            Infragistics.Windows.DataPresenter.SummaryResult summaryResult = e.SummaryResult;

            TxtSummaryResult.Text =
                String.Format(
                DataGridStrings.AccessSummaryResults_SummaryResultArea_FormattedSummaryText,
                summaryResult.SourceField.Label,
                summaryResult.DisplayText);
        }
    }
}