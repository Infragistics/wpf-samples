using System.Windows;
using System.Windows.Controls;
using IGShowcase.FinanceDashboard.ViewModels;

namespace IGShowcase.FinanceDashboard.Controls
{
    public class SeriesTemplateSelector : DataTemplateSelector
	{
		#region Public Properties
		public DataTemplate LineChartTemplate { get; set; }
		public DataTemplate OHLCChartTemplate { get; set; }
		public DataTemplate AreaChartTemplate { get; set; }
        public DataTemplate StepAreaChartTemplate { get; set; }
        public DataTemplate RangeAreaChartTemplate { get; set; }
        public DataTemplate CandelStickTemplate { get; set; }
		#endregion Public Properties

		#region Public Methods
		/// <summary>
		/// Selects the template.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="container">The container.</param>
		/// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
			var vm = item as StockInfoViewModel;

			if (vm == null) return null;

			switch (vm.Parent.SelectedChartType)
			{
                case StockChartType.Line: 
                    return LineChartTemplate;
                case StockChartType.OpenHighLowClose: 
                    return OHLCChartTemplate;
                case StockChartType.Area: 
                    return AreaChartTemplate;
                case StockChartType.StepArea:
                    return StepAreaChartTemplate;
                case StockChartType.RangeArea:
                    return RangeAreaChartTemplate;
              
                case StockChartType.CandleStick: 
                    return CandelStickTemplate;
			}

			return null;
        }
		#endregion Public Methods
	}
}
