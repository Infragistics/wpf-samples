using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;

namespace IGOrgChart.Samples.Navigation
{
    public partial class HighlightChildOrParentNodes : SampleContainer
    {
        public HighlightChildOrParentNodes()
        {
            InitializeComponent();
        }

        private OrgChartNode _selectedNode;
        private List<OrgChartNode> _highlightedNodes = new List<OrgChartNode>();

        private void OrgChart_SelectedNodesCollectionChanged(object sender, OrgChartNodeSelectionEventArgs e)
        {
            _selectedNode = e.CurrentSelectedNodes.FirstOrDefault();

            bool isEnabled = _selectedNode == null ? false : true;

            ButtonHighlightChildren.IsEnabled = isEnabled;
            ButtonHighlightParents.IsEnabled = isEnabled;
        }

        private void HighlightParents_Click(object sender, RoutedEventArgs e)
        {
            //Reset the background of the previously highlighted nodes.
            ClearHighlights();

            //Get the highlighted parents.
            _highlightedNodes = _selectedNode.HighlightParents(Convert.ToInt32(SliderLevels.Value)).ToList();

            var highlightedStyle = this.Resources["HighlightedStyle"] as System.Windows.Style;

            //Set the background on the new highlighted nodes.
            SetHighlights(highlightedStyle);
        }

        private void HighlightChildren_Click(object sender, RoutedEventArgs e)
        {
            //Reset the background of the previously highlighted nodes.
            ClearHighlights();

            //Get the highlighted children.
            _highlightedNodes = _selectedNode.HighlightChildren(Convert.ToInt32(SliderLevels.Value)).ToList();

            var highlightedStyle = this.Resources["HighlightedStyle"] as System.Windows.Style;

            //Set the background on the new highlighted nodes.
            SetHighlights(highlightedStyle);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //Reset the background of the highlighted nodes.
            ClearHighlights();
        }

        private void SetHighlights(System.Windows.Style style)
        {
            //Set the background of the highlighted nodes.
            foreach (var node in _highlightedNodes)
            {
                node.Style = style;
            }
        }

        private void ClearHighlights()
        {
            foreach (OrgChartNode node in _highlightedNodes)
            {
                node.Style = null;
            }
        }
    }
}
