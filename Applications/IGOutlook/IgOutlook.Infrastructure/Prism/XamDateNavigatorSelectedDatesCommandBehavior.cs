using Infragistics.Controls.Editors;
using Infragistics.Controls.Schedules;
using Prism.Interactivity;
using System;
using System.Collections.ObjectModel;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamDateNavigatorSelectedDatesCommandBehavior : CommandBehaviorBase<XamDateNavigator>
    {
        public XamDateNavigatorSelectedDatesCommandBehavior(XamDateNavigator dateNavigator)
            : base(dateNavigator)
        {
            dateNavigator.SelectedDatesChanged += DateNavigator_SelectedDatesChanged;
        }

        private void DateNavigator_SelectedDatesChanged(object sender, SelectedDatesChangedEventArgs e)
        {
            var dateNavigator = sender as XamDateNavigator;

            CommandParameter = new ObservableCollection<DateTime>(dateNavigator.SelectedDates);

            ExecuteCommand(CommandParameter);
        }
    }
}
