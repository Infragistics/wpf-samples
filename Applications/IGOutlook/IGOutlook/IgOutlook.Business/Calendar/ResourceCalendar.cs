using IgOutlook.Business.Core;
using System.Windows.Media;

namespace IgOutlook.Business.Calendar
{

    public class ResourceCalendar : BusinessBase
    {
        private Color? baseColor;
        private string id;
        private bool? isVisible;
        private string description;
        private string name;
        private string owningResourceId;

        public Color? BaseColor
        {
            get { return baseColor; }
            set
            {
                if (this.baseColor == value)
                    return;

                baseColor = value;
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

        public void SetCalendarVisibility(bool isVisible)
        {
            this.isVisible = isVisible;
        }
    }
}
