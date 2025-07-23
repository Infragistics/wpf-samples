using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Infragistics.Controls.Charts;

namespace IGDataChart.Models
{
    public class FinancialSeriesModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Type Type { get; set; }

        public FinancialSeriesModel(Type indicatorType)
        {
            Type = indicatorType;
            Name = indicatorType.Name;
            DisplayName = indicatorType.Name;
        }
        public bool IsOverlaySeries()
        {
            return this.Name.EndsWith("Overlay");
        }
        public bool IsIndicatorSeries()
        {
            return this.Name.EndsWith("Indicator");
        }
        public bool IsPriceSeries()
        {
            return this.Name.EndsWith("PriceSeries");
        }
        public override string ToString()
        {
            return this.DisplayName;
        }

    }
    public class FinancialSeriesCollection : ObservableCollection<FinancialSeriesModel>
    {
        public FinancialSeriesCollection()
        {
        }
        public static FinancialSeriesCollection GetKnownFinancialSeries()
        {
            var items = new FinancialSeriesCollection();
            var assm = typeof(XamDataChart).Assembly;
            //get all classes ending with "Indicator" or "Overlay" from the DataChart assembly
            var indicators = from type in assm.GetTypes()
                             where type.IsPublic
                              && (type.Name.EndsWith("Indicator") || type.Name.EndsWith("Overlay"))
                              && type.Name != "CustomIndicator"
                              && type.Name != "FinancialOverlay"
                              && type.Name != "StrategyBasedIndicator"
                              && type.Name != "ItemwiseStrategyBasedIndicator"
                              && (type.IsSubclassOf(typeof(FinancialOverlay)) ||
                                  type.IsSubclassOf(typeof(StrategyBasedIndicator)) ||
                                  type.IsSubclassOf(typeof(ItemwiseStrategyBasedIndicator)))
                             orderby type.Name
                             select new FinancialSeriesModel(type);
            //add each Indicator and overlay.
            foreach (var model in indicators)
            {
                items.Add(model);
            }
            return items;
        }
    }
}