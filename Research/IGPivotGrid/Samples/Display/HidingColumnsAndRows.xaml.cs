using System.Windows;
using System.Windows.Controls;
using IGPivotGrid.Resources;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Display
{
    public partial class HidingColumnsAndRows : SampleContainer
    {
        private int columnIdx;
        private int rowIdx;

        public HidingColumnsAndRows()
        {
            InitializeComponent();
        }

        private void ColumnIdx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(ColumnIdx.Text, out columnIdx) && ColumnsToggle != null)
            {
                if (columnIdx >= 0 && columnIdx < pivotGrid.GridLayout.Columns.Count)
                {
                    ColumnsToggle.SetValue(IsEnabledProperty, true);
                    ColumnsToggle.IsChecked = !pivotGrid.GridLayout.Columns[columnIdx].IsVisible;
                    ColumnsToggle.Content = ColumnsToggle.IsChecked.Value ? PivotGridStrings.XPG_Show : PivotGridStrings.XPG_Hide;
                    return;
                }
            }
            if (ColumnsToggle != null)
                ColumnsToggle.SetValue(IsEnabledProperty, false);
        }

        private void RowIdx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(RowIdx.Text, out rowIdx) && RowsToggle != null)
            {
                if (rowIdx >= 0 && rowIdx < pivotGrid.GridLayout.Rows.Count)
                {
                    RowsToggle.SetValue(IsEnabledProperty, true);
                    RowsToggle.IsChecked = !pivotGrid.GridLayout.Rows[rowIdx].IsVisible;
                    RowsToggle.Content = RowsToggle.IsChecked.Value ? PivotGridStrings.XPG_Show : PivotGridStrings.XPG_Hide;
                    return;
                }
            }
            if (RowsToggle != null)
                RowsToggle.SetValue(IsEnabledProperty, false);
        }

        private void ColumnsToggle_Click(object sender, RoutedEventArgs e)
        {
            pivotGrid.GridLayout.Columns[columnIdx].IsVisible = !ColumnsToggle.IsChecked.Value;
            ColumnsToggle.Content = ColumnsToggle.IsChecked.Value ? PivotGridStrings.XPG_Show : PivotGridStrings.XPG_Hide;
        }

        private void RowsToggle_Click(object sender, RoutedEventArgs e)
        {
            pivotGrid.GridLayout.Rows[rowIdx].IsVisible = !RowsToggle.IsChecked.Value;
            RowsToggle.Content = RowsToggle.IsChecked.Value ? PivotGridStrings.XPG_Show : PivotGridStrings.XPG_Hide;
        }

        private void HiddenAxisSetting_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // When dynamically changing the value of the HiddenAxis setting the grid will not be automatically updated.
            // This will force an update.
            pivotGrid.ArrangeLayout();
        }
    }
}
