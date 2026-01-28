using System;
using System.IO;
using System.Windows.Controls;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Themes;

namespace IGRichTextEditor.Samples.Style
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();
            InitializeSample();
        }

        public void InitializeSample()
        { 

            Stream fs = FilesProvider.GetStreamForFile(RichTextEditorStrings.fileDocxText);
            RichTextDocument doc = new RichTextDocument();
            doc.LoadFromWord(fs);
            this.xamRichTextEditor1.Document = doc;
        }
        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme  
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }
        
    }
}
