﻿using System.Windows;
using System.Windows.Controls;

namespace IGDataChart.Samples.Display.Axes
{
    public partial class PolarAxes : Infragistics.Samples.Framework.SampleContainer
    {
        public PolarAxes()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // add event handlers for axis controls
            this.angleAxisInvertion.Click += OnAngleAxisInvertionChecked;
            this.angleAxisVisibility.Click += OnAngleAxisVisibilityChecked;

            this.radiusAxisInvertion.Click += OnRadiusAxisInvertionChecked;
            this.radiusAxisVisibility.Click += OnRadiusAxisVisibilityChecked;
        }
        private void OnRadiusAxisInvertionChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cbk = (sender as CheckBox);
            if (cbk == null) return;
            if (cbk.IsChecked == null) return;
            this.DataChart.Axes["radiusAxis"].IsInverted = cbk.IsChecked.Value;
        }
        private void OnRadiusAxisVisibilityChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cbk = (sender as CheckBox);
            if (cbk == null) return;
            if (cbk.IsChecked == null) return;
            this.DataChart.Axes["radiusAxis"].Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
            this.DataChart.Axes["radiusAxis"].LabelSettings.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
        }
        private void OnAngleAxisInvertionChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cbk = (sender as CheckBox);
            if (cbk == null) return;
            if (cbk.IsChecked == null) return;
            this.DataChart.Axes["angleAxis"].IsInverted = cbk.IsChecked.Value;
        }
        private void OnAngleAxisVisibilityChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cbk = (sender as CheckBox);
            if (cbk == null) return;
            if (cbk.IsChecked == null) return;
            this.DataChart.Axes["angleAxis"].Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
            this.DataChart.Axes["angleAxis"].LabelSettings.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
