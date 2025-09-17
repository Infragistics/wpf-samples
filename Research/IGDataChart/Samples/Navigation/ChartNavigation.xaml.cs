using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Infragistics;
using Infragistics.Controls;
using Infragistics.Samples.Framework;

namespace IGDataChart.Samples.Navigation
{
    public partial class ChartNavigation : SampleContainer
    {
        public ChartNavigation()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.xmDataChart.WindowRectChanged += OnDataChartWindowRectChanged;
            this.sldVerticalZoom.ValueChanged += OnVerticalZoomValueChanged;
            this.sldHorizontalZoom.ValueChanged += OnHorizontalZoomValueChanged;

            xmDataChart.DragModifier = ModifierKeys.Shift;
        }

        private void OnDataChartWindowRectChanged(object sender, RectChangedEventArgs e)
        {
            _isUpdating = true;
            this.sldHorizontalZoom.Value = xmDataChart.WindowScaleHorizontal;
            this.sldVerticalZoom.Value = xmDataChart.WindowScaleVertical;
            _isUpdating = false;
        }

        private bool _isUpdating;

        private void OnPanButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (sender as Button);
            if (btn == null) return;
            string tag = btn.Tag.ToString();
            // #######################################################################################################
            if (tag.Equals("FitWindowButton"))
            {
                this.xmDataChart.WindowRect = new Rect(0, 0, 1, 1);
            }            
            // #######################################################################################################
            else if (tag.Equals("PanNorthButton"))
            {
                // pan up (north direction) by factor of 0.05
                this.xmDataChart.WindowPositionVertical -= 0.05;
            }
            else if (tag.Equals("PanSouthButton"))
            {
                // pan down (south direction) by factor of 0.05
                this.xmDataChart.WindowPositionVertical += 0.05;
            }
            else if (tag.Equals("PanWestButton"))
            {
                // pan left (west direction) by factor of 0.05
                this.xmDataChart.WindowPositionHorizontal -= 0.05;
            }
            else if (tag.Equals("PanEastButton"))
            {
                // pan right (east direction) by factor of 0.05
                this.xmDataChart.WindowPositionHorizontal += 0.05;
            }
            // #######################################################################################################
        }
        private void OnVerticalZoomValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating) return;
            double scale = e.NewValue;
            this.xmDataChart.WindowScaleVertical = scale;
        }
        private void OnHorizontalZoomValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating) return;
            double scale = e.NewValue;
            this.xmDataChart.WindowScaleHorizontal = scale;
        }

        private void OnDefaultInteractionSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmb = sender as ComboBox;
            if (cmb == null) return;
            var item = cmb.SelectedItem as ComboBoxItem;
            if (item == null) return;
            var tag = item.Tag.ToString();

            if (this.xmDataChart == null) return;
            this.xmDataChart.DefaultInteraction = (InteractionState)Enum.Parse(typeof(InteractionState), tag, true);
        }
        private void OnPanModifierSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb == null) return;
            ComboBoxItem item = cmb.SelectedItem as ComboBoxItem;
            if (item == null) return;
            string tag = item.Tag.ToString();

            if (this.xmDataChart == null) return;
            this.xmDataChart.PanModifier = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), tag, true);
        }
        private void OnDragModifierSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb == null) return;
            ComboBoxItem item = cmb.SelectedItem as ComboBoxItem;
            if (item == null) return;
            string tag = item.Tag.ToString();

            if (this.xmDataChart == null) return;
            this.xmDataChart.DragModifier = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), tag, true);
        }
    }


}
