using IGSurfaceChart.Samples.Models;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using ColorCollection = Infragistics.Controls.Charts.ColorCollection; 

namespace IGSurfaceChart.Samples.ViewModels
{
    public class ColorPalettesViewModel : ObservableModel
    {
        public List<DataPoint3D> DataCollection { get; set; }
        public List<ColorCollection> ColorCollectionList { get; set; }
        public Array SeriesColorInterpolationOptions { get; set; }

        private ColorCollection _seriesColorsPalette;
        public ColorCollection SeriesColorsPalette
        {
            get { return _seriesColorsPalette; }
            set
            {
                _seriesColorsPalette = value;
                OnPropertyChanged("SeriesColorsPalette");
            }
        }

        private SeriesColorPaletteInterpolation _seriesColorInterpolationOption = SeriesColorPaletteInterpolation.ARGB;
        public SeriesColorPaletteInterpolation SeriesColorInterpolationOption
        {
            get { return _seriesColorInterpolationOption; }
            set
            {
                _seriesColorInterpolationOption = value;
                OnPropertyChanged("SeriesColorInterpolationOption");
            }
        }

        public ColorPalettesViewModel()
        {
            DataCollection = Data.Generate_XSqPlusYSq();
            ColorCollectionList = GenerateColorPalletesList();
            SeriesColorInterpolationOptions = Enum.GetValues(typeof(SeriesColorPaletteInterpolation));
        }

        private List<ColorCollection> GenerateColorPalletesList()
        {
            var palettes = new List<ColorCollection>();

            var palette1 = new ColorCollection()
            {
                Color.FromRgb(198, 40, 40),
                Color.FromRgb(229, 57, 53),
                Color.FromRgb(255, 87, 34),
                Color.FromRgb(251, 140, 0),
                Color.FromRgb(124, 179, 66),
                Color.FromRgb(56, 142, 60),
            };

            var palette2 = new ColorCollection()
            {
                Color.FromRgb(0, 71, 102),
                Color.FromRgb(0, 136, 141),
                Color.FromRgb(7, 152, 131),
                Color.FromRgb(68, 172, 86),
                Color.FromRgb(125, 190, 75),
                Color.FromRgb(203, 219, 58),
            }; 

            var palette3 = new ColorCollection()
            {
                Color.FromRgb(40, 32, 73),
                Color.FromRgb(66, 31, 101),
                Color.FromRgb(91, 27, 127),
                Color.FromRgb(120, 22, 120),
                Color.FromRgb(165, 23, 85),
                Color.FromRgb(208, 25, 55),
            };

            var palette4 = new ColorCollection()
            {
                Color.FromRgb(198, 40, 40),
                Color.FromRgb(216, 67, 21),
                Color.FromRgb(245, 124, 0),
                Color.FromRgb(255, 179, 0),
                Color.FromRgb(255, 235, 59),
                Color.FromRgb(212, 225, 87),
            };

            var palette5 = new ColorCollection()
            {
                Color.FromRgb(19, 74, 129),
                Color.FromRgb(117, 53, 159),
                Color.FromRgb(168, 37, 99),
                Color.FromRgb(216, 39, 53),
                Color.FromRgb(248, 91, 48),
                Color.FromRgb(255, 152, 29),
            };

            palettes.Add(palette1);
            palettes.Add(palette2);
            palettes.Add(palette3);
            palettes.Add(palette4);
            palettes.Add(palette5);

            return palettes;

        }
    }
}
