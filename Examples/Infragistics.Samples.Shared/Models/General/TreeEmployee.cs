using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models.General
{
    public class TreeEmployee : ObservableModel
    {

        public TreeEmployee()
        {
        }

        private string id = string.Empty;
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        private string imagePath = string.Empty;
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }
            set
            {
                if (this.imagePath != value)
                {
                    this.imagePath = value;
                    this.OnPropertyChanged("ImagePath");
                }
            }
        }

        private string name = string.Empty;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private string jobTitle = string.Empty;
        public string JobTitle
        {
            get
            {
                return this.jobTitle;
            }
            set
            {
                if (this.jobTitle != value)
                {
                    this.jobTitle = value;
                    this.OnPropertyChanged("JobTitle");
                }
            }
        }

        private string employeeSince = string.Empty;
        public string EmployeeSince
        {
            get
            {
                return this.employeeSince;
            }
            set
            {
                if (this.employeeSince != value)
                {
                    this.employeeSince = value;
                    this.OnPropertyChanged("EmployeeSince");
                }
            }
        }

        private string managerName = string.Empty;
        public string ManagerName
        {
            get
            {
                return this.managerName;
            }
            set
            {
                if (this.managerName != value)
                {
                    this.managerName = value;
                    this.OnPropertyChanged("ManagerName");
                }
            }
        }

        private IList<TreeEmployee> directReports = new List<TreeEmployee>();
        public IList<TreeEmployee> DirectReports
        {
            get
            {
                return this.directReports;
            }
            set
            {
                if (this.directReports != value)
                {
                    this.directReports = value;
                    this.OnPropertyChanged("DirectReports");
                }
            }
        }
    }
}