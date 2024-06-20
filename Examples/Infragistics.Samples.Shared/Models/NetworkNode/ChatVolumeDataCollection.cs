using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class ChatVolumeDataCollection : ObservableCollection<ChatVolumeDataPoint>
    {
        public ChatVolumeDataCollection()
        {
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 0.4 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "02:00", ChatsPerHour = 0.4 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 0.3 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "04:00", ChatsPerHour = 0.2 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 0.1 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "06:00", ChatsPerHour = 0.2 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 0.4 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "08:00", ChatsPerHour = 0.5 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 0.5 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "10:00", ChatsPerHour = 1.1 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 0.8 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "12:00", ChatsPerHour = 0.7 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 0.4 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "14:00", ChatsPerHour = 0.8 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 0.9 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "16:00", ChatsPerHour = 1.3 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 1.5 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "18:00", ChatsPerHour = 1.8 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 1.8 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "20:00", ChatsPerHour = 1.7 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 1.3 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "22:00", ChatsPerHour = 0.8 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "", ChatsPerHour = 0.4 });
            this.Add(new ChatVolumeDataPoint { HourOfDay = "24:00", ChatsPerHour = 0.5 });
        }
    }

    public class ChatVolumeDataPoint
    {
        public string HourOfDay { get; set; }
        public double ChatsPerHour { get; set; }
    }
}
