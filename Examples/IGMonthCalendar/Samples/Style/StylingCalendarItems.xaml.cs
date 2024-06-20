using System;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Editors;

namespace IGMonthCalendar.Samples.Style
{
    /// <summary>
    /// Interaction logic for StylingCalendarItems.xaml
    /// </summary>
    public partial class StylingCalendarItems : SampleContainer
    {
        private StyleSelector _dayStyleSelector;
        private StyleSelector _itemStyleSelector;

        public StylingCalendarItems()
        {
            this._dayStyleSelector = new CalendarDayStyleSelector(this);
            this._itemStyleSelector = new CalendarItemStyleSelector(this);

            InitializeComponent();
        }

        public StyleSelector DayStyleSelector
        {
            get { return this._dayStyleSelector; }
        }

        public StyleSelector ItemStyleSelector
        {
            get { return this._itemStyleSelector; }
        }
    }

    public class CalendarDayStyleSelector : StyleSelector
    {
        private StylingCalendarItems _page;

        internal CalendarDayStyleSelector(StylingCalendarItems page)
        {
            this._page = page;
        }

        public override System.Windows.Style SelectStyle(object item, DependencyObject container)
        {
            CalendarDay day = (CalendarDay)container;
            DateTime date = day.StartDate;

            if (date.Month == 12 && date.Day == 25)
                return (System.Windows.Style)this._page.Resources["Christmas"];

            if (date.Month == 10 && date.Day == 31)
                return (System.Windows.Style)this._page.Resources["Halloween"];

            if (date.Month == 7 && date.Day == 4)
                return (System.Windows.Style)this._page.Resources["July4th"];

            if (date.Month == 11 && date.Day == 11)
                return (System.Windows.Style)this._page.Resources["Birthday"];

            return null;
        }
    }

    public class CalendarItemStyleSelector : StyleSelector
    {
        private StylingCalendarItems _page;

        internal CalendarItemStyleSelector(StylingCalendarItems page)
        {
            this._page = page;
        }

        public override System.Windows.Style SelectStyle(object item, DependencyObject container)
        {
            Infragistics.Windows.Editors.CalendarMode mode = CalendarItemGroup.GetCurrentCalendarMode(container);

            switch (mode)
            {
                case Infragistics.Windows.Editors.CalendarMode.Months:
                    return (System.Windows.Style)this._page.Resources["Months"];
                case Infragistics.Windows.Editors.CalendarMode.Years:
                    return (System.Windows.Style)this._page.Resources["Years"];
                case Infragistics.Windows.Editors.CalendarMode.Decades:
                    return (System.Windows.Style)this._page.Resources["Decades"];
                case Infragistics.Windows.Editors.CalendarMode.Centuries:
                    return (System.Windows.Style)this._page.Resources["Centuries"];
            }

            return null;
        }
    }
}
