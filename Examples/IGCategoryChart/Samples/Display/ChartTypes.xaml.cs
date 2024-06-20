using IGCategoryChart.Samples.ViewModels;
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
using System.Globalization;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;

namespace IGCategoryChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for ChartTypes.xaml
    /// </summary>
    public partial class ChartTypes :  Infragistics.Samples.Framework.SampleContainer
    {
        public ChartTypes()
        {
            InitializeComponent();

            chart1.ItemsSource = new EnergyProductionDataSample();
        }

        private void OnPrevButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.cmbChartType.SelectedIndex == 0)
            {
                this.cmbChartType.SelectedIndex = this.cmbChartType.Items.Count - 1;
            }
            else
            {
                this.cmbChartType.SelectedIndex -= 1;
            }
        }

        private void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            this.cmbChartType.SelectedIndex = (this.cmbChartType.SelectedIndex + 1) % this.cmbChartType.Items.Count;
        }
    }
       
}
