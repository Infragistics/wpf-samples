using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IgOutlook.Infrastructure
{
    public class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _iconSource;
        public string IconSource
        {
            get { return _iconSource; }
            set
            {
                _iconSource = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateTitleOnPropertyChanged(INotifyPropertyChanged source, string propertyName, string concatedString = "", string nullValue = "")
        {
            source.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == propertyName)
                {
                    var value = (string)source.GetType().GetProperty(propertyName).GetValue(source);

                    if (string.IsNullOrEmpty(value))
                        Title = nullValue + concatedString;
                    else
                        Title = value + concatedString;
                }
            };
        }

        public void HookOnPropertyChanged(INotifyPropertyChanged source, string propertyName, System.Action action)
        {
            source.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == propertyName)
                {
                    action.Invoke();
                }
            };
        }

        public void HookOnPropertyChanged(INotifyPropertyChanged source, System.Action action)
        {
            source.PropertyChanged += (s, a) =>
            {
                action.Invoke();
            };
        }

        public void RefreshTitle()
        {
            OnPropertyChanged("Title");
        }
    }
}
