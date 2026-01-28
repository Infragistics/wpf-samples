using Infragistics.Samples.Framework;
using System.Windows.Controls;
using System.Windows;

namespace IGFinancialChart.Samples.Display
{ 
    public partial class AxisSettings : SampleContainer
    {
        public AxisSettings()
        {
            InitializeComponent(); 
        }

        private void XAxisLabelVisibility_Checked(object sender, RoutedEventArgs e)
        {
            var isChecked = (sender as CheckBox).IsChecked.Value;
            this.Chart.XAxisLabelVisibility = isChecked ? Visibility.Visible : Visibility.Collapsed;
        }

        private void YAxisLabelVisibility_Checked(object sender, RoutedEventArgs e)
        {
            var isChecked = (sender as CheckBox).IsChecked.Value;
            this.Chart.YAxisLabelVisibility = isChecked ? Visibility.Visible : Visibility.Collapsed;
        }
    }
     
}
