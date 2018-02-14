using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
namespace IGShowcase.AirplaneSeatingChart.Models
{
	/// <summary>
	/// Represents a seat class in the plane data with information about the seats and customer satisfaction.
	/// </summary>
    public class SeatClass : INotifyPropertyChanged
	{
        private int _SeatCount;

		#region Public Properties
		/// <summary>
        /// Gets or sets the category.
		/// </summary>
        /// <value>The category.</value>
        public string SeatCategory { get; set; }
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
		/// <summary>
		/// Gets or sets the seat count.
		/// </summary>
		/// <value>The seat count.</value>
        //public int SeatCount { get; set; }
        public int SeatCount 
        { 
            get
            {
                return _SeatCount;
            }
            set
            {
                _SeatCount = value;
                OnPropertyChanged("SeatCount");
            }
        }
        /// <summary>
		/// Gets or sets the seat pitch.
		/// </summary>
		/// <value>The seat pitch.</value>
		public int SeatPitch { get; set; }
		/// <summary>
		/// Gets or sets the width of the seat.
		/// </summary>
		/// <value>The width of the seat.</value>
		public int SeatWidth { get; set; }

		/// <summary>
		/// The data for the customer satisfaction chart.
		/// </summary>
       //Old code

        //public CustomerSatisfactionDataCollection CustomerSatisfaction { get; set; }

        //New code
        public ArrayList CustomerSatisfactionArrayList { get; set; }
        public List<CustomerSatisfactionData> CustomerSatisfaction { get; set; }
        internal void PopulateCustomerSatsfaction()
        {
            this.CustomerSatisfaction = this.CustomerSatisfactionArrayList.Cast<CustomerSatisfactionData>().ToList();

        }

        #endregion Public Properties
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
