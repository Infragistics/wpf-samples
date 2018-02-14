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
    public class XamDataTreeDataItemUnselected
    {
        private static readonly DependencyProperty DataItemUnselectedProperty = DependencyProperty.RegisterAttached("DataItemUnselected", typeof(XamDataTreeNodeUnselectedCommandBehavior), typeof(XamDataTreeDataItemUnselected), null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(XamDataTreeDataItemUnselected), new PropertyMetadata(OnSetCommandCallback));
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
                XamDataTreeNodeUnselectedCommandBehavior behavior = GetOrCreateBehavior(tree);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(XamDataTreeDataItemUnselected), new PropertyMetadata(OnSetCommandParameterCallback));
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
                XamDataTreeNodeUnselectedCommandBehavior behavior = GetOrCreateBehavior(tree);
                behavior.CommandParameter = e.NewValue;
            }
        }
        private static XamDataTreeNodeUnselectedCommandBehavior GetOrCreateBehavior(XamDataTree tree)
        {
            XamDataTreeNodeUnselectedCommandBehavior behavior = tree.GetValue(DataItemUnselectedProperty) as XamDataTreeNodeUnselectedCommandBehavior;
            if (behavior == null)
            {
                behavior = new XamDataTreeNodeUnselectedCommandBehavior(tree);
                tree.SetValue(DataItemUnselectedProperty, behavior);
            }

            return behavior;
        }

    }
}
