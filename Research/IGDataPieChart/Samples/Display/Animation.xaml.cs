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
    public partial class Animation : SampleContainer
    {
        public Animation()
        {
            InitializeComponent();
            this.animationModeCombo.ItemsSource = Enum.GetValues(typeof(CategoryTransitionInMode));
            this.animationModeCombo.SelectedItem = CategoryTransitionInMode.Auto;
            
        }
        private void animationModeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.dataPieChart.TransitionInMode = (CategoryTransitionInMode)e.AddedItems[0];
        }

        private void animationDurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.dataPieChart.TransitionInDuration = new TimeSpan(0, 0, 0, 0, (int)e.NewValue);
        }

        private void replayAnimationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.dataPieChart.ReplayTransitionIn();
        }
    }
}
