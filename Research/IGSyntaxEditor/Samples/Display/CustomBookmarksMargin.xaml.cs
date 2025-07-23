using System;
using System.Windows.Controls;
using System.Windows.Data;
using IGSyntaxEditor.Resources;
using IGSyntaxEditor.Samples.CustomMargins;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Display
{
    public partial class CustomBookmarksMargin : SampleContainer
    {
        private BookmarksMargin bmMargin;

        public CustomBookmarksMargin()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            // create a text document with plain text language and add a long text in the document
            var td = new TextDocument();
            td.Language = PlainTextLanguage.Instance;
            td.InitializeText(SyntaxEditorStrings.LoremIpsumLong);
            this.xamSyntaxEditor1.Document = td;

            // create the custom bookmarks margin
            this.bmMargin = new BookmarksMargin();

            // set the "Bookmarks" property of the margin as ItemsSource of the combobox in the sample
            this.bookmarksList.ItemsSource = bmMargin.Bookmarks;

            // add the margin in the CustomMargins collection of the xamSyntaxEditor
            this.xamSyntaxEditor1.CustomMargins.Add(bmMargin);
        }

        private void bookmarksList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // when the selected margin in the combobox is changed - focus the new selected line
            int selectedLineIndex = ((ComboBox)sender).SelectedIndex;
            this.bmMargin.FocusBookmarkIndex(selectedLineIndex);
            this.xamSyntaxEditor1.Focus();
        }
    }

    // this convertor will be used to convert line indexes to a string containg "Line " and the number
    public class MyCnv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return (((int)value) + 1).ToString();
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
