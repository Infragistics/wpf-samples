using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Infragistics.Samples.Shared.Controls
{
    /// <summary>
    /// Interaction logic for ucColorPicker.xaml
    /// </summary>
    public partial class ucColorPicker : UserControl
    {
        //public event ColorChangedHandler ColorChanged;
        //public delegate void ColorChangedHandler(object sender, Color e);
        #region Members

        // The number of columns we want to have in the color picker
        private const int GalleryColumnItems = 10;

        // The list of colors that will exist in the "Theme Colors" group.
        // These colors are arranged as they will appear in the color picker
        // (i.e. the first item in the third row (216,216,216) is the same color
        // that appears in the same position in the color picker)
        private Color[] ThemeColors = new Color[] 
        {
            Color.FromArgb(255,255,255,255),Color.FromArgb(255,000,000,000),Color.FromArgb(255,238,236,225),Color.FromArgb(255,031,073,125),Color.FromArgb(255,079,129,189),Color.FromArgb(255,192,080,077),Color.FromArgb(255,155,187,089),Color.FromArgb(255,128,100,162),Color.FromArgb(255,075,172,198),Color.FromArgb(255,247,150,070),
            Color.FromArgb(255,242,242,242),Color.FromArgb(255,127,127,127),Color.FromArgb(255,221,217,195),Color.FromArgb(255,198,217,240),Color.FromArgb(255,219,229,241),Color.FromArgb(255,242,220,219),Color.FromArgb(255,235,241,221),Color.FromArgb(255,229,224,236),Color.FromArgb(255,219,238,243),Color.FromArgb(255,253,234,218),
            Color.FromArgb(255,216,216,216),Color.FromArgb(255,089,089,089),Color.FromArgb(255,196,189,151),Color.FromArgb(255,141,179,226),Color.FromArgb(255,184,204,228),Color.FromArgb(255,229,185,183),Color.FromArgb(255,215,227,188),Color.FromArgb(255,204,193,217),Color.FromArgb(255,183,221,232),Color.FromArgb(255,251,213,181),
            Color.FromArgb(255,191,191,191),Color.FromArgb(255,063,063,063),Color.FromArgb(255,147,137,083),Color.FromArgb(255,084,141,212),Color.FromArgb(255,149,179,215),Color.FromArgb(255,217,150,148),Color.FromArgb(255,195,214,155),Color.FromArgb(255,178,162,199),Color.FromArgb(255,146,205,220),Color.FromArgb(255,250,192,143),
            Color.FromArgb(255,165,165,165),Color.FromArgb(255,038,038,038),Color.FromArgb(255,073,068,041),Color.FromArgb(255,023,054,093),Color.FromArgb(255,054,096,146),Color.FromArgb(255,149,055,052),Color.FromArgb(255,118,146,060),Color.FromArgb(255,095,073,122),Color.FromArgb(255,049,133,155),Color.FromArgb(255,227,108,009),
            Color.FromArgb(255,127,127,127),Color.FromArgb(255,012,012,012),Color.FromArgb(255,029,027,016),Color.FromArgb(255,015,036,062),Color.FromArgb(255,036,064,097),Color.FromArgb(255,099,036,035),Color.FromArgb(255,079,097,040),Color.FromArgb(255,063,049,081),Color.FromArgb(255,032,088,103),Color.FromArgb(255,151,072,006),

        };

        // The list of standard colors, whose length should be equal to GalleryColumnItems
        private Color[] StandardColors = new Color[] 
        {
            Color.FromArgb(255,192,000,000), Color.FromArgb(255,255,000,000), Color.FromArgb(255,255,192,000), Color.FromArgb(255,255,255,000), Color.FromArgb(255,146,208,080),
            Color.FromArgb(255,000,176,080), Color.FromArgb(255,000,176,240), Color.FromArgb(255,000,112,192), Color.FromArgb(255,000,032,096), Color.FromArgb(255,112,048,160),
        };

        #endregion //Members

        public Color SelectedColor
        {
            get
            {
                return (Color)GetValue(selectedColorProperty);
            }
            set
            {
                SetValue(selectedColorProperty, value);
                rectDestination.Fill = new SolidColorBrush(value);
            }
        }

        public event DependencyPropertyChangedEventHandler SelectedColorChanged;

        public static readonly DependencyProperty selectedColorProperty = DependencyProperty.Register(
            "SelectedColor", typeof(Color), typeof(ucColorPicker),
            new PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(selectedColor_Changed)));

        private static void selectedColor_Changed(DependencyObject depObj, DependencyPropertyChangedEventArgs dpcea)
        {
            ucColorPicker cp = (ucColorPicker)depObj;
            cp.OnSelectedColorChanged(dpcea);
        }

        protected virtual void OnSelectedColorChanged(DependencyPropertyChangedEventArgs dpcea)
        {
            if (SelectedColorChanged != null)
            {
                SelectedColorChanged(this, dpcea);
            }
        }

        public ucColorPicker()
        {
            InitializeComponent();

            for (int i = 0; i < ThemeColors.Length; i++)
            {
                Color tColor = ThemeColors[i];
                Rectangle themeItem = NewRectangle(tColor);
                LayoutRoot.Children.Add(themeItem);
                Grid.SetColumn(themeItem, i - (i / 10) * 10);
                Grid.SetRow(themeItem, i / 10 + 1);
            }
            for (int i = 0; i < StandardColors.Length; i++)
            {
                Color tColor = StandardColors[i];
                Rectangle themeItem = NewRectangle(tColor);
                LayoutRoot.Children.Add(themeItem);
                Grid.SetColumn(themeItem, i);
                Grid.SetRow(themeItem, 8);
            }
        }

        private Rectangle NewRectangle(Color tColor)
        {
            Rectangle themeItem = new Rectangle();
            themeItem.Width = 13;
            themeItem.Height = 13;
            themeItem.Margin = new Thickness(1);
            themeItem.Tag = tColor;
            themeItem.Fill = new SolidColorBrush(tColor);
            themeItem.MouseLeftButtonDown += new MouseButtonEventHandler(themeItem_MouseLeftButtonDown);
            return themeItem;
        }

        void themeItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle selectedRect = sender as Rectangle;
            if (selectedRect != null)
            {
                SelectedColor = (Color)selectedRect.Tag;
            }
        }
    }
}
