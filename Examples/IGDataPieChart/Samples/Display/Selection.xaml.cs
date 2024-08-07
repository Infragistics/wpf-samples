using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace IGDataPieChart.WPF.Samples.Display
{    
    public partial class Selection : SampleContainer
    {
        public Selection()
        {
            InitializeComponent();           
            this.selectionModeCombo.ItemsSource = Enum.GetValues(typeof(SeriesSelectionMode));
            this.selectionBehaviorCombo.ItemsSource = new List<string>() { "Single", "Multiple" };
        }

        private void selectionBrushPicker_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            this.dataPieChart.SelectionBrush = new SolidColorBrush() { Color = (Color)e.NewColor };
        }

        private void focusBrushPicker_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            this.dataPieChart.FocusBrush = new SolidColorBrush() { Color = (Color)e.NewColor };
        }

        private void selectionModeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.dataPieChart.SelectionMode = (SeriesSelectionMode)e.AddedItems[0];
        }

        private void selectionBehaviorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] == "Single")
            {
                this.dataPieChart.SelectionBehavior = SeriesSelectionBehavior.PerDataItemSingleSelect;
            }
            else
            {
                this.dataPieChart.SelectionBehavior = SeriesSelectionBehavior.PerDataItemMultiSelect;
            }
        }
    }
}
