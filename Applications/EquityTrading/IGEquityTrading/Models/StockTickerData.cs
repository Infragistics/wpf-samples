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
