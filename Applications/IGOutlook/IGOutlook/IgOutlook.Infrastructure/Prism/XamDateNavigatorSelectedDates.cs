using Infragistics.Controls.Menus;
using Infragistics.Controls.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamDateNavigatorSelectedDates
    {
        private static readonly DependencyProperty DateNavigatorSelectedDatesProperty = DependencyProperty.RegisterAttached("DateNavigatorSelectedDates", typeof(XamDateNavigatorSelectedDatesCommandBehavior), typeof(XamDateNavigatorSelectedDates), null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(XamDateNavigatorSelectedDates), new PropertyMetadata(OnSetCommandCallback));
        public static ICommand GetCommand(XamDateNavigator dateNavigator)
        {
            return dateNavigator.GetValue(CommandProperty) as ICommand;
        }
        public static void SetCommand(XamDateNavigator menuItem, ICommand command)
        {
            menuItem.SetValue(CommandProperty, command);
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            XamDateNavigator dateNavigator = dependencyObject as XamDateNavigator;
            if (dateNavigator != null)
            {
                XamDateNavigatorSelectedDatesCommandBehavior behavior = GetOrCreateBehavior(dateNavigator);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(XamDateNavigatorSelectedDates), new PropertyMetadata(OnSetCommandParameterCallback));
        public static object GetCommandParameter(XamDateNavigator dateNavigator)
        {
            return dateNavigator.GetValue(CommandParameterProperty);
        }
        public static void SetCommandParameter(XamDateNavigator dateNavigator, object parameter)
        {
            dateNavigator.SetValue(CommandParameterProperty, parameter);
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            XamDateNavigator dateNavigator = dependencyObject as XamDateNavigator;
            if (dateNavigator != null)
            {
                XamDateNavigatorSelectedDatesCommandBehavior behavior = GetOrCreateBehavior(dateNavigator);
                behavior.CommandParameter = e.NewValue;
            }
        }
        private static XamDateNavigatorSelectedDatesCommandBehavior GetOrCreateBehavior(XamDateNavigator dateNavigator)
        {
            XamDateNavigatorSelectedDatesCommandBehavior behavior = dateNavigator.GetValue(DateNavigatorSelectedDatesProperty) as XamDateNavigatorSelectedDatesCommandBehavior;
            if (behavior == null)
            {
                behavior = new XamDateNavigatorSelectedDatesCommandBehavior(dateNavigator);
                dateNavigator.SetValue(DateNavigatorSelectedDatesProperty, behavior);
            }

            return behavior;
        }
    }
}
