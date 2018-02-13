using Infragistics.Controls.Schedules;
using System.Windows;
using System.Windows.Input;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamOutlookCalendarViewSelectedTimeRange
    {
        private static readonly DependencyProperty SelectedTimeRangeCommandBehaviorProperty = DependencyProperty.RegisterAttached("SelectedTimeRangeCommandBehavior", typeof(XamOutlookCalendarViewCommandBehavior), typeof(XamOutlookCalendarViewSelectedTimeRange), null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(XamOutlookCalendarViewSelectedTimeRange), new PropertyMetadata(OnSetCommandCallback));
        public static ICommand GetCommand(XamOutlookCalendarView outlookCalendarView)
        {
            return outlookCalendarView.GetValue(CommandProperty) as ICommand;
        }
        public static void SetCommand(XamOutlookCalendarView menuItem, ICommand command)
        {
            menuItem.SetValue(CommandProperty, command);
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            XamOutlookCalendarView outlookCalendarView = dependencyObject as XamOutlookCalendarView;
            if (outlookCalendarView != null)
            {
                XamOutlookCalendarViewCommandBehavior behavior = GetOrCreateBehavior(outlookCalendarView);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(XamOutlookCalendarViewSelectedTimeRange), new PropertyMetadata(OnSetCommandParameterCallback));
        public static object GetCommandParameter(XamOutlookCalendarView outlookCalendarView)
        {
            return outlookCalendarView.GetValue(CommandParameterProperty);
        }
        public static void SetCommandParameter(XamOutlookCalendarView outlookCalendarView, object parameter)
        {
            outlookCalendarView.SetValue(CommandParameterProperty, parameter);
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            XamOutlookCalendarView outlookCalendarView = dependencyObject as XamOutlookCalendarView;
            if (outlookCalendarView != null)
            {
                XamOutlookCalendarViewCommandBehavior behavior = GetOrCreateBehavior(outlookCalendarView);
                behavior.CommandParameter = e.NewValue;
            }
        }
        private static XamOutlookCalendarViewCommandBehavior GetOrCreateBehavior(XamOutlookCalendarView outlookCalendarView)
        {
            XamOutlookCalendarViewCommandBehavior behavior = outlookCalendarView.GetValue(SelectedTimeRangeCommandBehaviorProperty) as XamOutlookCalendarViewCommandBehavior;
            if (behavior == null)
            {
                behavior = new XamOutlookCalendarViewCommandBehavior(outlookCalendarView);
                outlookCalendarView.SetValue(SelectedTimeRangeCommandBehaviorProperty, behavior);
            }

            return behavior;
        }

    }
}
