using System; 
using System.Linq;
using System.Windows;
using System.Windows.Controls; 
using Infragistics.Controls.Charts; 

namespace IGDataChart.Samples.Layers
{
    public partial class CrosshairLayers : Infragistics.Samples.Framework.SampleContainer
    { 
        public CrosshairLayers()
        {
            InitializeComponent();
            UseDefaultTheme = true;

            this.Loaded += CrosshairLayers_Loaded;
        } 
        
        private CrosshairLayer layer = null;

        private void CrosshairLayers_Loaded(object sender, RoutedEventArgs e)
        {
            layer = DataChart.Series.OfType<CrosshairLayer>().FirstOrDefault();             
        }
  
        private void HorizontalCrosshairVisibilty_OnChecked(object sender, RoutedEventArgs e)
        {
            if (layer == null) return;

            var isChecked = (sender as CheckBox).IsChecked.Value;
            layer.HorizontalLineVisibility = isChecked ? Visibility.Visible : Visibility.Collapsed;             
        }
         
        private void VerticalCrosshairVisibilty_OnChecked(object sender, RoutedEventArgs e)
        { 
            if (layer == null) return;

            var isChecked = (sender as CheckBox).IsChecked.Value;
            layer.VerticalLineVisibility = isChecked ? Visibility.Visible : Visibility.Collapsed;
        } 
    }
}