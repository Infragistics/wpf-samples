using System;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Models
{
    public class ContactDetails : ObservableModel
    {
        #region ContactInfo
        private Contact _contactInfo;
        public Contact ContactInfo
        {
            get
            {
                return _contactInfo;
            }
            set
            {
                if (_contactInfo != value)
                {
                    _contactInfo = value;
                    this.OnPropertyChanged("ContactInfo");
                }
            }
        }
        #endregion
        #region Birthday
        private DateTime _birthday;
        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    this.OnPropertyChanged("Birthday");
                }
            }
        }
        #endregion
        #region Reminder
        private bool _reminder;
        public bool Reminder
        {
            get
            {
                return _reminder;
            }
            set
            {
                if (_reminder != value)
                {
                    _reminder = value;
                    this.OnPropertyChanged("Reminder");
                }
            }
        }
        #endregion
    }
    public class Contact : ObservableModel, IComparable<Contact>
    {
        #region NickName
        private string _nickName;
        public string NickName
        {
            get
            {
                return _nickName;
            }
            set
            {
                if (_nickName != value)
                {
                    _nickName = value;
                    this.OnPropertyChanged("NickName");
                }
            }
        }
        #endregion
        #region FullName
        private string _fullName;
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    this.OnPropertyChanged("FullName");
                }
            }
        }
        #endregion

        #region IComparable Members
        public int CompareTo(Contact other)
        {
            if (other == null) return 1;

            if (other != null)
            {
                return this.NickName.CompareTo(other.NickName);
            }
            else
            {
                throw new ArgumentException("Object is not a Contact!");
            }
        }
        #endregion
    }
    public class Coworker : Contact
    {
        #region Department
        private string _department;
        public string Department
        {
            get
            {
                return _department;
            }
            set
            {
                if (_department != value)
                {
                    _department = value;
                    this.OnPropertyChanged("Department");
                }
            }
        }
        #endregion
    }
    public class Family : Contact
    {
        #region Relation
        private string _relation;
        public string Relation
        {
            get
            {
                return _relation;
            }
            set
            {
                if (_relation != value)
                {
                    _relation = value;
                    this.OnPropertyChanged("Relation");
                }
            }
        }
        #endregion
    }
    public class Friend : Contact
    {
        #region Email
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    this.OnPropertyChanged("Email");
                }
            }
        }
        #endregion
    }
}
