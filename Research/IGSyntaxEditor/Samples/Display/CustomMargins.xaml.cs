using System.Windows;
using System.Windows.Media;
using IGSyntaxEditor.Samples.CustomMargins;
using IGSyntaxEditor.Resources;
using Infragistics.Controls.Editors;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Display
{
    public partial class CustomMargins : SampleContainer
    {
        public CustomMargins()
        {
            InitializeComponent();
            InitializeDocument();
        }

        public void InitializeDocument()
        {
            this.UseDefaultTheme = true;
            if (this.xamSyntaxEditor1.Document == null)
            {
                var td = new TextDocument();
                td.Language = PlainTextLanguage.Instance;
                td.InitializeText(SyntaxEditorStrings.LoremIpsum);
                this.xamSyntaxEditor1.Document = td;

                // change the color for the undefined text chunks to black (default is orange)
                var map = new ClassificationAppearanceMap();
                map.AddMapEntry(ClassificationType.Undefined, new TextDocumentAppearance { Foreground = new SolidColorBrush(Colors.Black) });
                this.xamSyntaxEditor1.ClassificationAppearanceMap = map;
            }
        }

        private void ButtonAddTop_Click(object sender, RoutedEventArgs e)
        {
            this.xamSyntaxEditor1.CustomMargins.Add(new CustomMarginTop());
        }

        private void ButtonAddBottom_Click(object sender, RoutedEventArgs e)
        {
            this.xamSyntaxEditor1.CustomMargins.Add(new CustomMarginBottom());
        }

        private void ButtonAddLeft_Click(object sender, RoutedEventArgs e)
        {
            this.xamSyntaxEditor1.CustomMargins.Add(new CustomMarginLeft());
        }

        private void ButtonAddRight_Click(object sender, RoutedEventArgs e)
        {
            this.xamSyntaxEditor1.CustomMargins.Add(new CustomMarginRight());
        }

        private void ButtonRemoveTop_Click(object sender, RoutedEventArgs e)
        {
            RemoveCustomMargin(EditorDocumentViewMarginLocation.Top);
        }

        private void ButtonRemoveBottom_Click(object sender, RoutedEventArgs e)
        {
            RemoveCustomMargin(EditorDocumentViewMarginLocation.Bottom);
        }

        private void ButtonRemoveLeft_Click(object sender, RoutedEventArgs e)
        {
            RemoveCustomMargin(EditorDocumentViewMarginLocation.Left);
        }

        private void ButtonRemoveRight_Click(object sender, RoutedEventArgs e)
        {
            RemoveCustomMargin(EditorDocumentViewMarginLocation.Right);
        }

        private void RemoveCustomMargin(EditorDocumentViewMarginLocation location)
        {
            for (int i = this.xamSyntaxEditor1.CustomMargins.Count - 1; i > -1; i--)
            {
                if (this.xamSyntaxEditor1.CustomMargins[i].Location == location)
                {
                    this.xamSyntaxEditor1.CustomMargins.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
