using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using IGGantt.Resources;
using IGGantt.Samples.Models;

namespace IGGantt.Samples.ViewModel
{
    public class CustomCalendarsViewModel : INotifyPropertyChanged
    {
        #region Private variables

        private ObservableCollection<CustomCalendar> calendars;
        private ObservableCollection<CustomTask> tasks;
        private CustomCalendar selectedCalendar;

        #endregion // Private variables

        #region Constructor

        public CustomCalendarsViewModel()
        {
            SelectedCalendar = Calendars[0];
        }

        #endregion // Constructor

        #region Public properties

        public ObservableCollection<CustomCalendar> Calendars
        {
            get
            {
                if (calendars == null)
                {
                    calendars = GenerateCustomCalendars();
                }
                return calendars;
            }
            set
            {
                calendars = value;
            }
        }

        public ObservableCollection<CustomTask> Tasks
        {
            get
            {
                if (tasks == null)
                {
                    tasks = GenerateCustomTasks();
                }
                return tasks;
            }
            set
            {
                tasks = value;
            }
        }

        public CustomCalendar SelectedCalendar
        {
            get { return selectedCalendar; }
            set
            {
                if (value != selectedCalendar)
                {
                    selectedCalendar = value;
                    OnPropertyChanged("SelectedCalendar");
                }
            }
        }

        public DateTime Today
        {
            get { return DateTime.Today; }
        }

        #endregion // Public properties

        #region Private helper methods

        private ObservableCollection<CustomCalendar> GenerateCustomCalendars()
        {
            return new ObservableCollection<CustomCalendar>()
            {
                new CustomCalendar
                {
                    Id = "C1",
                    Name =  String.Format("{0} 1", GanttStrings.CustomCalendarName), //"Calendar 1",
                    DaysOfWeek=@"
						<ScheduleDaysOfWeek>
							<Monday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""false"" />
								</DaySettings>
							</ScheduleDayOfWeek>
							</Monday>
							<Saturday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT8H"" End=""PT12H"" />
												<TimeRange Start=""PT13H"" End=""PT17H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Saturday>
						</ScheduleDaysOfWeek>",
                    CustomDescription = GanttStrings.custCalendar01Description //"This custom calendar has a Monday as a nonworking day and Saturday as a working day"
                },

                new CustomCalendar
                {
                    Id = "C2",
                    Name = String.Format("{0} 2", GanttStrings.CustomCalendarName), //"Calendar 2",
                    BaseCalendarId="C2",
                    IsBaseCalendar = true,
                    DaysOfWeek = @"
						<ScheduleDaysOfWeek>
							<Monday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT7H"" End=""PT12H"" />
												<TimeRange Start=""PT13H"" End=""PT16H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Monday>
							<Tuesday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT7H"" End=""PT12H"" />
												<TimeRange Start=""PT13H"" End=""PT16H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Tuesday>
							<Wednesday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT7H"" End=""PT12H"" />
												<TimeRange Start=""PT13H"" End=""PT16H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Wednesday>
							<Thursday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT7H"" End=""PT12H"" />
												<TimeRange Start=""PT13H"" End=""PT16H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Thursday>
							<Friday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT7H"" End=""PT12H"" />
												<TimeRange Start=""PT13H"" End=""PT16H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Friday>
						</ScheduleDaysOfWeek>",
                CustomDescription = GanttStrings.custCalendar02Description01 + Environment.NewLine + GanttStrings.custCalendar02Description02
                //"This custom calendar sets the following working times" + Environment.NewLine + "From 7:00to 12:00 and from 13:00 to 16:00"
                },
                new CustomCalendar
                {
                    Id = "C3",
                    Name = String.Format("{0} 3", GanttStrings.CustomCalendarName), //"Calendar 3",
                    DaysOfWeek = @"
						<ScheduleDaysOfWeek>
							<Monday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT6H"" End=""PT11H"" />
												<TimeRange Start=""PT12H"" End=""PT13H"" />
                                                <TimeRange Start=""PT18H"" End=""PT20H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Monday>
							<Tuesday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT7H"" End=""PT11H"" />
												<TimeRange Start=""PT13H"" End=""PT17H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Tuesday>
							<Wednesday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT6H"" End=""PT11H"" />
												<TimeRange Start=""PT12H"" End=""PT13H"" />
                                                <TimeRange Start=""PT18H"" End=""PT20H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Wednesday>
							<Thursday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT7H"" End=""PT11H"" />
												<TimeRange Start=""PT13H"" End=""PT17H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Thursday>
							<Friday>
							<ScheduleDayOfWeek>
								<DaySettings>
									<DaySettings IsWorkday=""true"">
										<WorkingHours>
											<WorkingHoursCollection>
												<TimeRange Start=""PT6H"" End=""PT11H"" />
												<TimeRange Start=""PT12H"" End=""PT13H"" />
                                                <TimeRange Start=""PT18H"" End=""PT20H"" />
											</WorkingHoursCollection>
										</WorkingHours>
									</DaySettings>
								</DaySettings>
							</ScheduleDayOfWeek>
							</Friday>
						</ScheduleDaysOfWeek>",
                CustomDescription = GanttStrings.custCalendar03Description01 + Environment.NewLine + GanttStrings.custCalendar03Description02 +
                Environment.NewLine + GanttStrings.custCalendar03Description03
                /* "This custom calendar sets two sets of working time shifts for the work week." + Environment.NewLine + 
                "For Monday, Wednesday and Friday there are three shifts: from 6:00 to 11:00, from 12:00 to 13:00 and from 18:00 to 20:00." + Environment.NewLine + 
                "For Tuesday and Thursday there are two shifts: from 7:00 to 11:00 and from 13:00 to 17:00" */
                }
            };
        }

        private ObservableCollection<CustomTask> GenerateCustomTasks()
        {
            return new ObservableCollection<CustomTask>()
			{
				new CustomTask
				{
					DataItemId = "t1",
					TaskName = String.Format("{0} 1", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8)
				},
                new CustomTask
				{
					DataItemId = "t2",
					TaskName = String.Format("{0} 2", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t1"
				},
				new CustomTask
				{
					DataItemId = "t3",
					TaskName = String.Format("{0} 3", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t2"
				},
				new CustomTask
				{
					DataItemId = "t4",
					TaskName = String.Format("{0} 4", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t3"
				},
				new CustomTask
				{
					DataItemId = "t5",
					TaskName = String.Format("{0} 5", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t4"
				},
                new CustomTask
				{
					DataItemId = "t6",
					TaskName = String.Format("{0} 6", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t5"
				},
                new CustomTask
				{
					DataItemId = "t7",
					TaskName = String.Format("{0} 7", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t6"
				},
                new CustomTask
				{
					DataItemId = "t8",
					TaskName = String.Format("{0} 8", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t7"
				}
			};
        }

        #endregion // Private helper methods

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
