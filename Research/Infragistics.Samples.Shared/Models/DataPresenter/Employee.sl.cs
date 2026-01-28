using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Infragistics.Samples.Shared.Models.DataPresenter
{
    /// <summary>
    /// Summary description for Employee
    /// </summary>
    public class Employee : INotifyPropertyChanged
    {
        #region Member Variables

        private string _employeeName = string.Empty;
        private DateTime _originalHireDate = DateTime.Now;
        private Uri _photoFileUri;
        private string _department = string.Empty;
        private decimal _salary = 0.0M;
        private string _phoneNumber = string.Empty;
        private string _cellNumber = string.Empty;
        private string _address1 = string.Empty;
        private string _address2 = string.Empty;
        private string _city = string.Empty;
        private string _state = string.Empty;
        private string _zip = string.Empty;
        private string _email = string.Empty;
        private IEnumerable _history;
        private Image _photo = null;

        #endregion Member Variables

        public Employee() { }

        public Employee(string employeeName, DateTime originalHireDate, Uri photoFileUri, string department, decimal salary,
                        string phoneNumber, string cellNumber, string address1, string address2, string city,
                        string state, string zip, string email)
        {
            this._employeeName = employeeName;
            this._originalHireDate = originalHireDate;
            this._photoFileUri = photoFileUri;
            this._department = department;
            this._salary = salary;
            this._phoneNumber = phoneNumber;
            this._cellNumber = cellNumber;
            this._address1 = address1;
            this._address2 = address2;
            this._city = city;
            this._state = state;
            this._zip = zip;
            this._email = email;
        }

        public string Address1
        {
            get { return this._address1; }
            set
            {
                if (this._address1 != value)
                {
                    this._address1 = value;
                    NotifyPropertyChanged("Address1");
                }
            }
        }

        public string Address2
        {
            get { return this._address2; }
            set
            {
                if (this._address2 != value)
                {
                    this._address2 = value;
                    NotifyPropertyChanged("Address2");
                }
            }
        }

        public string City
        {
            get { return this._city; }
            set
            {
                if (this._city != value)
                {
                    this._city = value;
                    NotifyPropertyChanged("City");
                }
            }
        }

        public string Department
        {
            get { return this._department; }
            set
            {
                if (this._department != value)
                {
                    this._department = value;
                    NotifyPropertyChanged("Department");
                }
            }
        }

        public string CellNumber
        {
            get { return this._cellNumber; }
            set
            {
                if (this._cellNumber != value)
                {
                    this._cellNumber = value;
                    NotifyPropertyChanged("CellNumber");
                }
            }
        }

        public string Email
        {
            get { return this._email; }
            set
            {
                if (this._email != value)
                {
                    this._email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        public string EmployeeName
        {
            get { return this._employeeName; }
            set
            {
                if (this._employeeName != value)
                {
                    this._employeeName = value;
                    NotifyPropertyChanged("EmployeeName");
                }
            }
        }

        public string PhoneNumber
        {
            get { return this._phoneNumber; }
            set
            {
                if (this._phoneNumber != value)
                {
                    this._phoneNumber = value;
                    NotifyPropertyChanged("PhoneNumber");
                }
            }
        }

        public Uri PhotoFileUri
        {
            get { return this._photoFileUri; }
            set
            {
                if (this._photoFileUri != value)
                {
                    this._photoFileUri = value;
                    NotifyPropertyChanged("PhotoFileUri");
                }
            }
        }

        public Image Photo
        {
            get
            {
                if (this._photo == null)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(this._photoFileUri);
                    //image.StretchDirection = StretchDirection.Both;
                    image.Stretch = Stretch.UniformToFill;
                    image.Width = 48;
                    image.Height = 48;
                    image.Margin = new Thickness(4);
                    this._photo = image;
                }

                return this._photo;
            }
        }

        public string State
        {
            get { return this._state; }
            set
            {
                if (this._state != value)
                {
                    this._state = value;
                    NotifyPropertyChanged("State");
                }
            }
        }

        public string Zip
        {
            get { return this._zip; }
            set
            {
                if (this._zip != value)
                {
                    this._zip = value;
                    NotifyPropertyChanged("Zip");
                }
            }
        }

        public IEnumerable History
        {
            get { return this._history; }
            set
            {
                if (this._history != value)
                {
                    this._history = value;
                    NotifyPropertyChanged("History");
                }
            }
        }

        #region INotifyPropertyChanged Members

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
