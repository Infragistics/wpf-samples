using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using System.Xml.Linq;
using IGTimeline.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using XmlDataProvider = Infragistics.Samples.Shared.DataProviders.XmlDataProvider;

namespace IGTimeline.Samples
{
    public partial class BindingLiveData : Infragistics.Samples.Framework.SampleContainer
    {
        private LiveDataViewModel _viewModel;

        public BindingLiveData()
        {
            InitializeComponent();

            _viewModel = new LiveDataViewModel();
            this.LayoutRoot.DataContext = _viewModel;
            _viewModel.LoadData();
            _viewModel.DataLoaded += OnViewModelDataLoaded;
        }
        private void OnViewModelDataLoaded(object sender, EventArgs e)
        {
            // Set DataSources: goal, red cards, yellow cards, fouls
            this.Timeline.Series[0].DataSource = _viewModel.TeamOneGoals;
            this.Timeline.Series[1].DataSource = _viewModel.TeamOneRedCards;
            this.Timeline.Series[2].DataSource = _viewModel.TeamOneYellowCards;
            this.Timeline.Series[3].DataSource = _viewModel.TeamOneFouls;

            this.Timeline.Series[4].DataSource = _viewModel.TeamTwoGoals;
            this.Timeline.Series[5].DataSource = _viewModel.TeamTwoRedCards;
            this.Timeline.Series[6].DataSource = _viewModel.TeamTwoYellowCards;
            this.Timeline.Series[7].DataSource = _viewModel.TeamTwoFouls;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb != null)
                _viewModel.SetSelectedIndex(lb.SelectedIndex);
        }
    }

    #region ViewModel
    public class LiveDataViewModel : ObservableModel
    {
        private string _teamOne = TimelineStrings.XWT_LiveData_TeamOne;
        private string _teamTwo = TimelineStrings.XWT_LiveData_TeamTwo;
        private int _selectedIndex = 0;
        private int _iteration = 0;
        private double _startTime;
        private double _endTime;
        private double _currentTime;
        private DispatcherTimer _timer = new DispatcherTimer();
        private List<FootballEntry> _entries = new List<FootballEntry>();
        private XmlDataProvider _dataSource;

        public LiveDataViewModel()
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;

            this.IsDataStreaming = true;
        }

        #region Events
        public event EventHandler DataLoaded;
        #endregion Events

        #region Public Methods

        public void SetSelectedIndex(int i)
        {
            _selectedIndex = i;
            this.OnPropertyChanged("SelectedPlayer");
            this.OnPropertyChanged("SelectedTeam");
            this.OnPropertyChanged("SelectedTime");
            this.OnPropertyChanged("SelectedEventType");
        }

        public void LoadData()
        {
            _dataSource = new XmlDataProvider();
            _dataSource.GetXmlDataCompleted += OnGetXmlDataCompleted;
            _dataSource.GetXmlDataAsync("FootballTimeline.xml");
        }
        #endregion Public Methods

        #region Properties

        #region Collections
        public ObservableCollection<FootballEntry> EventHistory
        {
            get
            {
                var entries = _entries.Where(a => a.EventTime <= _currentTime);
                ObservableCollection<FootballEntry> coll = new ObservableCollection<FootballEntry>();

                foreach (FootballEntry entry in entries)
                {
                    coll.Add(entry);
                }
                return coll;
            }
        }

        private ObservableCollection<FootballEntry> _teamOneGoals;
        public ObservableCollection<FootballEntry> TeamOneGoals
        {
            get
            {
                return _teamOneGoals;
            }
            set
            {
                _teamOneGoals = (ObservableCollection<FootballEntry>)value;
                this.OnPropertyChanged("TeamOneGoals");
            }
        }

        public ObservableCollection<FootballEntry> TeamOneRedCards
        {
            get
            {
                var entries = _entries.Where(a => a.TeamName == _teamOne && a.EventType == FootballEntryType.RedCard && a.EventTime <= _currentTime);
                List<FootballEntry> list = entries.ToList();
                ObservableCollection<FootballEntry> coll = new ObservableCollection<FootballEntry>();

                foreach (FootballEntry i in list)
                {
                    coll.Add(i);
                }


                return coll;
            }
        }

        public ObservableCollection<FootballEntry> TeamOneYellowCards
        {
            get
            {
                var entries = _entries.Where(a => a.TeamName == _teamOne && a.EventType == FootballEntryType.YellowCard && a.EventTime <= _currentTime);
                List<FootballEntry> list = entries.ToList();
                ObservableCollection<FootballEntry> coll = new ObservableCollection<FootballEntry>();

                foreach (FootballEntry i in list)
                {
                    coll.Add(i);
                }


                return coll;
            }
        }

        public ObservableCollection<FootballEntry> TeamOneFouls
        {
            get
            {
                var entries = _entries.Where(a => a.TeamName == _teamOne && a.EventType == FootballEntryType.Foul && a.EventTime <= _currentTime);
                List<FootballEntry> list = entries.ToList();
                ObservableCollection<FootballEntry> coll = new ObservableCollection<FootballEntry>();

                foreach (FootballEntry i in list)
                {
                    coll.Add(i);
                }


                return coll;
            }
        }

        public ObservableCollection<FootballEntry> TeamTwoGoals
        {
            get
            {
                var entries = _entries.Where(a => a.TeamName == _teamTwo && a.EventType == FootballEntryType.Goal && a.EventTime <= _currentTime);
                List<FootballEntry> list = entries.ToList();
                ObservableCollection<FootballEntry> coll = new ObservableCollection<FootballEntry>();

                foreach (FootballEntry i in list)
                {
                    coll.Add(i);
                }


                return coll;
            }
        }

        public ObservableCollection<FootballEntry> TeamTwoRedCards
        {
            get
            {
                var entries = _entries.Where(a => a.TeamName == _teamTwo && a.EventType == FootballEntryType.RedCard && a.EventTime <= _currentTime);
                List<FootballEntry> list = entries.ToList();
                ObservableCollection<FootballEntry> coll = new ObservableCollection<FootballEntry>();

                foreach (FootballEntry i in list)
                {
                    coll.Add(i);
                }


                return coll;
            }
        }

        public ObservableCollection<FootballEntry> TeamTwoYellowCards
        {
            get
            {
                var entries = _entries.Where(a => a.TeamName == _teamTwo && a.EventType == FootballEntryType.YellowCard && a.EventTime <= _currentTime);
                List<FootballEntry> list = entries.ToList();
                ObservableCollection<FootballEntry> coll = new ObservableCollection<FootballEntry>();

                foreach (FootballEntry i in list)
                {
                    coll.Add(i);
                }


                return coll;
            }
        }

        public ObservableCollection<FootballEntry> TeamTwoFouls
        {
            get
            {
                var entries = _entries.Where(a => a.TeamName == _teamTwo && a.EventType == FootballEntryType.Foul && a.EventTime <= _currentTime);
                List<FootballEntry> list = entries.ToList();
                ObservableCollection<FootballEntry> coll = new ObservableCollection<FootballEntry>();

                foreach (FootballEntry i in list)
                {
                    coll.Add(i);
                }


                return coll;
            }
        }
        #endregion Collections

        #region SelectedEntryDetails
        public string SelectedPlayer
        {
            get
            {
                return this.EventHistory.Count > 0 ? this.EventHistory[_selectedIndex].PlayerName : String.Empty;
            }
        }

        public string SelectedTeam
        {
            get
            {
                return this.EventHistory.Count > 0 ? this.EventHistory[_selectedIndex].TeamName : String.Empty;
            }
        }

        public string SelectedTime
        {
            get
            {
                return this.EventHistory.Count > 0 ? this.EventHistory[_selectedIndex].EventTime.ToString() : String.Empty;
            }
        }

        public FootballEntryType SelectedEventType
        {
            get
            {
                return this.EventHistory.Count > 0 ? this.EventHistory[_selectedIndex].EventType : FootballEntryType.Goal;
            }
        }
        #endregion SelectedEntryDetails

        public double CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                _currentTime = value;
                this.OnPropertyChanged("CurrentTime");
                this.OnPropertyChanged("CurrentTimeClock");
            }
        }

        public string CurrentTimeClock
        {
            get
            {
                return _currentTime.ToString();
            }
        }

        private bool _isDataStreaming;
        public bool IsDataStreaming
        {
            get
            {
                return _isDataStreaming;
            }
            set
            {
                _isDataStreaming = value;
                this.OnPropertyChanged("IsDataStreaming");
            }
        }

        #endregion Properties

        #region Event Handlers
        void OnTimerTick(object sender, EventArgs e)
        {
            if (_iteration < _entries.Count)
            {
                this.CurrentTime = _entries[_iteration].EventTime;

                var entries = _entries.Where(a => a.TeamName == _teamOne && a.EventType == FootballEntryType.Goal && a.EventTime <= _currentTime);
                List<FootballEntry> list = entries.ToList();
                ObservableCollection<FootballEntry> coll = new ObservableCollection<FootballEntry>();

                foreach (FootballEntry i in list)
                {
                    coll.Add(i);
                }
                this.TeamOneGoals = coll;

                this.OnPropertyChanged("EventHistory");
                this.OnPropertyChanged("TeamOneGoals");
                this.OnPropertyChanged("TeamOneRedCards");
                this.OnPropertyChanged("TeamOneYellowCards");
                this.OnPropertyChanged("TeamOneFouls");
                this.OnPropertyChanged("TeamTwoGoals");
                this.OnPropertyChanged("TeamTwoRedCards");
                this.OnPropertyChanged("TeamTwoYellowCards");
                this.OnPropertyChanged("TeamTwoFouls");

                if (this.DataLoaded != null)
                    this.DataLoaded(this, EventArgs.Empty);

                this.SetSelectedIndex(_iteration);
            }
            else
            {
                _timer.Tick -= OnTimerTick;
                this.IsDataStreaming = false;
            }
            _iteration++;
        }

        void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.HasError) return;

            try
            {
                XDocument doc = e.Result;
                var entries = from entry in doc.Descendants("DateTimeEntry")
                              orderby Convert.ToDouble(entry.Attribute("EventTime").Value)
                              select new FootballEntry
                                          {
                                              EventTime = entry.Attribute("EventTime").GetDouble(),
                                              EventType =
                                                  (FootballEntryType)
                                                  Enum.Parse(typeof(FootballEntryType),
                                                          entry.Attribute("EventType").Value, true),
                                              PlayerName = entry.Attribute("PlayerName").GetString(),
                                              TeamName = entry.Attribute("TeamName").GetString()
                                          };
                _entries = (List<FootballEntry>)entries.ToList();
                _startTime = _entries[0].EventTime;
                _endTime = _entries[_entries.Count - 1].EventTime;
                _currentTime = _startTime;
                _timer.Start();

                if (this.DataLoaded != null)
                {
                    this.DataLoaded(this, EventArgs.Empty);
                }
            }
            catch (Exception)
            {
            }

        }

        #endregion Event Handlers
    }
    #endregion ViewModel

    public class NegationConverter : IValueConverter
    {
        public NegationConverter()
        {

        }

        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null ? !(bool)value : DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null ? (bool)value : DependencyProperty.UnsetValue;
        }

        #endregion
    }
}
