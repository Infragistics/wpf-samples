using System.Collections.Generic;
using Infragistics.Samples.Shared.Models;
using Infragistics.Undo;

namespace IGUndoRedoFramework.Model
{
    public class Customer : ObservableModel
    {
        #region Members
        #region Owner
        private object _owner;
        internal object Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        #endregion
        #region CustomerID
        private string _customerID;
        public string CustomerID
        {
            get { return _customerID; }
            set { this.SetField(ref _customerID, value, "CustomerID"); }
        }
        #endregion
        #region Company
        private string _company;
        public string Company
        {
            get { return _company; }
            set { this.SetField(ref _company, value, "Company"); }
        }
        #endregion
        #region ContactName
        private string _contactName;
        public string ContactName
        {
            get { return _contactName; }
            set { this.SetField(ref _contactName, value, "ContactName"); }
        }
        #endregion
        #region ContactTitle
        private string _contactTitle;
        public string ContactTitle
        {
            get { return _contactTitle; }
            set { this.SetField(ref _contactTitle, value, "ContactTitle"); }
        }
        #endregion
        #region Country
        private string _country;
        public string Country
        {
            get { return _country; }
            set { this.SetField(ref _country, value, "Country"); }
        }
        #endregion
        #endregion
        #region Methods
        #region SetField
        protected bool SetField<T>(ref T member, T newValue, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(member, newValue))
                return false;

            if (_owner != null)
                UndoManager.FromReference(_owner).AddPropertyChange(this, propertyName, member, newValue);

            member = newValue;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
        #endregion
    }
}
