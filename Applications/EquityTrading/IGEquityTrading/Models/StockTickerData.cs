//-------------------------------------------------------------------------
// <copyright file="StockTickerData.cs" company="Infragistics">
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
using System.Runtime.Serialization;
using System.Globalization;

namespace IGTrading.Models
{
    [Serializable]
	[DataContract]
	public class StockTickerData
	{
		[DataMember(Name = "Open")]
		public double Open { get; set; }
		[DataMember(Name = "Low")]
		public double Low { get; set; }
		[DataMember(Name = "Volume")]
		public long Volume { get; set; }
		[DataMember(Name = "High")]
		public double High { get; set; }
		[DataMember(Name = "Close")]
		public double Close { get; set; }

		//Workaround datetime deserialization
		[DataMember(Name = "Date")]
		public string DateString { get; set; }
		public DateTime Date
		{
			get { return DateTime.ParseExact(DateString, "yyyy-MM-dd", CultureInfo.InvariantCulture); }
			set { DateString = value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture); }
		}
	}
}
