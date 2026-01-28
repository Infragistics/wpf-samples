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
    public partial class AggregateData : SampleContainer
    {
       
        public AggregateData()
        {
            InitializeComponent();

            DataContext = new SampleViewModel();            
        }
    }
}
