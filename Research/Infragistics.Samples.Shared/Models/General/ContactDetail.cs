namespace Infragistics.Samples.Shared.Models.General
{
    public class ContactDetail : ObservableModel
    {

        public ContactDetail()
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
    }
}
