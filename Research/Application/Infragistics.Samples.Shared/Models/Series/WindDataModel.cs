using System;
using System.Collections.ObjectModel;


namespace Infragistics.Samples.Shared.Models
{
    public class WindDataModel : ObservableModel
    {
        public WindDataModel()
        {
            _directionChange = 0;
            _speedModifier = 1;

            this.WindData2008 = new WindDataCollection();
            this.WindData2009 = new WindDataCollection();
            this.WindData2010 = new WindDataCollection();
      
            _data = new WindDataCollection();
            this.Generate();
        }
        
        public void Generate()
        {
            _originalData = new WindDataCollection
            {
                new WindDataPoint {Direction = 0, Speed = 10},
                new WindDataPoint {Direction = 45, Speed = 15},
                new WindDataPoint {Direction = 90, Speed = 10},
                new WindDataPoint {Direction = 135, Speed = 5},
                new WindDataPoint {Direction = 180, Speed = 10},
                new WindDataPoint {Direction = 225, Speed = 25},
                new WindDataPoint {Direction = 270, Speed = 10},
                new WindDataPoint {Direction = 315, Speed = 30},
                new WindDataPoint {Direction = 360, Speed = 10}
            };
            _data = _originalData.Clone();


            this.WindData2008.AddRange(this.Data.Clone());

            this.WindData2009.AddRange(this.WindData2008.Clone());
            foreach (WindDataPoint windDataPoint in WindData2009)
            {
                windDataPoint.Speed *= 1.5;
            }

            this.WindData2010.AddRange(this.WindData2009.Clone());
            foreach (WindDataPoint windDataPoint in WindData2010)
            {
                windDataPoint.Speed *= 1.5;
            }

        }
        public void Update()
        {
            if (_data == null) return;
            _data = _originalData.Clone();

            foreach(WindDataPoint wdp in _data)
            {
                wdp.Speed *= SpeedModifier;
                wdp.Direction += DirectionChange;
                wdp.Direction %= 360;

            }
        }
        #region Properties

        private double _directionChange;
        public double DirectionChange
        {
            get { return _directionChange; }
            set
            {
                if (_directionChange == value) return;
                _directionChange = value;
                this.Update();
                OnPropertyChanged("DirectionChange");
            }
        }

        private double _speedModifier;
        public double SpeedModifier
        {
            get { return _speedModifier; }
            set
            {
                if (_speedModifier == value) return;
                _speedModifier = value;
                this.Update(); 
                OnPropertyChanged("SpeedModifier");
            }
        } 
  
        private WindDataCollection _originalData;
        private WindDataCollection _data;
        public WindDataCollection Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _originalData = value;
                _data = value;
                this.Update();
                OnPropertyChanged("Data");
            }
        }

        public WindDataCollection WindData2008 { get; set; }
        public WindDataCollection WindData2009 { get; set; }
        public WindDataCollection WindData2010 { get; set; }
    
        #endregion
    }

    public class WindDataSettings : DataSettings 
    {
        #region Properties
        private double _directionMin;
        public double DirectionMin
        {
            get { return _directionMin; }
            set { if (_directionMin == value) return; _directionMin = value; OnPropertyChanged("DirectionMin"); }
        }

        private double _directionInterval;
        public double DirectionInterval
        {
            get { return _directionInterval; }
            set { if (_directionInterval == value) return; _directionInterval = value; OnPropertyChanged("DirectionInterval"); }
        }

        private double _speedMin;
        public double SpeedMin
        {
            get { return _speedMin; }
            set { if (_speedMin == value) return; _speedMin = value; OnPropertyChanged("SpeedMin"); }
        }
        private double _speedInterval;
        public double SpeedInterval
        {
            get { return _speedInterval; }
            set { if (_speedInterval == value) return; _speedInterval = value; OnPropertyChanged("SpeedInterval"); }
        }
        #endregion
  
    }

    public class WindDataCollection : ObservableCollection<WindDataPoint>
    {

        public string Label { get; set; }
        public WindDataCollection Clone()
        {
            WindDataCollection clone = new WindDataCollection();
            foreach (WindDataPoint dataPoint in this)
            {
                clone.Add(dataPoint.Clone());
            }
            return clone;
        }
    
        public void AddRange(ObservableCollection<WindDataPoint> newData)
        {
            foreach (WindDataPoint dataPoint in newData)
            {
                this.Add(dataPoint.Clone());
            }
        }
    }
    public class WindDataPoint : DataPoint
    {
        #region Constructor
        public WindDataPoint()
           : this(DateTime.Now, 0, 0, string.Empty)
        { }  

        public WindDataPoint(WindDataPoint newDataPoint)
        {
            Index = newDataPoint.Index;
            Label = newDataPoint.Label;
            _date = newDataPoint.Date;
            _direction = newDataPoint.Direction;
            _speed = newDataPoint.Speed;
        }
        public WindDataPoint(DateTime date, double direction, double speed, string label)
        {
            Label = label;
            _date = date;
            _direction = direction;
            _speed = speed;
        }
        public WindDataPoint(DateTime date, double direction, double speed)
            : this(date, direction, speed, string.Empty)
        { }

        public WindDataPoint(string date, double direction, double speed)
            : this(DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture),
                   direction, speed, string.Empty)
        { }

        public WindDataPoint(string date, double direction, double speed, string label)
            : this(DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture),
                direction, speed, label)
        { }

        
        #endregion

        #region Properties
 
        private double _direction;
        public double Direction
        {
            get { return _direction; }
            set
            {
                if (_direction == value) return;
                _direction = value;
                OnPropertyChanged("Direction");
            }
        }

        private double _speed;
        public double Speed
        {
            get { return _speed; }
            set
            {
                if (_speed == value) return;
                _speed = value;
                OnPropertyChanged("Speed");
            }
        }

        private DateTime _date;
        public DateTime Date { get { return _date; } set { if (_date == value) return; _date = value; OnPropertyChanged("Date"); } }
        
        #endregion  
        public string ToolTip { get { return this.ToString(); } }
        public WindDataPoint Clone()
        {
            return new WindDataPoint(this);
        } 
        public new string ToString()
        {
            return String.Format("{0}, Direction {1}, Speed {2}", Date, Direction, Speed);
        }

    }
 
}
