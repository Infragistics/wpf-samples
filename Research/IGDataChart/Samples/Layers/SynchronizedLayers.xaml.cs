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
    public partial class SynchronizedLayers : Infragistics.Samples.Framework.SampleContainer
    {
        SolidColorBrush columnColor = new SolidColorBrush(Color.FromArgb(255, 58, 179, 231));

        public SynchronizedLayers()
        {
            InitializeComponent();
            _column = DataChart.Series.OfType<ColumnSeries>().FirstOrDefault();
            _column2 = DataChart2.Series.OfType<ColumnSeries>().FirstOrDefault();
        }

        private void durationSlider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DataChart == null)
            {
                return;
            }
            var layers = DataChart.Series.OfType<AnnotationLayer>();
            foreach (var layer in layers)
            {
                if (layer != null)
                {
                    layer.TransitionDuration = TimeSpan.FromMilliseconds(e.NewValue);
                }
            }

            layers = DataChart2.Series.OfType<AnnotationLayer>();
            foreach (var layer in layers)
            {
                if (layer != null)
                {
                    layer.TransitionDuration = TimeSpan.FromMilliseconds(e.NewValue);
                }
            }
        }

        private Infragistics.Controls.Charts.Series _column = null;
        private Infragistics.Controls.Charts.Series _column2 = null;

        private void columnPresent_Checked_1(object sender, RoutedEventArgs e)
        {
            if (_column != null && DataChart != null && !DataChart.Series.Contains(_column))
            {
                _column.Brush = columnColor;
                DataChart.Series.Insert(0, _column);
            }
            if (_column2 != null && DataChart2 != null && !DataChart2.Series.Contains(_column2))
            {
                _column2.Brush = columnColor;
                DataChart2.Series.Insert(0, _column2);
            }
        }

        private void columnPresent_Unchecked_1(object sender, RoutedEventArgs e)
        {
            if (_column != null && DataChart != null && DataChart.Series.Contains(_column))
            {
                DataChart.Series.Remove(_column);
            }
            if (_column2 != null && DataChart2 != null && DataChart2.Series.Contains(_column2))
            {
                DataChart2.Series.Remove(_column2);
            }
        }

    }

    
   
}
