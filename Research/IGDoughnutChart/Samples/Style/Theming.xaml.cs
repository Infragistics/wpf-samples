using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models; 
using Infragistics.Themes; 
using System.Windows.Controls;

namespace IGDoughnutChart.Samples
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

        private void DoughnutChart_SliceClick(object sender, SliceClickEventArgs e)
        {
            e.IsExploded = !e.IsExploded;
        }
    }
}
