using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for RowSummariesStyling.xaml
    /// </summary>
    public partial class RowSummariesStyling : SampleContainer
    {
        public RowSummariesStyling()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(RowSummariesStyling_Loaded);
        }

        private void RowSummariesStyling_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable orderDetailsTable = NWindDataSource.GetTable(NWindTable.OrderDetails);
            orderDetailsTable.DefaultView.RowFilter = "OrderID <= 10252";

            this.XamDataGrid1.DataSource = orderDetailsTable.DefaultView;

            this.SummaryPosition.SelectedIndex = 3;
            this.SummaryPositionGroup.SelectedIndex = 0;
        }

        private void SummaryPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateRowSummaryLocation();
        }

        private void SummaryPositionGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateRowSummaryLocation();
        }

        private void UpdateRowSummaryLocation()
        {
            SummaryDisplayAreas sumamryPosition = (SummaryDisplayAreas)this.SummaryPosition.SelectedItem;

            SummaryDisplayAreas sumamryPositionGroup = SummaryDisplayAreas.None;
            if (this.SummaryPositionGroup.SelectedItem != null)
            {
                sumamryPositionGroup = (SummaryDisplayAreas)this.SummaryPositionGroup.SelectedItem;
            }

            this.XamDataGrid1.FieldSettings.SummaryDisplayArea = sumamryPosition | sumamryPositionGroup;
        }
    }
    public class SummaryDisplayPosition
    {
        public SummaryDisplayPosition()
        {
        }

        public IList<SummaryDisplayAreas> GetValues()
        {
            IList<SummaryDisplayAreas> displayAreas = new List<SummaryDisplayAreas>();

            displayAreas.Add(SummaryDisplayAreas.None);
            displayAreas.Add(SummaryDisplayAreas.Top);
            displayAreas.Add(SummaryDisplayAreas.TopFixed);
            displayAreas.Add(SummaryDisplayAreas.Bottom);
            displayAreas.Add(SummaryDisplayAreas.BottomFixed);

            return displayAreas;
        }

        public IList<SummaryDisplayAreas> GetGroupByValues()
        {
            IList<SummaryDisplayAreas> displayAreas = new List<SummaryDisplayAreas>();

            displayAreas.Add(SummaryDisplayAreas.None);
            displayAreas.Add(SummaryDisplayAreas.InGroupByRecords);
            displayAreas.Add(SummaryDisplayAreas.TopLevelOnly);
            displayAreas.Add(SummaryDisplayAreas.DataRecordsOnly);

            return displayAreas;
        }
    }
}
