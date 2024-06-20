using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models; 
using Infragistics.Themes; 
using System.Threading;
using System.Windows;

namespace IGComboEditor.Samples.Style
{
    /// <summary>
    /// Interaction logic for Theming.xaml
    /// </summary>
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();            
            Loaded += new RoutedEventHandler(Theming_Loaded);
        }

        private void Theming_Loaded(object sender, RoutedEventArgs e)
        {  
            JPCustomValueEnteredActionCheck();   
        }
        
        private void OnSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme  
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }

        private void JPCustomValueEnteredActionCheck()
        {
            string isoLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            // customization
            if (isoLanguage.ToLower().Equals("ja"))
            {
                ComboEditor.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
            }
        }
    }
}
