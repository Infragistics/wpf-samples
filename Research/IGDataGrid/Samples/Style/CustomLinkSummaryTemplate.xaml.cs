using System.Data;
using System.Windows;
using System.Windows.Controls;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for CustomLinkSummaryTemplate.xaml
    /// </summary>
    public partial class CustomLinkSummaryTemplate : SampleContainer
    {
        public CustomLinkSummaryTemplate()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(CustomLinkSummaryTemplate_Loaded);
        }

        void CustomLinkSummaryTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable orderDetailsTable = NWindDataSource.GetTable(NWindTable.OrderDetails);
            orderDetailsTable.DefaultView.RowFilter = "OrderID <= 10252";

            this.XamDataGrid1.DataSource = orderDetailsTable.DefaultView;
        }

        public void btnSummary_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            ItemCollection summaries = b.Tag as ItemCollection;

            ItemsControl summaryItems = this.wndSummary.Items[0] as ItemsControl;
            if (summaryItems != null)
            {
                summaryItems.Items.Clear();

                foreach (Infragistics.Windows.DataPresenter.SummaryResult r in summaries)
                {
                    string format = "";
                    switch (r.SummaryDefinition.Calculator.Name)
                    {
                        case "Count":
                            format = DataGridStrings.XDG_CountQuantity;
                            break;
                        case "Average":
                            format = DataGridStrings.XDG_AverageQuantity;
                            break;
                        case "Maximum":
                            format = DataGridStrings.XDG_MaximumQuantity;
                            break;
                        case "Minimum":
                            format = DataGridStrings.XDG_MinimumQuantity;
                            break;
                        case "Sum":
                            format = DataGridStrings.XDG_SumQuantity;
                            break;
                        default:
                            format = DataGridStrings.FormattedSummaryResultDisplayText;
                            break;
                    }
                    string summaryText = string.Format(
                        format,
                        r.SourceField.Label,
                        r.Value);
                    summaryItems.Items.Add(summaryText);
                }
            }
        }
    }
}
