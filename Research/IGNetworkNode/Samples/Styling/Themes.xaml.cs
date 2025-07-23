using System; 
using System.Windows.Controls;
using Infragistics.Samples.Shared.Models;
using Infragistics.Themes;

namespace IGNetworkNode.Samples.Styling
{
    public partial class Themes : Infragistics.Samples.Framework.SampleContainer
    {
        public Themes()
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


