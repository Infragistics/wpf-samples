using Infragistics.Controls.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamDataTreeDataItemSelected
    {
        private static readonly DependencyProperty DataItemSelectedProperty = DependencyProperty.RegisterAttached("DataItemSelected", typeof(XamDataTreeNodeSelectedCommandBehavior), typeof(XamDataTreeDataItemSelected), null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(XamDataTreeDataItemSelected), new PropertyMetadata(OnSetCommandCallback));
        public static ICommand GetCommand(XamDataTree tree)
        {
            return tree.GetValue(CommandProperty) as ICommand;
        }
        public static void SetCommand(XamDataTree menuItem, ICommand command)
        {
            menuItem.SetValue(CommandProperty, command);
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            XamDataTree tree = dependencyObject as XamDataTree;
            if (tree != null)
            {
                XamDataTreeNodeSelectedCommandBehavior behavior = GetOrCreateBehavior(tree);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(XamDataTreeDataItemSelected), new PropertyMetadata(OnSetCommandParameterCallback));
        public static object GetCommandParameter(XamDataTree tree)
        {
            return tree.GetValue(CommandParameterProperty);
        }
        public static void SetCommandParameter(XamDataTree tree, object parameter)
        {
            tree.SetValue(CommandParameterProperty, parameter);
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            XamDataTree tree = dependencyObject as XamDataTree;
            if (tree != null)
            {
                XamDataTreeNodeSelectedCommandBehavior behavior = GetOrCreateBehavior(tree);
                behavior.CommandParameter = e.NewValue;
            }
        }
        private static XamDataTreeNodeSelectedCommandBehavior GetOrCreateBehavior(XamDataTree tree)
        {
            XamDataTreeNodeSelectedCommandBehavior behavior = tree.GetValue(DataItemSelectedProperty) as XamDataTreeNodeSelectedCommandBehavior;
            if (behavior == null)
            {
                behavior = new XamDataTreeNodeSelectedCommandBehavior(tree);
                tree.SetValue(DataItemSelectedProperty, behavior);
            }

            return behavior;
        }
    }
}
