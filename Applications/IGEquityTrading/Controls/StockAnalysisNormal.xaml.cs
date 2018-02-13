﻿//-------------------------------------------------------------------------
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

using System.Windows;
using System.Windows.Controls;

namespace IGTrading.Controls
{
    /// <summary>
    /// Interaction logic for StockAnalysisNormal.xaml
    /// </summary>
    public partial class StockAnalysisNormal : UserControl
    {
        public StockAnalysisNormal()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StockAnalysisNormal_Loaded);
        }

        void StockAnalysisNormal_Loaded(object sender, RoutedEventArgs e)
        {
            // Unlink the MiniChart from its previous owner, if any
            if (((DataChartEx)Application.Current.Resources["MiniStocksChart"]).Parent != null )
                ((ContentControl)((DataChartEx)Application.Current.Resources["MiniStocksChart"]).Parent).Content = null;
            this.MiniChart.Content = Application.Current.Resources["MiniStocksChart"];
        }
    }
}
