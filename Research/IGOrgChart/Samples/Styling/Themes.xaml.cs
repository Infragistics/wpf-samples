using System;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Themes;
using System.Windows.Controls;

namespace IGOrgChart.Samples.Styling
{
    public partial class Themes : SampleContainer
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
