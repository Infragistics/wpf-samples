using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace IGSurfaceChart.Samples.Models
{
    public class ObservableCollector<T> : ObservableCollection<T>
    {

        public void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action));
        }

        public void OnCollectionReset()
        {
            this.OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }
        public void OnCollectionAdded()
        {
            this.OnCollectionChanged(NotifyCollectionChangedAction.Add);
        }
        public void OnCollectionRemoved()
        {
            this.OnCollectionChanged(NotifyCollectionChangedAction.Remove);
        }
        public void OnCollectionMoved()
        {
            this.OnCollectionChanged(NotifyCollectionChangedAction.Move);
        }
    }
}
