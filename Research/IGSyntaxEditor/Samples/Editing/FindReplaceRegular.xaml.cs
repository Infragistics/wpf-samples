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
    /// Interaction logic for FindReplaceRegular.xaml
    /// </summary>
    public partial class FindReplaceRegular : SampleContainer
    {
        public FindReplaceRegular()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            var td = new TextDocument();
            td.Language = PlainTextLanguage.Instance;
            td.InitializeText(SyntaxEditorStrings.LoremIpsumWrong);
            this.xamSyntaxEditor1.Document = td;

            // change the color for the undefined text chunks to black (default is orange)
            var map = new ClassificationAppearanceMap();
            map.AddMapEntry(ClassificationType.Undefined, new TextDocumentAppearance { Foreground = new SolidColorBrush(Colors.Black) });
            this.xamSyntaxEditor1.ClassificationAppearanceMap = map;
        }

        private void bReplaceAll_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbFind.Text))
            {
                MessageBox.Show(SyntaxEditorStrings.errTheSearchInputFieldIsEmpty, CommonStrings.UnexpectedError, MessageBoxButton.OK);
                return;
            }

            var tsc = new TextSearchCriteria(
                false,
                this.tbFind.Text, // regular expression
                false,
                false
            );

            this.xamSyntaxEditor1.Document.FindReplaceAll(
                tsc, // criteria
                this.tbReplace.Text // replacement text
                );
        }

    }
}
