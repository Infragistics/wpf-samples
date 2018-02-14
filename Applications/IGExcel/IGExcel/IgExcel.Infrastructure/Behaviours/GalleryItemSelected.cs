using Infragistics.Windows.Ribbon;
using System.Windows;
using System.Windows.Input;

namespace IgExcel.Infrastructure.Behaviours
{
    public class GalleryItemSelected
    {
        private static readonly DependencyProperty SelectedCommandBehaviorProperty = DependencyProperty.RegisterAttached("SelectedCommandBehavior", typeof(GalleryItemSelectedCommandBehavior), typeof(GalleryItemSelected), null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(GalleryItemSelected), new PropertyMetadata(OnSetCommandCallback));
        public static ICommand GetCommand(GalleryItemGroup menuItem)
        {
            return menuItem.GetValue(CommandProperty) as ICommand;
        }
        public static void SetCommand(GalleryItemGroup menuItem, ICommand command)
        {
            menuItem.SetValue(CommandProperty, command);
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GalleryItemGroup menuItem = dependencyObject as GalleryItemGroup;
            if (menuItem != null)
            {
                GalleryItemSelectedCommandBehavior behavior = GetOrCreateBehavior(menuItem);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(GalleryItemSelected), new PropertyMetadata(OnSetCommandParameterCallback));
        public static object GetCommandParameter(GalleryItemGroup menuItem)
        {
            return menuItem.GetValue(CommandParameterProperty);
        }
        public static void SetCommandParameter(GalleryItemGroup menuItem, object parameter)
        {
            menuItem.SetValue(CommandParameterProperty, parameter);
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GalleryItemGroup menuItem = dependencyObject as GalleryItemGroup;
            if (menuItem != null)
            {
                GalleryItemSelectedCommandBehavior behavior = GetOrCreateBehavior(menuItem);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static GalleryItemSelectedCommandBehavior GetOrCreateBehavior(GalleryItemGroup menuItem)
        {
            GalleryItemSelectedCommandBehavior behavior = menuItem.GetValue(SelectedCommandBehaviorProperty) as GalleryItemSelectedCommandBehavior;
            if (behavior == null)
            {
                behavior = new GalleryItemSelectedCommandBehavior(menuItem);
                menuItem.SetValue(SelectedCommandBehaviorProperty, behavior);
            }

            return behavior;
        }
    }
}

