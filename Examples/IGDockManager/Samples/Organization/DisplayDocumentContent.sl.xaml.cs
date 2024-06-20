using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;

namespace IGDockManager.Samples.Organization
{
    public partial class DisplayDocumentContent : SampleContainer
    {
        public DisplayDocumentContent()
        {
            InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Text Files (.txt)|*.txt|C# Files (.cs)|*.cs|XAML Files (.xaml)|*.xaml"
            };

            bool? openDialogResult = fileDialog.ShowDialog();

            if (openDialogResult == true)
            {
                // Open the selected file to read.
                Stream fileStream = fileDialog.File.OpenRead();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string fileName = fileDialog.File.Name;

                    // Create RichTextBox control to display the text file content.
                    RichTextBox richTextBox = new RichTextBox()
                    {
                        FontFamily = new FontFamily("Consolas"),
                        FontSize = 12,
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                        TextWrapping = TextWrapping.NoWrap
                    };

                    // Create document Content Pane to hold the RichTextBox control as Content
                    ContentPane docPane = new ContentPane();
                    docPane.IsDocumentPane = true;
                    docPane.Header = fileName;

                    // Read the file and write it the richtextbox.
                    richTextBox.Selection.Text = reader.ReadToEnd();

                    docPane.Content = richTextBox;

                    // Find the tab group that will contain the document pane.
                    TabGroupPane tbg = (TabGroupPane)xamDockManager.FindPane("docTabGroup");
                    // Add the newly created document pane to the tab group's Panes collection
                    tbg.Panes.Add(docPane);
                    docPane.IsActivePane = true;

                    // Add the recently opened documents as text entries in the History pane 
                    ListBox historyListBox = this.FindName("recentDocs") as ListBox;
                    ListBoxItem item = new ListBoxItem() { Content = fileName };
                    historyListBox.Items.Add(item);
                }

                fileStream.Close();
            }
        }
    }
}
