using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Controls.Charts;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace IGDataChart.Samples.Layers
{
    public partial class CategoryToolTipLayers : Infragistics.Samples.Framework.SampleContainer
    {

        SolidColorBrush columnColor = new SolidColorBrush(Color.FromArgb(255, 58, 179, 231));

        public CategoryToolTipLayers()
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
            var layer = DataChart.Series.OfType<CategoryToolTipLayer>().FirstOrDefault();
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

        private void toolTipPosition_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (toolTipPosition == null)
            {
                return;
            }
            var selectedItem = (string)toolTipPosition.SelectedItem;
            var layer = DataChart.Series.OfType<CategoryToolTipLayer>().FirstOrDefault();
            if (layer == null)
            {
                return;
            }
            switch (selectedItem)
            {
                case "Auto":
                    layer.ToolTipPosition = CategoryTooltipLayerPosition.Auto;
                    break;
                case "InsideStart":
                    layer.ToolTipPosition = CategoryTooltipLayerPosition.InsideStart;
                    break;
                case "InsideEnd":
                    layer.ToolTipPosition = CategoryTooltipLayerPosition.InsideEnd;
                    break;
                case "OutsideStart":
                    layer.ToolTipPosition = CategoryTooltipLayerPosition.OutsideStart;
                    break;
                case "OutsideEnd":
                    layer.ToolTipPosition = CategoryTooltipLayerPosition.OutsideEnd;
                    break;
            }
        }


    }

    public class ToolTipPositions
        : List<string>
    {
        public ToolTipPositions()
        {
            Add("Auto");
            Add("InsideStart");
            Add("InsideEnd");
            Add("OutsideEnd");
            Add("OutsideStart");
        }
    }
}
