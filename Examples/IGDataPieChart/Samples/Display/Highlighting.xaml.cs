using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
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

namespace IGDataPieChart.WPF.Samples.Display
{    
    public partial class Highlighting : SampleContainer
    {         
        public Highlighting()
        {
            InitializeComponent();            
            this.highlightingModeCombo.ItemsSource = Enum.GetValues(typeof(SeriesHighlightingMode));
            this.highlightingBehaviorCombo.ItemsSource = Enum.GetValues(typeof(SeriesHighlightingBehavior));
        }

        private void highlightingBehaviorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.dataPieChart.HighlightingBehavior = (SeriesHighlightingBehavior)e.AddedItems[0];
        }

        private void highlightingModeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.dataPieChart.HighlightingMode = (SeriesHighlightingMode)e.AddedItems[0];
        }
    }
}
