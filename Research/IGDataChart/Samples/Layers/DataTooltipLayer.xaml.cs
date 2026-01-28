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

namespace IGDataChart.Samples.Layers
{
    /// <summary>
    /// Interaction logic for DataTooltipLayer.xaml
    /// </summary>
    public partial class DataTooltipLayer : Infragistics.Samples.Framework.SampleContainer
    {
        public DataTooltipLayer()
        {
            InitializeComponent();
        }

        private void OnFilterHighIncome(object sender, FilterEventArgs e)
        {
            var model = e.Item as CountryDataModel;
            if (model != null)
            {
                e.Accepted = model.GdpPerCapita >= 30000;
            }
        }

        private void OnFilterLowIncome(object sender, FilterEventArgs e)
        {
            var model = e.Item as CountryDataModel;
            if (model != null)
            {
                e.Accepted = model.GdpPerCapita < 30000;
            }
        }
    }
}
