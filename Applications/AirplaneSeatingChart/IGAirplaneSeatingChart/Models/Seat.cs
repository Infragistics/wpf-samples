
namespace IGShowcase.AirplaneSeatingChart.Models
{
	/// <summary>
	/// Describes the properties of a single seat.
	/// </summary>
	public class Seat
	{
		#region Public Properties
		/// <summary>
		/// Gets or sets the row.
		/// </summary>
		/// <value>The row.</value>
		public string Row { get; set; }
		/// <summary>
		/// Gets or sets the column.
		/// </summary>
		/// <value>The column.</value>
		public string Column { get; set; }
		/// <summary>
		/// Gets or sets the class.
		/// </summary>
		/// <value>The class.</value>
		public string Class { get; set; }
		/// <summary>
		/// Gets or sets the type of the seat.
		/// </summary>
		/// <value>The type of the seat.</value>
		public SeatType SeatType { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is occupied.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is occupied; otherwise, <c>false</c>.
		/// </value>
		public bool IsOccupied { get; set; }
		#endregion Public Properties
	}
}
