using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;
using Infragistics.DragDrop;

namespace IGOrgChart.Samples.Navigation
{
    public partial class HighlightPathBetweenNodes : SampleContainer
    {
        private OrgChartNodeControl currentDragged;
        
        //The DataTemplate for the dragged nodes.
        private DataTemplate _dragTemplate;

        public HighlightPathBetweenNodes()
        {
            InitializeComponent();
            _dragTemplate = this.Resources["NodeDragTemplate"] as DataTemplate;
        }
        
        // This method is called whenever a node is brought into view. It makes the node a drag source.
        private void OrgChart_NodeControlAttachedEvent(object sender, OrgChartNodeEventArgs e)
        {
            //NOTE: 
            //The OrgChartNode class holds the information for a node of the OrgChart control.
            //The OrgChartNodeControl class is a container and a visual representation of the OrgChartNode.
            //Whenever a node is move out of the visible are of the OrgChart,
            //its associated OrgChartNodeControl is disposed.

            //Make the new OrgChartNodeControl a Drag Source.
            DragSource dragSource = new DragSource() { IsDraggable = true, DragTemplate = _dragTemplate };
            dragSource.DragStart += DragStart;
            dragSource.Drop += NodeDrop;

            DragDropManager.SetDragSource(e.Node, dragSource);
        }
        private void OrgChart_NodeControlDetachedEvent(object sender, OrgChartNodeEventArgs e)
        {
            if (DragDropManager.IsDragging && e.Node == currentDragged)
            {
                DragDropManager.EndDrag(true);
            }
        }
        
        private void DragStart(object sender, DragDropStartEventArgs e)
        {
            //Assing the Node's Data as data context for the DragTemplate.
            e.Data = e.DragSource.GetValue(DataContextProperty);
            currentDragged = (OrgChartNodeControl)e.DragSource;
        }

        private OrgChartNode _sourceNode;
        private OrgChartNode _destinationNode;
        private List<OrgChartNode> _path = new List<OrgChartNode>();

        private void NodeDrop(object sender, DropEventArgs e)
        {
            //Get the dragged node.
            OrgChartNodeControl dragSource = e.DragSource as OrgChartNodeControl;
            //Get the drop target.
            Border dropTarget = e.DropTarget as Border;

            switch (dropTarget.Name)
            {
                case "BorderSource":
                    _sourceNode = dragSource.Node;
                    BorderSource.DataContext = _sourceNode;
                    break;
                case "BorderDestination":
                    _destinationNode = dragSource.Node;
                    BorderDestination.DataContext = _destinationNode;
                    break;
            }

            if (_sourceNode != null && _destinationNode != null)
            {
                //Find the path between the source and the destination nodes.

                //Clear the previous path.
                ClearHighlights();

                //Get the parth between the source and the destination nodes.
                _path = _sourceNode.Highlight(_destinationNode).ToList();
                ListBoxPath.ItemsSource = _path;

                var highlightedStyle = this.Resources["HighlightedStyle"] as System.Windows.Style;

                //Highlight the new path.
                SetHighlights(highlightedStyle);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _sourceNode = null;
            _destinationNode = null;
            BorderSource.DataContext = null;
            BorderDestination.DataContext = null;

            //Clear the path.
            ClearHighlights();

            ListBoxPath.ItemsSource = null;
        }

        private void SetHighlights(Style style)
        {
            //Set the background of the highlighted nodes.
            foreach (var node in _path)
            {
                node.Style = style;
            }
        }

        private void ClearHighlights()
        {
            foreach (OrgChartNode node in _path)
            {
                node.Style = null;
            }
        }
    }
}
