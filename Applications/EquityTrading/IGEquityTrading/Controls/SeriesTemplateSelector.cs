//-------------------------------------------------------------------------
// <copyright file="SeriesTemplateSelector.cs" company="Infragistics">
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
