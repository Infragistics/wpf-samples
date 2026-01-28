using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using IGPivotGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Olap;
using Infragistics.Olap.Data;
using Infragistics.Olap.FlatData;
using Infragistics.Samples.Framework;
using IGPivotGrid.Samples.Controls;

namespace IGPivotGrid.Samples.Editing
{
    public partial class CellEditing : SampleContainer
    {
        public CellEditing()
        {
            InitializeComponent();
            this.pivotGrid.DataSource.Measures.CollectionChanged += Measures_CollectionChanged;
            this.pivotGrid.CellEdited += PivotGrid_CellEdited;
        }

        //This event is thrown when the editing of a cell has completed.
        //Enter the logic to update the source in here
        private void PivotGrid_CellEdited(object sender, PivotCellEditedEventArgs e)
        {
            double newValue;
            Double.TryParse(e.EditedValue.ToString(), out newValue);

            List<int> indexes = ((FlatDataSource)this.pivotGrid.DataSource).GetCellItemsIndexes(e.Cell.Data as ICell);

            if (e.Measure.Caption == PivotGridStrings.XPG_Data_NumberOfUnits_DisplayName)
            {
                Sale obj;
                foreach (int index in indexes)
                {
                    obj = ((FlatDataSource)this.pivotGrid.DataSource).GetRecord(index) as Sale;
                    obj.NumberOfUnits = 0;
                }
                obj = ((FlatDataSource)this.pivotGrid.DataSource).GetRecord(indexes[0]) as Sale;
                obj.NumberOfUnits = (int)newValue;
            }
            else
            {
                foreach (int index in indexes)
                {
                    Sale obj = ((FlatDataSource)this.pivotGrid.DataSource).GetRecord(index) as Sale;
                    obj.UnitPrice = newValue;
                }
            }

            this.pivotGrid.DataSource.RefreshGrid();
        }

        private void Measures_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (IMeasureViewModel mvm in e.NewItems)
                    {
                        if (mvm.Caption != PivotGridStrings.XPG_Data_AmountOfSale_DisplayName)
                            this.pivotGrid.EditSettings.EditableMeasures.Add(mvm.Measure);

                        if (mvm.Caption == PivotGridStrings.XPG_Data_UnitPrice_DisplayName)
                        {
                            this.pivotGrid.DataSource.SetMeasureAggregator(
                                (IMeasureViewModel)mvm,
                                AggregationHelper.GetDefaultAverageAggregator(typeof(double))
                            );
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (IMeasureViewModel mvm in e.OldItems)
                    {
                        this.pivotGrid.EditSettings.EditableMeasures.Remove(mvm.Measure);
                    }

                    break;
            }
        }
    }
}
