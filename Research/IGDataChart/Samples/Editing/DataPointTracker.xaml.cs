using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Editing
{
    public partial class DataPointTracker : Infragistics.Samples.Framework.SampleContainer
    {
        public DataPointTracker()
        {
            InitializeComponent();
            this.Loaded += DataPointTracker_Loaded;
            
        }

        void DataPointTracker_Loaded(object sender, RoutedEventArgs e)
        {
            this.OptionsPanel.MouseEnter += OptionsPanel_MouseEnter;
            this.OptionsPanel.MouseLeave += OptionsPanel_MouseLeave;
        }

       
        private void TransitionDurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataChart == null) return;
            var trackingLayer = this.DataChart.Series.OfType<CategoryItemHighlightLayer>().FirstOrDefault();
            if (trackingLayer != null) trackingLayer.TransitionDuration = TimeSpan.FromMilliseconds(e.NewValue);

            var toolTipLayer = this.DataChart.Series.OfType<ItemToolTipLayer>().FirstOrDefault();
            if (toolTipLayer != null) toolTipLayer.TransitionDuration = TimeSpan.FromMilliseconds(e.NewValue);

            var crosshairLayer = this.DataChart.Series.OfType<CrosshairLayer>().FirstOrDefault();
            if (crosshairLayer != null) crosshairLayer.TransitionDuration = TimeSpan.FromMilliseconds(e.NewValue);
        }
        private void UseToolTipsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataChart == null) return;
            if (this.UseToolTipsCheckBox.IsChecked.HasValue)
            {
                var visibility = this.UseToolTipsCheckBox.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                var toolTipLayer = this.DataChart.Series.OfType<ItemToolTipLayer>().FirstOrDefault();
                ToggleLayerVisibility(toolTipLayer, visibility);
            }
        }
        private void UseInterpolationCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataChart == null) return;
            if (this.UseInterpolationCheckBox.IsChecked.HasValue)
            {
                var isChecked = this.UseInterpolationCheckBox.IsChecked.Value;
                var toolTipLayer = this.DataChart.Series.OfType<ItemToolTipLayer>().FirstOrDefault();
                if (toolTipLayer != null) toolTipLayer.UseInterpolation = isChecked;

                var trackingLayer = this.DataChart.Series.OfType<CategoryItemHighlightLayer>().FirstOrDefault();
                if (trackingLayer != null) trackingLayer.UseInterpolation = isChecked;

                var crosshairLayer = this.DataChart.Series.OfType<CrosshairLayer>().FirstOrDefault();
                if (crosshairLayer != null) crosshairLayer.UseInterpolation = isChecked;
            }
        }
       
        private void UseCrosshairCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataChart == null) return;
            if (this.UseCrosshairCheckBox.IsChecked.HasValue)
            {
                var crosshairLayer = this.DataChart.Series.OfType<CrosshairLayer>().FirstOrDefault();
                CrosshairVisibility = this.UseCrosshairCheckBox.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                ToggleLayerVisibility(crosshairLayer, CrosshairVisibility);
            }  
        }

        protected Visibility CrosshairVisibility = Visibility.Visible;

        void OptionsPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var layer = this.DataChart.Series.OfType<CrosshairLayer>().FirstOrDefault();
            if (layer != null)
            {
                CrosshairVisibility = layer.Visibility;
                layer.Visibility = Visibility.Collapsed;
            }
        }
        void OptionsPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var layer = this.DataChart.Series.OfType<CrosshairLayer>().FirstOrDefault();
            ToggleLayerVisibility(layer, CrosshairVisibility);
        }

       
        private void ToggleLayerVisibility(UIElement layer, Visibility visibility)
        {
            if (layer != null)
            {
                layer.Visibility = visibility;
            }
        }
       


    }
}
