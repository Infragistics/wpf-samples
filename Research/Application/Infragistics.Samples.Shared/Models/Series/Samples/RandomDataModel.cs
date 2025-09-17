using System;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class RandomDataModel : ObservableModel
    {
        public RandomDataModel()
        {
            this.DataSettings = new RandomDataSettings();
            this.Data = new RandomDataCollection();
            this.GenerateData();
        }
        public void GenerateData()
        {
            this.Data.Clear();
            RandomDataGenerator.DataSettings = this.DataSettings;
            RandomDataGenerator.Generate();
            foreach (RandomDataPoint dataPoint in RandomDataGenerator.Data)
            {
                this.Data.Add(dataPoint);
            }
        }
        private RandomDataCollection _data;
        public RandomDataCollection Data
        {
            get
            {
                return _data;
            }
            set
            {
                if (_data == value) return;
                _data = value;
                this.GenerateData();
                this.OnPropertyChanged("Data");
            }
        }

        private RandomDataSettings _dataSettings;
        public RandomDataSettings DataSettings
        {
            get
            {
                return _dataSettings;
            }
            set
            {
                if (_dataSettings == value) return;
                _dataSettings = value;
                this.GenerateData();
                this.OnPropertyChanged("DataSettings");
            }
        }
    }
    public class RandomDataPoint : DataPoint
    {
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
    public class RandomDataCollection : ObservableCollection<RandomDataPoint>
    {
        public RandomDataCollection()
        {
             this.Generate();
        }

        public void Generate()
        {
            this.Clear();
            RandomDataGenerator.Generate();
            foreach (RandomDataPoint dataPoint in RandomDataGenerator.Data)
            {
                this.Add(dataPoint);
            }
        }
    }
    public static class RandomDataGenerator
    {
        static RandomDataGenerator()
        {
            Generator = new Random();
            DataSettings = new RandomDataSettings();
            Data = new ObservableCollection<RandomDataPoint>();
            Generate();
        }
        private static readonly Random Generator;
        public static RandomDataSettings DataSettings { get; set; }
        public static ObservableCollection<RandomDataPoint> Data { get; set; }
        public static void Generate()
        {
            Data.Clear();
            double curr = DataSettings.ValueStart;
            DateTime date = DataSettings.DateStart;

            for (int i = 0; i < DataSettings.DataPoints; i++)
            {
                if (Generator.NextDouble() > .5)
                {
                    curr += Generator.NextDouble();
                }
                else
                {
                    curr -= Generator.NextDouble();
                }
                Data.Add(
                    new RandomDataPoint
                    {
                        Date = date,
                        Index = i,
                        Label = i.ToString(),
                        Value = curr
                    });
                date = date.Add(DataSettings.DateInterval);
            }
        }
    }
    public class RandomDataSettings : DataSettings
    {
        public RandomDataSettings()
        {
            DataPoints = 1000;

            ValueStart = 100;
    
            DateInterval = TimeSpan.FromSeconds(10);
            DateStart = new DateTime(2010, 10, 01, 12, 00, 00);
        }

        #region Properties
   
        /// <summary>
        /// Determines starting Value value for the first StockMarketDataPoint object
        /// </summary>
        public double ValueStart { get; set; }

        /// <summary>
        /// Determines time intervals between StockMarketDataPoint objects
        /// </summary>
        public TimeSpan DateInterval { get; set; }
        /// <summary>
        /// Determines starting date value for the first StockMarketDataPoint object
        /// </summary>
        public DateTime DateStart { get; set; }
        #endregion
    }
    public class RandomGenerator
    {
        static  RandomGenerator()
        {
            Random = new Random();
        }
        protected static readonly Random Random;
        public static double NextDouble()
        {
            if (Random.NextDouble() > .5)
                _value += Random.NextDouble();
            else
                _value -= Random.NextDouble();
            return _value;
        }
        private static double _value;

    }

   
    public class RandomCategoryDataCollection : CategoryDataCollection
    {
        public RandomCategoryDataCollection()
        {
            double value = 50;
            for (int i = 0; i < 25; i++)
            {
                double change = RandomCategoryDataGenerator.Random.NextDouble();
                if (change > .5)
                {
                    value += (change * 10);
                }
                else
                {
                    value -= (change * 10);
                }

                this.Add(new CategoryDataPoint
                {
                    Category = i.ToString(),
                    Value = value,
                    High = value + 10,
                    Low = value - 10
                });
            }
        }
        public static class RandomCategoryDataGenerator
        {
            public static Random Random = new Random();
        }
    }

}
