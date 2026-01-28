using IGCategoryChart.Samples.ViewModels;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGCategoryChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for TransitionAnimations.xaml
    /// </summary>
    public partial class TransitionAnimations : Infragistics.Samples.Framework.SampleContainer
    {
        protected TimeSpan TransDuration = TimeSpan.FromSeconds(2);
        protected TransitionInSpeedType TransSpeed = TransitionInSpeedType.Auto;
        protected CategoryTransitionInMode TransMode = CategoryTransitionInMode.Auto;
        protected EasingMode TransEasingMode = EasingMode.EaseInOut;
        protected EasingFunctionBase TransEasingFunction = new BackEase();

        public TransitionAnimations()
        {
            InitializeComponent();

            chart1.ItemsSource = new EnergyProductionDataSample();

            this.ComboBoxTransitionType.ItemsSource = EnumValuesProvider.GetEnumValues<CategoryTransitionInMode>();
            this.ComboBoxTransitionType.SelectedIndex = 0;

            this.ComboBoxTransitionSpeedType.ItemsSource = EnumValuesProvider.GetEnumValues<TransitionInSpeedType>();
            this.ComboBoxTransitionSpeedType.SelectedIndex = 0;
            this.ComboBoxTransitionEasingFunction.SelectedIndex = 0;
            this.ComboBoxTransitionEasingMode.SelectedIndex = 0;

            cmbChartType.SelectedIndex = 3;
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

        private void ComboBoxTransitionType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TransMode = (CategoryTransitionInMode)e.AddedItems[0];
            UpdateChart();
        }

        private void ComboBoxTransitionEasingFunction_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxTransitionEasingFunction == null) return;
            if (ComboBoxTransitionEasingFunction.SelectedIndex == -1)
            {
                TransEasingFunction = EasingFunctions[0];
            }
            else
            {
                TransEasingFunction = EasingFunctions[ComboBoxTransitionEasingFunction.SelectedIndex];
            }
            TransEasingFunction.EasingMode = TransEasingMode;
            UpdateChart();
        }

        private void ComboBoxTransitionEasingMode_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxTransitionEasingMode == null) return;
            if (ComboBoxTransitionEasingMode.SelectedIndex == -1)
            {
                TransEasingMode = EasingModes[0];
            }
            else
            {
                TransEasingMode = EasingModes[ComboBoxTransitionEasingMode.SelectedIndex];
            }
            TransEasingFunction.EasingMode = TransEasingMode;
            UpdateChart();
        }

        private void TransitionDurationSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TransitionDurationSlider == null) return;
            TransDuration = TimeSpan.FromSeconds(TransitionDurationSlider.Value);
        }

        private void TransitionReplayButton_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateChart();
        }

        private void ComboBoxTransitionSpeedType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxTransitionSpeedType == null) return;
            TransSpeed = (TransitionInSpeedType)e.AddedItems[0];
            UpdateChart();
        }

        private void UpdateChart()
        {
            chart1.TransitionInSpeedType = TransSpeed;
            chart1.TransitionInEasingFunction = TransEasingFunction;
            chart1.TransitionInDuration = TransDuration;

            cmbChartType.SelectedIndex += 1;
            cmbChartType.SelectedIndex -= 1;

            chart1.TransitionInMode = TransMode;
            chart1.IsTransitionInEnabled = true;
        }

        static readonly EasingFunctionBase[] EasingFunctions =
        {
            new PowerEase(),
            new BackEase(),
            new BounceEase(),
            new CircleEase(),
            new CubicEase(),
            new ElasticEase(),
            new ExponentialEase(),
            new QuadraticEase(),
            new QuarticEase(),
            new QuinticEase(),
            new SineEase()
        };
        static readonly EasingMode[] EasingModes =
        {
            EasingMode.EaseOut,
            EasingMode.EaseInOut,
            EasingMode.EaseIn
        };
    }
}
