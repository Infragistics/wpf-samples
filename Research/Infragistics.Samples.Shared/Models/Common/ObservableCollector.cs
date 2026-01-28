using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Threading;

namespace Infragistics.Models
{
    public class ObservableCollector<T> : ObservableCollection<T>, INotifyPropertyChanged
    {
        public ObservableCollector()
        {

            UpdateTimer.Interval = TimeSpan.FromSeconds(UpdateInterval);
            UpdateTimer.Tick += OnUpdateTimerTick;
        }
        public void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action));
        }

        public void RaiseCollectionReset()
        {
            this.OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }
        public void RaiseCollectionAdded()
        {
            this.OnCollectionChanged(NotifyCollectionChangedAction.Add);
        }
        public void RaiseCollectionRemoved()
        {
            this.OnCollectionChanged(NotifyCollectionChangedAction.Remove);
        }
        public void RaiseCollectionMoved()
        {
            this.OnCollectionChanged(NotifyCollectionChangedAction.Move);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        public void ToggleUpdating()
        {
            if (UpdateTimer.IsEnabled)
            {
                UpdateTimer.Stop();
            }
            else
            {
                UpdateTimer.Start();
            } 
        }
        public DispatcherTimer UpdateTimer = new DispatcherTimer();
        private double _interval = 0.5;
        /// <summary> Gets or sets Interval </summary>
        public double UpdateInterval
        {
            get { return _interval; }
            set
            {
                if (_interval == value) return; _interval = value;
                OnPropertyChanged("UpdateInterval");
                if (_interval > 0)
                    UpdateTimer.Interval = TimeSpan.FromSeconds(UpdateInterval);
            }
        }
        protected double DataSeed = 0.0;
        protected void OnUpdateTimerTick(object sender, EventArgs e)
        {
            DataSeed += 0.01;
            foreach (var item in this)
            {
                Update(item, DataSeed);
            }

            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        } 
        public virtual void Update(T item, double seed)
        {

        }
    }
}