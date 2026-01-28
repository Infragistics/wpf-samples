using Infragistics.Documents;

namespace IGSyntaxEditor.Samples.Helpers
{
    /// <summary>
    /// This class will hold information about one search result. It contains:
    /// - the line number
    /// - the content of the line
    /// - the span of the text document containing the search result
    /// </summary>
    public class FindResultInfo
    {
        public FindResultInfo(int lineNumber, string lineText, SnapshotSpan span)
        {
            this.LineNumber = lineNumber;
            this.LineText = lineText;
            this.Span = span;
        }

        public int LineNumber { get; internal set; }
        public string LineText { get; internal set; }
        public SnapshotSpan Span { get; internal set; }
    }

}
