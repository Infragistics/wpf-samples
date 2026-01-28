using System.Windows;
using System.Windows.Controls;
using Infragistics;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Display.Scales
{
    public partial class BubbleBrushScale : Infragistics.Samples.Framework.SampleContainer
    {
        public BubbleBrushScale()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        #region Event Handlers

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.BrushScaleSelectionComboBox.SelectionChanged += OnBrushScaleSelectionComboBoxChanged;
            this.BrushScaleSelectionComboBox.SelectedIndex = 0;

            // controls for ValueBrushScale
            this.ValueBrushPaletteSelectionComboBox.SelectionChanged += OnValueBrushPaletteSelectionComboBoxChanged;
            this.ValueBrushPaletteSelectionComboBox.SelectedIndex = 0;

            this.ValueBrushScaleMinimumSlider.ValueChanged += OnValueBrushScaleMinimumSliderChanged;
            this.ValueBrushScaleMaximumSlider.ValueChanged += OnValueBrushScaleMaximumSliderChanged;
            this.ValueBrushScaleLogCheckBox.Click += OnValueBrushScaleLogCheckBoxClick;
            
            //controls for CustomPaletteBrushScale
            this.CustomBrushScaleModeComboBox.SelectionChanged += OnCustomBrushScaleModeComboBoxChanged;
            this.CustomBrushScaleModeComboBox.SelectedIndex = 0;

            this.CustomBrushPaletteSelectionComboBox.SelectionChanged += OnCustomBrushPaletteSelectionComboBoxChanged;
            this.CustomBrushPaletteSelectionComboBox.SelectedIndex = 0;
        }
        private void OnBrushScaleSelectionComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmb = sender as ComboBox;
            if (cmb != null)
            {
                if (this.DataChart == null) return;
                var tag = ((ComboBoxItem)cmb.SelectedItem).Tag as string;
                var isValueBrushScale = tag.Equals("ValueBrushScale");
                this.DataChart.Series[0].Visibility = isValueBrushScale ? Visibility.Visible : Visibility.Collapsed;
                this.DataChart.Series[1].Visibility = isValueBrushScale ? Visibility.Collapsed : Visibility.Visible;
                this.ValueBrushScalePanel.Visibility = isValueBrushScale ? Visibility.Visible : Visibility.Collapsed;
                this.CustomBrushScalePanel.Visibility = isValueBrushScale ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        private void OnCustomBrushPaletteSelectionComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmb = sender as ComboBox;
            if (cmb != null)
            {
                if (this.DataChart == null) return;
                var brushes = ((BrushCollection)cmb.SelectedItem);
                var series = (BubbleSeries)this.DataChart.Series[1];
                ((CustomPaletteBrushScale)series.FillScale).Brushes = brushes;
            }
        }
        private void OnCustomBrushScaleModeComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmb = sender as ComboBox;
            if (cmb != null)
            {
                if (this.DataChart == null) return;
                var tag = ((ComboBoxItem)cmb.SelectedItem).Tag as string;
                var mode = (BrushSelectionMode)System.Enum.Parse(typeof(BrushSelectionMode), tag, true);
                var series = (BubbleSeries)this.DataChart.Series[1];
                ((CustomPaletteBrushScale)series.FillScale).BrushSelectionMode = mode;
            }
        }
        private void OnValueBrushPaletteSelectionComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmb = sender as ComboBox;
            if (cmb != null)
            {
                if (this.DataChart == null) return;
                var brushes = ((BrushCollection)cmb.SelectedItem);
                var series = (BubbleSeries)this.DataChart.Series[0];
                ((ValueBrushScale)series.FillScale).Brushes = brushes;
            }
        }
        private void OnValueBrushScaleMinimumSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataChart == null) return;
            var series = (BubbleSeries)this.DataChart.Series[0];
            ((ValueBrushScale)series.FillScale).MinimumValue = e.NewValue;
        }
        private void OnValueBrushScaleMaximumSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataChart == null) return;
            var series = (BubbleSeries)this.DataChart.Series[0];
            ((ValueBrushScale)series.FillScale).MaximumValue = e.NewValue;
        }
        private void OnValueBrushScaleLogCheckBoxClick(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            if (chk != null)
            {
                if (chk.IsChecked != null)
                {
                    var isLogarithmic = chk.IsChecked.Value;
                    if (this.DataChart == null) return;
                    var series = (BubbleSeries)this.DataChart.Series[0];
                    ((ValueBrushScale)series.FillScale).IsLogarithmic = isLogarithmic;
                }
            }
        }
        #endregion
    }


}
