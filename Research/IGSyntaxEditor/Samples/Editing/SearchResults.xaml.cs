using IGSyntaxEditor.Resources;
using IGSyntaxEditor.Samples.Helpers;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Resources;
using System.Windows;
using System.Windows.Controls;

namespace IGSyntaxEditor.Samples.Editing
{
    /// <summary>
    /// Interaction logic for SearchResults.xaml
    /// </summary>
    public partial class SearchResults : SampleContainer
    {
        public SearchResults()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            // Initialize a text document with Visual Basic language in the xamSyntaxEditor
            var td = new TextDocument();
            td.Language = VisualBasicLanguage.Instance;
            td.InitializeText(SyntaxEditorStrings.SourceVB);
            this.xamSyntaxEditor1.Document = td;
        }

        private void bSearch_Click(object sender, RoutedEventArgs e)
        {
            // Clear the current results in the list
            this.listFindResults.Items.Clear();

            if (string.IsNullOrEmpty(this.tbFind.Text))
            {
                MessageBox.Show(SyntaxEditorStrings.errTheSearchInputFieldIsEmpty, CommonStrings.UnexpectedError, MessageBoxButton.OK);
                return;
            }

            // Create the search criteria object
            var tsc = new TextSearchCriteria(this.tbFind.Text,
                false, // match whole words
                false); // case sensitive

            // Perform a FindAll using the 'find text' entered by the user
            TextSearchResultInfo searchResults = this.xamSyntaxEditor1.Document.CurrentSnapshot.FindAll(tsc);

            if (searchResults != null)
            {
                // Populate the search results in the list with the custom class FindResultInfo
                foreach (TextSearchResult searchResult in searchResults.Results)
                {
                    // Obtain the span containing the search result
                    SnapshotSpan span = searchResult.SpanFound;

                    // Obtain the line at which the result is found
                    SnapshotLineInfo lineInfo = span.Snapshot.LineFromOffset(span.Offset);

                    // Create a custom object witch will be used for displaying search results in the list and
                    // for navigating through search results
                    var fri = new FindResultInfo(
                        lineInfo.LineNumber, // information about the line number
                        lineInfo.GetText(false), // the content of the line
                        span);

                    // add the result info object in the list
                    this.listFindResults.Items.Add(fri);
                }
            }
        }

        private void listFindResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the FindResultInfo object of the current find results selection
            var findResultInfo = this.listFindResults.SelectedItem as FindResultInfo;
            if (null != findResultInfo)
            {
                // Obtain the current snapshot of the document, which represents the current state of the document
                TextDocumentSnapshot currentSnapshot = this.xamSyntaxEditor1.Document.CurrentSnapshot;

                // Compare it with the snapshot (state) of the find result
                if (findResultInfo.Span.Snapshot == currentSnapshot)
                {
                    // If there are not changes -> just scroll to make the find result visible
                    this.xamSyntaxEditor1.ActiveDocumentView.GoToOffset(findResultInfo.Span.Offset);
                }
                else
                {
                    // If the document has been changed you need to translate the span to the new snapshot
                    SnapshotSpan translatedSpan = findResultInfo.Span.TranslateTo(currentSnapshot, SpanTrackingMode.EdgePositive);
                    // Scroll to make the find result visible
                    this.xamSyntaxEditor1.ActiveDocumentView.GoToOffset(translatedSpan.Offset);
                }

                // focus the xamSyntaxEditor, so that the caret will become visible
                this.xamSyntaxEditor1.Focus();
            }
        }

    }
}
