using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Infragistics.Samples.Shared.Controls
{
    public class BrushPaletteViewer : ItemsControl
    {
        public BrushPaletteViewer()
        {
           // TODO: load default styles in themes\generic.xaml
            string stylePath = "/Infragistics.Samples.Shared;component/Controls/BrushPaletteViewer.xaml";
            
            this.DefaultStyleKey = typeof(BrushPaletteViewer);
            
            // Add the default style to the control's resources
            ResourceDictionary style = new ResourceDictionary();
            style.Source = new System.Uri(stylePath, System.UriKind.Relative);
            this.Resources.MergedDictionaries.Add(style);
        }
    }
	public class BrushPaletteCollection : List<BrushPalette>
    {

    }
	public class BrushPalette : List<Brush>
    {

    }

    public class PaletteColorsViewer : ItemsControl // Control // ItemsControl
    {
        public PaletteColorsViewer()
        {
            //var palette = new PaletteColors();
            //palette.Add(Colors.White);
            //palette.Add(Colors.Red);

            //this.PaletteColors = palette;
             
            // TODO: load default styles in themes\generic.xaml
            string stylePath = "/Infragistics.Samples.Shared;component/Controls/BrushPaletteViewer.xaml";

            this.DefaultStyleKey = typeof(PaletteColorsViewer);

            // Add the default style to the control's resources
            ResourceDictionary style = new ResourceDictionary();
            style.Source = new System.Uri(stylePath, System.UriKind.Relative);
            this.Resources.MergedDictionaries.Add(style);
        }
        #region IsReversedPaletteBrush Dependency Property

        public const string IsReversedPaletteBrushPropertyName = "IsReversedPaletteBrush";

        /// <summary>
        /// Identifies the PaletteColors dependency property.
        /// </summary>
        public static readonly DependencyProperty IsReversedPaletteBrushProperty =
            DependencyProperty.Register(IsReversedPaletteBrushPropertyName, typeof(bool), typeof(PaletteColorsViewer),
            new PropertyMetadata(false, (sender, e) =>
            {
                (sender as PaletteColorsViewer).OnPropertyChanged(new PropertyChangedEventArgs(IsReversedPaletteBrushPropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public bool IsReversedPaletteBrush
        {
            get
            {
                return (bool)GetValue(IsReversedPaletteBrushProperty);
            }
            set
            {
                SetValue(IsReversedPaletteBrushProperty, value);
            }
        }
        #endregion
        #region PaletteColors Dependency Property

        public const string PaletteColorsPropertyName = "PaletteColors";

        /// <summary>
        /// Identifies the PaletteColors dependency property.
        /// </summary>
        public static readonly DependencyProperty PaletteColorsProperty =
            DependencyProperty.Register(PaletteColorsPropertyName, typeof(PaletteColors), typeof(PaletteColorsViewer),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as PaletteColorsViewer).OnPropertyChanged(new PropertyChangedEventArgs(PaletteColorsPropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public PaletteColors PaletteColors
        {
            get
            {
                return (PaletteColors)GetValue(PaletteColorsProperty);
            }
             set
            {
                SetValue(PaletteColorsProperty, value);
            }
        }
        #endregion
        #region PaletteBrush Dependency Property

        public const string PaletteBrushPropertyName = "PaletteBrush";

        /// <summary>
        /// Identifies the PaletteColors dependency property.
        /// </summary>
        public static readonly DependencyProperty PaletteBrushProperty =
            DependencyProperty.Register(PaletteBrushPropertyName, typeof(LinearGradientBrush), typeof(PaletteColorsViewer),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as PaletteColorsViewer).OnPropertyChanged(new PropertyChangedEventArgs(PaletteBrushPropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public LinearGradientBrush PaletteBrush
        {
            get
            {
                return (LinearGradientBrush)GetValue(PaletteBrushProperty);
            }
            private set
            {
                SetValue(PaletteBrushProperty, value);
            }
        }
        #endregion

        public void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case PaletteColorsPropertyName:
                    {
                        UpdatePaletteBrush();
                        break;
                    }
                case IsReversedPaletteBrushPropertyName:
                    {
                        UpdatePaletteBrush();
                        break;
                    }

            }
        }
  
        private void UpdatePaletteBrush()
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
                if(this.IsReversedPaletteBrush)
                {
                    brush.SpreadMethod = GradientSpreadMethod.Reflect;
                }
            }
           
            this.PaletteBrush = brush;
         
        }
    }
    public class PaletteColorsCollection : ObservableCollection<PaletteColors>
    {

    }
    public class PaletteColors : ObservableCollection<Color>
    {

    }
 
}