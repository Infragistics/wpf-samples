using Infragistics.Documents;

namespace IGSyntaxEditor.Samples.Helpers
{
    public class SyntaxErrorInfo
    {
        public int Line { get; set; }
        public int Character { get; set; }
        public string Message { get; set; }
        public SnapshotSpan Span { get; set; }
    }
}
