using System;
using System.Collections.ObjectModel;


namespace Infragistics.Samples.Shared.Models 
{
    public class EconomicDataModel : ObservableModel
    {
        public EconomicDataModel()
        {
            _data = new EconomicDataCollection();
        }

        private EconomicDataCollection _data;
        public EconomicDataCollection Data
        {
            get
            {
                return _data;
            }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    this.OnPropertyChanged("Data");
                }

            }
        }
    }
    public class EconomicDataCollection : ObservableCollection<EconomicDataPoint>
    {

    }

    public class EconomicDataPoint : DataPoint
    {
        private double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                if (_price == value) return;
                _price = value;
                OnPropertyChanged("Price");
            }
        }
        private double _demand;
        public double Demand
        {
            get { return _demand; }
            set
            {
                if (_demand == value) return;
                _demand = value;
                OnPropertyChanged("Demand");
            }
        }
        private double _supply;
        public double Supply
        {
            get { return _supply; }
            set
            {
                if (_supply == value) return;
                _supply = value;
                OnPropertyChanged("Supply");
            }
        }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date == value) return;
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        
    }
}