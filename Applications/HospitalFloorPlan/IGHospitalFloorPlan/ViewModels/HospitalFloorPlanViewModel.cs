using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Infragistics;
using IGShowcase.HospitalFloorPlan.Models;

namespace IGShowcase.HospitalFloorPlan.ViewModels
{
	public sealed class HospitalFloorPlanViewModel : INotifyPropertyChanged
	{
		#region Private Memeber Variables
		private Dictionary<string, MapElementViewModel> _hospitalFloorData;

		private IEnumerable<MapElementViewModel> _mapElements;
		private FilterViewModel _filter;

		private BedData							_selectedElementData;

		private int								_totalNumberOfBeds;
		private int								_totalNumberOfFreeBeds;

		private BrushCollection					_bedStatusPalette;
		private BrushCollection					_genderPalette;
		#endregion Private Memeber Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="HospitalFloorPlanViewModel"/> class.
		/// </summary>
		public HospitalFloorPlanViewModel()
		{
		}
		#endregion Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the bed status palette.
		/// </summary>
		/// <value>The bed status palette.</value>
		public BrushCollection BedStatusPalette
		{
			get { return _bedStatusPalette; } 
			set
			{
				if (value == null || value.Count != 5)
				{
					throw new ArgumentException("Invalid Bed Status Palette -- Please specify exactly 5 brushes");
				}
				_bedStatusPalette = value;
			}
		}

		/// <summary>
		/// Gets or sets the gender palette.
		/// </summary>
		/// <value>The gender palette.</value>
		public BrushCollection GenderPalette
		{
			get { return _genderPalette; }
			set
			{
				if (value == null || value.Count != 4)
				{
					throw new ArgumentException("Invalid Gender Palette -- Please specify exactly 4 brushes");
				}
				_genderPalette = value;
			}
		}

		/// <summary>
		/// Gets or sets the map.
		/// </summary>
		/// <value>The map.</value>
		public IEnumerable<MapElementViewModel> MapElements
		{
			get { return _mapElements; }
			private set
			{
				if (_mapElements != value)
				{
					_mapElements = value;
					OnPropertyChanged("MapElements");
				}
			}
		}

		public FilterViewModel Filter
		{
			get { return _filter; }
			private set
			{
				if(_filter != value)
				{
					_filter = value;
					OnPropertyChanged("Filter");
				}
			}
		}

		/// <summary>
		/// Gets or sets the selected element.
		/// </summary>
		/// <value>The selected element.</value>
		public BedData SelectedElement
		{
			get { return _selectedElementData; }
			private set
			{
				if (_selectedElementData != value)
				{
					_selectedElementData = value;
					OnPropertyChanged("SelectedElement");
				}
			}
		}

		/// <summary>
		/// Gets the total number of beds.
		/// </summary>
		/// <value>The total number of beds.</value>
		public int TotalNumberOfBeds
		{
			get { return _totalNumberOfBeds;}
			private set
			{
				if (_totalNumberOfBeds!=value)
				{
					_totalNumberOfBeds = value;
					OnPropertyChanged("TotalNumberOfBeds");
				}
			}
		}

		/// <summary>
		/// Gets the total number of beds free.
		/// </summary>
		/// <value>The total number of beds free.</value>
		public int TotalNumberOfFreeBeds
		{
			get { return _totalNumberOfFreeBeds; }
			private set
			{
				if(_totalNumberOfFreeBeds !=value)
				{
					_totalNumberOfFreeBeds = value;
					OnPropertyChanged("TotalNumberOfFreeBeds");
				}
			}
		}
		#endregion Public Properties

		#region Public Methods
		/// <summary>
		/// Registers the elements.
		/// </summary>
		/// <param name="elements">The elements.</param>
		public void RegisterElements(IEnumerable<string> elements)
		{
			if(_bedStatusPalette == null || _genderPalette == null)
			{
				throw new InvalidOperationException("BedStatusPalette and GenderPalette must be set before calling this method.");
			}

			_hospitalFloorData = new Dictionary<string, MapElementViewModel>(elements.Count());

			IEnumerable<BedData> floorData = BedData.Load();
			int freeBedsCount = 0;
			foreach (string id in elements)
			{
				string id1 = id;
				BedData data = floorData.FirstOrDefault(x => x.Id == id1);
				if (data != null && data.BedStatus == BedStatus.Free)
				{
					++freeBedsCount;
				}
				_hospitalFloorData.Add(id, new MapElementViewModel(id, data, BedStatusPalette, GenderPalette));
			}

			MapElements = _hospitalFloorData.Values;

			Filter = new FilterViewModel(_hospitalFloorData.Where(x => !x.Value.IsStub).Select(x => x.Value.BedData));
			Filter.FilterChanged += OnFilterChanged;

			TotalNumberOfBeds= _hospitalFloorData.Count;
			TotalNumberOfFreeBeds = freeBedsCount;
		}
        public void RegisterElements(HospitalElementList elements)
        {
            if (_bedStatusPalette == null || _genderPalette == null)
            {
                throw new InvalidOperationException("BedStatusPalette and GenderPalette must be set before calling this method.");
            }

            _hospitalFloorData = new Dictionary<string, MapElementViewModel>(elements.Count());

            IEnumerable<BedData> floorData = BedData.Load();
            int freeBedsCount = 0;
            foreach (var element in elements)
            {
                string id = element.Id;
                BedData data = floorData.FirstOrDefault(x => x.Id == id);
                if (data != null)
                {
                    data.BedShape = element.Shape;
                    if (data.BedStatus == BedStatus.Free)
                    {
                        data.BedShape = element.Shape;
                        ++freeBedsCount;
                    }
                    _hospitalFloorData.Add(id, new MapElementViewModel(id, data, BedStatusPalette, GenderPalette));
                }
            }

            MapElements = _hospitalFloorData.Values;

            Filter = new FilterViewModel(_hospitalFloorData.Where(x => !x.Value.IsStub).Select(x => x.Value.BedData));
            Filter.FilterChanged += OnFilterChanged;

            TotalNumberOfBeds = _hospitalFloorData.Count;
            TotalNumberOfFreeBeds = freeBedsCount;
        }
		/// <summary>
		/// Selects the element.
		/// </summary>
		/// <param name="id">The id.</param>
		public void SelectElement(string id)
		{
			//NOTE: _hospitalFloorData always contains id because _hospitalFloorData is built 
			// using all the elements from the layer
			SelectedElement = _hospitalFloorData[id].BedData;
		}

		/// <summary>
		/// Unselects the element.
		/// </summary>
		public void UnselectElement()
		{
			SelectedElement = null;
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Called when [filter changed].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void OnFilterChanged(object sender, EventArgs e)
		{
			//Setting a new collection to the DataSource of the map layer forces the control to redraw the maps elements.
			MapElementViewModel[] newElements = new MapElementViewModel[_hospitalFloorData.Count];
			int i = 0;
			foreach (var keyValuePair in _hospitalFloorData)
			{
				MapElementViewModel mevm = keyValuePair.Value;
				if (!mevm.IsStub)
				{
					mevm.IsFilteredOut = !(Filter.FilterItem(mevm.BedData));
				}
				newElements[i] = mevm;
				++i;
			}
			MapElements = newElements;
		}
		#endregion Private Methods

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
