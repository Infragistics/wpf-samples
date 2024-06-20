using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.Events;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for DefiningSummariesForASpecificFieldByCode.xaml
    /// </summary>
    public partial class DefiningSummariesForASpecificFieldByCode : SampleContainer
    {
        public DefiningSummariesForASpecificFieldByCode()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Page_Loaded);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable ordersTable = NWindDataSource.GetTable(NWindTable.Orders, true);
            ordersTable.DefaultView.RowFilter = "CustomerID <= 'ANTON'";

            this.XamDataGrid1.DataSource = ordersTable.DefaultView;
        }

        private void XamDataGrid1_FieldLayoutInitialized(object sender, FieldLayoutInitializedEventArgs e)
        {
            FieldLayout fieldLayout = e.FieldLayout;
            SummaryDefinition summaryDefinition = new SummaryDefinition();
            summaryDefinition.SourceFieldName = "CustomerID";
            summaryDefinition.Calculator = Infragistics.Windows.DataPresenter.SummaryCalculator.Count;
            fieldLayout.SummaryDefinitions.Add(summaryDefinition);
        }
    }
}