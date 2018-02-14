//-------------------------------------------------------------------------
// <copyright file="FinancialDataBaseViewModel.cs" company="Infragistics">
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

using System.Xml.Linq;
using IGTrading.Models;
using System.Globalization;

namespace IGTrading.ViewModels
{
    public class FinancialDataBaseViewModel : FinancialData//, INotifyPropertyChanged
    {
        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public FinancialDataBaseViewModel()
        { }

        /// <summary>
        /// Creates a FinancialDataBaseViewModel instance from an XElement instance.
        /// </summary>
        public FinancialDataBaseViewModel(XElement element)
        {
            Description = element.Attribute("Description").Value;

            Change = element.Attribute("Change").Value;
            MarketCap = element.Attribute("MarketCap").Value;
            PE = element.Attribute("PE").Value;
            ROE = element.Attribute("ROE").Value;
            DivYield = element.Attribute("DivYield").Value;
            PriceToFreeCashFlow = element.Attribute("PriceToFreeCashFlow").Value;

            XAttribute priceToBook = element.Attribute("PriceToBook");
            PriceToBook = priceToBook == null ? string.Empty : priceToBook.Value;

            
            ParseFinancialData();
        }
        #endregion

        #region Public Properties
        public double ChangeValue { get; set; }
        public double MarketCapValue { get; set; }
        public double PEValue { get; set; }
        public double ROEValue { get; set; }
        public double DivYieldValue { get; set; }
        public double PriceToBookValue { get; set; }
        public double PriceToFreeCashFlowValue { get; set; }
        public double NetProfitMarginValue { get; set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the financial data fields.
        /// </summary>
        private void ParseFinancialData()
        {
            string marketCap = MarketCap;

            marketCap = marketCap.Replace(".", string.Empty);
            marketCap = marketCap.Replace("M", "0000");
            marketCap = marketCap.Replace("B", "0000000");

            ChangeValue = Parse(Change);
            MarketCapValue = Parse(marketCap);
            PEValue = Parse(PE);
            ROEValue = Parse(ROE);
            DivYieldValue = Parse(DivYield);
            PriceToBookValue = Parse(PriceToBook);
            PriceToFreeCashFlowValue = Parse(PriceToFreeCashFlow);
            NetProfitMarginValue = Parse(NetProfitMargin);
        }

        /// <summary>
        /// Parses a string to a double without crashing.
        /// </summary>
        /// <param name="stringValue">The string.</param>
        /// <returns>The parsed double.</returns>
        private double Parse(string stringValue)
        {
            double value = 0;
            NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;

            double.TryParse(stringValue, style, CultureInfo.InvariantCulture, out value);
            return value;
        }
        #endregion

        //#region INotifyPropertyChanged Members
        //public event PropertyChangedEventHandler PropertyChanged;
        ///// <summary>
        ///// Raises the property changed.
        ///// </summary>
        ///// <param name="propertyName">Name of the property.</param>
        //public void RaisePropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged == null) return;

        //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion
    }
}
