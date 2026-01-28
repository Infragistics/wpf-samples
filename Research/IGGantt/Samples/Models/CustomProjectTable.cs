using System.ComponentModel;

namespace IGGantt.Samples.Models
{
    public class CustomProjectTable : INotifyPropertyChanged
    {
        #region Private variables

        private string key;
        private string columnIds;
        private string dateFormat;
        private bool showInMenu;

        #endregion // Private variables

        #region Public properties

        public string Key
        {
            get { return key; }
            set
            {
                if (key != value)
                {
                    key = value;
                    OnPropertyChanged("Key");
                }
            }
        }

        public string ColumnIds
        {
            get { return columnIds; }
            set
            {
                if (columnIds != value)
                {
                    columnIds = value;
                    OnPropertyChanged("ColumnIds");
                }
            }
        }

        public string DateFormat
        {
            get { return dateFormat; }
            set
            {
                if (dateFormat != value)
                {
                    dateFormat = value;
                    OnPropertyChanged("DateFormat");
                }
            }
        }

        public bool ShowInMenu
        {
            get { return showInMenu; }
            set
            {
                if (showInMenu != value)
                {
                    showInMenu = value;
                    OnPropertyChanged("ShowInMenu");
                }
            }
        }

        #endregion // Public properties

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged
    }
}