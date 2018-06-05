using System.Windows;
using System.Windows.Controls;
using IGTrading.ViewModels;

namespace IGTrading.Controls
{
    public class SeriesTemplateSelector : DataTemplateSelector
	{
		#region Public Properties
		public DataTemplate LineChartTemplate { get; set; }
		public DataTemplate OHLCChartTemplate { get; set; }
		public DataTemplate AreaChartTemplate { get; set; }
		public DataTemplate CandleStickTemplate { get; set; }
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
			StockInfoViewModel vm = item as StockInfoViewModel;

			if (vm == null) return null;

			switch (vm.Parent.SelectedChartType)
			{
				case 0: return LineChartTemplate;
				case 1: return OHLCChartTemplate;
				case 2: return AreaChartTemplate;
				case 3: return CandleStickTemplate;
			}

			return null;
        }
		#endregion Public Methods
	}
}
