using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models; 
using Infragistics.Themes; 
using System.Windows.Controls;

namespace IGSurfaceChart.Samples.Style
{ 
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
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
