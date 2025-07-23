using System.ComponentModel;
using IGRibbon.Resources;

namespace IGRibbon.Model
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            command = new SimpleCommand(UpdateDisplayName);
            secondCommand = new SimpleCommand(AnotherUpdate, CanAnotherUpdate);
        }

        void UpdateDisplayName(object parameter)
        {
            ButtonValue = parameter.ToString();
            DisplayName = string.Format("{0} {1}.", RibbonStrings.Commanding_DisplayNameAt,
                System.DateTime.Now.ToLongTimeString());
        }

        void AnotherUpdate(object parameter)
        {
            DisplayName = RibbonStrings.Commanding_UpdateFromThirdCommand; //"This is update from third command.";
        }

        bool CanAnotherUpdate(object parameter)
        {
            if (parameter != null && parameter.Equals("Test1"))
            {
                return true;
            }
            return false;
        }

        private SimpleCommand command;
        public SimpleCommand Command
        {
            get
            {
                return command;
            }
            set
            {
                if (value != command)
                {
                    command = value;
                    OnPropertyChanged("Command");
                }
            }
        }

        private SimpleCommand secondCommand;
        public SimpleCommand SecondCommand
        {
            get
            {
                return secondCommand;
            }
            set
            {
                if (value != secondCommand)
                {
                    secondCommand = value;
                    OnPropertyChanged("SecondCommand");
                }
            }
        }

        private string displayName;
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                if (displayName != value)
                {
                    displayName = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }

        private string buttonValue;
        public string ButtonValue
        {
            get { return buttonValue; }
            set
            {
                buttonValue = value;
                OnPropertyChanged("ButtonValue");
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion // INotifyPropertyChanged
    }
}