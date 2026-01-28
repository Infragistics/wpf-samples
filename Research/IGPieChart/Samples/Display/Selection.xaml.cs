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

namespace IGPieChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for Selection.xaml
    /// </summary>
    public partial class Selection : Infragistics.Samples.Framework.SampleContainer
    {
        public Selection()
        {
            InitializeComponent();
        }

        private void OnSelectionModeChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            switch (cb.SelectedIndex)
            {
                case 0:
                    pieChart.SelectionMode = Infragistics.Controls.Charts.SliceSelectionMode.Single;
                    break;
                case 1:
                    pieChart.SelectionMode = Infragistics.Controls.Charts.SliceSelectionMode.Multiple;
                    break;
                case 2:
                    pieChart.SelectionMode = Infragistics.Controls.Charts.SliceSelectionMode.Manual;
                    break;
            }
        }
    }
}
