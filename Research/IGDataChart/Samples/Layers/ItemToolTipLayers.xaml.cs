using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Controls.Charts;
using System.Windows.Media;

namespace IGDataChart.Samples.Layers
{
    public partial class ItemToolTipLayers : Infragistics.Samples.Framework.SampleContainer
    {
        SolidColorBrush columnColor = new SolidColorBrush(Color.FromArgb(255, 58, 179, 231));
        public ItemToolTipLayers()
        {
            InitializeComponent();
            _column = DataChart.Series.OfType<ColumnSeries>().FirstOrDefault();
        }

        private void durationSlider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DataChart == null)
            {
                return;
            }
            var layer = DataChart.Series.OfType<ItemToolTipLayer>().FirstOrDefault();
            if (layer != null)
            {
                layer.TransitionDuration = TimeSpan.FromMilliseconds(e.NewValue);
            }
        }

        private Infragistics.Controls.Charts.Series _column = null;

        private void columnPresent_Checked_1(object sender, RoutedEventArgs e)
        {
            if (_column != null && DataChart != null && !DataChart.Series.Contains(_column))
            {
                _column.Brush = columnColor;                
                DataChart.Series.Insert(0, _column);
            }
        }

        private void columnPresent_Unchecked_1(object sender, RoutedEventArgs e)
        {
            if (_column != null && DataChart != null && DataChart.Series.Contains(_column))
            {
                DataChart.Series.Remove(_column);
            }
        }

        private void targetSeries_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (targetSeries == null)
            {
                return;
            }
            var selectedItem = (string)targetSeries.SelectedItem;
            var tooltipLayer = DataChart.Series.OfType<ItemToolTipLayer>().FirstOrDefault();
            if (tooltipLayer == null)
            {
                return;
            }
            switch (selectedItem)
            {
                case "All":
                    tooltipLayer.TargetSeries = null;
                    break;
                case "Column":
                    tooltipLayer.TargetSeries = _column;
                    break;
                case "Line":
                    tooltipLayer.TargetSeries = DataChart.Series.OfType<LineSeries>().FirstOrDefault();
                    break;
            }
        }

    }
}