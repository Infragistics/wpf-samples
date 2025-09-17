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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGDataChart.Samples.Layers
{
    /// <summary>
    /// Interaction logic for HighlightFilterLayerMultipleSeries.xaml
    /// </summary>
    public partial class HighlightFilterLayerMultipleSeries : SampleContainer
    {
        public RenewableEnergyDataFull FullData { get; set; }
        public RenewableEnergyDataFiltered FilteredData { get; set; }
        public HighlightFilterLayerMultipleSeries()
        {
            InitializeComponent();

            this.FullData = new RenewableEnergyDataFull();
            this.FilteredData = new RenewableEnergyDataFiltered();

            this.DataContext = this;
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
    }

    public class RenewableEnergyItem
    {
        public string Year { get; set; }        
        public int America { get; set; }
        public int China { get; set; }
        public int Europe { get; set; }
    }

    public class RenewableEnergyDataFull : List<RenewableEnergyItem>
    {
        public RenewableEnergyDataFull()
        {
            this.Add(new RenewableEnergyItem() { Year = "2009", Europe = 34, China = 21, America = 19 });
            this.Add(new RenewableEnergyItem() { Year = "2010", Europe = 43, China = 26, America = 24 });
            this.Add(new RenewableEnergyItem() { Year = "2011", Europe = 66, China = 29, America = 28 });
            this.Add(new RenewableEnergyItem() { Year = "2012", Europe = 69, China = 32, America = 26 });
            this.Add(new RenewableEnergyItem() { Year = "2013", Europe = 58, China = 47, America = 38 });
            this.Add(new RenewableEnergyItem() { Year = "2014", Europe = 40, China = 46, America = 31 });
            this.Add(new RenewableEnergyItem() { Year = "2015", Europe = 78, China = 50, America = 19 });
            this.Add(new RenewableEnergyItem() { Year = "2016", Europe = 13, China = 90, America = 52 });
            this.Add(new RenewableEnergyItem() { Year = "2017", Europe = 78, China = 132, America = 50 });
            this.Add(new RenewableEnergyItem() { Year = "2018", Europe = 40, China = 134, America = 34 });
            this.Add(new RenewableEnergyItem() { Year = "2018", Europe = 40, China = 134, America = 34 });
            this.Add(new RenewableEnergyItem() { Year = "2019", Europe = 80, China = 96, America = 38 });
        }
    }

    public class RenewableEnergyDataFiltered : List<RenewableEnergyItem>
    {
        public RenewableEnergyDataFiltered()
        {
            this.Add(new RenewableEnergyItem() { Year = "2009", Europe = 26, China = 14, America = 12 });
            this.Add(new RenewableEnergyItem() { Year = "2010", Europe = 29, China = 17, America = 18 });
            this.Add(new RenewableEnergyItem() { Year = "2011", Europe = 50, China = 21, America = 22 });
            this.Add(new RenewableEnergyItem() { Year = "2012", Europe = 48, China = 20, America = 20 });
            this.Add(new RenewableEnergyItem() { Year = "2013", Europe = 37, China = 23, America = 26 });
            this.Add(new RenewableEnergyItem() { Year = "2014", Europe = 26, China = 34, America = 21 });
            this.Add(new RenewableEnergyItem() { Year = "2015", Europe = 55, China = 38, America = 12 });
            this.Add(new RenewableEnergyItem() { Year = "2016", Europe = 7, China = 55, America = 38 });
            this.Add(new RenewableEnergyItem() { Year = "2017", Europe = 57, China = 101, America = 32 });
            this.Add(new RenewableEnergyItem() { Year = "2018", Europe = 23, China = 112, America = 19 });
            this.Add(new RenewableEnergyItem() { Year = "2018", Europe = 18, China = 118, America = 22 });
            this.Add(new RenewableEnergyItem() { Year = "2019", Europe = 40, China = 70, America = 26 });
        }
    }
}
