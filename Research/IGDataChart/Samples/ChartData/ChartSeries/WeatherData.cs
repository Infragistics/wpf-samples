using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using IGDataChart.Resources;

namespace Infragistics.Samples
{
    public class DataItem
    {
        public DateTime Date { get; set; }
        public double temperatureHigh { get; set; }
        public double temperatureLow { get; set; }
        public double averageTemperature { get; set; }
        public string Label { get; set; }
        public int DayOfMonth { get; set; }
    }

    public class WeatherData : ObservableCollection<DataItem>
    {
        public WeatherData()
        {
            ResourceManager dataChartStringResx = DataChartStrings.ResourceManager;

            this.Add(new DataItem { DayOfMonth = 1, Label = dataChartStringResx.GetString("XWDC_WeatherData_Thursday"), Date = DateTime.Today, temperatureHigh = 59, temperatureLow = 46, averageTemperature = 52.5 });
            this.Add(new DataItem { DayOfMonth = 2, Label = dataChartStringResx.GetString("XWDC_WeatherData_Friday"), Date = DateTime.Today.AddDays(1), temperatureHigh = 74, temperatureLow = 43, averageTemperature = 52.5 });
            this.Add(new DataItem { DayOfMonth = 3, Label = dataChartStringResx.GetString("XWDC_WeatherData_Saturday"), Date = DateTime.Today.AddDays(2), temperatureHigh = 68, temperatureLow = 46, averageTemperature = 57 });
            this.Add(new DataItem { DayOfMonth = 4, Label = dataChartStringResx.GetString("XWDC_WeatherData_Sunday"), Date = DateTime.Today.AddDays(3), temperatureHigh = 78, temperatureLow = 57, averageTemperature = 67.5 });
            this.Add(new DataItem { DayOfMonth = 5, Label = dataChartStringResx.GetString("XWDC_WeatherData_Monday"), Date = DateTime.Today.AddDays(4), temperatureHigh = 83, temperatureLow = 64, averageTemperature = 73.5 });
            this.Add(new DataItem { DayOfMonth = 6, Label = dataChartStringResx.GetString("XWDC_WeatherData_Tuesday"), Date = DateTime.Today.AddDays(5), temperatureHigh = 87, temperatureLow = 67, averageTemperature = 77 });
            this.Add(new DataItem { DayOfMonth = 7, Label = dataChartStringResx.GetString("XWDC_WeatherData_Wednesday"), Date = DateTime.Today.AddDays(6), temperatureHigh = 86, temperatureLow = 66, averageTemperature = 76 });
            this.Add(new DataItem { DayOfMonth = 8, Label = dataChartStringResx.GetString("XWDC_WeatherData_Thursday"), Date = DateTime.Today.AddDays(7), temperatureHigh = 87, temperatureLow = 65, averageTemperature = 76 });
            this.Add(new DataItem { DayOfMonth = 9, Label = dataChartStringResx.GetString("XWDC_WeatherData_Friday"), Date = DateTime.Today.AddDays(8), temperatureHigh = 85, temperatureLow = 59, averageTemperature = 72 });
            this.Add(new DataItem { DayOfMonth = 10, Label = dataChartStringResx.GetString("XWDC_WeatherData_Saturday"), Date = DateTime.Today.AddDays(9), temperatureHigh = 76, temperatureLow = 54, averageTemperature = 65 });
            this.Add(new DataItem { DayOfMonth = 11, Label = dataChartStringResx.GetString("XWDC_WeatherData_Sunday"), Date = DateTime.Today.AddDays(10), temperatureHigh = 75, temperatureLow = 63, averageTemperature = 69 });
            this.Add(new DataItem { DayOfMonth = 12, Label = dataChartStringResx.GetString("XWDC_WeatherData_Monday"), Date = DateTime.Today.AddDays(11), temperatureHigh = 83, temperatureLow = 63, averageTemperature = 78 });
            this.Add(new DataItem { DayOfMonth = 13, Label = dataChartStringResx.GetString("XWDC_WeatherData_Tuesday"), Date = DateTime.Today.AddDays(12), temperatureHigh = 79, temperatureLow = 54, averageTemperature = 66.5 });
            this.Add(new DataItem { DayOfMonth = 14, Label = dataChartStringResx.GetString("XWDC_WeatherData_Wednesday"), Date = DateTime.Today.AddDays(13), temperatureHigh = 82, temperatureLow = 66, averageTemperature = 74 });
        }
    }

    public abstract class ObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
