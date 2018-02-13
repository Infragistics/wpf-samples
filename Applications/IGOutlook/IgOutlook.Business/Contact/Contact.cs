using IgOutlook.Business.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IgOutlook.Business.Contacts
{
    public class Contact : BusinessBase, IDataErrorInfo
    {
        public Contact()
        {
            errors = new Dictionary<string, string>();
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; NotifyPropertyChanged(); }
        }

        private Uri photoUri;
        public Uri PhotoUri
        {
            get { return photoUri; }
            set { photoUri = value; NotifyPropertyChanged(); }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; NotifyPropertyChanged(); }
        }

        private string middleName;
        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; NotifyPropertyChanged(); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; NotifyPropertyChanged(); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; NotifyPropertyChanged(); }
        }

        private string jobTitle;
        public string JobTitle
        {
            get { return jobTitle; }
            set { jobTitle = value; NotifyPropertyChanged(); }
        }

        private string company;
        public string Company
        {
            get { return company; }
            set { company = value; NotifyPropertyChanged(); }
        }

        private string department;
        public string Department
        {
            get { return department; }
            set { department = value; NotifyPropertyChanged(); }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set { notes = value; NotifyPropertyChanged(); }
        }

        private string fileAs;
        public string FileAs
        {
            get { return fileAs; }
            set { fileAs = value; NotifyPropertyChanged(); }
        }

        private string displayAs;

        public string DisplayAs
        {
            get { return displayAs; }
            set { displayAs = value; NotifyPropertyChanged(); }
        }
        private string phoneBusiness;

        public string PhoneBusiness
        {
            get { return phoneBusiness; }
            set { phoneBusiness = value; NotifyPropertyChanged(); }
        }
        private string phoneHome;

        public string PhoneHome
        {
            get { return phoneHome; }
            set { phoneHome = value; NotifyPropertyChanged(); }
        }
        private string phoneMobile;

        public string PhoneMobile
        {
            get { return phoneMobile; }
            set { phoneMobile = value; NotifyPropertyChanged(); }
        }
        private string adressBusiness;

        public string AdressBusiness
        {
            get { return adressBusiness; }
            set { adressBusiness = value; NotifyPropertyChanged(); }
        }

        #region IDataErrorInfo

        public string Error
        {
            get;
            private set;
        }

        public string this[string propertyName]
        {
            get
            {
                Error = string.Empty;

                switch (propertyName)
                {
                    //TODO: The rest of the properties need to be validated as well 
              //      case "Id": Error = string.IsNullOrEmpty(id) ? "Id is not valid" : null; break;
                    case "FirstName": Error = string.IsNullOrEmpty(firstName) ? propertyName + "is not valid" : null; break;
                    case "MiddleName": Error = string.IsNullOrEmpty(middleName) ? propertyName + "is not valid" : null; break;
                    case "LastName": Error = string.IsNullOrEmpty(lastName) ? propertyName + "is not valid" : null; break;
                    case "Email": Error = string.IsNullOrEmpty(email) ? propertyName + "is not valid" : null; break;
                    case "Company": Error = string.IsNullOrEmpty(company) ? propertyName + "is not valid" : null; break;
                    case "FileAs": Error = string.IsNullOrEmpty(fileAs) ? propertyName + "is not valid" : null; break;
                    default: break;
                }

                UpdateErrors(propertyName, Error);

                return Error;

            }
        }

        private Dictionary<string, string> errors;

        private void UpdateErrors(string propertyName, string errorMsg)
        {
            if (errors.ContainsKey(propertyName))
            {
                if (string.IsNullOrEmpty(errorMsg))
                    errors.Remove(propertyName);
                else
                    errors[propertyName] = errorMsg;
            }
            else
            {
                if (!string.IsNullOrEmpty(errorMsg))
                    errors.Add(propertyName, errorMsg);
            }
        }

        public bool HasErrors()
        {
            return errors.Count > 0;
        }

        #endregion //IDataErrorInfo



    }
}
