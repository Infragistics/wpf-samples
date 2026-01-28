using System; 
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Themes;

namespace IGGantt.Samples.Styling
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
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
