using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using IGShowcase.HospitalFloorPlan.Models;
using Infragistics;

namespace IGShowcase.HospitalFloorPlan.ViewModels
{
	public sealed class MapElementViewModel : INotifyPropertyChanged
	{
		#region Private members
		private readonly Brush _fill;
		private readonly Brush _inactiveFill;
		private readonly Brush _stroke;
        private readonly Thickness _strokeThickness;
		private readonly Brush _inactiveStroke;

		private bool _isFilteredOut;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="MapElementViewModel"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="bedData">The bed data.</param>
		/// <param name="fillBrushes">The fill brushes.</param>
		/// <param name="strokeBrushes">The stroke brushes.</param>
		public MapElementViewModel(string name, BedData bedData, BrushCollection fillBrushes, BrushCollection strokeBrushes)
		{
			Name = name;
			
			if(bedData != null)
			{

				BedData = bedData;
				_fill = fillBrushes[(int)(BedData.BedStatus) + 1];
				_inactiveFill = fillBrushes[0];
				
				var strokeIndex = BedData.BedStatus != BedStatus.Free ? ((int)(BedData.Patient.Gender) + 1) : 0;
				
				_stroke = strokeBrushes[strokeIndex];
                _strokeThickness = strokeIndex == 0 ? new Thickness(0) : new Thickness(3);
				_inactiveStroke = strokeBrushes[0];
			}
			else
			{
				IsStub = true;
				_fill = fillBrushes[0];
				_stroke = strokeBrushes[0];
                _strokeThickness = new Thickness(1);
			}
		}
      

	    #endregion Constructors

		#region Public Properties
       
		/// <summary>
		/// Gets or sets a value indicating whether this instance is stub.
		/// </summary>
		/// <value><c>true</c> if this instance is stub; otherwise, <c>false</c>.</value>
		public bool IsStub { get; private set; }

		/// <summary>
		/// Gets or sets the bed data.
		/// </summary>
		/// <value>The bed data.</value>
		public BedData BedData { get; private set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the fill.
		/// </summary>
		/// <value>The fill.</value>
		public Brush Fill
		{
			get { return _isFilteredOut ? _inactiveFill : _fill;}
		}

		/// <summary>
		/// Gets the stroke.
		/// </summary>
		/// <value>The stroke.</value>
		public Brush Stroke
		{
			get { return _isFilteredOut ? _inactiveStroke : _stroke; }
		}

		/// <summary>
		/// Gets the stroke thickness.
		/// </summary>
		/// <value>The stroke thickness.</value>
        public Thickness StrokeThickness
		{
			get { return _isFilteredOut ? new Thickness(1) : _strokeThickness; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is filtered out.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is filtered out; otherwise, <c>false</c>.
		/// </value>
		public bool IsFilteredOut
		{
			get { return _isFilteredOut; }
			set
			{
				if (IsStub || _isFilteredOut == value) return;

				_isFilteredOut = value;
				OnPropertyChanged("IsFilteredOut");
				OnPropertyChanged("Fill");
				OnPropertyChanged("Stroke");
				OnPropertyChanged("StrokeThickness");
			}
		}
		#endregion Public Properties

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Called when [property changed].
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}
