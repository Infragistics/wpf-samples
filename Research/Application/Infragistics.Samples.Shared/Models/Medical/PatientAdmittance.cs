using System;

namespace Infragistics.Samples.Shared.Models
{
    public class PatientAdmittance : ObservableModel
	{
		public PatientAdmittance()
		{
		}

		private string _admittanceId;
		public string AdmittanceId
		{
			get
			{
				return _admittanceId;
			}
			set
			{
				if (_admittanceId != value)
				{
					_admittanceId = value;
					this.OnPropertyChanged("AdmittanceId");
				}
			}
		}

		private string _firstName;
		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				if (_firstName != value)
				{
					_firstName = value;
					this.OnPropertyChanged("FirstName");
				}
			}
		}

		private string _lastName;
		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				if (_lastName != value)
				{
					_lastName = value;
					this.OnPropertyChanged("LastName");
				}
			}
		}

        public string FullName
        {
            get
            {
                return _firstName + " " + _lastName;
            }
        }
		
		private DateTime _dateOfBirth;
		public DateTime DOB
		{
			get
			{
				return _dateOfBirth;
			}
			set
			{
				if (_dateOfBirth != value)
				{
					_dateOfBirth = value;
					this.OnPropertyChanged("DOB");
				}
			}
		}

		private string _gender;
		public string Gender
		{
			get
			{
				return _gender;
			}
			set
			{
				if (_gender != value)
				{
					_gender = value;
					this.OnPropertyChanged("Gender");
				}
			}
		}

		private DateTime _admittanceDate;
		public DateTime AdmittanceDate
		{
			get
			{
				return _admittanceDate;
			}
			set
			{
				if (_admittanceDate != value)
				{
					_admittanceDate = value;
					this.OnPropertyChanged("AdmittanceDate");
				}
			}
		}
		
		private string _location;
		public string Location
		{
			get
			{
				return _location;
			}
			set
			{
				if (_location != value)
				{
					_location = value;
					this.OnPropertyChanged("Location");
				}
			}
		}

		private string _severity;
		public string Severity
		{
			get
			{
				return _severity;
			}
			set
			{
				if (_severity != value)
				{
					_severity = value;
					this.OnPropertyChanged("Severity");
				}
			}
		}

        private bool _takeMedications;
        public bool TakeMedications
        {
            get
            {
                return _takeMedications;
            }
            set
            {
                if (_takeMedications != value)
                {
                    _takeMedications = value;
                    this.OnPropertyChanged("TakeMedications");
                }
            }
        }

        private bool _hasAllergies;
        public bool HasAllergies
        {
            get
            {
                return _hasAllergies;
            }
            set
            {
                if (_hasAllergies != value)
                {
                    _hasAllergies = value;
                    this.OnPropertyChanged("HasAllergies");
                }
            }
        }
	}
}
