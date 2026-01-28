using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
    public class PatientDataSource : ObservableModel 
    {
        private IEnumerable<PatientAdmittance> _patients;
        public IEnumerable<PatientAdmittance> Patients
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
    }
}
