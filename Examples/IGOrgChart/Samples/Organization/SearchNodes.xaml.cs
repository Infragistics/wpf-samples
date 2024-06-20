using IGOrgChart.Converters;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;
using System.Collections.Generic;
using System.Linq;

namespace IGOrgChart.Samples.Organization
{
    public partial class SearchNodes : SampleContainer
    {
        public SearchNodes()
        {
            InitializeComponent();
        }

        //A list with the search results.
        private List<OrgChartNode> _searchResults = new List<OrgChartNode>();

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Reset the background of the previous search results nodes.
            ClearHighlights();

            //If the Search Text Box is empty, the search results are cleared.
            if (string.IsNullOrWhiteSpace(TextBoxSearchText.Text))
            {
                //Clear the list with the highlighted nodes.
                _searchResults.Clear();

                //Clear the list box with the search results.
                ListBoxSearchResults.ItemsSource = null;
            }
            else
            {
                //Make a new search.

                //Searching using a method delegate.
                _searchResults = OrgChart.Search(CompareDescription).ToList();

                //###############################
                //Searching using a lambda expression.
                //
                //Note: You can choose between specifying a search
                //condition with a method delegate or a lambda expression.
                //
                //_highlightedNodes = OrgChart.Search
                //    (
                //        x => new NodeDepartmentDataToDescriptionConverter()
                //            .Convert(x, null, null, null).ToString().ToLower()
                //            .Contains(TextBoxSearchText.Text.ToLower())
                //    ).ToList();
                //###############################

                System.Windows.Style highlightedStyle = this.Resources["HighlightedStyle"] as System.Windows.Style;

                //Set the background of the nodes from the serach results.
                SetHighlights(highlightedStyle);

                //Update the list box with the new search results.
                ListBoxSearchResults.ItemsSource = _searchResults;
            }
        }

        /// <summary>
        /// A boolean method, which determines if a node will be included in the search results based on the node's Data.
        /// </summary>
        /// <param name="data">The data of the node.</param>
        /// <returns>Returns true if the node's data corresponds to the search criteria.</returns>
        private bool CompareDescription(object data)
        {
            //Get the text from the Search Text Box.
            string searchText = TextBoxSearchText.Text.ToLower();

            //Create a new instance of the NodeDepartmentDataToDescriptionConverter class.
            NodeDepartmentDataToDescriptionConverter converter =
                new NodeDepartmentDataToDescriptionConverter();

            //Get the description for the current node's Data.
            string description = converter.Convert(data, null, null, null).ToString().ToLower();

            //Check if the description contains the searched text.
            return description.Contains(searchText);
        }

        private void SetHighlights(System.Windows.Style style)
        {
            //Set the background of the highlighted nodes.
            foreach (var node in _searchResults)
            {
                node.Style = style;
            }
        }

        private void ClearHighlights()
        {
            foreach (OrgChartNode node in _searchResults)
            {
                node.Style = null;
            }
        }
    }
}
