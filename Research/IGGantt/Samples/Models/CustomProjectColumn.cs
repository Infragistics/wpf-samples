using System.ComponentModel;

namespace IGGantt.Samples.Models
{
    public class CustomProjectColumn  : INotifyPropertyChanged
    {
        #region Private variables

        private string id;
        private string key;
        private string fixedIndicatorDirection;
        private string dataTemplateKey;
        private string headerText;
        private string headerTextHorizontalAlignment;
        private string headerTextVerticalAlignment;
        private bool? isReadOnly;
        private string width;

        #endregion // Private variables

        #region Public properties

        public string Id
        {
            get { return id; }
            set 
            { 
                if (id != value)
                {
                    id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        // In order to map the desired task property to this key, the ProjectTask property names should be set as a key.
        // For usage details you can consult GenerateColumns() method of the ViewProviderViewModel class of this sample.
        public string Key
        {
            get { return key; }
            set
            {
                if (key != value)
                {
                    key = value;
                    this.OnPropertyChanged("Key");
                }
            }
        }

        public string FixedIndicatorDirection
        {
            get { return fixedIndicatorDirection; }
            set
            {
                if (fixedIndicatorDirection != value)
                {
                    fixedIndicatorDirection = value;
                    this.OnPropertyChanged("FixedIndicatorDirection");
                }
            }
        }

        public string DataTemplateKey
        {
            get { return dataTemplateKey; }
            set
            {
                if (dataTemplateKey != value)
                {
                    dataTemplateKey = value;
                    this.OnPropertyChanged("DataTemplateKey");
                }
            }
        }

        public string HeaderText
        {
            get{ return headerText; }
            set
            {
                if (headerText != value)
                {
                    headerText = value;
                    this.OnPropertyChanged("HeaderText");
                }
            }
        }

        public string HeaderTextHorizontalAlignment
        {
            get { return headerTextHorizontalAlignment; }
            set
            {
                if (headerTextHorizontalAlignment != value)
                {
                    headerTextHorizontalAlignment = value;
                    this.OnPropertyChanged("HeaderTextHorizontalAlignment");
                }
            }
        }

        public string HeaderTextVerticalAlignment
        {
            get { return headerTextVerticalAlignment; }
            set
            {
                if (headerTextVerticalAlignment != value)
                {
                    headerTextVerticalAlignment = value;
                    this.OnPropertyChanged("HeaderTextVerticalAlignment");
                }
            }
        }

        public bool? IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                if (isReadOnly != value)
                {
                    isReadOnly = value;
                    this.OnPropertyChanged("IsReadOnly");
                }
            }
        }

        public string Width
        {
            get { return width; }
            set
            {
                if (width != value)
                {
                    width = value;
                    this.OnPropertyChanged("Width");
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