using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Maps;

namespace IGMap.Samples.Display
{
    public partial class MapValueScales : Infragistics.Samples.Framework.SampleContainer
    {
        private readonly List<ValueScale> valueScales = new List<ValueScale>();

        public MapValueScales()
        {
            InitializeComponent();

            Loaded += ValueScales_Loaded;

            valueScales.Add(new LinearScale {IsAutoRange = true});
            valueScales.Add(new LogarithmicScale {IsAutoRange = true});
            valueScales.Add(new DistributionScale {ValueStopCount = 3, IsAutoRange = true});
            valueScales.Add(new DistributionScale {ValueStopCount = 4, IsAutoRange = true});
            valueScales.Add(new DistributionScale {ValueStopCount = 6, IsAutoRange = true});
            valueScales.Add(new DistributionScale {ValueStopCount = 11, IsAutoRange = true});
        }

        private void ValueScales_Loaded(object sender, RoutedEventArgs e)
        {
            lbScaleSettings.SelectedIndex = 0;
        }

        private void lbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            int index = listBox != null ? listBox.SelectedIndex : -1;

            if (index != -1 && theMap.Layers[0] != null)
            {
                theMap.Layers[0].ValueScale = valueScales[index];
            }
        }
    }
}