using Infragistics.Controls.Charts;
using System;
using System.Windows;
using System.Windows.Controls;

namespace IGDataChart.Samples.Display.Axes
{
    public partial class RadialAxes : Infragistics.Samples.Framework.SampleContainer
    {
        public RadialAxes()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private AxisAngleLabelMode[] labelModeValues = (AxisAngleLabelMode[])Enum.GetValues(typeof(AxisAngleLabelMode));

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // add event handlers for axis controls
            this.angleAxisInvertion.Click += OnAngleAxisInvertionChecked;
            this.angleAxisVisibility.Click += OnAngleAxisVisibilityChecked;

            this.radiusAxisInvertion.Click += OnRadiusAxisInvertionChecked;
            this.radiusAxisVisibility.Click += OnRadiusAxisVisibilityChecked;

            labelModeValues = (AxisAngleLabelMode[])Enum.GetValues(typeof(AxisAngleLabelMode));
            this.LabelModeComboBox.ItemsSource = labelModeValues;
            this.LabelModeComboBox.SelectionChanged += LabelModeComboBox_SelectionChanged; ;
            this.LabelModeComboBox.SelectedIndex = 2;
        }

        private void LabelModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.angleAxis.LabelMode = labelModeValues[LabelModeComboBox.SelectedIndex];
        }

        private void OnRadiusAxisInvertionChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cbk = (sender as CheckBox);
            if (cbk == null) return;
            if (cbk.IsChecked == null) return;
            this.DataChart.Axes["radiusAxis"].IsInverted = cbk.IsChecked.Value;
        }
        private void OnRadiusAxisVisibilityChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cbk = (sender as CheckBox);
            if (cbk == null) return;
            if (cbk.IsChecked == null) return;
            this.DataChart.Axes["radiusAxis"].Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
            this.DataChart.Axes["radiusAxis"].LabelSettings.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
        }
        private void OnAngleAxisInvertionChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cbk = (sender as CheckBox);
            if (cbk == null) return;
            if (cbk.IsChecked == null) return;
            this.DataChart.Axes["angleAxis"].IsInverted = cbk.IsChecked.Value;
        }
        private void OnAngleAxisVisibilityChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cbk = (sender as CheckBox);
            if (cbk == null) return;
            if (cbk.IsChecked == null) return;
            this.DataChart.Axes["angleAxis"].Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
            this.DataChart.Axes["angleAxis"].LabelSettings.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
        }

        private void angleAxisLabelExtent_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.angleAxis.LabelSettings.Extent = e.NewValue;
        }
    }
}
