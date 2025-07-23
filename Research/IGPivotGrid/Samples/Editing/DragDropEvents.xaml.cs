using System;
using System.Linq;
using System.Runtime.InteropServices;
using Infragistics.Controls.Grids;
using Infragistics.Olap;
using Infragistics.Olap.Data;
using IGPivotGrid.Resources;

namespace IGPivotGrid.Samples.Editing
{
    public partial class DragDropEvents : Infragistics.Samples.Framework.SampleContainer
    {
        private const string dateHierarchyUniqueName = "[Date].[Date]";

        public DragDropEvents()
        {
            InitializeComponent();
        }

        // Here we check if the Date hierarchy is being dragged and disable adding it to the rows or filters.
        private void PivotGrid_PivotItemDragStart(object sender, PivotItemDragDropStartEventArgs e)
        {
            var datasource = ((IPivotViewModel)sender).DataSource as DataSourceBase;

            // When drag has started from the dataselector's metadata tree, the PivotItem property of the EventArgs is IOlapElement
            // otherwise it is IAreaItemViewModel
            var olapElement = e.PivotItem as IOlapElement;
            string elementCaption = olapElement != null ? olapElement.Caption :
                ((IAreaItemViewModel)e.PivotItem).Caption;

            IHierarchy hierarchy = datasource.Cube.Dimensions.SelectMany(d => d.Hierarchies).FirstOrDefault(h => h.Caption == elementCaption);

            if (hierarchy != null && hierarchy.UniqueName == dateHierarchyUniqueName)
            {
                datasource.AreaFieldSettings.AllowFiltersEditing = false;
                datasource.AreaFieldSettings.AllowColumnsEditing = false;
            }

            LogEvent("PivotItemDragStart", e.PivotItem, PivotPartType.None);
        }

        private void PivotGrid_PivotItemDragLeave(object sender, PivotItemDragDropEventArgs e)
        {
            LogEvent("PivotItemDragLeave", e.PivotItem, e.PivotPartType);
        }

        private void PivotGrid_PivotItemDragEnter(object sender, PivotItemDragDropEventArgs e)
        {
            LogEvent("PivotItemDragEnter", e.PivotItem, e.PivotPartType);
        }

        private void PivotGrid_PivotItemDrop(object sender, PivotItemDragDropEventArgs e)
        {
            LogEvent("PivotItemDragDrop", e.PivotItem, e.PivotPartType);
        }

        private void PivotGrid_PivotItemDragEnd(object sender, PivotItemDragDropEventArgs e)
        {
            var datasource = ((IPivotViewModel)sender).DataSource as DataSourceBase;

            datasource.AreaFieldSettings.AllowFiltersEditing = true;
            datasource.AreaFieldSettings.AllowColumnsEditing = true;

            LogEvent("PivotItemDragEnd", e.PivotItem, e.PivotPartType);
        }

        public void LogEvent(string eventName, object pivotItem, PivotPartType pivotPart)
        {
            var olapElement = pivotItem as Infragistics.Olap.Data.IOlapElement;

            var itemCaption = olapElement != null ? olapElement.Caption :
                ((Infragistics.Olap.IAreaItemViewModel)pivotItem).Caption;

            eventsLog.Items.Insert(0, String.Format(PivotGridStrings.XPG_DragDropLogString, eventName, itemCaption, pivotPart));
            eventsLog.SelectedIndex = 0;
        }
    }
}
