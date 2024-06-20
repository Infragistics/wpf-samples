using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics;

namespace IGMap.Samples.Display
{
    public partial class MapBrushScales : Infragistics.Samples.Framework.SampleContainer
    {
        public MapBrushScales()
        {
            InitializeComponent();
            LoadBruses();
            Loaded += OnSampleLoaded;
        }

        private readonly List<BrushCollection> brushes = new List<BrushCollection>();

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            lbPaletteSettings.SelectedIndex = 0;
        }

        private void LoadBruses()
        {
            Color min = Color.FromArgb(0xff, 0xFD, 0xB9, 0x34);
            Color max = Color.FromArgb(0xff, 0xDF, 0x51, 0x20);

            {
                var brushCollection = new BrushCollection();
                brushCollection.Add(new SolidColorBrush(min));
                brushCollection.Add(new SolidColorBrush(max));
                brushes.Add(brushCollection);
            }

            {
                var brushCollection = new BrushCollection();
                brushCollection.Add(Gradient(min,
                                             new RadialGradientBrush
                                                 {
                                                     GradientOrigin = new Point(0.5, 0.5),
                                                     Center = new Point(0.5, 0.5),
                                                     RadiusX = 0.5,
                                                     RadiusY = 0.5
                                                 }));
                brushCollection.Add(Gradient(max,
                                             new RadialGradientBrush
                                                 {
                                                     GradientOrigin = new Point(0.5, 0.5),
                                                     Center = new Point(0.5, 0.5),
                                                     RadiusX = 0.5,
                                                     RadiusY = 0.5
                                                 }));
                brushes.Add(brushCollection);
            }

            {
                var brushCollection = new BrushCollection();
                brushCollection.Add(Gradient(min,
                                             new LinearGradientBrush
                                                 {StartPoint = new Point(0, 0), EndPoint = new Point(1, 0)}));
                brushCollection.Add(Gradient(max,
                                             new LinearGradientBrush
                                                 {StartPoint = new Point(0, 0), EndPoint = new Point(1, 0)}));
                brushes.Add(brushCollection);
            }

            {
                var brushCollection = new BrushCollection();
                brushCollection.Add(Gradient(min,
                                             new LinearGradientBrush
                                                 {StartPoint = new Point(0, 0), EndPoint = new Point(0, 1)}));
                brushCollection.Add(Gradient(max,
                                             new LinearGradientBrush
                                                 {StartPoint = new Point(0, 0), EndPoint = new Point(0, 1)}));
                brushes.Add(brushCollection);
            }

            {
                var brushCollection = new BrushCollection();
                brushCollection.Add(Gradient(min,
                                             new LinearGradientBrush
                                                 {StartPoint = new Point(0, 0), EndPoint = new Point(0, 1)}));
                brushCollection.Add(Gradient(Colors.White,
                                             new LinearGradientBrush
                                                 {StartPoint = new Point(0, 0), EndPoint = new Point(0, 1)}));
                brushCollection.Add(Gradient(max,
                                             new LinearGradientBrush
                                                 {StartPoint = new Point(0, 0), EndPoint = new Point(0, 1)}));
                brushes.Add(brushCollection);
            }
        }


        private void lbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            int index = listBox != null ? listBox.SelectedIndex : -1;

            if (index != -1 && theMap.Layers[0] != null)
            {
                theMap.Layers[0].Brushes = brushes[index];
            }
        }


        private static GradientBrush Gradient(Color color, GradientBrush gradientBrush)
        {
            gradientBrush.GradientStops.Add(new GradientStop {Color = color.GetLightened(0.2), Offset = 0.0});
            gradientBrush.GradientStops.Add(new GradientStop {Color = color.GetLightened(-0.2), Offset = 1.0});

            return gradientBrush;
        }
    }
}