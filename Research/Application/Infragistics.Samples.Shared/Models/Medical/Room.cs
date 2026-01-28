namespace Infragistics.Samples.Shared.Models.Medical
{
    public class Room : ObservableModel
    {
        public Room()
        {
        }

        private string _roomName;
        public string RoomName
        {
            get { return _roomName; }
            set
            {
                if (_roomName != value)
                {
                    _roomName = value;
                    this.OnPropertyChanged("RoomName");
                }
            }
        }
    }
}
