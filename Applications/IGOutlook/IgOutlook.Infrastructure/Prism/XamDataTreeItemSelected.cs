using System.Windows;
using System.Windows.Input;
using Infragistics.Controls.Menus;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamDataTreeItemSelected
    {
        private static readonly DependencyProperty SelectedCommandBehaviorProperty = DependencyProperty.RegisterAttached("SelectedCommandBehavior", typeof(XamDataTreeNodeActivatedCommandBehavior), typeof(XamDataTreeItemSelected), null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(XamDataTreeItemSelected), new PropertyMetadata(OnSetCommandCallback));
        public static ICommand GetCommand(XamDataTree menuItem)
        {
            return menuItem.GetValue(CommandProperty) as ICommand;
        }
        public static void SetCommand(XamDataTree menuItem, ICommand command)
        {
            menuItem.SetValue(CommandProperty, command);
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            XamDataTree menuItem = dependencyObject as XamDataTree;
            if (menuItem != null)
            {
                XamDataTreeNodeActivatedCommandBehavior behavior = GetOrCreateBehavior(menuItem);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(XamDataTreeItemSelected), new PropertyMetadata(OnSetCommandParameterCallback));
        public static object GetCommandParameter(XamDataTree menuItem)
        {
            return menuItem.GetValue(CommandParameterProperty);
        }
        public static void SetCommandParameter(XamDataTree menuItem, object parameter)
        {
            menuItem.SetValue(CommandParameterProperty, parameter);
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            XamDataTree menuItem = dependencyObject as XamDataTree;
            if (menuItem != null)
            {
                XamDataTreeNodeActivatedCommandBehavior behavior = GetOrCreateBehavior(menuItem);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static XamDataTreeNodeActivatedCommandBehavior GetOrCreateBehavior(XamDataTree menuItem)
        {
            XamDataTreeNodeActivatedCommandBehavior behavior = menuItem.GetValue(SelectedCommandBehaviorProperty) as XamDataTreeNodeActivatedCommandBehavior;
            if (behavior == null)
            {
                behavior = new XamDataTreeNodeActivatedCommandBehavior(menuItem);
                menuItem.SetValue(SelectedCommandBehaviorProperty, behavior);
            }

            return behavior;
        }
    }
}
