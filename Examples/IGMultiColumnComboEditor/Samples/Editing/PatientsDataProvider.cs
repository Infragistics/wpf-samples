using IGMultiColumnComboEditor.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Models.Medical;
using Infragistics.Samples.Shared.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace IGMultiColumnComboEditor.Samples.Editing
{
    public class PatientsDataProvider : ObservableModel
    {
        public PatientsDataProvider()
        {
            DownloadDataSource();
        }

        private ObservableCollection<PatientAdmittance> _patients = null;
        public ObservableCollection<PatientAdmittance> Patients
        {
            get
            {
                return this._patients;
            }
            set
            {
                if (this._patients != value)
                {
                    this._patients = value;
                    this.OnPropertyChanged("Patients");
                }
            }
        }

        private ObservableCollection<SeverityLevel> _severityLevels = null;
        public ObservableCollection<SeverityLevel> SeverityLevels
        {
            get
            {
                return this._severityLevels;
            }
            set
            {
                if (this._severityLevels != value)
                {
                    this._severityLevels = value;
                    this.OnPropertyChanged("SeverityLevels");
                }
            }
        }

        private ObservableCollection<Room> _locations = null;
        public ObservableCollection<Room> Locations
        {
            get
            {
                return this._locations;
            }
            set
            {
                if (this._locations != value)
                {
                    this._locations = value;
                    this.OnPropertyChanged("Locations");
                }
            }
        }

        private PatientAdmittance _selectedPatient;
        public PatientAdmittance SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                if (_selectedPatient != value)
                {
                    _selectedPatient = value;
                    this.OnPropertyChanged("SelectedPatient");
                }
            }
        }

        private void GetSeverityLevels()
        {
            var collection = new ObservableCollection<SeverityLevel>();

            collection.Add(new SeverityLevel(CommonStrings.XG_Severity_Low));
            collection.Add(new SeverityLevel(CommonStrings.XG_Severity_Medium));
            collection.Add(new SeverityLevel(CommonStrings.XG_Severity_High));

            this.SeverityLevels = collection;
        }

        private void DownloadDataSource()
        {
            var dataProvider = new Infragistics.Samples.Shared.DataProviders.XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Patients.xml");

            GetSeverityLevels();

            SetSelectedDataPropertiesOptions();
        }

        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("PatientAdmittance")
                              select new PatientAdmittance
                              {
                                  AdmittanceId = d.Attribute("AdmittanceId").GetString(),
                                  FirstName = d.Attribute("FirstName").GetString(),
                                  LastName = d.Attribute("LastName").GetString(),
                                  DOB = d.Attribute("DOB").GetDateTime(),
                                  Gender = d.Attribute("Gender").GetString(),
                                  AdmittanceDate = d.Attribute("AdmittanceDate").GetDateTime(),
                                  Location = d.Attribute("Location").GetString(),
                                  Severity = d.Attribute("Severity").GetString(),
                                  TakeMedications = d.Attribute("TakeMedications").GetBool(),
                                  HasAllergies = d.Attribute("HasAllergies").GetBool()
                              });

            this.Patients = new ObservableCollection<PatientAdmittance>(dataSource.ToList());

            GetDistinctLocations(this.Patients);
        }

        private void GetDistinctLocations(ObservableCollection<PatientAdmittance> patients)
        {
            var locations = new ObservableCollection<Room>();

            var rooms = (from rs in patients
                         orderby rs.Location ascending
                         select rs.Location).Distinct().ToList();

            foreach (string room in rooms)
            {
                locations.Add(new Room() { RoomName = room });
            }

            this.Locations = locations;
        }

        private List<SelectedDataProperty> _propOptions = null;
        public List<SelectedDataProperty> PropOptions
        {
            get
            {
                return this._propOptions;
            }
            set
            {
                if (this._propOptions != value)
                {
                    this._propOptions = value;
                    this.OnPropertyChanged("PropOptions");
                }
            }
        }

        private void SetSelectedDataPropertiesOptions()
        {
            var options = new List<SelectedDataProperty>();

            options.Add(new SelectedDataProperty("HasAllergies", MultiColumnComboEditorStrings.CE_Allergies));
            options.Add(new SelectedDataProperty("TakeMedications", MultiColumnComboEditorStrings.CE_ExtaMeds));

            this.PropOptions = options;
        }
    }

    public class SelectedDataProperty : ObservableModel
    {
        public SelectedDataProperty(string id, string name)
        {
            _identifier = id;
            _name = name;
        }

        private string _identifier;
        public string Identifier
        {
            get
            {
                return this._identifier;
            }
            set
            {
                if (this._identifier != value)
                {
                    this._identifier = value;
                    this.OnPropertyChanged("Identifier");
                }
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
    }
}
