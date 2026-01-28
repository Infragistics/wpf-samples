using IGCategoryChart.Samples.Models;
using IGCategoryChart.Samples.ViewModels;
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

namespace IGCategoryChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for CategoryChart_AxisTitle.xaml
    /// </summary>
    public partial class AxisTitle : SampleContainer
    {
        public AxisTitle()
        {
            InitializeComponent();
        }

        private void AlignmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = e.AddedItems[0] is VerticalAlignment ? (VerticalAlignment)e.AddedItems[0] : VerticalAlignment.Top;
            chart1.YAxisTitleAlignment = value;
        }
    }
}
