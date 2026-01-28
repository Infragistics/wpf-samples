using IGBusyIndicator.Resources;
using Infragistics.Samples.Shared.Models;
using System.Timers;
using System.Windows.Input;

namespace IGBusyIndicator.Samples.DataProviders
{
    public class DisplayAfterViewModel : ObservableModel
    {
        #region Members
        private bool _isInProgress;
        public bool IsInProgress
        {
            get
            {
                return this._isInProgress;
            }
            set
            {
                if (this._isInProgress != value)
                {
                    this._isInProgress = value;
                    this.OnPropertyChanged("IsInProgress");
                }
            }
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get
            {
                return this._isEnabled;
            }
            set
            {
                if (this._isEnabled != value)
                {
                    this._isEnabled = value;
                    this.OnPropertyChanged("IsEnabled");
                }
            }
        }

        private string _text;
        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                if (this._text != value)
                {
                    this._text = value;
                    this.OnPropertyChanged("Text");
                }
            }
        }

        private DelegateCommand _showCommand;
        private DelegateCommand _closeCommand;
        private DelegateCommand _stopCommand;
        private Timer _timer;
        private int _counter;
        #endregion // Members

        #region Commands

        private bool CanDoAction(object obj)
        {
            return true;
        }

        public ICommand ShowCommand
        {
            get { return _showCommand; }
        }

        private void Show(object param)
        {
            this.Text = string.Format(BusyIndicatorStrings.TotalSeconds, _counter);
            this.IsInProgress = true;
            this.IsEnabled = false;
            _counter = 1;

            _timer.Start();
        }

        public ICommand CloseCommand
        {
            get { return _closeCommand; }
        }

        private void Close(object param)
        {
            this.IsInProgress = false;
            this._counter = 1;
        }

        public ICommand StopCommand
        {
            get { return _stopCommand; }
        }

        private void Stop(object param)
        {
            _timer.Stop();
            this.IsEnabled = true;
        }

        #endregion // Commands

        public DisplayAfterViewModel()
        {
            _counter = 1;
            this.Text = string.Format(BusyIndicatorStrings.TotalSeconds, 0);

            _timer = new Timer(1000); // 1 sec
            _timer.Elapsed += OnTimeElapsed;
            

            _showCommand = new DelegateCommand(Show, CanDoAction);
            _closeCommand = new DelegateCommand(Close, CanDoAction);
            _stopCommand = new DelegateCommand(Stop, CanDoAction);
        }

        private void OnTimeElapsed(object sender, ElapsedEventArgs e)
        {
            this.Text = string.Format(BusyIndicatorStrings.TotalSeconds, ++_counter);
        }
    }
}
