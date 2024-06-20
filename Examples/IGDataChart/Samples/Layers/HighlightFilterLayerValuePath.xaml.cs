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

namespace IGDataChart.Samples.Layers
{
    /// <summary>
    /// Interaction logic for HighlightFilterLayerValuePath.xaml
    /// </summary>
    public partial class HighlightFilterLayerValuePath : SampleContainer
    {
        public OlympicDataWithTotals Data { get; set; }
        public HighlightFilterLayerValuePath()
        {
            InitializeComponent();
            this.Data = new OlympicDataWithTotals();
            this.DataContext = this;
        }        

        private void displayModeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = e.AddedItems[0] as ComboBoxItem;
            
            if(item != null)
            {
                string str = item.Content.ToString();

                if(str == "Hidden")
                {
                    chart.HighlightedValuesDisplayMode = SeriesHighlightedValuesDisplayMode.Hidden;
                }
                else
                {
                    chart.HighlightedValuesDisplayMode = SeriesHighlightedValuesDisplayMode.Overlay;
                }
            }
        }

        private void valuePathCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = e.AddedItems[0] as ComboBoxItem;
            if(item != null)
            {
                string valuePath = item.Content.ToString();
                ((ColumnSeries)chart.Series[0]).HighlightedValueMemberPath = valuePath;
            }
        }
    }

    public class OlympicDataWithTotalsItem
    {
        public string Year { get; set; }
        public int America { get; set; }
        public int China { get; set; }
        public int Russia { get; set; }
        public int Total { get; set; }
    }

    public class OlympicDataWithTotals : List<OlympicDataWithTotalsItem>
    {
        public OlympicDataWithTotals()
        {
            this.Add(new OlympicDataWithTotalsItem() { Year = "1996", America = 148, China = 110, Russia = 95, Total = 353 });
            this.Add(new OlympicDataWithTotalsItem() { Year = "2000", America = 142, China = 115, Russia = 91, Total = 348 });
            this.Add(new OlympicDataWithTotalsItem() { Year = "2004", America = 134, China = 121, Russia = 86, Total = 341 });
            this.Add(new OlympicDataWithTotalsItem() { Year = "2008", America = 131, China = 129, Russia = 65, Total = 325 });
            this.Add(new OlympicDataWithTotalsItem() { Year = "2012", America = 135, China = 115, Russia = 77, Total = 327 });
            this.Add(new OlympicDataWithTotalsItem() { Year = "2016", America = 146, China = 112, Russia = 88, Total = 346 });
        }
    }
}
