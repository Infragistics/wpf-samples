using Infragistics.Documents.RichText;

namespace IGRichTextEditor.Samples.Helpers
{
    public class DocumentSpanHelper
    {
        public static DocumentSpan GetMergedSpan(DocumentSpan startSpan, DocumentSpan endSpan)
        {
            DocumentSpan mergedSpan = new DocumentSpan(startSpan.Offset, endSpan.Offset + endSpan.Length - startSpan.Offset);
            return mergedSpan;
        }
    }
}
