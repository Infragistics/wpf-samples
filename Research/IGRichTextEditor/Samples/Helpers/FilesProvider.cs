using System;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace IGRichTextEditor.Samples.Helpers
{
    public static class FilesProvider
    {
        public static Stream GetStreamForFile(string fileName)
        {
            Uri uri = new Uri("/IGRichTextEditor;component/Files/" + fileName, UriKind.Relative);
            StreamResourceInfo sri = Application.GetResourceStream(uri);
            return sri.Stream;
        }
    }
}
