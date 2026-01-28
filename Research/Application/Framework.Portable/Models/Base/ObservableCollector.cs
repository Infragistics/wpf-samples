using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Infragistics.Framework
{ 
    public class ObservableCollector<T> : ObservableCollection<T>
    {  
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
    }
}