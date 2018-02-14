//-------------------------------------------------------------------------
// <copyright file="StringComparisonToBoolValueConverter.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------

using System.Collections.Generic;
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
