using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class Employee : ObservableModel
    {

        public Employee()
        {
            _directReports = new List<Employee>(); this.Subordinates = new ObservableCollection<Employee>();
        }

        private string _id = string.Empty;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

		private string _imagePath = string.Empty;
		public string ImagePath
		{
			get
			{
				return _imagePath;
			}
			set
			{
				if (_imagePath != value)
				{
					_imagePath = value;
					this.OnPropertyChanged("ImagePath");
				}
			}
		}

        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private string _jobTitle = string.Empty;
        public string JobTitle
        {
            get
            {
                return _jobTitle;
            }
            set
            {
                if (_jobTitle != value)
                {
                    _jobTitle = value;
                    this.OnPropertyChanged("JobTitle");
                }
            }
        }

        private string _employeeSince = string.Empty;
        public string EmployeeSince
        {
            get
            {
                return _employeeSince;
            }
            set
            {
                if (_employeeSince != value)
                {
                    _employeeSince = value;
                    this.OnPropertyChanged("EmployeeSince");
                }
            }
        }
        
        private string _managerName = string.Empty;
        public string ManagerName
        {
            get
            {
                return _managerName;
            }
            set
            {
                if (_managerName != value)
                {
                    _managerName = value;
                    this.OnPropertyChanged("ManagerName");
                }
            }
        }
        
        private IList<Employee> _directReports;
        public IList<Employee> DirectReports
        {
            get
            {
                return _directReports;
            }
            set
            {
                if (_directReports != value)
                {
                    _directReports = value;
                    this.OnPropertyChanged("DirectReports");
                }
            }
        }

        private bool _hasHealthInsurance;
        public bool HasHealthInsurance
        {
            get
            {
                return _hasHealthInsurance;
            }
            set
            {
                if (_hasHealthInsurance != value)
                {
                    _hasHealthInsurance = value;
                    this.OnPropertyChanged("HasHealthInsurance");
                }
            }
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string _fullName;
        public string FullName
        {
            get
            {
                return string.IsNullOrEmpty(_fullName) ? this.FirstName + " " + this.LastName : _fullName;
            }
            set
            {
                _fullName = value;
            }
        }

        public ObservableCollection<Employee> Subordinates { get; set; }
    }
 
}
