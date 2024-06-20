using IGSyntaxEditor.Resources;
using Infragistics.Controls.Editors;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Resources;
using System.Windows;
using System.Windows.Media;

namespace IGSyntaxEditor.Samples.Editing
{
    /// <summary>
    /// Interaction logic for FindReplace.xaml
    /// </summary>
    public partial class FindReplace : SampleContainer
    {
        public FindReplace()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            var td = new TextDocument();
            td.Language = PlainTextLanguage.Instance;
            td.InitializeText(SyntaxEditorStrings.LoremIpsum);
            this.xamSyntaxEditor1.Document = td;

            // change the color for the undefined text chunks to black (default is orange)
            var map = new ClassificationAppearanceMap();
            map.AddMapEntry(ClassificationType.Undefined, new TextDocumentAppearance { Foreground = new SolidColorBrush(Colors.Black) });
            this.xamSyntaxEditor1.ClassificationAppearanceMap = map;
        }

        private void bReplace_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbFind.Text))
            {
                MessageBox.Show(SyntaxEditorStrings.errTheSearchInputFieldIsEmpty, CommonStrings.UnexpectedError, MessageBoxButton.OK);
                return;
            }

            var tsc = new TextSearchCriteria(
                this.tbFind.Text, // text to find
                this.cbWWO.IsChecked.Value, // whole word only
                this.cbCS.IsChecked.Value, // is case sensitive
                this.rbDirUp.IsChecked.Value // search backwards
                );

            var tl = this.xamSyntaxEditor1.Caret.TextLocation;
            int offset = this.xamSyntaxEditor1.Document.CurrentSnapshot.OffsetFromLocation(tl);

            this.xamSyntaxEditor1.Document.FindReplace(
                this.tbReplace.Text, // newText
                tsc, // criteria
                offset, // start offset
                this.cbWEF.IsChecked.Value
                );
        }

        private void bReplaceAll_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbFind.Text))
            {
                MessageBox.Show(SyntaxEditorStrings.errTheSearchInputFieldIsEmpty, CommonStrings.UnexpectedError, MessageBoxButton.OK);
                return;
            }

            var tsc = new TextSearchCriteria(
                this.tbFind.Text, // text to find
                this.cbWWO.IsChecked.Value, // whole word only
                this.cbCS.IsChecked.Value, // is case sensitive
                this.rbDirUp.IsChecked.Value // search backwards
                );

            this.xamSyntaxEditor1.Document.FindReplaceAll(
                tsc, // criteria
                this.tbReplace.Text // replacement text
                );
        }

    }
}
