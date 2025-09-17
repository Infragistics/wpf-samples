using System;
using System.Collections.Generic;
using System.Windows;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents non-observable list of <see cref="FinacialDataItem"/> objects
    /// </summary> 
    public class FinancialData : List<FinacialDataItem>
    {


        public FinancialData()
        {
            _generateRandom = true;
            _generateCount = 100;
            Update();
        }

        public FinancialData(int itemsCount)
        {
            _generateRandom = true;
            _generateCount = itemsCount;
            Update();
        }

        public FinancialData(int itemsCount, double volumeStart, double volumeMultiplier)
        {
            _generateRandom = true;
            _generateCount = itemsCount;
            _volumeStart = volumeStart;
            _volumeMultiplier = volumeMultiplier;
            Update();
        }

        private double _volumeStart = 10000.0;
        private double _volumeMultiplier = 500.0;

        private bool _generateRandom;
        /// <summary> Gets or sets GenerateRandom </summary>
        public bool GenerateRandom
        {
            get { return _generateRandom; }
            set { if (_generateRandom == value) return; _generateRandom = value; Update(); }
        }

        private int _generateCount = 100;
        /// <summary> Gets or sets Count </summary>
        public int GenerateCount
        {
            get { return _generateCount; }
            set { if (_generateCount == value) return; _generateCount = value; Update(); }
        }
        

        public void Update()
        {
            if (GenerateRandom)
                Generate(this.GenerateCount);
            else
                this.Clear();
        }
        
        public void Generate(int points)
        { 
            var rand = new Random();

            var date = DateTime.Now.AddDays(-points);
            var high = 220.0;
            var low = 200.0;
            var volume = _volumeStart;
            var value = 200.0;
            var variance = 6; // rand.Next(2, 10);
            for (var i = 0; i < points; i++)
            {
                var item = new FinacialDataItem();
                item.Label = date.ToString("MMM dd, yyyy");
                item.High = high;
                item.Low = low;
                item.Date = date;
                item.Low = low;
                item.Volume = volume;
                item.Open = rand.Next((int)low, (int)high);
                item.Close = rand.Next((int)low, (int)high);
                 
                var increment = rand.NextDouble();
                if (increment <= 0.5)
                {
                    value -= (increment * 5);
                    volume -= (variance * _volumeMultiplier);
                }
                else
                {
                    value += (increment * 5);
                    volume += (variance * _volumeMultiplier);
                } 
                //increment = rand.NextDouble();
                //if (increment <= 0.75)
                //{
                //    low -= (increment * 5);
                //}
                //else
                //{
                //    low += (increment * 5);
                //}
                high = value + rand.Next(2, variance);
                low = value - rand.Next(2, variance);

                date = date.AddDays(1);

                Add(item);
            }
        }
    }

    /// <summary>
    /// Represents financial data point  
    /// </summary>
    public class FinacialDataItem : ObservableModel
    {
        public FinacialDataItem()
        {
        }

        private DateTime _date;
        /// <summary> Gets or sets Date </summary>
        public DateTime Date
        {
            get { return _date; }
            set { if (_date == value) return; _date = value; OnPropertyChanged("Date"); }
        }

        private double _volume;
        /// <summary> Gets or sets Value </summary>
        public double Volume
        {
            get { return _volume; }
            set { if (_volume == value) return; _volume = value; OnPropertyChanged("v"); }
        }

        private double _high;
        /// <summary> Gets or sets High </summary>
        public double High
        {
            get { return _high; }
            set { if (_high == value) return; _high = value; OnPropertyChanged("High"); }
        }

        private double _low;
        /// <summary> Gets or sets Low </summary>
        public double Low
        {
            get { return _low; }
            set { if (_low == value) return; _low = value; OnPropertyChanged("Low"); }
        }

        private string _label;
        /// <summary> Gets or sets Label </summary>
        public string Label
        {
            get { return _label; }
            set { if (_label == value) return; _label = value; OnPropertyChanged("Label"); }
        }

        private double _Close;
        /// <summary> Gets or sets Close </summary>
        public double Close
        {
            get { return _Close; }
            set { if (_Close == value) return; _Close = value; OnPropertyChanged("Close"); }
        }

        private double _Open;
        /// <summary> Gets or sets Open </summary>
        public double Open
        {
            get { return _Open; }
            set { if (_Open == value) return; _Open = value; OnPropertyChanged("Open"); }
        }


    }

}