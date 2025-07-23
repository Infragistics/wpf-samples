using System; 
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models; 
using Infragistics.Themes;

namespace IGSpellChecker.Samples.Styling
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();
        }
        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme  
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }

        private void btnCheckSpelling_Click(object sender, RoutedEventArgs e)
        {
            igSpellChecker.DictionaryUri = DictionaryFileProvider.GetDictionaryLocalUri("us-english-v2-whole.dict");

            igSpellChecker.SpellCheck();
        }
    }
}
