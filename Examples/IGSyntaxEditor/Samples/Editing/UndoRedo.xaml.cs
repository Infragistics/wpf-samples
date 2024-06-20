using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;
using Infragistics.Undo;
using System.Windows;

namespace IGSyntaxEditor.Samples.Editing
{
    public partial class UndoRedo : SampleContainer
    {
        bool sharedMan = false;

        public UndoRedo()
        {
            InitializeComponent();
            this.UseDefaultTheme = true;
        }

        // configure the two xamSyntaxEditor-s to use separate Undo/Redo managers
        private void rbSeparate_Click(object sender, RoutedEventArgs e)
        {
            if (!sharedMan) return;
            sharedMan = false;
            this.spSharedControls.Visibility = System.Windows.Visibility.Collapsed;
            this.spSeparateControls.Visibility = System.Windows.Visibility.Visible;
            this.xamSyntaxEditorLeft.Document = new TextDocument() { Language = PlainTextLanguage.Instance };
            this.xamSyntaxEditorLeft.Document.UndoManager = new UndoManager();
            this.xamSyntaxEditorRight.Document = new TextDocument() { Language = PlainTextLanguage.Instance };
            this.xamSyntaxEditorRight.Document.UndoManager = new UndoManager();
        }

        // configure the two xamSyntaxEditor-s to use a single Undo/Redo manager
        private void rbShared_Click(object sender, RoutedEventArgs e)
        {
            if (sharedMan) return;
            sharedMan = true;
            this.spSeparateControls.Visibility = System.Windows.Visibility.Collapsed;
            this.spSharedControls.Visibility = System.Windows.Visibility.Visible;
            UndoManager undoMan = new UndoManager();
            this.xamSyntaxEditorLeft.Document = new TextDocument() { Language = PlainTextLanguage.Instance };
            this.xamSyntaxEditorLeft.Document.UndoManager = undoMan;
            this.xamSyntaxEditorRight.Document = new TextDocument() { Language = PlainTextLanguage.Instance };
            this.xamSyntaxEditorRight.Document.UndoManager = undoMan;
        }
    }
}
