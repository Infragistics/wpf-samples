using System;
using System.Windows;
using Infragistics;
using Infragistics.Controls.Maps;

namespace IGMap.Samples.Performance
{
    public partial class MapPerformance : Infragistics.Samples.Framework.SampleContainer
    {
        public MapPerformance()
        {
            InitializeComponent();

            this.Brushes = this.Resources["MapBrushes"] as BrushCollection;
 
        }

        protected BrushCollection Brushes;

        private void OnLoadMapSymbolsButtonClick(object sender, RoutedEventArgs e)
        {
            LoadMapSymbols();
        }
        private void LoadMapSymbols()
        {
            this.Map.Layers[1].WorldRect = this.Map.Layers[0].WorldRect;
            this.Map.Layers[1].ShadowFill = null;

            var r = new Random();
            var mapElements = new MapElementCollection();
            for (int i = 0; i < 400; i++)
            {
                var origin = new Point(r.Next((int)Map.ActualWorldRect.Left, (int)Map.ActualWorldRect.Right),
                                       r.Next((int)Map.ActualWorldRect.Top, (int)Map.ActualWorldRect.Bottom));

                var symbol = new SymbolElement { SymbolOrigin = origin, SymbolSize = 5, Value = 1, Fill = this.Brushes[r.Next(0, this.Brushes.Count - 1)] };
                mapElements.Add(symbol);
            }
            this.Map.Layers[1].Elements = mapElements;
        }

 

    }
}
