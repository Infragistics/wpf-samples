using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Infragistics.Framework
{
    public class RandomData : ObservableCollection<RandomDataItem>
    {
        public RandomData(int items = 10)
        {
            var random = new Random();

            for (double i = 0; i < items; i++)
            {
                var item = new RandomDataItem();
                item.Index = i;
                item.Positive = random.Next(1, 10);
                item.Negative = random.Next(-10, -1);
                item.Mixed = random.Next(-10, 10);
                this.Add(item);
            }
        }
    }
    public class RandomDataItem : ObservableModel, IEnumerable
    {
        public double? positive;
        public double? negative;
        public double? mixed;
        public double? index;

        public double? Positive
        {
            get { return positive; }
            set
            {
                positive = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Positive");
            }
        }
        public double? Negative
        {
            get { return negative; }
            set
            {
                negative = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Negative");
            }
        }
        public double? Mixed
        {
            get { return mixed; }
            set
            {
                mixed = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Mixed");
            }
        }

        public double? Index
        {
            get { return index; }
            set
            {
                index = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Index");
            }
        }

     

        public IEnumerator GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class RandomLargeData : RandomData
    {
        public RandomLargeData() : base(1000)
        {
        }
    }
    public class RandomMediumData : RandomData
    {
        public RandomMediumData() : base(100)
        {
        }
    }
    public class RandomSmallData : RandomData
    {
        public RandomSmallData() : base(10)
        {
        }
    }

}
