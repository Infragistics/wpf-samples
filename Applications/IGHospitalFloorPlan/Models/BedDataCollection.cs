using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IGShowcase.HospitalFloorPlan.Models
{
	public class BedDataCollection : Collection<BedData>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="BedDataCollection"/> class.
		/// </summary>
		public BedDataCollection()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BedDataCollection"/> class.
		/// </summary>
		/// <param name="collection">The collection.</param>
		public BedDataCollection(IEnumerable<BedData> collection)
		{
			foreach (BedData data in collection)
			{
				Add(data);
			}
		}
		#endregion Constructors
	}
}
