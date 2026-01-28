using System;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class CorrelationDataSample : CorrelationDataCollection
    {
        public CorrelationDataSample()
        {
            _settings = new CorrelationDataSettings
            {
                DataPoints = 100,
                XMin = 0,
                YMin = 0,
            };
            this.Generate();
        }
        #region Methods
        protected void Generate()
        {
            this.Clear();
            if (this.Settings.DataDistribution == DataDistribution.Random)
                this.GenerateRandomData();

            else if (this.Settings.DataDistribution == DataDistribution.RandomlyIncreasing)
                this.GenerateRandomlyIncreasingData();

            else if (this.Settings.DataDistribution == DataDistribution.RandomlyDecreasing)
                this.GenerateRandomlyDecreasingData();

            else if (this.Settings.DataDistribution == DataDistribution.LinearlyIncreasing)
                this.GenerateLinearlyIncreasingData();

            else if (this.Settings.DataDistribution == DataDistribution.LinearlyDecreasing)
                this.GenerateLinearlyDecreasingData();

        }

        protected void GenerateRandomData()
        {
            this.Clear();
            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double x = CorrelationDataGenerator.Random.Next(this.Settings.XMin, this.Settings.XMax);
                double y = CorrelationDataGenerator.Random.Next(this.Settings.YMin, this.Settings.YMax);
                this.Add(new CorrelationDataPoint { X = x, Y = y });
            }
        }
        protected void GenerateRandomlyIncreasingData()
        {
            this.Clear();
            int value = this.Settings.YMin;
            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double change = CorrelationDataGenerator.Random.NextDouble();
                if (change > .5)
                    value += (int)(change * 20);
                else
                    value -= (int)(change * 20);

                double x = CorrelationDataGenerator.Random.Next(i, i + this.Settings.XRange);
                double y = CorrelationDataGenerator.Random.Next(value - this.Settings.YRange, value + this.Settings.YRange);
                this.Add(new CorrelationDataPoint { X = x, Y = y });
            }
        }
        protected void GenerateRandomlyDecreasingData()
        {
            this.Clear();
            int value = this.Settings.YMax;
            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double change = CorrelationDataGenerator.Random.NextDouble();
                if (change < .5)
                    value += (int)(change * 20);
                else
                    value -= (int)(change * 20);

                double x = CorrelationDataGenerator.Random.Next(i, i + this.Settings.XRange);
                double y = CorrelationDataGenerator.Random.Next(value - this.Settings.YRange, value + this.Settings.YRange);
                this.Add(new CorrelationDataPoint { X = x, Y = y });
            }
        }
        protected void GenerateLinearlyIncreasingData()
        {
            this.Clear();
            double y = this.Settings.YMin;
            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double x = i * this.Settings.XInterval;
                this.Add(new CorrelationDataPoint { X = x, Y = y });
                y += this.Settings.YInterval;
            }
        }
        protected void GenerateLinearlyDecreasingData()
        {
            this.Clear();
            double y = this.Settings.YMax;
            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double x = i * this.Settings.XInterval;
                this.Add(new CorrelationDataPoint { X = x, Y = y });
                y -= this.Settings.YInterval;
            }
        }

        #endregion
        #region Properties
        private CorrelationDataSettings _settings;
        public CorrelationDataSettings Settings
        {
            get { return _settings; }
            set
            {
                if (_settings == value) return;
                _settings = value;
                this.Generate();
            }
        }
        #endregion
    }
    public enum DataDistribution
    {
        LinearlyIncreasing,
        LinearlyDecreasing,
        RandomlyIncreasing,
        RandomlyDecreasing,
        Random,
    }

    public static class CorrelationDataGenerator
    {
        public static Random Random = new Random();
    }
    public class CorrelationDataSettings
    {
        public CorrelationDataSettings()
        {
            DataDistribution = DataDistribution.RandomlyIncreasing;

            DataPoints = 100;

            XMin = 5;
            XInterval = 5;
            XMax = 95;
            XRange = 5;

            YMin = 100;
            YInterval = 5;
            YMax = 900;
            YRange = 150;
        }

        #region Properties
        public DataDistribution DataDistribution { get; set; }

        public double XInterval { get; set; }
        public double YInterval { get; set; }

        public int DataPoints { get; set; }

        public int XMin { get; set; }
        public int XMax { get; set; }

        public int YMin { get; set; }
        public int YMax { get; set; }

        public int XRange { get; set; }
        public int YRange { get; set; }
        #endregion


    }
    public class CorrelationDataCollection : ObservableCollection<CorrelationDataPoint> { }

    public class CorrelationDataPoint 
    {
        #region Properties
        public double X { get; set; }
        public double Y { get; set; }
        

        #endregion
    }
}