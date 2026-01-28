namespace Infragistics.Samples.Shared.Models.Medical
{
    public class SeverityLevel : ObservableModel
    {
        public SeverityLevel()
        {
        }

        public SeverityLevel(string level)
        {
            _level = level;
        }
        private string _level;
        public string Level
        {
            get
            {
                return _level;
            }
            set
            {
                if (_level != value)
                {
                    _level = value;
                    this.OnPropertyChanged("Level");
                }
            }
        }
    }
}
