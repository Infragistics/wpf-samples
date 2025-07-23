using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Editing
{
    public partial class Selection : SampleContainer
    {
        public Selection()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            Stream fs = FilesProvider.GetStreamForFile(RichTextEditorStrings.fileDocxText);
            RichTextDocument doc = new RichTextDocument();
            doc.LoadFromWord(fs);
            this.xamRichTextEditor1.Document = doc;
        }

		private void xamRichTextEditor1_SelectionChanged(object sender, Infragistics.Controls.Editors.RichTextSelectionChangedEventArgs e)
        {
            // Get the selected text
            string selection = this.xamRichTextEditor1.ActiveDocumentView.Selection.Text;

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
                this.startSelection.Text = this.xamRichTextEditor1.ActiveDocumentView.Selection.Start.ToString();
                this.endSelection.Text = this.xamRichTextEditor1.ActiveDocumentView.Selection.End.ToString();
                this.lengthOfSelection.Text = this.xamRichTextEditor1.ActiveDocumentView.Selection.Length.ToString();
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
                if (startIndexToInt < 0 || endIndexToInt < 0)
                {
                    MessageBox.Show(RichTextEditorStrings.errValidNumbers);
                    return;
                }
            }
            catch
            {
                MessageBox.Show(RichTextEditorStrings.errValidNumbers);
                return;
            }

            // set the start and the end of the selection
            this.xamRichTextEditor1.ActiveDocumentView.Selection.Start = startIndexToInt;
            this.xamRichTextEditor1.ActiveDocumentView.Selection.End = endIndexToInt;
        }

        private void cb_DropDownClosed(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox cb = sender as ComboBox;
                if (cb.Tag.Equals("cbAColor"))
                {
                    // change the active brush
                    object obj = cb.SelectedItem;
                    if (obj is Color)
                    {
                        Color c = (Color)obj;
                        c.A = (byte)128; // make the color semitransparent
                        this.xamRichTextEditor1.SelectionHighlightActiveBrush = new SolidColorBrush(c);
                        this.xamRichTextEditor1.Focus();
                    }
                }
                else if (cb.Tag.Equals("cbIColor"))
                {
                    // change the inactive brush
                    object obj = cb.SelectedItem;
                    if (obj is Color)
                    {
                        Color c = (Color)obj;
                        c.A = (byte)128; // make the color semitransparent
                        this.xamRichTextEditor1.SelectionHighlightInactiveBrush = new SolidColorBrush(c);
                        this.xamRichTextEditor1.Focus();
                    }
                }
            }
        }

    }
}
