using System.Collections.Generic;
using System.Windows.Media;
using IGSyntaxEditor.Resources;
using IGSyntaxEditor.Samples.Helpers;
using Infragistics.Controls.Editors;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Display
{
    public partial class ErrorReporting : SampleContainer
    {
        public ErrorReporting()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            // attach a text loaded event handler
            this.xamSyntaxEditor1.Document.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Document_PropertyChanged);

            // fill colors in the foreground and background combo boxes
            List<Color> colorList = new List<Color>();
            colorList.Add(Colors.Black);
            colorList.Add(Colors.DarkOrange);
            colorList.Add(Colors.OrangeRed);
            colorList.Add(Colors.Crimson);
            colorList.Add(Colors.DeepPink);
            colorList.Add(Colors.Purple);
            colorList.Add(Colors.DodgerBlue);
            colorList.Add(Colors.DarkGreen);
            colorList.Add(Colors.YellowGreen);
            colorList.Add(Colors.White);
            this.cbColor.ItemsSource = colorList;
            this.cbColor.SelectedIndex = 7; // select initially red

            // load a wrong formatted C# content
            this.xamSyntaxEditor1.Document.InitializeText(SyntaxEditorStrings.SourceCSWrong);
        }

        // after the syntax tree is built, populate the list with syntax errors
        void Document_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SyntaxTree")
            {
                // obtain document
                TextDocument doc = this.xamSyntaxEditor1.Document;

                // create a text span of the whole document
                TextSpan ts = new TextSpan(0, doc.CurrentSnapshot.Length);

                // obtain errors from the whole document
				IEnumerable<NodeDiagnostic> diagnostics = doc.SyntaxTree.RootNode.GetDiagnostics(ts);

                List<SyntaxErrorInfo> list = new List<SyntaxErrorInfo>();
                SyntaxErrorInfo seInfo;
				foreach (NodeDiagnostic diagnostic in diagnostics)
                {
					// Obtain the location of the error
					SnapshotSpan span = diagnostic.SnapshotSpan;
					TextLocation location = span.Snapshot.LocationFromOffset(span.Offset);

					// Create an info object and set message, location and snapshot span
					seInfo = new SyntaxErrorInfo()
					{
						Message = diagnostic.Message,
						Line = location.Line + 1,
						Character = location.Character + 1,
						Span = diagnostic.SnapshotSpan
					};

					// Add the error in the list
					list.Add(seInfo);
                }

                // set the errors as items source of the list
                this.errorsList.ItemsSource = list;
            }
        }

        // change the color of the squiggles
        private void cbColor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // create a new classification appearance map
            ClassificationAppearanceMap cam = new ClassificationAppearanceMap();

			// add a solid color brush with the selected color as "DiagnosticError" classification type
            cam.AddMapEntry(
                ClassificationType.DiagnosticError,
                new TextDocumentAppearance()
                {
                    Foreground = new SolidColorBrush((Color)this.cbColor.SelectedItem)
                }
            );
            
            // set the map to the xamSyntaxEditor
            this.xamSyntaxEditor1.ClassificationAppearanceMap = cam;
        }

        private void errorsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Get the SyntaxErrorInfo object of the selected error
            SyntaxErrorInfo syntaxErrorInfo = this.errorsList.SelectedItem as SyntaxErrorInfo;
            if (null != syntaxErrorInfo)
            {
                // Obtain the current snapshot of the document, which represents the current state of the document
                TextDocumentSnapshot currentSnapshot = this.xamSyntaxEditor1.Document.CurrentSnapshot;

                // Compare it with the snapshot (state) of the current error
                if (syntaxErrorInfo.Span.Snapshot == currentSnapshot)
                {
                    // If there are not changes -> just scroll to make the find result visible
                    this.xamSyntaxEditor1.ActiveDocumentView.GoToOffset(syntaxErrorInfo.Span.Offset);
                }
                else
                {
                    // If the document has been changed you need to translate the span to the new snapshot
                    SnapshotSpan translatedSpan = syntaxErrorInfo.Span.TranslateTo(currentSnapshot, SpanTrackingMode.EdgePositive);
                    // Scroll to make the find result visible
                    this.xamSyntaxEditor1.ActiveDocumentView.GoToOffset(translatedSpan.Offset);
                }
                this.xamSyntaxEditor1.Focus();
            }
        }

    }
}
