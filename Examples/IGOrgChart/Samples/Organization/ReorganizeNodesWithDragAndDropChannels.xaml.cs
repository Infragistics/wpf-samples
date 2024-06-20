using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;
using Infragistics.Controls.Maps;
using Infragistics.DragDrop;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGOrgChart.Samples.Organization
{
    public partial class ReorganizeNodesWithDragAndDropChannels : SampleContainer
    {
        private OrgChartNodeControl currentDragged;

        //The template applied when dragging.
        private DataTemplate _nodeDragTemplate;

        //The style applied to the node drop targets.
        private Style _dropTargetStyle;

        public ReorganizeNodesWithDragAndDropChannels()
        {
            InitializeComponent();

            //Get the drag template from the resources.
            _nodeDragTemplate = this.Resources["NodeDragTemplate"] as DataTemplate;

            //Get the drop target style from the resources.
            _dropTargetStyle = this.Resources["DropTargetStyle"] as System.Windows.Style;
        }

        //This method is called whenever a node is brought into view. It makes the node a drop target.
        private void OrgChart_NodeControlAttachedEvent(object sender, OrgChartNodeEventArgs e)
        {
            //Get the name of the node's data type.
            string dataType = e.Node.Node.Data.GetType().Name;

            DragSource dragSource = new DragSource();
            dragSource.IsDraggable = true;
            dragSource.DragChannels.Add(dataType);
            dragSource.DragChannels.Add("OrgChart");
            dragSource.FindDropTargetMode = FindDropTargetMode.TopMostMatchedChannelTarget;
            dragSource.DragTemplate = _nodeDragTemplate;
            dragSource.DragStart += Node_DragStart;
            dragSource.Drop += Node_Drop;

            //Make the node a drag source.
            DragDropManager.SetDragSource(e.Node, dragSource);

            DropTarget dropTarget = new DropTarget();
            dropTarget.IsDropTarget = true;
            //Assign drop channels according to the data type.
            if (e.Node.Node.Data is DepartmentGroup)
            {
                dropTarget.DropChannels.Add("Department");
            }
            else if (e.Node.Node.Data is Department)
            {
                dropTarget.DropChannels.Add("EmployeePosition");
            }
            else if (e.Node.Node.Data is EmployeePosition)
            {
                dropTarget.DropChannels.Add("Employee");
            }
            dropTarget.DropTargetStyle = _dropTargetStyle;

            //Make the node a drop target.
            DragDropManager.SetDropTarget(e.Node, dropTarget);
        }

        private void OrgChart_NodeControlDetachedEvent(object sender, OrgChartNodeEventArgs e)
        {
            if (DragDropManager.IsDragging && e.Node == currentDragged)
            {
                DragDropManager.EndDrag(true);
            }
        }

        private void Node_DragStart(object sender, DragDropStartEventArgs e)
        {
            //Assing the Employee of the dragged node as data context for the Dragged Item.
            //This is needed for the DragTemplate.
            e.Data = ((OrgChartNodeControl)e.DragSource).Node.Data;
            currentDragged = (OrgChartNodeControl)e.DragSource;
        }

        // Perform a drop and update the underlying data source.
        private void Node_Drop(object sender, DropEventArgs e)
        {
            if (e.DragSource == e.DropTarget) return;

            OrgChartNode dragSource = ((OrgChartNodeControl)e.DragSource).Node;

            if (e.DropTarget is OrgChartNodeControl)
            {
                OrgChartNode dropTarget = ((OrgChartNodeControl)e.DropTarget).Node;

                //Get the collection with child items depending on 
                //the type of the Drop Target.
                if (dropTarget.Data is DepartmentGroup)
                {
                    OrgChartExtensions.DropOnNode(dragSource, dropTarget, "Departments");
                }
                else if (dropTarget.Data is Department)
                {
                    OrgChartExtensions.DropOnNode(dragSource, dropTarget, "EmployeePositions");
                }
                else if (dropTarget.Data is EmployeePosition)
                {
                    OrgChartExtensions.DropOnNode(dragSource, dropTarget, "Employees");
                }
            }
            else //if the Drop Target is the XamOrgChart
            {
                XamOrgChart orgChart = (XamOrgChart)e.DropTarget;

                //Get the collection with sibling items depending on 
                //the type of the Drag Source.
                if (dragSource.Data is Department)
                {
                    OrgChartExtensions.DropOnOrgChart(dragSource, orgChart, "Departments");
                }
                else if (dragSource.Data is EmployeePosition)
                {
                    OrgChartExtensions.DropOnOrgChart(dragSource, orgChart, "EmployeePositions");
                }
                else if (dragSource.Data is Employee)
                {
                    OrgChartExtensions.DropOnOrgChart(dragSource, orgChart, "Employees");
                }
            }
        }
    }

    public static class OrgChartExtensions
    {
        public static void DropOnNode(OrgChartNode dragSource, OrgChartNode dropTarget, string collectionName)
        {
            //Check if the drop target already contains the dragged node.
            if (dropTarget.ContainsNode(1, dragSource.Data)) return;

            //Check for a recursive relation between the drag source and the drop target.
            int levels = dragSource.OrgChart.ActualDepth - dragSource.Level;
            if (dragSource.ContainsNode(levels, dropTarget.Data)) return;

            //Remove the dragged node from its parent.
            dragSource
                .GetSiblingsCollection(collectionName)
                .InvokeMethod("Remove", new object[] { dragSource.Data });
            //Add the dragged node to the drop target node.
            dropTarget
                .GetChildrenCollection(collectionName)
                .InvokeMethod("Add", new object[] { dragSource.Data });
        }

        public static void DropOnOrgChart(OrgChartNode dragSource, XamOrgChart dropTarget, string collectionName)
        {
            //Get the items source of the XamOrgChart.
            object itemsSource = dropTarget.GetItemsSource();

            //Check if the drop target already contains the dragged item.
            if (dropTarget.RootNode.ChildNodes.Select(node => node.Data).Contains(dragSource)) return;

            //Remove the dragged node from its parent.
            dragSource
                .GetSiblingsCollection(collectionName)
                .InvokeMethod("Remove", new object[] { dragSource.Data });

            //Add the dragged node to the org chart's items source.
            itemsSource
                .InvokeMethod("Add", new object[] { dragSource.Data });
        }

        //Retrieves the collection, which contains organizational child items.
        public static object GetChildrenCollection(this OrgChartNode node, string childrenCollection)
        {
            PropertyInfo property = node.Data.GetType().GetProperty(childrenCollection);

            if (property != null)
            {
                return property.GetValue(node.Data, null);
            }

            return null;
        }

        //Retrieves the collection, which contains nodes on the same organization level.
        public static object GetSiblingsCollection(this OrgChartNode node, string siblingsCollection)
        {
            if (node.ParentNode.Data == null)
            {
                return node.OrgChart.GetItemsSource();
            }

            return node.ParentNode.GetChildrenCollection(siblingsCollection);
        }

        //Retrieves the collection, which is assigned to the ItemsSource property of the OrgChart.
        public static object GetItemsSource(this XamOrgChart orgChart)
        {
            Binding binding = orgChart.GetBindingExpression(XamOrgChart.ItemsSourceProperty).ParentBinding;
            object dataContext = orgChart.GetValue(XamOrgChart.DataContextProperty);

            return binding.Path == null
                       ? dataContext
                       : dataContext.GetType().GetProperty(binding.Path.Path).GetValue(dataContext, null);
        }

        //Determines if an organization item contains another item in its children.
        public static bool ContainsNode(this OrgChartNode orgChartNode, int levels, object data)
        {
            return orgChartNode.HighlightChildren(levels)
                .Select(node => node.Data)
                .Contains(data);
        }

        //Invokes a method on a target item - used to add and remove items from the
        //child collections.
        public static void InvokeMethod(this object obj, string method, object[] parameters)
        {
            obj.GetType().GetMethod(method).Invoke(obj, parameters);
        }
    }

}
