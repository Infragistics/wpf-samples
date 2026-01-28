using System.Collections.Generic;
using System.Linq;
using System.Windows;
using IGOrgChart.Model;
using Infragistics.Controls.Maps;
using Infragistics.DragDrop;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGOrgChart.Samples.Organization
{
    public partial class ReorganizeNodes : SampleContainer
    {
        //An instance of the SampleEmployeeModel.
        private EmployeesModel _employeeModel;

        //The template applied when dragging.
        private DataTemplate _nodeDragTemplate;

        //The style applied to the node drop targets.
        private System.Windows.Style _dropTargetStyle;

        public ReorganizeNodes()
        {
            InitializeComponent();
            this.SampleLoaded += new System.EventHandler(ReorganizeNodes_SampleLoaded);
        }

        void ReorganizeNodes_SampleLoaded(object sender, System.EventArgs e)
        {
            //Get the EmployeeModel.
            _employeeModel = OrgChart.DataContext as EmployeesModel;

            //Get the drag template from the resources.
            _nodeDragTemplate = this.Resources["NodeDragTemplate"] as DataTemplate;

            //Get the drop target style from the resources.
            _dropTargetStyle = this.Resources["DropTargetStyle"] as System.Windows.Style;
        }

        //This method is called whenever a node is brought into view. It makes the node a drop target.
        private void OrgChart_NodeControlAttachedEvent(object sender, OrgChartNodeEventArgs e)
        {
            DragSource dragSource = new DragSource();
            dragSource.IsDraggable = true;
            dragSource.DragTemplate = _nodeDragTemplate;
            dragSource.DragStart += Node_DragStart;
            dragSource.Drop += Node_Drop;

            //Make the node a drag source.
            DragDropManager.SetDragSource(e.Node, dragSource);

            //Make the node a drop target.
            DragDropManager.SetDropTarget(e.Node, new DropTarget()
            {
                IsDropTarget = true,
                DropTargetStyle = _dropTargetStyle
            });
        }

        private void Node_DragStart(object sender, DragDropStartEventArgs e)
        {
            //Assing the Employee of the dragged node as data context for the Dragged Item.
            //This is needed for the DragTemplate.
            e.Data = ((OrgChartNodeControl)e.DragSource).Node.Data;
        }

        // Perform a drop and update the underlying data source.
        private void Node_Drop(object sender, DropEventArgs e)
        {
            //Get the dragged OrgChartNode object.
            OrgChartNode dragSourceNode = ((OrgChartNodeControl)e.DragSource).Node as OrgChartNode;

            if (dragSourceNode == null)
                return;

            //Get the dragged employee.
            Employee dragSource = dragSourceNode.Data as Employee;

            if (dragSource == null) 
                return;

            //Get the collection with sibling Employees.
            IList<Employee> siblingsCollection;

            if (dragSourceNode.ParentNode.Data != null)
            {
                Employee superiorEmployee = dragSourceNode.ParentNode.Data as Employee;
                siblingsCollection = superiorEmployee.Subordinates;
            }
            else
            {
                siblingsCollection = _employeeModel.Employees;
            }

            //Perform the drop.
            if (e.DropTarget is OrgChartNodeControl)
            {
                Employee dropTarget = ((OrgChartNodeControl)e.DropTarget).Node.Data as Employee;

                if (dropTarget == null)
                    return;

                //Prevent drop on self.
                if (dragSource == dropTarget) 
                    return;

                //Check if the drop target already contains the dragged item.
                if (dropTarget.Subordinates.Contains(dragSource)) return;

                //Check for a recursive relation between the drag source and the drop target.
                int levels = OrgChart.ActualDepth - dragSourceNode.Level;
                if (dragSourceNode.HighlightChildren(levels)
                    .Select(node => node.Data)
                    .Contains(dropTarget)) return;

                //Remove the dragged node from its parent.
                siblingsCollection.Remove(dragSource);

                //Add the dragged node to the drop target node.
                dropTarget.Subordinates.Add(dragSource);
            }
            else //if the Drop Target is the XamOrgChart
            {
                //Check if the drop target already contains the dragged item.
                if (_employeeModel.Employees.Contains(dragSource)) return;

                //Remove the dragged node from its parent.
                siblingsCollection.Remove(dragSource);

                //Add the dragged node to the drop target node.
                _employeeModel.Employees.Add(dragSource);
            }
        }
    }
}
