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

using System;
using System.Windows;
using System.Windows.Controls;
using IGTrading.ViewModels;

namespace IGTrading.Controls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StockHistoryChart : UserControl
    {
        #region Private Variables
        private StockViewModel _vm;
        #endregion Private Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StockHistoryChart"/> class.
        /// </summary>
        public StockHistoryChart()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region Event Handlers
        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lets grab the data context
            _vm = DataContext as StockViewModel;

            if (_vm == null)
            {
                throw new NullReferenceException("DataContext must be of type StockViewModel");
            }
        }

        /// <summary>
        /// Handles the DropDownClosed event of the txtbxSymbolNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="bool"/> instance containing the event data.</param>
        private void TxtbxSymbolNewDropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            var symbol = ((AutoCompleteBox)sender).Text.ToUpper();

            if ((bool)_vm.CheckForExistingStockBySymbolName(symbol))
            {
                ((AutoCompleteBox)sender).Text = "";

                if (_vm.AddStockCommand.CanExecute(symbol))
                {
                    _vm.AddStockCommand.Execute(symbol);
                }
            }
        }

        #endregion Event Handlers
    }
}
