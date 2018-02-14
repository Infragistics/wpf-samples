using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using IGShowcase.HospitalFloorPlan.Models;

namespace IGShowcase.HospitalFloorPlan.ViewModels
{
	public sealed class FilterViewModel : INotifyPropertyChanged
	{
		private static readonly DateTime Today = new DateTime(2010,2,12);

		#region Private fields
		private readonly IEnumerable<string> _allPatientNames;
		private string _searchByName;
		private int _minDays;
		private int _maxDays;
		private bool _hasVitalSignsMonitor;
		private bool _hasXRay;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="FilterViewModel"/> class.
		/// </summary>
		/// <param name="bedData">The bed data.</param>
		public FilterViewModel(IEnumerable<BedData> bedData)
		{
			_minDays = 1;
			_maxDays = 30;
			var allPatientNames = new List<string>();

			var freeCount = 0; 
			var assignedCount = 0; 
			var occupiedCount = 0; 
			var pendingCount = 0;

			var maleCount = 0; 
			var femaleCount = 0; 
			var unknownCount = 0;

			foreach (var data in bedData)
			{
				var bs = data.BedStatus;

				if (bs == BedStatus.Free)
				{
					++freeCount;
				}
				else
				{
					allPatientNames.Add(data.Patient.Name);
					if (bs == BedStatus.Assigned) { ++assignedCount; }
					else if (bs == BedStatus.Occupied) { ++occupiedCount; }
					else if (bs == BedStatus.PendingDischarge) { ++pendingCount; }
					else throw new ArgumentOutOfRangeException();

					switch (data.Patient.Gender)
					{
						case Gender.Male: ++maleCount; break;
						case Gender.Female: ++femaleCount; break;
						case Gender.Unknown: ++unknownCount; break;
						default: throw new ArgumentOutOfRangeException();
					}
				}
			}

			_allPatientNames = allPatientNames;

			BedStatusFilter = new[]
				{
					new BedStatusFilterItem(BedStatus.Free, freeCount, this),
					new BedStatusFilterItem(BedStatus.Assigned, assignedCount, this),
					new BedStatusFilterItem(BedStatus.Occupied, occupiedCount, this),
					new BedStatusFilterItem(BedStatus.PendingDischarge, pendingCount, this)
				};

			GenderFilter = new[]
				{
					new GenderFilterItem(Gender.Male, maleCount, this), 
					new GenderFilterItem(Gender.Female, femaleCount, this), 
					new GenderFilterItem(Gender.Unknown, unknownCount, this)
				};
		}
		#endregion

		#region Public members
		/// <summary>
		/// Gets all patient names.
		/// </summary>
		/// <value>All patient names.</value>
		public IEnumerable<string> AllPatientNames
		{
			get { return _allPatientNames; }
		}

		/// <summary>
		/// Gets or sets the name of the search by.
		/// </summary>
		/// <value>The name of the search by.</value>
		public string SearchByName
		{
			get { return _searchByName; }
			set
			{
				if (_searchByName == value) return;

				_searchByName = value;
				OnPropertyChanged("SearchByName");
				OnFilterChanged();
			}
		}

		/// <summary>
		/// Gets or sets the bed status filter.
		/// </summary>
		/// <value>The bed status filter.</value>
		public BedStatusFilterItem[] BedStatusFilter { get; private set; }

		/// <summary>
		/// Gets or sets the gender filter.
		/// </summary>
		/// <value>The gender filter.</value>
		public GenderFilterItem[] GenderFilter { get; private set; }

		/// <summary>
		/// Gets or sets the min days.
		/// </summary>
		/// <value>The min days.</value>
		public int MinDays
		{
			get { return _minDays; }
			set
			{
				if (_minDays == value) return;

				_minDays = value;
				OnPropertyChanged("MinDays");
				OnFilterChanged();
			}
		}

		/// <summary>
		/// Gets or sets the max days.
		/// </summary>
		/// <value>The max days.</value>
		public int MaxDays
		{
			get { return _maxDays; }
			set
			{
				if (_maxDays == value) return;

				_maxDays = value;
				OnPropertyChanged("MaxDays");
				OnFilterChanged();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance has vital signs monitor.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has vital signs monitor; otherwise, <c>false</c>.
		/// </value>
		public bool HasVitalSignsMonitor
		{
			get { return _hasVitalSignsMonitor; }
			set
			{
				if (_hasVitalSignsMonitor == value) return;

				_hasVitalSignsMonitor = value;
				OnPropertyChanged("HasVitalSignsMonitor");
				OnFilterChanged();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance has X ray.
		/// </summary>
		/// <value><c>true</c> if this instance has X ray; otherwise, <c>false</c>.</value>
		public bool HasXRay
		{
			get { return _hasXRay; }
			set
			{
				if (_hasXRay == value) return;

				_hasXRay = value;
				OnPropertyChanged("HasXRay");
				OnFilterChanged();
			}
		}
		#endregion Public member

		#region Public Events
		/// <summary>
		/// Occurs when [filter changed].
		/// </summary>
		public event EventHandler FilterChanged;
		#endregion Public Events

		#region Public Methods
		/// <summary>
		/// Filters the item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public bool FilterItem(BedData item)
		{
			bool result = BedStatusFilter.Any(x => x.IsChecked && x.BedStatus == item.BedStatus);

			if(item.BedStatus != BedStatus.Free)
			{
				result &= GenderFilter.Any(x => x.IsChecked && x.Gender == item.Patient.Gender);

				if(item.BedStatus != BedStatus.Assigned)
				{
					int days = (Today - item.Patient.AdmissionDate).Days;
					result &= days >= MinDays && days <= MaxDays;
				}

				if(SearchByName != null)
				{
					string searchByNameLower = _searchByName.ToLower();
					result &= item.Patient.Name.ToLower().Contains(searchByNameLower);
				}
			}

			result &= (!HasVitalSignsMonitor || item.HasVitalSignsMonitor) && (!HasXRay || item.HasXRay);

			return result;
		}
		#endregion Public Methods

		#region Private methods
		/// <summary>
		/// Called when [filter changed].
		/// </summary>
		private void OnFilterChanged()
		{
			EventHandler filterChanged = FilterChanged;
			if (filterChanged != null)
			{
				filterChanged(this, EventArgs.Empty);
			}
		}
		#endregion

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		public sealed class BedStatusFilterItem : INotifyPropertyChanged
		{
			#region Private Variables
			private bool _isChecked;
			private readonly FilterViewModel _vm;
			#endregion Private Variables

			#region Constructors
			/// <summary>
			/// Initializes a new instance of the <see cref="BedStatusFilterItem"/> class.
			/// </summary>
			/// <param name="bedStatus">The bed status.</param>
			/// <param name="count">The count.</param>
			/// <param name="vm">The vm.</param>
			public BedStatusFilterItem(BedStatus bedStatus, int count, FilterViewModel vm)
			{
				_vm = vm;
				_isChecked = true;
				BedStatus = bedStatus;
				Count = count;
			}
			#endregion Constructors

			#region Public Properties
			/// <summary>
			/// 
			/// </summary>
			public readonly BedStatus BedStatus;

			/// <summary>
			/// Gets or sets the count.
			/// </summary>
			/// <value>The count.</value>
			public int Count { get; private set; }

			/// <summary>
			/// Gets or sets a value indicating whether this instance is checked.
			/// </summary>
			/// <value>
			/// 	<c>true</c> if this instance is checked; otherwise, <c>false</c>.
			/// </value>
			public bool IsChecked
			{
				get { return _isChecked; }
				set
				{
					if (_isChecked == value) return;

					_isChecked = value;
					OnPropertyChanged("IsChecked");
					_vm.OnFilterChanged();
				}
			}
			#endregion Public Properties

			#region INotifyPropertyChanged Members

			public event PropertyChangedEventHandler PropertyChanged;

			private void OnPropertyChanged(string propertyName)
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				}
			}

			#endregion
		}

		public sealed class GenderFilterItem : INotifyPropertyChanged
		{
			#region Private Variables
			private bool _isChecked;
			private readonly FilterViewModel _vm;
			#endregion Private Variables

			#region Constructors
			/// <summary>
			/// Initializes a new instance of the <see cref="GenderFilterItem"/> class.
			/// </summary>
			/// <param name="gender">The gender.</param>
			/// <param name="count">The count.</param>
			/// <param name="vm">The vm.</param>
			public GenderFilterItem(Gender gender, int count, FilterViewModel vm)
			{
				_vm = vm;
				_isChecked = true;
				Gender = gender;
				Count = count;
			}
			#endregion Constructors

			#region Public Properties
			/// <summary>
			/// 
			/// </summary>
			public readonly Gender Gender;

			/// <summary>
			/// Gets or sets the count.
			/// </summary>
			/// <value>The count.</value>
			public int Count { get; private set; }

			/// <summary>
			/// Gets or sets a value indicating whether this instance is checked.
			/// </summary>
			/// <value>
			/// 	<c>true</c> if this instance is checked; otherwise, <c>false</c>.
			/// </value>
			public bool IsChecked
			{
				get { return _isChecked; }
				set
				{
					if (_isChecked == value) return;

					_isChecked = value;
					OnPropertyChanged("IsChecked");
					_vm.OnFilterChanged();
				}
			}
			#endregion Public Properties

			#region INotifyPropertyChanged Members

			public event PropertyChangedEventHandler PropertyChanged;

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
}
