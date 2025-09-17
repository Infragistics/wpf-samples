using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Infragistics.Samples.Controls
{
    public partial class ColorsPaletteViewer : UserControl
    {
        public ColorsPaletteViewer()
        {
            InitializeComponent();
        }

        #region IsPaletteReversed  

        public const string IsPaletteReversedPropertyName = "IsPaletteReversed";

        /// <summary>
        /// Identifies the PaletteColors dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPaletteReversedProperty =
            DependencyProperty.Register(IsPaletteReversedPropertyName, typeof(bool), typeof(ColorsPaletteViewer),
            new PropertyMetadata(true, (sender, e) =>
            {
                (sender as ColorsPaletteViewer).OnPropertyChanged(new PropertyChangedEventArgs(IsPaletteReversedPropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public bool IsPaletteReversed
        {
            get
            {
                return (bool)GetValue(IsPaletteReversedProperty);
            }
            set
            {
                SetValue(IsPaletteReversedProperty, value);
            }
        }
        #endregion
        #region PaletteColors  

        public const string PaletteColorsPropertyName = "PaletteColors";

        /// <summary>
        /// Identifies the PaletteColors dependency property.
        /// </summary>
        public static readonly DependencyProperty PaletteColorsProperty =
            DependencyProperty.Register(PaletteColorsPropertyName, typeof(IList<Color>), typeof(ColorsPaletteViewer),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as ColorsPaletteViewer).OnPropertyChanged(new PropertyChangedEventArgs(PaletteColorsPropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public IList<Color> PaletteColors
        {
            get
            {
                return (IList<Color>)GetValue(PaletteColorsProperty);
            }
            set
            {
                SetValue(PaletteColorsProperty, value);
            }
        }
        #endregion
        #region PaletteFill 

        public const string PaletteFillPropertyName = "PaletteFill";

        /// <summary>
        /// Identifies the PaletteColors dependency property.
        /// </summary>
        public static readonly DependencyProperty PaletteFillProperty =
            DependencyProperty.Register(PaletteFillPropertyName, typeof(LinearGradientBrush), typeof(ColorsPaletteViewer),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as ColorsPaletteViewer).OnPropertyChanged(new PropertyChangedEventArgs(PaletteFillPropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public LinearGradientBrush PaletteFill
        {
            get
            {
                return (LinearGradientBrush)GetValue(PaletteFillProperty);
            }
            private set
            {
                SetValue(PaletteFillProperty, value);
            }
        }
        #endregion

        public void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case PaletteColorsPropertyName:
                    {
                        UpdatePaletteFill();
                        break;
                    }
                case IsPaletteReversedPropertyName:
                    {
                        UpdatePaletteFill();
                        break;
                    }
            }
        }

        private void UpdatePaletteFill()
        {
            var brush = new LinearGradientBrush();
            if (this.PaletteColors == null || this.PaletteColors.Count == 0)
            {
                brush.GradientStops.Add(new GradientStop { Color = Colors.White, Offset = 0 });
                brush.GradientStops.Add(new GradientStop { Color = Colors.DarkGray, Offset = 1 });
            }
            else
            {
                double offsetChange = 1.0 / this.PaletteColors.Count;
                double offset = 0;
                foreach (Color color in this.PaletteColors)
                {
                    brush.GradientStops.Add(new GradientStop { Color = color, Offset = offset });
                    offset += offsetChange;
                }
            }

            if (this.IsPaletteReversed)
            {
                brush.SpreadMethod = GradientSpreadMethod.Reflect;
            }
            this.PaletteFill = brush;
        }

    }

    public class ColorsPaletteCollection : List<ColorsPalette>
    {

    }
    public class ColorsPalette : ObservableCollection<Color>
    {

    }

}
