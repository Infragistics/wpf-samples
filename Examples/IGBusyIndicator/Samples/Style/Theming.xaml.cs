using IGBusyIndicator.Samples.DataProviders;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Themes;
using System.Windows;
using System.Windows.Controls;

namespace IGBusyIndicator.Samples.Style
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
