using System;
using System.Collections.Generic;
using Infragistics.Samples.Shared.Models;

namespace IGUndoRedoFramework.Data
{
    #region DataItemBase
    public class DataItemBase : ObservableModel
    {
        protected bool SetField<T>(ref T member, T newValue, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(member, newValue))
                return false;

            member = newValue;
            this.OnPropertyChanged(propertyName);
            return true;
        }
    }
    #endregion //DataItemBase

    #region ResourceItem
    public class ResourceItem : DataItemBase
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { this.SetField(ref _id, value, "Id"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { this.SetField(ref _name, value, "Name"); }
        }
    }
    #endregion //ResourceItem

    #region ResourceCalendarItem
    public class ResourceCalendarItem : DataItemBase
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { this.SetField(ref _id, value, "Id"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { this.SetField(ref _name, value, "Name"); }
        }

        private string _owningResourceOwningResourceId;
        public string OwningResourceId
        {
            get { return _owningResourceOwningResourceId; }
            set { this.SetField(ref _owningResourceOwningResourceId, value, "OwningResourceId"); }
        }
    }
    #endregion //ResourceCalendarItem

    #region ActivityItem
    public class ActivityItem : DataItemBase
    {
        #region Minimum Required Properties
        private string _id;
        public string Id
        {
            get { return _id; }
            set { this.SetField(ref _id, value, "Id"); }
        }

        private DateTime _start;
        public DateTime Start
        {
            get { return _start; }
            set { this.SetField(ref _start, value, "Start"); }
        }

        private DateTime _end;
        public DateTime End
        {
            get { return _end; }
            set { this.SetField(ref _end, value, "End"); }
        }

        private string _owningResourceId;
        public string OwningResourceId
        {
            get { return _owningResourceId; }
            set { this.SetField(ref _owningResourceId, value, "OwningResourceId"); }
        }

        // technically this required when using calendars which we are
        private string _owningCalendarId;
        public string OwningCalendarId
        {
            get { return _owningCalendarId; }
            set { this.SetField(ref _owningCalendarId, value, "OwningCalendarId"); }
        }
        #endregion //Minimum Required Properties

        #region Misc Properties

        // skipped IsLocked, MaxOccurrenceDateTime, IsVisible
        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set { this.SetField(ref _subject, value, "Subject"); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { this.SetField(ref _description, value, "Description"); }
        }

        #endregion //Misc Properties

        #region Activity Category Properties
        private string _categories;
        public string Categories
        {
            get { return _categories; }
            set { this.SetField(ref _categories, value, "Categories"); }
        }
        #endregion //Activity Category Properties

        #region TimeZone Properties
        private string _startTimeZoneId;
        public string StartTimeZoneId
        {
            get { return _startTimeZoneId; }
            set { this.SetField(ref _startTimeZoneId, value, "StartTimeZoneId"); }
        }

        private string _endTimeZoneId;
        public string EndTimeZoneId
        {
            get { return _endTimeZoneId; }
            set { this.SetField(ref _endTimeZoneId, value, "EndTimeZoneId"); }
        }

        private bool _isTimeZoneNeutral;
        public bool IsTimeZoneNeutral
        {
            get { return _isTimeZoneNeutral; }
            set { this.SetField(ref _isTimeZoneNeutral, value, "IsTimeZoneNeutral"); }
        }
        #endregion //TimeZone Properties

        #region Reminder Properties
        // note: make sure the CurrentUser or CurrentUserId of the XamScheduleDataManager is set since 
        // reminders are only shown for the current user's activities

        // required
        private TimeSpan _reminderInterval;
        public TimeSpan ReminderInterval
        {
            get { return _reminderInterval; }
            set { this.SetField(ref _reminderInterval, value, "ReminderInterval"); }
        }

        // required
        private bool _reminderEnabled;
        public bool ReminderEnabled
        {
            get { return _reminderEnabled; }
            set { this.SetField(ref _reminderEnabled, value, "ReminderEnabled"); }
        }

        #endregion //Reminder Properties

        #region Recurrence Required Properties

        #region Recurrence
        private string _recurrence;
        public string Recurrence
        {
            get { return _recurrence; }
            set { this.SetField(ref _recurrence, value, "Recurrence"); }
        }
        #endregion //Recurrence

        #endregion //Recurrence Required Properties

        #region Variance Properties
        private string _rootActivityId;
        public string RootActivityId
        {
            get { return _rootActivityId; }
            set { this.SetField(ref _rootActivityId, value, "RootActivityId"); }
        }

        private DateTime _originalOccurrenceStart;
        public DateTime OriginalOccurrenceStart
        {
            get { return _originalOccurrenceStart; }
            set { this.SetField(ref _originalOccurrenceStart, value, "OriginalOccurrenceStart"); }
        }

        private DateTime _originalOccurrenceEnd;
        public DateTime OriginalOccurrenceEnd
        {
            get { return _originalOccurrenceEnd; }
            set { this.SetField(ref _originalOccurrenceEnd, value, "OriginalOccurrenceEnd"); }
        }

        private bool _isOccurrenceDeleted;
        public bool IsOccurrenceDeleted
        {
            get { return _isOccurrenceDeleted; }
            set { this.SetField(ref _isOccurrenceDeleted, value, "IsOccurrenceDeleted"); }
        }

        private long _variantProperties;
        public long VariantProperties
        {
            get { return _variantProperties; }
            set { this.SetField(ref _variantProperties, value, "VariantProperties"); }
        }

        private long _recurrenceVersion;
        public long RecurrenceVersion
        {
            get { return _recurrenceVersion; }
            set { this.SetField(ref _recurrenceVersion, value, "RecurrenceVersion"); }
        }
        #endregion //Variance Properties
    }
    #endregion //ActivityItem

    #region AppointmentItem
    public class AppointmentItem : ActivityItem
    {
        private string _location;
        public string Location
        {
            get { return _location; }
            set { this.SetField(ref _location, value, "Location"); }
        }
    }
    #endregion //AppointmentItem

    #region JournalItem
    public class JournalItem : ActivityItem
    {
    }
    #endregion //JournalItem

    #region TaskItem
    public class TaskItem : ActivityItem
    {
        private int _percentComplete;
        public int PercentComplete
        {
            get { return _percentComplete; }
            set { this.SetField(ref _percentComplete, value, "PercentComplete"); }
        }
    }
    #endregion //TaskItem
}
