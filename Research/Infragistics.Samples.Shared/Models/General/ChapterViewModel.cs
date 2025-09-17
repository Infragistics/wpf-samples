namespace Infragistics.Samples.Shared.Models.General
{
    public class ChapterViewModel : ObservableModel
    {
        public ChapterViewModel()
        {
        }

        private string name = string.Empty;
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
    }
}