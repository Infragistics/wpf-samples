using IgOutlook.Business.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IgOutlook.Business.Calendar
{
    public class Appointment : BusinessBase, IEditableObject
    {
        private string description;
        private string id;
        private bool? isLocked;
        private bool isOccurrenceDeleted;
        private bool isTimeZoneNeutral;
        private bool? isVisible;
        private DateTime? maxOccurrenceDateTime;
        private DateTime originalOccurrenceEnd;
        private DateTime originalOccurrenceStart;
        private string owningCalendarId;
        private string owningResourceId;
        private string recurrence;
        private string reminder;
        private TimeSpan reminderInterval;
        private string rootActivityId;
        private DateTime start;
        private string startTimeZoneId;
        private string subject;
        private string unmappedProperties;
        private DateTime end;
        private string endTimeZoneId;
        private string location;
        private long variantProperties;
        private int recurrenceVersion;
        private bool reminderEnabled;
        private string categories;
        private bool isMeetingRequest;
        private ObservableCollection<string> to;
        private ObservableCollection<string> appointmentIds;
        private bool isLocation;
        private bool isNewVariance;

        public bool IsNewVariance
        {
            get { return isNewVariance; }
            set
            {
                if (this.isNewVariance == value)
                    return;

                isNewVariance = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsLocation
        {
            get { return isLocation; }
            set
            {
                if (this.isLocation == value)
                    return;

                isLocation = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<string> AppointmentIds
        {
            get { return appointmentIds; }
            set
            {
                if (this.appointmentIds == value)
                    return;

                appointmentIds = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<string> To
        {
            get { return to; }
            set
            {
                if (this.to == value)
                    return;

                to = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsMeetingRequest
        {
            get { return isMeetingRequest; }
            set
            {
                if (this.isMeetingRequest == value)
                    return;

                isMeetingRequest = value;
                NotifyPropertyChanged();
            }
        }
        public bool ReminderEnabled
        {
            get { return reminderEnabled; }
            set
            {
                if (this.reminderEnabled == value)
                    return;

                reminderEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public int RecurrenceVersion
        {
            get { return recurrenceVersion; }
            set
            {
                if (this.recurrenceVersion == value)
                    return;

                recurrenceVersion = value;
                NotifyPropertyChanged();
            }
        }
        public long VariantProperties
        {
            get { return variantProperties; }
            set
            {
                if (this.variantProperties == value)
                    return;

                variantProperties = value;
                NotifyPropertyChanged();
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                if (this.description == value)
                    return;

                description = value;
                NotifyPropertyChanged();
            }
        }
        public string Id
        {
            get { return id; }
            set
            {
                if (this.id == value)
                    return;

                id = value;
                NotifyPropertyChanged();
            }
        }
        public bool? IsLocked
        {
            get { return isLocked; }
            set
            {
                if (this.isLocked == value)
                    return;

                isLocked = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsOccurrenceDeleted
        {
            get { return isOccurrenceDeleted; }
            set
            {
                if (this.isOccurrenceDeleted == value)
                    return;

                isOccurrenceDeleted = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsTimeZoneNeutral
        {
            get { return isTimeZoneNeutral; }
            set
            {
                if (this.isTimeZoneNeutral == value)
                    return;

                isTimeZoneNeutral = value;
                NotifyPropertyChanged();
            }
        }
        public bool? IsVisible
        {
            get { return isVisible; }
            set
            {
                if (this.isVisible == value)
                    return;

                isVisible = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime? MaxOccurrenceDateTime
        {
            get { return maxOccurrenceDateTime; }
            set
            {
                if (this.maxOccurrenceDateTime == value)
                    return;

                maxOccurrenceDateTime = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime OriginalOccurrenceEnd
        {
            get { return originalOccurrenceEnd; }
            set
            {
                if (this.originalOccurrenceEnd == value)
                    return;

                originalOccurrenceEnd = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime OriginalOccurrenceStart
        {
            get { return originalOccurrenceStart; }
            set
            {
                if (this.originalOccurrenceStart == value)
                    return;

                originalOccurrenceStart = value;
                NotifyPropertyChanged();
            }
        }
        public string OwningCalendarId
        {
            get { return owningCalendarId; }
            set
            {
                if (this.owningCalendarId == value)
                    return;

                owningCalendarId = value;
                NotifyPropertyChanged();
            }
        }
        public string OwningResourceId
        {
            get { return owningResourceId; }
            set
            {
                if (this.owningResourceId == value)
                    return;

                owningResourceId = value;
                NotifyPropertyChanged();
            }
        }
        public string Recurrence
        {
            get { return recurrence; }
            set
            {
                if (this.recurrence == value)
                    return;

                recurrence = value;
                NotifyPropertyChanged();
            }
        }
        public string Reminder
        {
            get { return reminder; }
            set
            {
                if (this.reminder == value)
                    return;

                reminder = value;
                NotifyPropertyChanged();
            }
        }
        public TimeSpan ReminderInterval
        {
            get { return reminderInterval; }
            set
            {
                if (this.reminderInterval == value)
                    return;

                reminderInterval = value;
                NotifyPropertyChanged();
            }
        }
        public string RootActivityId
        {
            get { return rootActivityId; }
            set
            {
                if (this.rootActivityId == value)
                    return;

                rootActivityId = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime Start
        {
            get { return start; }
            set
            {
                if (this.start == value)
                    return;

                start = value;
                NotifyPropertyChanged();
            }
        }
        public string StartTimeZoneId
        {
            get { return startTimeZoneId; }
            set
            {
                if (this.startTimeZoneId == value)
                    return;

                startTimeZoneId = value;
                NotifyPropertyChanged();
            }
        }
        public string Subject
        {
            get { return subject; }
            set
            {
                if (this.subject == value)
                    return;

                subject = value;
                NotifyPropertyChanged();
            }
        }
        public string UnmappedProperties
        {
            get { return unmappedProperties; }
            set
            {
                if (this.unmappedProperties == value)
                    return;

                unmappedProperties = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime End
        {
            get { return end; }
            set
            {
                if (this.end == value)
                    return;

                end = value;
                NotifyPropertyChanged();
            }
        }
        public string EndTimeZoneId
        {
            get { return endTimeZoneId; }
            set
            {
                if (this.endTimeZoneId == value)
                    return;

                endTimeZoneId = value;
                NotifyPropertyChanged();
            }
        }
        public string Location
        {
            get { return location; }
            set
            {
                if (this.location == value)
                    return;

                location = value;
                NotifyPropertyChanged();
            }
        }
        public string Categories
        {
            get { return categories; }
            set
            {
                //if (this.myCategory == value)
                //    return;
                categories = value;
                //NotifyPropertyChanged();
            }
        }

        #region IEditableObject

        private Appointment originalObject;
        private bool isInEdit;

        public void BeginEdit()
        {
            if (isInEdit) return;

            originalObject = (Appointment)this.MemberwiseClone();

            isInEdit = true;
        }

        public void CancelEdit()
        {
            if (isInEdit)
            {
                CopyProperties(originalObject);

                isInEdit = false;
            }
        }

        public void EndEdit()
        {
            if (!isInEdit) return;

            isInEdit = false;
            originalObject = null;
        }

        public Appointment Clone()
        {
            var newobj = new Appointment();
            newobj.CopyProperties(this);
            return newobj;
        }

        #endregion //IEditableObject

    }
}
