using System;
using System.Windows;
using IGSyntaxEditor.Resources;
using Infragistics.Controls.Editors;
using Infragistics.Documents;
using Infragistics.Samples.Framework;
using Infragistics.Documents.Parsing;

namespace IGSyntaxEditor.Samples.Editing
{
    public partial class SelectionManager : SampleContainer
    {
        public SelectionManager()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            var td = new TextDocument();
            td.Language = VisualBasicLanguage.Instance;
            td.InitializeText(SyntaxEditorStrings.SourceVB);
            this.xamSyntaxEditor1.Document = td;
        }

		private void xamSyntaxEditor1_SelectionChanged(object sender, SyntaxEditorSelectionChangedEventArgs e)
        {
            // Get the selected text
            string selection = this.xamSyntaxEditor1.ActiveDocumentView.SelectionManager.SelectedText;

            if (string.IsNullOrEmpty(selection))
            {
                // clear all indicators
                this.selectedText.Text = string.Empty;
                this.startSelection.Text = string.Empty;
                this.endSelection.Text = string.Empty;
                this.lengthOfSelection.Text = string.Empty;
            }
            else
            {
                this.selectedText.Text = selection;

                // Get the start and the end position of the current selection
                this.startSelection.Text = this.xamSyntaxEditor1.ActiveDocumentView.SelectionManager.SelectionStart.Position.ToString();
                this.endSelection.Text = this.xamSyntaxEditor1.ActiveDocumentView.SelectionManager.SelectionEnd.Position.ToString();
                this.lengthOfSelection.Text = this.xamSyntaxEditor1.ActiveDocumentView.SelectionManager.SelectionLength.ToString();
            }
        }

        private void bSelect_Click(object sender, RoutedEventArgs e)
        {
            int startIndexToInt;
            int endIndexToInt;

            // try to parse user entered values for selection start and end index
            try
            {
                startIndexToInt = Int32.Parse(startIndex.Text);
                endIndexToInt = Int32.Parse(endIndex.Text);
            }
            catch
            {
                MessageBox.Show(SyntaxEditorStrings.errValidNumbers);
                return;
            }

            // obtain the current snapshot
            TextDocumentSnapshot tdsp = this.xamSyntaxEditor1.ActiveDocumentView.CurrentSnapshot;

            // get TextLocation objects from indexes
            TextLocation stl = tdsp.LocationFromOffset(startIndexToInt);
            TextLocation etl = tdsp.LocationFromOffset(endIndexToInt);

            // convert TextLocation object to SnapshotPoint objects
            SnapshotPoint ssp = SnapshotPoint.FromTextLocation(stl, tdsp);
            SnapshotPoint esp = SnapshotPoint.FromTextLocation(etl, tdsp);

            // set the start and the end of the selection using the SnapshotPoint objects
            this.xamSyntaxEditor1.ActiveDocumentView.SelectionManager.SelectionStart = ssp;
            this.xamSyntaxEditor1.ActiveDocumentView.SelectionManager.SelectionEnd = esp;
        }

        private void bIncIndent_Click(object sender, RoutedEventArgs e)
        {
            // increase indent
            this.xamSyntaxEditor1.ActiveDocumentView.SelectionManager.IncreaseIndent();
        }

        private void bDecIndent_Click(object sender, RoutedEventArgs e)
        {
            // decrease indent
            this.xamSyntaxEditor1.ActiveDocumentView.SelectionManager.DecreaseIndent();
        }

    }
}
