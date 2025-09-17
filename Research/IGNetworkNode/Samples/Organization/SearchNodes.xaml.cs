using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Infragistics.Controls.Maps;
using System.Windows;
using Infragistics.Samples.Shared.Models;

namespace IGNetworkNode.Samples.Organization
{
    public partial class SearchNodes : Infragistics.Samples.Framework.SampleContainer
    {
        public SearchNodes()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            //Get the style, which is going to be applied to the search results.
            _highlightedStyle = this.Resources["HighlightedStyle"] as System.Windows.Style;

            //Initialize the predicate with the search condition.
            _searchCondition = new Predicate<NodeModel>(SearchCondition);
        }

        //A list with the search results.
        private List<NetworkNodeNode> _searchResults = new List<NetworkNodeNode>();

        //The style applied to the highlighted nodes.
        private System.Windows.Style _highlightedStyle;

        //The predicate with the search condition.
        private Predicate<NodeModel> _searchCondition;

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Restore the default style of the cleared nodes.
            _searchResults.ForEach(node => node.Style = null);

            //Clear the list with the highlighted nodes.
            _searchResults.Clear();

            //Clear the list box with the search results.
            ListBoxSearchResults.ItemsSource = null;

            //Search the nodes if the search text box is not empty.
            if (!String.IsNullOrWhiteSpace(TextBoxSearchText.Text))
            {
                //Make a new search.

                //Searching using a predicate delegate.
                _searchResults = xnn.Search(_searchCondition).ToList();

                //Highlight the search results.
                _searchResults.ForEach(node => node.Style = _highlightedStyle);

                ListBoxSearchResults.ItemsSource = _searchResults;
            }
        }

        /// <summary>
        /// A predicate method, which checks if the node's label contains the
        /// thext from the search box.
        /// </summary>
        /// <param name="node">The current node.</param>
        /// <returns>
        /// A boolean value, which indicates whether the node's label
        /// contains the search text or not.
        /// </returns>
        /// <remarks>
        /// When Network Node's Search method is called, it cycles through all nodes
        /// and checks the search condition on each node. If the search condition returns
        /// True, the node is added to the returned search results.
        /// </remarks>
        private bool SearchCondition(NodeModel node)
        {
            string nodeLabel = node.Label.ToLower();
            string searchText = TextBoxSearchText.Text.ToLower();

            return nodeLabel.Contains(searchText);
        }
    }
}
