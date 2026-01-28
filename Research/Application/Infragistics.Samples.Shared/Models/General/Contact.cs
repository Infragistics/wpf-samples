using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models.General
{
    public class Contact : ObservableModel
    {

        public Contact()
        {
        }

        private string name;
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


        private IEnumerable<ContactDetail> contactDetails;
        public IEnumerable<ContactDetail> ContactDetails
        {
            get
            {
                return this.contactDetails;
            }
            set
            {
                if (this.contactDetails != value)
                {
                    this.contactDetails = value;
                    this.OnPropertyChanged("ContactDetails");
                }
            }
        }

    }
}
