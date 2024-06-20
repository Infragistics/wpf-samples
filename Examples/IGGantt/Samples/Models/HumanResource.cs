using Infragistics.Samples.Shared.Models;

namespace IGGantt.Samples.Models
{
    public class HumanResource : ObservableModel
    {
        private string _personalID;
        public string PersonalID
        {
            get
            {
                return _personalID;
            }
            set
            {
                if (_personalID != value)
                {
                    _personalID = value;
                    this.OnPropertyChanged("PersonalID");
                }
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
    }
}
