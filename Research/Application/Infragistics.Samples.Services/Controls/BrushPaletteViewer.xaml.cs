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
    public partial class BrushPaletteViewer : UserControl
    {
        public BrushPaletteViewer()
        {
            InitializeComponent();
        }

        #region IsPaletteReversed  

        public const string IsPaletteReversedPropertyName = "IsPaletteReversed";

        /// <summary>
        /// Identifies the PaletteBrushes dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPaletteReversedProperty =
            DependencyProperty.Register(IsPaletteReversedPropertyName, typeof(bool), typeof(BrushPaletteViewer),
            new PropertyMetadata(true, (sender, e) =>
            {
                (sender as BrushPaletteViewer).OnPropertyChanged(new PropertyChangedEventArgs(IsPaletteReversedPropertyName));
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
        #region PaletteBrushes  

        public const string PaletteBrushesPropertyName = "PaletteBrushes";

        /// <summary>
        /// Identifies the PaletteBrushes dependency property.
        /// </summary>
        public static readonly DependencyProperty PaletteBrushesProperty =
            DependencyProperty.Register(PaletteBrushesPropertyName, 
                typeof(IList<Brush>), typeof(BrushPaletteViewer),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as BrushPaletteViewer).OnPropertyChanged(new PropertyChangedEventArgs(PaletteBrushesPropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public IList<Brush> PaletteBrushes
        {
            get
            {
                return (IList<Brush>)GetValue(PaletteBrushesProperty);
            }
            set
            {
                SetValue(PaletteBrushesProperty, value);
            }
        }
        #endregion
        #region PaletteFill 

        public const string PaletteFillPropertyName = "PaletteFill";

        /// <summary>
        /// Identifies the PaletteBrushes dependency property.
        /// </summary>
        public static readonly DependencyProperty PaletteFillProperty =
            DependencyProperty.Register(PaletteFillPropertyName, typeof(LinearGradientBrush), typeof(BrushPaletteViewer),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as BrushPaletteViewer).OnPropertyChanged(new PropertyChangedEventArgs(PaletteFillPropertyName));
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
                case PaletteBrushesPropertyName:
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
            var fill = new LinearGradientBrush();

            if (this.PaletteBrushes == null || this.PaletteBrushes.Count == 0)
            {
                fill.GradientStops.Add(new GradientStop { Color = Colors.White, Offset = 0 });
                fill.GradientStops.Add(new GradientStop { Color = Colors.DarkGray, Offset = 1 });
            }
            else
            {
                double offsetChange = 1.0 / this.PaletteBrushes.Count;
                double offset = 0;
                foreach (Brush brush in this.PaletteBrushes)
                {
                    if (brush is SolidColorBrush)
                    {
                        var color = ((SolidColorBrush)brush).Color;
                        fill.GradientStops.Add(new GradientStop { Color = color, Offset = offset });
                    }
                    offset += offsetChange;
                }
            }

            if (this.IsPaletteReversed)
            {
                fill.SpreadMethod = GradientSpreadMethod.Reflect;
            }
            this.PaletteFill = fill;
        }

    }

    public class BrushCollections : List<BrushCollection>
    {

    }
    public class BrushPaletteCollection : List<BrushPalette>
    {

    }
    public class BrushPalette : List<Brush>
    {

    }

}
