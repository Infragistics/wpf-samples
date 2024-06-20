using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DockManager;

namespace IGDockManager.Samples.Organization
{
    /// <summary>
    /// Interaction logic for DynamicallyLoadDocumentContent.xaml
    /// </summary>
    public partial class DynamicallyLoadDocumentContentIntoPane : SampleContainer
    {
        public DynamicallyLoadDocumentContentIntoPane()
        {
            InitializeComponent();
        }

        // ==================================================================================
        // In this click event we will display a FileOpenDialog to let the user browse for a
        // file whose content we will load into a RichTextBox within a ContentPane.
        // ==================================================================================
        private void btnBrowseForDocument_Click(object sender, RoutedEventArgs e)
        {
            // Display the FileOpenDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Text Files|*.txt;*.rtf|All Files|*.*";

            if (dlg.ShowDialog() == true)
            {
                FileStream stream = null;
                try
                {
                    // Open a stream on the file and load it into a RichTextBox
                    stream = new FileStream(dlg.FileName, FileMode.Open);
                    ScrollViewer scrollV = new ScrollViewer();
                    RichTextBox rtb = new RichTextBox();
                    TextRange range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    range.Load(stream, DataFormats.Text);
                    scrollV.Content = rtb;

                    // ====================================================================================
                    // Call the XamDockManager.AddDocument method passing in the document's filename as the
                    // header (i.e., pane caption) and the RichTextBox as the content.  It will be added to
                    // the SplitPane that contains the current active document.

                    ContentPane newContentPane = this.dockManager.AddDocument(dlg.FileName, scrollV);
                    newContentPane.Activate();
                    // ====================================================================================
                }
                catch (IOException ex)
                {
                    MessageBox.Show("There was an error opening the specified file: \n\n" + ex.Message);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }
        }
    }
}
