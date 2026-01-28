using Infragistics;
using Infragistics.Samples.Controls;
using Infragistics.Samples.Framework;
using System.Windows.Controls;

namespace IGDataChart.Samples.Display.Series
{
    public partial class GalleryScatterContourSeries : SampleContainer
    {
        public GalleryScatterContourSeries()
        {
            InitializeComponent();            
        }
        
        private void OnBrushPaletteChanged(object sender, SelectionChangedEventArgs e)
        {
            var brushPalette = (BrushPalette)e.AddedItems[0];

            var brushes = new BrushCollection();
            foreach (var item in brushPalette)
            {
                brushes.Add(item);
            }
            if (FillScale != null)
            {
                FillScale.Brushes = brushes;
            }
        }        
    }
}
