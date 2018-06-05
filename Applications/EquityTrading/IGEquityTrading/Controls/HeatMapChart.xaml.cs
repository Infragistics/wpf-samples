using System.Windows;
using System.Windows.Controls;
using IGTrading.ViewModels;
using Infragistics.Controls.Charts;

namespace IGTrading.Controls
{
    /// <summary>
    /// Interaction logic for HeatMapChart.xaml
    /// </summary>
    public partial class HeatMapChart : UserControl
    {
        private HeatmapViewModel _vm;

        public HeatMapChart()
        {
			InitializeComponent();

            _vm = (HeatmapViewModel)Application.Current.Resources["heatmapViewModel"];

			//_vm = (HeatmapViewModel)this.DataContext;

			if (_vm.Description == null)
			{
				_vm.Description = Assets.LocalizedResources.Strings.Market_Description;
				_vm.SelectedIndustry = Assets.LocalizedResources.Strings.Company_Summary_Info;

				_vm.AddTreemapFilter(Assets.LocalizedResources.Strings.Change, "ChangeValue", -7, +7);
				_vm.AddTreemapFilter(Assets.LocalizedResources.Strings.PE, "PEValue", -10, 40);
				_vm.AddTreemapFilter(Assets.LocalizedResources.Strings.ROE, "ROEValue", -10, 40);
				_vm.AddTreemapFilter(Assets.LocalizedResources.Strings.Div_Yield, "DivYieldValue", -10, 10);
				_vm.AddTreemapFilter(Assets.LocalizedResources.Strings.Price_to_Book, "PriceToBookValue", -15, +15);
				_vm.AddTreemapFilter(Assets.LocalizedResources.Strings.Price_to_Free_Cash_Flow, "PriceToFreeCashFlowValue", -100, +100);
			}

            this.DataContext = _vm;
        }

        void HeatMapChart_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.InitializeTreemap(Treemap);
        }

        private void Treemap_NodeMouseLeftButtonDown(object sender, TreemapNodeClickEventArgs e)
        {
			_vm.DrillNode(e.Node.DataContext);
        }

        private void Treemap_NodeMouseRightButtonDown(object sender, TreemapNodeClickEventArgs e)
        {
			_vm.DrillBack();
        }
    }
}
