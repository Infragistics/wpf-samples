using IGShapeChart.Resources;
using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGShapeChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for BindingBreakEvenData.xaml
    /// </summary>
    public partial class BindingBreakEvenData : SampleContainer
    {
        public BindingBreakEvenData()
        {
            InitializeComponent();
        }

        private void XamShapeChart_SeriesAdded(object sender, Infragistics.Controls.Charts.ChartSeriesEventArgs args)
        {
            switch ((string)args.Series.Title)
            {
                case "Fixed Cost":
                    args.Series.Title = ShapeChartStrings.XW_FixedCost;
                    break;
                case "Variable Cost":
                    args.Series.Title = ShapeChartStrings.XW_VariableCost;
                    break;
                case "Margin of Safety":
                    args.Series.Title = ShapeChartStrings.XW_MarginOfSafety;
                    break;
                case "Marginal Profit":
                    args.Series.Title = ShapeChartStrings.XW_MarginalProfit;
                    break;
                case "Profit Area":
                    args.Series.Title = ShapeChartStrings.XW_ProfitArea;
                    break;
                case "Loss Area":
                    args.Series.Title = ShapeChartStrings.XW_LossArea;
                    break;
                case "Marginal Area":
                    args.Series.Title = ShapeChartStrings.XW_MarginalArea;
                    break;
                case "Sales Revenue":
                    args.Series.Title = ShapeChartStrings.XW_SalesRevenue;
                    break;
                case "Total Cost":
                    args.Series.Title = ShapeChartStrings.XW_TotalCost;
                    break;
                case "Break Even":
                    args.Series.Title = ShapeChartStrings.XW_BreakEven;
                    break;
            }
        }
    }

    public class BreakEvenItem
    {
        public BreakEvenItem()
        {
            MarginalProfit = double.NaN;
            VariableCost = double.NaN;
            FixedCost = double.NaN;
            Revenue = double.NaN;
            Units = double.NaN;
        }

        public double FixedCost { get; set; }
        public double VariableCost { get; set; }
        public double Revenue { get; set; }
        public double Units { get; set; }
        public double MarginalProfit { get; set; }
    }
    public class BreakEvenList : List<BreakEvenItem>
    {
        public BreakEvenList()
        {

        }
    }
}
