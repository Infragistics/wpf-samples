using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for SummaryLocationAndUIType.xaml
    /// </summary>
    public partial class SummaryLocationAndUIType : SampleContainer
    {
        public SummaryLocationAndUIType()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(SummaryLocationAndUIType_Loaded);
        }

        void SummaryLocationAndUIType_Loaded(object sender, RoutedEventArgs e)
        {
            this.SummaryPosition.SelectedIndex = 1;
            this.SummaryPositionGroup.SelectedIndex = 0;

            DataTable ordersTable = NWindDataSource.GetTable(NWindTable.Orders, true);
            ordersTable.DefaultView.RowFilter = "CustomerID <= 'ANTON'";

            this.XamDataGrid1.DataSource = ordersTable.DefaultView;
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