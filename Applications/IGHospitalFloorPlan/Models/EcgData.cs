namespace IGShowcase.HospitalFloorPlan.Models
{
	public class EcgData
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="EcgData"/> class.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		public EcgData(double x, double y)
		{
			X = x;
			Y = y;
		}
		#endregion Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the X.
		/// </summary>
		/// <value>The X.</value>
		public double X { get; private set; }
		/// <summary>
		/// Gets or sets the Y.
		/// </summary>
		/// <value>The Y.</value>
		public double Y { get; private set; }
		#endregion Public Properties
	}
}
