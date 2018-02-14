using IgOutlook.Business.Core;
using System;

namespace IgOutlook.Business.Calendar
{
    public class Resource : BusinessBase
    {
        private string daySettingsOverrides;
        private string daysOfWeek;
        private string description;
        private string emailAddress;
        private DayOfWeek? firstDayOfWeek;
        private string id;
        private bool isLocked;
        private bool? isVisible;
        private string name;
        private string primaryCalendarId;
        private string primaryTimeZoneId;
        private string unmappedProperties;

        public string DaySettingsOverrides
        {
            get { return daySettingsOverrides; }
            set
            {
                if (this.daySettingsOverrides == value)
                    return;

                daySettingsOverrides = value;
                NotifyPropertyChanged();
            }
        }
        public string DaysOfWeek
        {
            get { return daysOfWeek; }
            set
            {
                if (this.daysOfWeek == value)
                    return;

                daysOfWeek = value;
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
        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                if (this.emailAddress == value)
                    return;

                emailAddress = value;
                NotifyPropertyChanged();
            }
        }
        public DayOfWeek? FirstDayOfWeek
        {
            get { return firstDayOfWeek; }
            set
            {
                if (this.firstDayOfWeek == value)
                    return;

                firstDayOfWeek = value;
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
        public bool IsLocked
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
        public string Name
        {
            get { return name; }
            set
            {
                if (this.name == value)
                    return;

                name = value;
                NotifyPropertyChanged();
            }
        }
        public string PrimaryCalendarId
        {
            get { return primaryCalendarId; }
            set
            {
                if (this.primaryCalendarId == value)
                    return;

                primaryCalendarId = value;
                NotifyPropertyChanged();
            }
        }
        public string PrimaryTimeZoneId
        {
            get { return primaryTimeZoneId; }
            set
            {
                if (this.primaryTimeZoneId == value)
                    return;

                primaryTimeZoneId = value;
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

    }
}
