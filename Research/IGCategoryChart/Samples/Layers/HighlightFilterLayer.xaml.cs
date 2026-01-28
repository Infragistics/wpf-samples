using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGCategoryChart.Samples.Layers
{
    /// <summary>
    /// Interaction logic for HighlightFilterLayer.xaml
    /// </summary>
    public partial class HighlightFilterLayer : SampleContainer
    {
        public HighlightFilterLayer()
        {
            InitializeComponent();
        }

        private void displayModeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = e.AddedItems[0] as ComboBoxItem;

            if (item != null)
            {
                string str = item.Content.ToString();

                if (str == "Hidden")
                {
                    chart.HighlightedValuesDisplayMode = SeriesHighlightedValuesDisplayMode.Hidden;
                }
                else
                {
                    chart.HighlightedValuesDisplayMode = SeriesHighlightedValuesDisplayMode.Overlay;
                }
            }
        }

        private void filteredCountryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = e.AddedItems[0] as ComboBoxItem;

            if (item != null)
            {
                string str = item.Content.ToString();
                chart.InitialHighlightFilter = "Country eq '" + str + "'";
            }
        }
    }
}
