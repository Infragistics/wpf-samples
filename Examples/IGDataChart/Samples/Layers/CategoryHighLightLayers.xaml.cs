using System;
using System.Linq;
using System.Windows;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Layers
{
    public partial class CategoryHighLightLayers : Infragistics.Samples.Framework.SampleContainer
    {
        public CategoryHighLightLayers()
        {
            InitializeComponent();
            UseDefaultTheme = true;
        }
        
        private void TransitionDuration_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DataChart == null)
            {
                return;
            }
            var layer = DataChart.Series.OfType<CategoryHighlightLayer>().FirstOrDefault();
            if (layer != null)
            {
                layer.TransitionDuration = TimeSpan.FromMilliseconds(e.NewValue);
            }
        }       
    }
}
