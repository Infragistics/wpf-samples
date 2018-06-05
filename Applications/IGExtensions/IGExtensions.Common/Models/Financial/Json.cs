using System.Runtime.Serialization;
using System.Collections.Generic;

namespace IGExtensions.Common.Models
{
	[DataContract]
	public class JsonRoot<T>
	{
		[DataMember(Name = "value")]
		public JsonContent<T> value;
	}

	[DataContract]
	public class JsonContent<T>
	{
		[DataMember(Name = "items")]
		public IList<T> items;
	}
}
