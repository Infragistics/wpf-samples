using System.Windows;
using System.Windows.Controls;
using IGDataChart.Resources;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Samples.Display.Legends
{
    public partial class LegendTypes : Infragistics.Samples.Framework.SampleContainer
    {
        public LegendTypes()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private GallerySampleViewModel _vm;
        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            _vm = new GallerySampleViewModel();
            _vm.AddSample(this.GridWithScaleLegend, DataChartStrings.XWDC_LegendTypes_ScaleLegend);
            _vm.AddSample(this.GridWithItemLegend, DataChartStrings.XWDC_LegendTypes_ItemLegend);
            _vm.AddSample(this.GridWithTitleLegend, DataChartStrings.XWDC_LegendTypes_TitleLegend);

            this.PrevItemButton.Click += OnPrevItemButtonClick;
            this.NextItemButton.Click += OnNextItemButtonClick;

            this.ItemSelectionComboBox.ItemsSource = _vm.SamplesNames;
            this.ItemSelectionComboBox.SelectionChanged += OnItemSelectionChanged;
            this.ItemSelectionComboBox.SelectedIndex = 0;
        }

        private void OnItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.ShowSample(this.ItemSelectionComboBox.SelectedIndex);
        }

        private void OnNextItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ItemSelectionComboBox.SelectedIndex = _vm.GetNextSampleIndex();
        }

        private void OnPrevItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ItemSelectionComboBox.SelectedIndex = _vm.GetPreviousSampleIndex();
        }
    }
}
