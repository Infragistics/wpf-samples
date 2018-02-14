using System;
using System.ComponentModel;
using System.Windows.Media;
using IGShowcase.AirplaneSeatingChart.Resources;
using IGShowcase.AirplaneSeatingChart.Models;
using Infragistics;

namespace IGShowcase.AirplaneSeatingChart.ViewModels
{
	public sealed class SeatViewModel : PlaneElementViewModel
	{
		#region Private members
		private static readonly Brush TransparentBrush = new SolidColorBrush(Colors.Transparent);

		private bool _isSelected;
		private bool _isDisabled;
		#endregion

		#region Constructors
        public SeatViewModel()
        {

        }
		/// <summary>
		/// Initializes a new instance of the <see cref="SeatViewModel"/> class.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="seat">The seat.</param>
		/// <param name="brushes">The brushes.</param>
		#endregion Constructors

		#region Public Properties

		/// <summary>
		/// Gets or sets the seat row.
		/// </summary>
        public string Row { get; set; }
        /// <summary>
        /// Gets or sets the seat column.
        /// </summary>
        /// <value>The column.</value>
        public string Column { get; set; }
        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        /// <value>The class.</value>
        public string Class { get; set; }

		/// <summary>
		/// Gets the position.
		/// </summary>
		/// <value>The position.</value>
        public string Position { get { return string.Format("{0}{1}", this.Row, this.Column); } }
        
        /// Gets or sets the type of the seat.
        /// </summary>
        /// <value>The type of the seat.</value>
        public SeatType SeatType { get; set; }


        /// <summary>
        /// Gets the status text.
        /// </summary>
        /// <value>The status text.</value>
        /// <summary>
        public string StatusText { get { return this.IsOccupied ? AppStrings.Occupied : AppStrings.Available; } }

		/// <summary>
		/// Gets or sets a value indicating whether this seat is disabled when selecting seat class.
		/// </summary>
		public bool IsDisabled
		{
			get { return _isDisabled; }
			set
			{
				if (_isDisabled != value)
				{
					_isDisabled = value;
					OnPropertyChanged("IsDisabled");
				}
			}
		}


        /// <summary>
        /// Gets or sets a value indicating whether this instance is occupied.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is occupied; otherwise, <c>false</c>.
        /// </value>
        public bool IsOccupied { get; set; }
 
        /// <summary>
		/// Gets or sets a value indicating whether this seat is selected.
		/// </summary>
		public bool IsSelected
		{
			get{ return _isSelected; }
			set
			{
				if(_isSelected != value)
				{
					_isSelected = value;
					OnPropertyChanged("IsSelected");
                }
			}
		}
		#endregion Public Properties
	}
}
