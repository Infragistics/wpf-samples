using System.Windows;
using System.Windows.Controls;
using IGShowcase.FinanceDashboard.Resources;
using Infragistics.Controls.Charts;
using IGShowcase.FinanceDashboard.ViewModels;

namespace IGShowcase.FinanceDashboard.Views
{
    public partial class HeatmapView : IGExtensions.Framework.Controls.NavigationPage
    {
        #region Private Member Variables
        private HeatmapViewModel _vm;
        #endregion Private Member Variables

        public HeatmapView()
        {
            InitializeComponent();

            //_vm = (HeatmapViewModel)this.DataContext;
            //_vm.PropertyChanged += OnViewModelPropertyChanged;


            //this.BusyIndicator.Visibility = Visibility.Visible;
            this.Loaded += OnNavigationPageLoaded;

        }

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsInitialDataLoading")
            {
                this.BusyIndicator.Visibility = _vm.IsInitialDataLoading ? Visibility.Visible : Visibility.Collapsed;
            } 
        }

        private void OnNavigationPageLoaded(object sender, RoutedEventArgs e)
        {
            _vm = (HeatmapViewModel)this.DataContext;
            
            if (_vm.Description == null)
            {
                _vm.Description = AppStrings.Market_Description;
                _vm.SelectedIndustry = AppStrings.Company_Summary_Info;
                _vm.AddTreemapFilter(AppStrings.DataColumn_Change, "ChangeValue", -7, +7);
                _vm.AddTreemapFilter(AppStrings.DataColumn_P_E, "PEValue", -10, 40);
                _vm.AddTreemapFilter(AppStrings.DataColumn_ROE, "ROEValue", -10, 40);
                _vm.AddTreemapFilter(AppStrings.DataColumn_Div_Yield, "DivYieldValue", -10, 10);
                _vm.AddTreemapFilter(AppStrings.DataColumn_Price_to_Book, "PriceToBookValue", -15, +15);
                _vm.AddTreemapFilter(AppStrings.DataColumn_Net_Profit_Margin, "NetProfitMarginValue", -50, +50);
                _vm.AddTreemapFilter(AppStrings.DataColumn_Price_to_Free_Cash_Flow, "PriceToFreeCashFlowValue", -100, +100);
            }
            _vm.InitializeTreemap(Treemap);
            _vm.PropertyChanged += OnViewModelPropertyChanged;

            this.BusyIndicator.Visibility = _vm.IsInitialDataLoading ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Treemap_NodeMouseLeftButtonDown(object sender, TreemapNodeClickEventArgs e)
        {
            //_vm.IsInitialDataLoading = true;
            var treemapNoed = e.Node;
            _vm.DrillDownData(treemapNoed);
        }

        private void Treemap_NodeMouseRightButtonDown(object sender, TreemapNodeClickEventArgs e)
        {
            _vm.DrillBack();
        }
    }
}