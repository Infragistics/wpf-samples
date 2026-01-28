using System.ComponentModel;
using System.Windows;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Editing
{
    public partial class CellSelection : SampleContainer
    {
        public CellSelection()
        {
            InitializeComponent();

            this.pivotGrid.SelectionSettings.CellSelection = PivotSelectionType.Single;
            this.pivotGrid.CellClicked += pivotGrid_CellClicked;

            this.pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
        }
        private void pivotGrid_CellClicked(object sender, PivotCellClickedEventArgs e)
        {
            if (this.sellectionMode.SelectedIndex == 0)
            {
                SelectCells(e.Cell, true, true);
            }
            else if (this.sellectionMode.SelectedIndex == 1)
            {
                SelectCells(e.Cell, true, false);
            }
            else if (this.sellectionMode.SelectedIndex == 2)
            {
                SelectCells(e.Cell, false, true);
            }
            else if (this.sellectionMode.SelectedIndex == 3)
            {
                //Single cell selection comes from the control. No further code needed in this case.
            }
        }

        private void SelectCells(PivotCell clickedCell, bool includeRows, bool includeColumns)
        {
            this.pivotGrid.SelectionSettings.SelectedCells.Clear();
            if (includeColumns)
            {
                this.pivotGrid.SelectionSettings.SelectedColumns.Add(clickedCell.DataColumn);
            }
            if (includeRows)
            {
                this.pivotGrid.SelectionSettings.SelectedRows.Add(clickedCell.DataRow);
            }
        }

        private void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
