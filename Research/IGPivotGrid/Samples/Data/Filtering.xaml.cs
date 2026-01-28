using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using IGPivotGrid.Resources;
using Infragistics.Olap;
using Infragistics.Olap.Data;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Data
{
    public partial class Filtering : SampleContainer
    {
        private ManualResetEvent _manualResetEvent;
        public Filtering()
        {
            InitializeComponent();
            this.pivotGrid.DataSource.PropertyChanged += DataSourcePropertyChanged;
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            int startYear = DateTime.Today.Year;
            int numberOfMonths =
                int.Parse(((ComboBoxItem)this.monthsToFilter.SelectedItem).Content.ToString());
            int startMonth = DateTime.Today.Month - numberOfMonths;
            while (startMonth < 1)
            {
                startYear--;
                startMonth += 12;
            }
            DateTime startDate = new DateTime(startYear, startMonth, DateTime.Today.Day);

            FilterDates(startDate, DateTime.Today);
        }

        private void FilterDates(DateTime beginning, DateTime ending)
        {
            //Run filtering in background process in order to not hang the main UI of your app
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += (s, e) =>
            {
                bool allUnchecked = true;
                IFilterViewModel fvm = FindDateViewModel((DataSourceBase)this.pivotGrid.DataSource);
                foreach (IFilterMember filterMember in fvm.FilterMembers)
                {
                    if (filterMember.IsSelected != false)
                    {
                        allUnchecked = false;
                        break;
                    }
                }

                if (allUnchecked)
                {
                    MessageBox.Show(String.Format(PivotGridStrings.XPG_Filtering_ShowDataForTheLast, ((ComboBoxItem)monthsToFilter.SelectedItem).Content));
                }
                else
                {
                    this.pivotGrid.DataSource.RefreshGrid();
                    this.isBusyIndicator.Visibility = Visibility.Collapsed;
                }
            };

            this.isBusyIndicator.Visibility = Visibility.Visible;
            worker.RunWorkerAsync(new WorkerArguments()
            {
                DataSource = this.pivotGrid.DataSource as DataSourceBase,
                Beginning = beginning,
                Ending = ending
            });
        }

        void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            WorkerArguments args = e.Argument as WorkerArguments;
            if (args == null)
                return;

            //Find the Date view model or if not found create it in the Filters area
            IFilterViewModel fvm = FindDateViewModel(args.DataSource) ?? CreateViewModel(args.DataSource);

            if (fvm == null) return;

            //Manual reset event will be used to wait for the members to load
            //when they are expanded or selected
            fvm.LoadFilterMembersCompleted += (sender1, e1) =>
            {
                if (_manualResetEvent != null)
                    _manualResetEvent.Set();
            };

            if (fvm.FilterMembers != null)
            {
                foreach (IFilterMember member in fvm.FilterMembers)
                {
                    //Recursively go through all members that need to be interacted with
                    DrillIn(member, args);
                }
            }

            e.Result = fvm;
        }

        private IFilterViewModel FindDateViewModel(DataSourceBase dataSource)
        {

            for (int i = 0; i < dataSource.Filters.Count; i++)
            {
                if (((IFilterViewModel)dataSource.Filters[i]).Caption == PivotGridStrings.XPG_Data_PropertyNames_Date)
                {
                    return dataSource.Filters[i] as IFilterViewModel;
                }
            }
            for (int i = 0; i < dataSource.Rows.Count; i++)
            {
                if (((IFilterViewModel)dataSource.Rows[i]).Caption == PivotGridStrings.XPG_Data_PropertyNames_Date)
                {
                    return dataSource.Rows[i] as IFilterViewModel;
                }
            }
            for (int i = 0; i < dataSource.Columns.Count; i++)
            {
                if (((IFilterViewModel)dataSource.Columns[i]).Caption == PivotGridStrings.XPG_Data_PropertyNames_Date)
                {
                    return dataSource.Columns[i] as IFilterViewModel;
                }
            }

            return null;
        }
        private IFilterViewModel CreateViewModel(DataSourceBase dataSource)
        {
            foreach (IDimension dim in dataSource.Cube.Dimensions)
            {
                foreach (IHierarchy hierarchy in dim.Hierarchies)
                {
                    if (hierarchy.UniqueName == "[Date].[Date]")
                    {
                        IFilterViewModel fvm = dataSource.CreateFilterViewModel(hierarchy);
                        this.pivotGrid.Dispatcher.BeginInvoke((Action)(() =>
                            this.pivotGrid.DataSource.Filters.Add(fvm)
                        ));

                        return fvm;
                    }
                }
            }
            return null;
        }

        private void DrillIn(IFilterMember filterMember, WorkerArguments args)
        {
            _manualResetEvent = new ManualResetEvent(false);

            //Expand all levels to the individual date level
            //and then compare if each date is between the start and end date.
            //If it is select it, otherwise disselect it.                                                                
            if (filterMember.Member.LevelName == "Dates")
            {
                if (DateTime.Parse(filterMember.Member.Caption, Thread.CurrentThread.CurrentUICulture).CompareTo(args.Beginning) >= 0 &&
                    DateTime.Parse(filterMember.Member.Caption, Thread.CurrentThread.CurrentUICulture).CompareTo(args.Ending) < 0)
                {
                    this.Dispatcher.BeginInvoke((Action)(() => filterMember.FilterSource.IsSelected = true));
                }
                else
                {
                    this.Dispatcher.BeginInvoke((Action)(() => filterMember.FilterSource.IsSelected = false));
                }
                return;
            }
            else
            {
                if (!filterMember.IsExpanded)
                {
                    this.Dispatcher.BeginInvoke((Action)(() => filterMember.IsExpanded = true));
                    _manualResetEvent.WaitOne();
                }
            }

            for (int i = 0; i < filterMember.FilterMembers.Count; i++)
            {
                DrillIn(filterMember.FilterMembers[i], args);
            }
        }

        void DataSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                this.pivotGrid.Dispatcher.BeginInvoke((Action)(() =>
                {
                    isBusyIndicator.Visibility =
                        this.pivotGrid.DataSource.Processing ?
                            Visibility.Visible : Visibility.Collapsed;
                }));
            }
        }
    }

    class WorkerArguments
    {
        public DataSourceBase DataSource { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }

        public WorkerArguments()
        {
        }
    }
}
