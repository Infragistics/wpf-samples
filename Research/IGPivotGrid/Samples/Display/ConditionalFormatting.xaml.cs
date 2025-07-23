using System;
using System.ComponentModel;
using System.Windows;
using Infragistics.Controls.Grids;
using Infragistics.Olap.Data;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Display
{
    public partial class ConditionalFormatting : SampleContainer
    {
        public static double rowMaxValue = 0;
        public static XamPivotGrid pivotGridReference;
        public ConditionalFormatting()
        {
            InitializeComponent();

            pivotGridReference = this.pivotGrid;
            this.pivotGrid.DataSource.ResultChanged += DataSource_ResultChanged;
            this.pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
            this.pivotGrid.CellControlAttached += PivotGrid_CellControlAttached;
        }

        //This event is thrown whenever the UI component of each cell is loaded
        //We attach to it to apply the conditional formatting
        private void PivotGrid_CellControlAttached(object sender, PivotCellControlAttachedEventArgs e)
        {
            int column = this.pivotGrid.DataColumns.IndexOf(e.Cell.DataColumn);
            int row = this.pivotGrid.DataRows.IndexOf(e.Cell.DataRow);
            if (this.pivotGrid.DataSource.Result != null && column == 0)
            {
                ICell cellData = this.pivotGrid.DataSource.Result.Cells[row, column];
                if ((cellData != null) && (cellData.Value.ToString() != string.Empty))
                {
                    double value = Double.Parse(cellData.Value.ToString());
                    if (value >= 70000)
                    {
                        e.Cell.Style = this.Resources["High"] as System.Windows.Style;
                    }
                    else if (value >= 30000)
                    {
                        e.Cell.Style = this.Resources["Middle"] as System.Windows.Style;
                    }
                    else
                    {
                        e.Cell.Style = this.Resources["Low"] as System.Windows.Style;
                    }
                    e.IsDirty = true;
                }
            }
            if (this.pivotGrid.DataSource.Result != null && column == 1)
            {
                ICell cellData = this.pivotGrid.DataSource.Result.Cells[row, column];
                if ((cellData != null) && (cellData.Value.ToString() != string.Empty))
                {
                    e.Cell.Style = this.Resources["ScaledCell"] as System.Windows.Style;
                }
            }
        }

        //Whenever the result has changed we calculate max values for future use
        //This event is thrown before each cell is loaded
        private void DataSource_ResultChanged(object sender, AsyncCompletedEventArgs e)
        {
            IResult result = this.pivotGrid.DataSource.Result;
            if (result != null)
            {
                int numOfRows = this.pivotGrid.DataSource.Result.Cells.GetLength(0);
                int numOfCols = this.pivotGrid.DataSource.Result.Cells.GetLength(1);
                if (numOfCols < 2)
                    return;

                ConditionalFormatting.rowMaxValue = 0;
                for (int i = 0; i < numOfRows - 1; i++)
                {
                    ICell cell = this.pivotGrid.DataSource.Result.Cells[i, 1];
                    if ((cell != null) && (cell.Value.ToString() != string.Empty))
                    {
                        if (ConditionalFormatting.rowMaxValue < Double.Parse(cell.Value.ToString()))
                            ConditionalFormatting.rowMaxValue = Double.Parse(cell.Value.ToString());
                    }
                }
            }
        }

        private void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
