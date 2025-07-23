using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Infragistics.Controls.Charts;
using Infragistics.DragDrop;
using Infragistics.Olap;
using Infragistics.Olap.Data;
using Infragistics.Samples.Framework;

namespace IGDataChart.Samples.Editing
{
    public partial class OlapAxisDragDrop : SampleContainer
    {
        OlapXAxis olapXAxis;
        public OlapAxisDragDrop()
        {
            InitializeComponent();
            olapXAxis = (OlapXAxis)dataChart.Axes["olapAxis"];
            olapXAxis.DataSource.PropertyChanged += DataSource_PropertyChanged; // show indication when loading data
        }

        private void XamPivotDataSelector_PivotItemDragStart(object sender, Infragistics.Controls.Grids.PivotItemDragDropStartEventArgs e)
        {
            DragSource dragSource = DragDropManager.GetDragSource(e.DragDropEventArgs.DragSource);
            dragSource.DragOver += new EventHandler<DragDropMoveEventArgs>(dragSource_DragOver);
            dragSource.Drop += new EventHandler<DropEventArgs>(dragSource_Drop);
        }

        void dragSource_DragOver(object sender, DragDropMoveEventArgs e)
        {
            if (e.DropTarget == this.dataChart)
                e.OperationType = OperationType.Move;
        }

        void dragSource_Drop(object sender, DropEventArgs e)
        {
            DragSource dragSource = DragDropManager.GetDragSource(e.DragSource);
            dragSource.Drop -= dragSource_Drop;
            dragSource.DragOver -= dragSource_DragOver;

            if (!(e.DropTarget is XamDataChart))
                return;

            IOlapViewModel olapViewModel = olapXAxis.DataSource;
            if (olapViewModel == null)
                return;

            IMeasure measure = e.Data as IMeasure;
            if (measure != null)
            {
                IMeasureViewModel measureModel = olapViewModel.CreateMeasureViewModel(measure);

                // check if the measure is already added to the data source
                foreach (var measureViewModel in olapViewModel.Measures)
                {
                    if (((IMeasureViewModel)measureViewModel).Measure.UniqueName == measureModel.Measure.UniqueName)
                    {
                        MessageBox.Show(OlapResources.GetString("ItemAlreadyAdded"));
                        return;
                    }
                }
                olapViewModel.Measures.Add(measureModel);
                return;
            }

            IHierarchy hierarchy = e.Data as IHierarchy;
            if (hierarchy != null)
            {
                // check if the measure is already added to the data source
                var allHierarchiesUniqueNames = new List<string>();
                allHierarchiesUniqueNames.AddRange(olapViewModel.Columns.Select(p =>
                {
                    var filterViewModel = p as IFilterViewModel;
                    return filterViewModel != null ? filterViewModel.Hierarchy.UniqueName : null;
                }));
                allHierarchiesUniqueNames.AddRange(olapViewModel.Rows.Select(p =>
                {
                    var filterViewModel = p as IFilterViewModel;
                    return filterViewModel != null ? filterViewModel.Hierarchy.UniqueName : null;
                }));
                allHierarchiesUniqueNames.AddRange(olapViewModel.Filters.Select(p => ((IFilterViewModel)p).Hierarchy.UniqueName));

                foreach (var uniqueName in allHierarchiesUniqueNames)
                {
                    if (uniqueName == hierarchy.UniqueName)
                    {
                        MessageBox.Show(OlapResources.GetString("ItemAlreadyAdded"));
                        return;
                    }
                }

                IFilterViewModel fvm = olapViewModel.CreateFilterViewModel(hierarchy);
                if (olapXAxis.OlapAxisSource == OlapAxisSource.Columns)
                    olapViewModel.Columns.Add(fvm);
                else
                    olapViewModel.Rows.Add(fvm);
            }
        }

        void DataSource_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                busyBorder.Visibility = olapXAxis.DataSource.Processing ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
