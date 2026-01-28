namespace Infragistics.Samples.Shared.Models.General
{
    public class City : ObservableModel
    {

        public City()
        {
        }

        private string cityName;
        public string CityName
        {
            get
            {
                return this.cityName;
            }
            set
            {
                if (this.cityName != value)
                {
                    this.cityName = value;
                    this.OnPropertyChanged("CityName");
                }
            }
        }
    }
}