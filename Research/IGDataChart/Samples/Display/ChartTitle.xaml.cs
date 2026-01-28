using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;


namespace IGDataChart.Samples.Display
{
    public partial class ChartTitle : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartTitle()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
