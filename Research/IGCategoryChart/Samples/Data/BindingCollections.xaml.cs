using IGCategoryChart.Samples.Models;
using IGCategoryChart.Samples.ViewModels;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace IGCategoryChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for BindingCollections.xaml
    /// </summary>
    public partial class BindingCollections : SampleContainer
    {
       
        public BindingCollections()
        {
            InitializeComponent();

            this.DataContext = new VariedCollectionViewModel();
          
            chartTypeCombo.SelectedIndex = 0;

            
        }

        private void OnChangeDataSampleClicked(object sender, RoutedEventArgs e)
        { 
            VariedCollectionViewModel VM = (VariedCollectionViewModel) this.DataContext;
            
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == true)
                chart1.ItemsSource = VM.SmallSixDataPtCollection;
            else
                chart1.ItemsSource = VM.LargeSixDataPtCollection;
        }
    }

    public class ChartTypeCollection : List<ChartTypeInfo>
    {

    }

    public class ChartTypeInfo
    {
        public string Description { get; set; }
        public CategoryChartType Value { get; set; }
    }
}
