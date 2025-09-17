using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGSlider.Samples.Editing
{
    public partial class AddingAndRemovingThumbs : Infragistics.Samples.Framework.SampleContainer
    {
        public AddingAndRemovingThumbs()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
  
            
        }

        #region Event Handlers

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            InitializeThumbs();
        }

        private void InitializeThumbs()
        {
            if (this.slider.Thumbs.Count == 0)
            {
                LinearGradientBrush sourceBrush = (LinearGradientBrush)this.Resources["sourceLinearBrush"];
                foreach (GradientStop stop in sourceBrush.GradientStops)
                {
                    this.AddGradientStop(stop.Offset, stop.Color);
                }
            }
        }

        private void slider_TrackClick(object sender, TrackClickEventArgs<double> e)
        {
            this.AddGradientStop(e.Value, ColorPicker.SelectedColor);
        }

        private void ColorPicker_SelectedColorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.slider.ActiveThumb != null)
            {
                // Update the Thumb's Background
                this.slider.ActiveThumb.Background = new SolidColorBrush((Color)e.NewValue);

                // Get the Current Gradient Stop and Update Gradient Fill
                var currentStop = this.GetGradientStopAtOffset(this.slider.ActiveThumb.Value);
                if (currentStop != null)
                {
                    currentStop.Color = (Color)e.NewValue;
                }
            }
        }

        #endregion Event Handlers

        #region Private Methods

        private void AddGradientStop(double offset, Color color)
        {
            // Define New Thumb and Add to xamSlider's Thumbs Collection
            XamSliderNumericThumb thumb = new XamSliderNumericThumb();
            thumb.GotFocus += new RoutedEventHandler(thumb_GotFocus);

            // Set the Thumb's Background
            thumb.Background = new SolidColorBrush(color);

            // Set the Thumb Style
            thumb.Style = this.Resources["XamSliderNumericThumbStyle"] as System.Windows.Style;

            // Add to the Slider's Thumbs Collection
            this.slider.Thumbs.Add(thumb);

            // Set the Thumb to be active
            this.slider.ActiveThumb = thumb;

            // Define Gradient Stop and add to linearBrush's GradientStops Collection
            GradientStop gs = new GradientStop();
            gs.Color = color;
            gs.Offset = offset;
            this.linearBrush.GradientStops.Add(gs);

            // Define Offset Binding and Apply to Thumb
            Binding binding = new Binding() { Source = gs, Path = new PropertyPath("Offset"), Mode = BindingMode.TwoWay };
            thumb.SetBinding(XamSliderThumb<double>.ValueProperty, binding);
        }

        void thumb_GotFocus(object sender, RoutedEventArgs e)
        {
            XamSliderNumericThumb thumb = sender as XamSliderNumericThumb;
            if (thumb != null)
            {
                // Set the color of the selected thumb into the ColorPicker
                thumb.IsActive = true;
                ColorPicker.SelectedColor = (thumb.Background as SolidColorBrush).Color;
            }
        }

        private void RemoveSelectedThumb(object sender, RoutedEventArgs e)
        {
            if (this.slider.ActiveThumb != null)
            {
                // Find GradientStop Whose Offset == the ActiveThumb's Offset			
                int foundIndex = -1;
                for (int i = 0; i < this.linearBrush.GradientStops.Count; i++)
                {
                    if (Math.Round(this.linearBrush.GradientStops[i].Offset, 5) ==
                                Math.Round(this.slider.ActiveThumb.Value, 5))
                    {
                        foundIndex = i;
                    }
                }

                // Remove GradientStop if Found
                if (foundIndex != -1)
                {
                    this.linearBrush.GradientStops.RemoveAt(foundIndex);
                }

                // Remove the Thumb from the Slider's Thumbs Collection
                this.slider.Thumbs.Remove(this.slider.ActiveThumb);
            }
        }

        private GradientStop GetGradientStopAtOffset(double offset)
        {
            foreach (GradientStop stop in this.linearBrush.GradientStops)
            {
                if (stop.Offset == offset)
                    return stop;
            }

            return null;
        }

        #endregion Private Methods
    }
}
