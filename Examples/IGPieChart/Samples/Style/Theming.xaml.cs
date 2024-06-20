using System; 
using System.Windows.Controls;
using Infragistics.Samples.Shared.Models;     // ThemesViewModel
using Infragistics.Themes;

namespace IGPieChart.Samples.Style
{
    public partial class Theming : Infragistics.Samples.Framework.SampleContainer
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
    }
}
