using IGPropertyGrid.Resources;
using Infragistics;
using System.Collections.ObjectModel;
using System.ComponentModel;
 using System.Threading;

namespace IGPropertyGrid.Samples.DataModel
{
    // This class is used as a data source for the xamPropertyGrid.
    // It returns a different data source object depending of the current thread's culture.
    public class PersonInformation
    {
        public PersonInformationBase DataItems
        {
            get
            {
                if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
                {
                    return new PersonInformationJA();
                }
                else
                {
                    return new PersonInformationEN();
                }
            }
        }
    }

    public class PersonInformationBase : PropertyChangeNotifier
    {
        private PersonBasic _personBasic;
        private PersonPhoneNumbers _personPhoneNumbers;
        
        public PersonInformationBase()
        {
            this._personBasic = new PersonBasic()
            {
                FirstName = PropertyGridStrings.person1FirstName,
                LastName = PropertyGridStrings.person1LastName,
                Age = 34
            };

            this._personPhoneNumbers = new PersonPhoneNumbers();
            this._personPhoneNumbers.Add(new PhoneNumber() { Type = PropertyGridStrings.lblHome, Number = "0921-123465" });
            this._personPhoneNumbers.Add(new PhoneNumber() { Type = PropertyGridStrings.lblCell, Number = "0621-08460" });
            this._personPhoneNumbers.Add(new PhoneNumber() { Type = PropertyGridStrings.lblOffice, Number = "(171) 555-7788" });
        }

        public virtual PersonBasic BasicInformation
        {
            get
            {
                return this._personBasic;
            }
            set
            {
                this._personBasic = value;
                this.OnPropertyChanged("BasicInformation");
            }
        }

        public virtual PersonPhoneNumbers PhoneNumbers
        {
            get
            {
                return this._personPhoneNumbers;
            }
            set
            {
                this._personPhoneNumbers = value;
                this.OnPropertyChanged("PhoneNumbers");
            }
        }
    }

    // This class provides English localization information for the inherited public properties
    public class PersonInformationEN : PersonInformationBase
    {
        [DisplayName("Basic Information")]
        public override PersonBasic BasicInformation
        {
            get { return base.BasicInformation; }
            set { base.BasicInformation = value; }
        }

        [DisplayName("Phone Numbers")]
        public override PersonPhoneNumbers PhoneNumbers
        {
            get { return base.PhoneNumbers; }
            set { base.PhoneNumbers = value; }
        }
    }

    // This class provides Japanese localization information for the inherited public properties
    public class PersonInformationJA : PersonInformationBase
    {
        [DisplayName("基本情報")]
        public override PersonBasic BasicInformation
        {
            get { return base.BasicInformation; }
            set { base.BasicInformation = value; }
        }

        [DisplayName("電話番号")]
        public override PersonPhoneNumbers PhoneNumbers
        {
            get { return base.PhoneNumbers; }
            set { base.PhoneNumbers = value; }
        }
    }

    public class PersonBasic : PropertyChangeNotifier
    {
        private string _firstName;
        private string _lastName;
        private int _age;

        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this._firstName = value;
                this.OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                this._lastName = value;
                this.OnPropertyChanged("LastName");
            }
        }

        public int Age
        {
            get
            {
                return this._age;
            }
            set
            {
                this._age = value;
                this.OnPropertyChanged("Age");
            }
        }
    }

    public class PhoneNumber
    {
        public string Type { get; set; }
        public string Number { get; set; }
    }

    public class PersonPhoneNumbers : ObservableCollection<PhoneNumber>
    {
    }

}
