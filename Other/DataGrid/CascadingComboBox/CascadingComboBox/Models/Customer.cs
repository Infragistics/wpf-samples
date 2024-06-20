using CascadingComboBox.Utilities;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace CascadingComboBox.Models
{
    public class Customer : BindableBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                ZipCodes = new ObservableCollection<string>(ZipCodeData.GetZipCodes(value));
                SetProperty(ref _city, value);                
            }
        }

        private string _zipCode;
        public string ZipCode
        {
            get { return _zipCode; }
            set { SetProperty(ref _zipCode, value); }
        }

        private ObservableCollection<string> _zipCodes;
        public ObservableCollection<string> ZipCodes
        {
            get { return _zipCodes; }
            set { SetProperty(ref _zipCodes, value); }
        }
    }
}
